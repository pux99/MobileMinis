using UnityEngine;

public interface ICola<T>
{
    void InizializeQueue(int Length);

    void Enqueue(T x);

    T Dequeue();

    T Peek();

    bool EmptyQueue();
    
    int Count();
}