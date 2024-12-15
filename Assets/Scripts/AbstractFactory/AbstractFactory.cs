using UnityEngine;

namespace AbstractFactory
{
    public abstract class AbstractFactory<TObject, TConfig> where TObject : MonoBehaviour, IConfigurable<TConfig>
    {
        protected abstract TObject prefab { get; }
        public virtual TObject Create( TConfig config)
        {
            var newBorn = Object.Instantiate(prefab);
            newBorn.Configure(config);
            return newBorn;
        }
    }
}
