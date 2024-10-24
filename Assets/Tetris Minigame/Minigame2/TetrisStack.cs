using UnityEngine;

public class TetrisStack
{
    GameObject[] _stack; // arreglo en donde se guarda la informacion
    int _indice; // variable entera en donde se guarda la cantidad de elementos que se tienen guardados

    public TetrisStack(int length)
    {
        _stack = new GameObject[length];
        _indice = 0;
    }
    
    public void InizializeStack(int length)
    {
        _stack = new GameObject[length];
        _indice = 0;
    }

    public void Push(GameObject x)
    {
        _stack[_indice] = x;
        _indice++;
    }

    public GameObject Pop()
    {
        GameObject toReturn = Peek();
        if (!EmptyStack())
        {
            _indice--;
        }
        return toReturn;
    }

    public GameObject Peek()
    {
        if (!EmptyStack())
        {
            return _stack[_indice - 1];
        }
        return null;
    }

    public bool EmptyStack()
    {
        return (_indice == 0);
    }

    public int Count()
    {
        return _indice;
    }

    public void Clear()
    {
        int Tempindex=_indice;
        for (int i = 0; i < Tempindex; i++)
        {
            Pop();
        }
    }
}
