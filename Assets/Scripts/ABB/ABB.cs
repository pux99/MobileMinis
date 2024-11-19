using Enemies;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class ABB : IABBTDA
{
    private NodoABB raiz;

    public NodoABB Raiz()
    {
        return raiz;
    }

    public bool ArbolVacio()
    {
        return raiz == null;
    }

    public void InicializarArbol()
    {
        raiz = null;
    }

    public IABBTDA HijoIzq()
    {
        return raiz.hijoIzq;
    }

    public IABBTDA HijoDer()
    {
        return raiz.hijoDer;
    }

    public void AgregarElem(int x , SoEnemy enemy)
    {
        if (raiz == null)
        {
            raiz = new NodoABB();
            raiz.info = x;
            raiz.enemigo = enemy;
            raiz.hijoIzq = new ABB();
            raiz.hijoIzq.InicializarArbol();
            raiz.hijoDer = new ABB();
            raiz.hijoDer.InicializarArbol();
        }
        else if (x < raiz.info)
        {
            raiz.hijoIzq.AgregarElem(x, enemy);
        }
        else if (x >= raiz.info)
        {
            raiz.hijoDer.AgregarElem(x, enemy);
        }
        Balance();
    }

    public void EliminarElem(int x)
    {
        if (raiz == null) return;

        if (x < raiz.info)
        {
            raiz.hijoIzq.EliminarElem(x);
        }
        else if (x > raiz.info)
        {
            raiz.hijoDer.EliminarElem(x);
        }
        else
        {
            if (raiz.hijoIzq.ArbolVacio() && raiz.hijoDer.ArbolVacio())
            {
                raiz = null;
            }
            else if (!raiz.hijoIzq.ArbolVacio())
            {
                raiz.info = ((ABB)raiz.hijoIzq).FindMax();
                raiz.hijoIzq.EliminarElem(raiz.info);
            }
            else
            {
                raiz.info = ((ABB)raiz.hijoDer).FindMin();
                raiz.hijoDer.EliminarElem(raiz.info);
            }
        }
        Balance();
    }

    public NodoABB BuscarNodo(int valor)
    {
        if (raiz == null)
        {
            return null; // The tree is empty or the node was not found
        }

        if (raiz.info == valor)
        {
            return raiz; // Node found
        }
        else if (valor < raiz.info)
        {
            // Search in the left subtree
            return ((ABB)raiz.hijoIzq).BuscarNodo(valor);
        }
        else
        {
            // Search in the right subtree
            return ((ABB)raiz.hijoDer).BuscarNodo(valor);
        }
    }

    private void Balance()
    {
        int balance = BalanceFactor();

        if (balance > 1)
        {
            if (HijoIzq().BalanceFactor() >= 0)
            {
                RotacionDerecha();
            }
            else
            {
                HijoIzq().RotacionIzquierda();
                RotacionDerecha();
            }
        }
        else if (balance < -1)
        {
            if (HijoDer().BalanceFactor() <= 0)
            {
                RotacionIzquierda();
            }
            else
            {
                HijoDer().RotacionDerecha();
                RotacionIzquierda();
            }
        }
    }

    public int Altura()
    {
        if (ArbolVacio())
            return 0;

        return 1 + Mathf.Max(HijoIzq().Altura(), HijoDer().Altura());
    }

    public int BalanceFactor()
    {
        if (ArbolVacio())
            return 0;
        return HijoIzq().Altura() - HijoDer().Altura();
    }

    public void RotacionIzquierda()
    {
        if (raiz == null || raiz.hijoDer.ArbolVacio())
            return;

        // Save the current right child as the new root
        NodoABB nuevaRaiz = ((ABB)raiz.hijoDer).raiz;

        // The left child of the new root becomes the right child of the current root
        raiz.hijoDer = nuevaRaiz.hijoIzq;

        // Create a new tree for the left child of the new root
        ABB arbolIzq = new ABB();
        arbolIzq.raiz = raiz; // The current root becomes the left child of the new root
        nuevaRaiz.hijoIzq = arbolIzq;

        // Update the root
        raiz = nuevaRaiz;
    }

    public void RotacionDerecha()
    {
        if (raiz == null || raiz.hijoIzq.ArbolVacio())
            return;

        // Save the current left child as the new root
        NodoABB nuevaRaiz = ((ABB)raiz.hijoIzq).raiz;

        // The right child of the new root becomes the left child of the current root
        raiz.hijoIzq = nuevaRaiz.hijoDer;

        // Create a new tree for the right child of the new root
        ABB arbolDer = new ABB();
        arbolDer.raiz = raiz; // The current root becomes the right child of the new root
        nuevaRaiz.hijoDer = arbolDer;

        // Update the root
        raiz = nuevaRaiz;
    }

    public int FindMax()
    {
        if (ArbolVacio())
            throw new System.Exception("Tree is empty.");
        if (HijoDer().ArbolVacio())
            return Raiz().info;
        return ((ABB)HijoDer()).FindMax();
    }

    public int FindMin()
    {
        if (ArbolVacio())
            throw new System.Exception("Tree is empty.");
        if (HijoIzq().ArbolVacio())
            return Raiz().info;
        return ((ABB)HijoIzq()).FindMin();
    }
}
