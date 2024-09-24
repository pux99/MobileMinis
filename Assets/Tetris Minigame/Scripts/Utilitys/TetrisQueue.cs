using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisQueue :ICola<GameObject>
{ 
    GameObject[] _queue; // arreglo en donde se guarda la informacion
    int _indice; // variable entera en donde se guarda la cantidad de elementos que se tienen guardados

    public TetrisQueue(int length)
    {
        _queue = new GameObject[length];
        _indice = 0;
    }


    public void InizializeQueue(int length)
    {
        _queue = new GameObject[length];
        _indice = 0;
    }

    public void Enqueue(GameObject x)
    {
        for (int i = _indice; i > 0; i--)
        {
            _queue[i] = _queue[i - 1];
        }

        _queue[0] = x;
        _indice++;
    }

    public GameObject Dequeue()
    {
        GameObject toReturn = Peek();
        if (!EmptyQueue())
        {
            _indice--;
        }

        return toReturn;
    }

    public GameObject Peek()
    {
        if (!EmptyQueue())
        {
            return _queue[_indice - 1];
        }
        return null;
    }

    public bool EmptyQueue()
    {
        return (_indice == 0);
    }

    public int Count()
    {
        return _indice;
    }
}
