using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ABBTDA
{
    int Raiz();
    ABBTDA HijoIzq();
    ABBTDA HijoDer();
    bool ArbolVacio();
    void InicializarArbol();
    void AgregarElem(int x);
    void EliminarElem(int x);
}
