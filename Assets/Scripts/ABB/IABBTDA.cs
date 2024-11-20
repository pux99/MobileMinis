using Enemies;


public interface IABBTDA
{
    void InicializarArbol();
    bool ArbolVacio();
    NodoABB Raiz();
    IABBTDA HijoIzq();
    IABBTDA HijoDer();
    void AgregarElem(int x, SoEnemy enemy);
    void EliminarElem(int x);
    int Altura(); // Calculate the height of the tree
    int BalanceFactor(); // Compute the balance factor
    void RotacionIzquierda(); // Perform a left rotation
    void RotacionDerecha(); // Perform a right rotation
}
