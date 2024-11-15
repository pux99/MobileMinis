using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_GrafoTDA
{
    void InicializarGrafo();
    void AgregarVertice(int v);
    void EliminarVertice(int v);
    void AgregarArista(int v1, int v2, int peso);
    void EliminarArista(int v1, int v2);
    bool ExisteArista(int v1, int v2);
}
