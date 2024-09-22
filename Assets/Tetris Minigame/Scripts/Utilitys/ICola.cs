using UnityEngine;

public interface ICola<T>
{
    void InizializeQueue(int Length);

    void Enqueue(T x);

    T Dequeue();

    T Peak();

    bool EmptyQueue();
    
    int Count();
}