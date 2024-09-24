using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class InicializeTetrisMiniGame : MonoBehaviour
{
    #region CreatingPices
    [SerializeField] private SO_GruopOfBaseTetrisPieces groupOfBaseTetrisPieces;
    [SerializeField] private SO_GroupOfColors groupOfColors;
    [SerializeField] private TetrisFactory factory;
    #endregion
    
    [SerializeField] private RectTransform goalPiecesContainer;
    [SerializeField] private RectTransform pileOfPieces;
    [SerializeField] private DropHandler containerDropHandler;
    private readonly TetrisQueue _goalPiecesQueue=new TetrisQueue(10);
    private readonly TetrisQueue _containerPiecesQueue=new TetrisQueue(10);
    private readonly List<GameObject> _listOfPiecesInThePile = new List<GameObject>();
    private readonly List<GameObject> _listOfGoalPieces = new List<GameObject>();

    #region AudioAndCombatUI
    [SerializeField] private Animator animator;
    [SerializeField] private Slider goalSlider;
    [SerializeField] private AudioList audioList;
    private static readonly int Ataque = Animator.StringToHash("ataque");
    #endregion  
    
    void Start()
    {
        containerDropHandler.AddToContainer += AddContainerPiecesToQueue;
        StartGame();
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
    }
    
    void CheckIfTheQueuesAreEqual()
    {
        int pieceCount = _goalPiecesQueue.Count();
        for (int i = 0; i < pieceCount; i++)
        {
            Image goalPiece = _goalPiecesQueue.Dequeue().GetComponent<Image>();
            Image containerPiece = _containerPiecesQueue.Dequeue().GetComponent<Image>();
            StartCoroutine(OutOfContainer(containerPiece));
            if (containerPiece.sprite != goalPiece.sprite || containerPiece.color != goalPiece.color)
            {
                Debug.Log("Wrong Pattern");
                audioList.PlaySound(1);
                StartOver();
                return;
            }
        }
        Debug.Log("Correct Pattern");
        audioList.PlaySound(0);
        StartOver();
        Attack();
    }
    
    void StartOver()
    {
        factory.RandomizePieces(groupOfBaseTetrisPieces, groupOfColors, _listOfPiecesInThePile);
        factory.RandomizePieces(groupOfBaseTetrisPieces, groupOfColors, _listOfGoalPieces);
        foreach (var piece in _listOfGoalPieces)
        {
            _goalPiecesQueue.Enqueue(piece);
        }
        int pieceCount = _containerPiecesQueue.Count();
        for (int i = 0; i < pieceCount; i++)
        {
            _goalPiecesQueue.Dequeue();
            Image containerPiece = _containerPiecesQueue.Dequeue().GetComponent<Image>();
            StartCoroutine(OutOfContainer(containerPiece));
            containerPiece.gameObject.transform.SetParent(pileOfPieces);
        }

        for (int i = 0; i < _listOfGoalPieces.Count(); i++)
        {
            Image currentGoalImage = _listOfGoalPieces[i].GetComponent<Image>();
            Image currentPileImage = _listOfPiecesInThePile[i].GetComponent<Image>();
            currentPileImage.color = currentGoalImage.color;
            currentPileImage.sprite = currentGoalImage.sprite;
        }
        ShuffleChildren();
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
    
    void Attack()
    {
        animator.SetTrigger(Ataque);
        goalSlider.value -= .25f;
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
