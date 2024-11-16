using UnityEngine;

namespace Minigames.Tetris.OrderThePieces
{
    public class TetrisStack
    {
        GameObject[] _stack;
        int _indice;

        public TetrisStack(int length)
        {
            _stack = new GameObject[length];
            _indice = 0;
        }
        public void InizializeStack(int length)
        {
            _stack = new GameObject[length];
            _indice = 0;
        }
        public void Push(GameObject x)
        {
            _stack[_indice] = x;
            _indice++;
        }
        public GameObject Pop()
        {
            GameObject toReturn = Peek();
            if (!EmptyStack())
            {
                _indice--;
            }

            return toReturn;
        }
        public GameObject Peek()
        {
            if (!EmptyStack())
            {
                return _stack[_indice - 1];
            }

            return null;
        }
        private bool EmptyStack()
        {
            return (_indice == 0);
        }
        public int Count()
        {
            return _indice;
        }
        public void Clear()
        {
            int tempindex = _indice;
            for (int i = 0; i < tempindex; i++)
            {
                Pop();
            }
        }
    }
}