namespace Pool
{
    public interface IPool<T>
    {
        public T GetElement<T1>();
        public void ReturnElement(T element);

    }
}