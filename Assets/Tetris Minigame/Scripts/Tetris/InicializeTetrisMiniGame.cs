using System.Collections.Generic;
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
    private TetrisQueue _goalPieces=new TetrisQueue(10);
    private TetrisQueue _containerPieces=new TetrisQueue(10);

    void Start()
    {
        containerDropHandler.AddToContainer += AddContainerPiecesToQueue;
    }
    
    [ContextMenu("StartGame")]
    public void StartGame()
    {
        List<GameObject> PiecesForThePile = new List<GameObject>();
        Vector2 presentOccupied = pileOfPieces.anchorMax - pileOfPieces.anchorMin;
        presentOccupied.x *= Screen.currentResolution.width;
        presentOccupied.y *= Screen.currentResolution.height;
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
            _goalPieces.Enqueue(newPiece);
        }

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                GameObject newPiece = factory.CreateRandomTetrisPieceTetris(groupOfBaseTetrisPieces, groupOfColors);
                newPiece.GetComponent<RectTransform>().rotation =
                    quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));
                PiecesForThePile.Add(newPiece);
                Vector3 vector = new Vector3((-4+j)*(presentOccupied.x / 10),(-4+i)*(presentOccupied.y / 10),0);
                newPiece.GetComponent<RectTransform>().position = vector + pileOfPieces.transform.position;
            }
        }

        for (int i = 0; i < 50; i++)
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
        for (int i = 0; i < _goalPieces.Count(); i++)
        {
            Image goalPiece = _goalPieces.Dequeue().GetComponent<Image>();
            Image containerPiece = _containerPieces.Dequeue().GetComponent<Image>();
            if (containerPiece.sprite != goalPiece.sprite && containerPiece.color != goalPiece.color)
            {
                Debug.Log("PatronEquivocado");
                return;
            }
        }
        Debug.Log("PatronCorrecto");

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
