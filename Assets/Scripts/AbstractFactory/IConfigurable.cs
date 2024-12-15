namespace AbstractFactory
{
    public interface IConfigurable<in TConfig>
    {
        void Configure(TConfig config);
    }
}

