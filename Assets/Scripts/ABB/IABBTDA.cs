using Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IABBTDA
{
    SoEnemy Raiz();
    IABBTDA HijoIzq();
    IABBTDA HijoDer();
    bool ArbolVacio();
    void InicializarArbol();
    void AgregarElem(int x);
    void EliminarElem(int x);
}
