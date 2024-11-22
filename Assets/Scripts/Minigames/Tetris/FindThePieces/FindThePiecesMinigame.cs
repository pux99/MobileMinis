using System;
using System.Collections.Generic;
using System.Linq;
using Minigames.Tetris.General;
using Tetris_Minigame.Scripts.UI;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Tetris_Minigame.Scripts.Tetris
{
    public class FindThePiecesMinigame : MonoBehaviour
    {
        #region CreatingPices
        [SerializeField] private SO_GruopOfBaseTetrisPieces groupOfBaseTetrisPieces;
        [SerializeField] private SO_GroupOfColors groupOfColors;
        [SerializeField] private FindThePiecesFactory factory;
        #endregion
    
        [SerializeField] private RectTransform goalPiecesContainer;
        [SerializeField] private RectTransform pileOfPieces;
        [SerializeField] private DropHandler containerDropHandler;
        [SerializeField] private Canvas minigameCanvas;
        private readonly TetrisQueue _goalPiecesQueue=new TetrisQueue(11);
        private readonly TetrisQueue _containerPiecesQueue=new TetrisQueue(11);
        private readonly List<GameObject> _listOfPiecesInThePile = new List<GameObject>();
        private readonly List<GameObject> _listOfGoalPieces = new List<GameObject>();
        private List<Drag> _piecesDragComponentList=new List<Drag>();
        

        public Action WinMinigame;
        public Action LossMinigame;
    
        void Start()
        {
            containerDropHandler.AddToContainer += AddContainerPiecesToQueue;
            
        }
        [ContextMenu("StartGame")]
        public void StartGame()
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject newPiece = CreateRandomPieceInRandomPositionAndRotations();
                newPiece = Instantiate(newPiece, goalPiecesContainer.transform, true);
                RectTransform rectTransform = newPiece.GetComponent<RectTransform>();
                rectTransform.rotation = quaternion.identity;
                rectTransform.localScale = Vector3.one;
                newPiece.GetComponent<Image>().raycastTarget = false;
                _listOfGoalPieces.Add(newPiece);
                _goalPiecesQueue.Enqueue(newPiece);
            }

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    GameObject newPiece = CreateRandomPieceInRandomPositionAndRotations();
                    Vector3 vector = new Vector3((-2+j)*(pileOfPieces.rect.width / 10),(-2+i)*(pileOfPieces.rect.height / 10),0);
                    newPiece.GetComponent<RectTransform>().position = vector + pileOfPieces.transform.position;
                }
            }
            for (int i = 0; i < 25; i++)
                CreateRandomPieceInRandomPositionAndRotations();
            ShuffleChildren();
            if(_listOfPiecesInThePile!=null)
                foreach (var piece in _listOfPiecesInThePile)
                {
                    _piecesDragComponentList.Add(piece.GetComponent<Drag>());
                }
        }
    
        void CheckIfTheQueuesAreEqual()
        {
            int pieceCount = _goalPiecesQueue.Count();
            for (int i = 0; i < pieceCount; i++)
            {
                Image goalPiece = _goalPiecesQueue.Dequeue().GetComponent<Image>();
                Image containerPiece = _containerPiecesQueue.Dequeue().GetComponent<Image>();
                Image goalPieceColorImage = goalPiece.transform.GetChild(0).GetComponent<Image>();
                Image containerPieceColorImage = containerPiece.transform.GetChild(0).GetComponent<Image>();
                StartCoroutine(OutOfContainer(containerPiece));
                if (containerPiece.sprite != goalPiece.sprite || containerPieceColorImage.color != goalPieceColorImage.color)
                {
                    Debug.Log("Wrong Pattern");
                    LossMinigame?.Invoke();
                    return;
                }
            }
            Debug.Log("Correct Pattern");
            WinMinigame?.Invoke();
        }
    
        public void StartOver()
        {
            factory.RandomizePieces(groupOfBaseTetrisPieces, groupOfColors, _listOfPiecesInThePile);
            factory.RandomizePieces(groupOfBaseTetrisPieces, groupOfColors, _listOfGoalPieces);
            int numbreOfGoalpieces = _goalPiecesQueue.Count();
            for (int i = 0; i < numbreOfGoalpieces; i++)
            {
                _goalPiecesQueue.Dequeue();
            }
            foreach (var piece in _listOfGoalPieces)
            {
                _goalPiecesQueue.Enqueue(piece);
            }
            int pieceCount = _containerPiecesQueue.Count();
            for (int i = 0; i < pieceCount; i++)
            {   
                Image containerPiece = _containerPiecesQueue.Dequeue().GetComponent<Image>();
                Debug.Log(_goalPiecesQueue.Count()+ " " +_containerPiecesQueue.Count());
                //containerPiece.gameObject.transform.SetParent(pileOfPieces);
                StartCoroutine(OutOfContainer(containerPiece));
            }

            for (int i = 0; i < _listOfGoalPieces.Count(); i++)
            {
                Image currentGoalImage = _listOfGoalPieces[i].GetComponent<Image>();
                Image currentGoalImageChild = _listOfGoalPieces[i].transform.GetChild(0).GetComponent<Image>();
                Image currentPileImage = _listOfPiecesInThePile[i].GetComponent<Image>();
                Image currentPileImageChild = _listOfPiecesInThePile[i].transform.GetChild(0).GetComponent<Image>();
                currentPileImageChild.color = currentGoalImageChild.color;
                currentPileImageChild.sprite = currentGoalImageChild.sprite;
                currentPileImage.sprite = currentGoalImage.sprite;
                //currentPileImage.rectTransform.sizeDelta = new Vector2(
                //    data.size.x * Screen.currentResolution.width*.05,
                //    data.size.y * Screen.currentResolution.width*.05);
            }
            ShuffleChildren();
            if(_piecesDragComponentList!=null)
                foreach (var piece in _piecesDragComponentList)
                {
                    piece.Canvas = minigameCanvas;
                }
        }
    
        GameObject CreateRandomPieceInRandomPositionAndRotations()
        {
            GameObject newPiece=factory.CreateRandomTetrisPiece(groupOfBaseTetrisPieces, groupOfColors);
            RectTransform rectTransform = newPiece.GetComponent<RectTransform>();
            _listOfPiecesInThePile.Add(newPiece);
            newPiece.transform.SetParent(pileOfPieces);
            rectTransform.rotation = quaternion.Euler(new Vector3(0,0,Random.Range(0, 360)));
            rectTransform.position = GetRandomPosition()*.8f+pileOfPieces.transform.position;
            rectTransform.localScale = Vector3.one;
            return newPiece;
        }
    
        Vector3 GetRandomPosition()
        {
            return new Vector3(
                Random.Range(pileOfPieces.rect.xMin/2,pileOfPieces.rect.xMax/2),
                Random.Range(pileOfPieces.rect.yMin/2,pileOfPieces.rect.yMax/2),
                0);
        }
    
        void AddContainerPiecesToQueue(GameObject newPiece)
        {
            _containerPiecesQueue.Enqueue(newPiece);
            if(_containerPiecesQueue.Count()==_goalPiecesQueue.Count())
                CheckIfTheQueuesAreEqual();
        }
    
        public void PullOutOfContainer()
        {
            int count = _containerPiecesQueue.Count();
            for (int i = 0; i < count; i++)
            {
                StartCoroutine(OutOfContainer(_containerPiecesQueue.Dequeue().GetComponent<Image>()));
            }
        }
    
        IEnumerator<int> OutOfContainer(Image image)
        {
            yield return 2;
            image.transform.SetParent(pileOfPieces);
            image.raycastTarget = true;
            RectTransform rectTransform = image.gameObject.GetComponent<RectTransform>();
            rectTransform.position = GetRandomPosition()+pileOfPieces.transform.position;
            rectTransform.rotation= quaternion.Euler(new Vector3(0,0,Random.Range(0, 360)));
            rectTransform.localScale = Vector3.one;
        }
    
        void ShuffleChildren()
        {
            for (int i = 0; i < pileOfPieces.childCount; i++)
            {
                pileOfPieces.GetChild(Random.Range(0,pileOfPieces.childCount)).transform.SetAsFirstSibling();
            }
        }
    
        void Shuffle<T>(List<T> ts) {
            var count = ts.Count;
            var last = count - 1;
            for (var i = 0; i < last; ++i) {
                var r = Random.Range(i, count);
                (ts[i], ts[r]) = (ts[r], ts[i]);
            }
        }//Save for future Protects
    }
}
