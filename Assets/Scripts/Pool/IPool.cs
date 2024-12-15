namespace Pool
{
    public interface IPool<T, TConfig>
    {
        public T GetElement(TConfig config);
        public void ReturnElement(T element);

    }
}