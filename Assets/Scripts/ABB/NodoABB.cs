using Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodoABB
{
    // datos a almacenar, en este caso un entero
    public int info;
    public SoEnemy enemigo;
    // referencia los nodos izquiero y derecho
    public IABBTDA hijoIzq;
    public IABBTDA hijoDer;
}
