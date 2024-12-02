using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AbstractFactory
{
    public interface ICrazyEnemyFactory 
    {
        ICrazyEnemy CreateEnemy();
        ICrazyWeapon CreateWeapon();

        bool CheckType<T>();
    }
}
public abstract class AbstractFactory<TObject, TConfig> where TObject : MonoBehaviour, IConfigurable<TConfig>
{
    protected abstract TObject prefab { get; }
    public virtual TObject Create(Vector3 position, Quaternion rotation, TConfig config)
    {
        var newBorn = Object.Instantiate(prefab, position, rotation);
        newBorn.Configure(config);
        return newBorn;
    }
}
public class Factory<TObject, TConfig> : AbstractFactory<TObject, TConfig> where TObject : MonoBehaviour, IConfigurable<TConfig>
{
    public Factory(TObject prefab)
    {
        this.prefab = prefab;
    }

    protected override TObject prefab { get; }
}

public interface IConfigurable<in TConfig>
{
    void Configure(TConfig config);
}