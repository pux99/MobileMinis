using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class InicializeTetrisMiniGame : MonoBehaviour
{
    [SerializeField] private RectTransform objectivePieces;
    [SerializeField] private RectTransform pileOfPieces;
    [SerializeField] private SO_GruopOfBaseTetrisPieces groupOfBaseTetrisPieces;
    [SerializeField] private SO_GroupOfColors groupOfColors;
    [SerializeField] private TetrisFactory factory;
    [SerializeField] private DropHandler containerDropHandler;
    [SerializeField] private Camera MainCamera;
    private TetrisQueue _goalPieces=new TetrisQueue(10);
    private TetrisQueue _containerPieces=new TetrisQueue(10);
    List<GameObject> PiecesForThePile = new List<GameObject>();
    List<GameObject> GoalForThePile = new List<GameObject>();
    [SerializeField] private Animator animator;
    [SerializeField] private Slider goalSlider;
    [SerializeField] private AudioList audioList;

    void Start()
    {
        containerDropHandler.AddToContainer += AddContainerPiecesToQueue;
        StartGame();
    }
    
    [ContextMenu("StartGame")]
    public void StartGame()
    {
        Vector2 presentOccupied = pileOfPieces.anchorMax - pileOfPieces.anchorMin;
        presentOccupied.x *= MainCamera.pixelWidth*.7f;
        presentOccupied.y *= MainCamera.pixelHeight*.7f;
        for (int i = 0; i < 4; i++)
        {
            GameObject newPiece=factory.CreateRandomTetrisPieceTetris(groupOfBaseTetrisPieces, groupOfColors);
            newPiece.GetComponent<RectTransform>().rotation = quaternion.Euler(new Vector3(0,0,Random.Range(0, 360)));
            PiecesForThePile.Add(newPiece);
             Vector3 vector= new Vector3(
                Random.Range(-(presentOccupied.x/2),presentOccupied.x/2),
                Random.Range(-(presentOccupied.y/2),presentOccupied.y/2),
                0);
             newPiece.GetComponent<RectTransform>().position = vector+pileOfPieces.transform.position;
            newPiece = Instantiate(newPiece);
            newPiece.GetComponent<RectTransform>().rotation = quaternion.identity;
            newPiece.transform.SetParent(objectivePieces.transform);
            newPiece.GetComponent<RectTransform>().localScale = Vector3.one;
            GoalForThePile.Add(newPiece);
            _goalPieces.Enqueue(newPiece);
        }

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GameObject newPiece = factory.CreateRandomTetrisPieceTetris(groupOfBaseTetrisPieces, groupOfColors);
                newPiece.GetComponent<RectTransform>().rotation =
                    quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));
                PiecesForThePile.Add(newPiece);
                Vector3 vector = new Vector3((-2+j)*(presentOccupied.x / 10),(-2+i)*(presentOccupied.y / 10),0);
                newPiece.GetComponent<RectTransform>().position = vector + pileOfPieces.transform.position;
            }
        }

        for (int i = 0; i < 25; i++)
        {
            GameObject newPiece;
            newPiece=factory.CreateRandomTetrisPieceTetris(groupOfBaseTetrisPieces, groupOfColors);
            newPiece.GetComponent<RectTransform>().rotation = quaternion.Euler(new Vector3(0,0,Random.Range(0, 360)));
            PiecesForThePile.Add(newPiece);
            Vector3 vector= new Vector3(
                Random.Range(-presentOccupied.x/2,presentOccupied.x/2),
                Random.Range(-presentOccupied.y/2,presentOccupied.y/2),
                0);
            newPiece.GetComponent<RectTransform>().position = vector+pileOfPieces.transform.position;
        }
        Shuffle(PiecesForThePile);
        foreach (var piece in PiecesForThePile)
        {
            piece.transform.SetParent(pileOfPieces);
            piece.GetComponent<RectTransform>().localScale = Vector3.one;
        }
    }

    void AddContainerPiecesToQueue(GameObject newPiece)
    {
        
        _containerPieces.Enqueue(newPiece);
        if(_containerPieces.Count()==_goalPieces.Count())
            CheckIfTheQueuesAreEqual();
    }

    void CheckIfTheQueuesAreEqual()
    {
        int PieceCount = _goalPieces.Count();
        for (int i = 0; i < PieceCount; i++)
        {
            Image goalPiece = _goalPieces.Dequeue().GetComponent<Image>();
            Image containerPiece = _containerPieces.Dequeue().GetComponent<Image>();
            containerPiece.transform.SetParent(pileOfPieces);
            StartCoroutine(OutOfCountainer(containerPiece));
            if (containerPiece.sprite != goalPiece.sprite || containerPiece.color != goalPiece.color)
            {
                Debug.Log("PatronEquivocado");
                StartOver();
                return;
            }
        }
        StartOver();
        Debug.Log("PatronCorrecto");
        attack();

    }

    void attack()
    {
        animator.SetTrigger("ataque");
        goalSlider.value -= .25f;
    }
    IEnumerator<int> OutOfCountainer(Image image)
    {
        yield return 1;
        image.raycastTarget = true;
        Vector2 presentOccupied = pileOfPieces.anchorMax - pileOfPieces.anchorMin;
        presentOccupied.x *= MainCamera.pixelWidth*.7f;
        presentOccupied.y *= MainCamera.pixelHeight*.7f;
        Vector3 vector= new Vector3(
            Random.Range(-presentOccupied.x/2,presentOccupied.x/2),
            Random.Range(-presentOccupied.y/2,presentOccupied.y/2),
            0);
        image.gameObject.GetComponent<RectTransform>().position = vector+pileOfPieces.transform.position;

    }

    void StartOver()
    {
        factory.RandomicePieces(groupOfBaseTetrisPieces, groupOfColors, PiecesForThePile);
        factory.RandomicePieces(groupOfBaseTetrisPieces, groupOfColors, GoalForThePile);
        foreach (var piece in GoalForThePile)
        {
            _goalPieces.Enqueue(piece);
        }
        int PieceCount = _containerPieces.Count();
        for (int i = 0; i < PieceCount; i++)
        {
            _goalPieces.Dequeue();
            Image containerPiece = _containerPieces.Dequeue().GetComponent<Image>();
            StartCoroutine(OutOfCountainer(containerPiece));
            containerPiece.gameObject.transform.SetParent(pileOfPieces);
        }

        for (int i = 0; i < GoalForThePile.Count(); i++)
        {
            Image CurrentGoalImage = GoalForThePile[i].GetComponent<Image>();
            Image currentPileImage = PiecesForThePile[i].GetComponent<Image>();
            currentPileImage.color = CurrentGoalImage.color;
            currentPileImage.sprite = CurrentGoalImage.sprite;
        }
        shuffleChilds();
    }

    private void shuffleChilds()
    {
        for (int i = 0; i < pileOfPieces.childCount; i++)
        {
            pileOfPieces.GetChild(Random.Range(0,pileOfPieces.childCount)).transform.SetAsFirstSibling();
        }
    }

    public static void Shuffle<T>(List<T> ts) {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i) {
            var r = UnityEngine.Random.Range(i, count);
            (ts[i], ts[r]) = (ts[r], ts[i]);
        }
    }
}
