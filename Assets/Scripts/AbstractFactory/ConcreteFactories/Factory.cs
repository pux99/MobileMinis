using UnityEngine;

namespace AbstractFactory.ConcreteFactories
{
    public class Factory<TObject, TConfig> : AbstractFactory<TObject, TConfig>
        where TObject : MonoBehaviour, IConfigurable<TConfig>
    {
        public Factory(TObject prefab)
        {
            this.prefab = prefab;
        }

        protected override TObject prefab { get; }
    }
}