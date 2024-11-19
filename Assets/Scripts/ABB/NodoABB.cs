using Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodoABB
{
    // Datos a almacenar
    public int info;
    public SoEnemy enemigo;
    // Referencia los nodos izquiero y derecho
    public IABBTDA hijoIzq;
    public IABBTDA hijoDer;
}
