using Enemies;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public interface IABBTDA
{
    void InicializarArbol();
    bool ArbolVacio();
    int Raiz();
    IABBTDA HijoIzq();
    IABBTDA HijoDer();
    void AgregarElem(int x);
    void EliminarElem(int x);
    int Altura(); // Calculate the height of the tree
    int BalanceFactor(); // Compute the balance factor
    void RotacionIzquierda(); // Perform a left rotation
    void RotacionDerecha(); // Perform a right rotation
}
