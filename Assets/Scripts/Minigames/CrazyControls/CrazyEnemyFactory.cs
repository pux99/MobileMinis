using System.Collections.Generic;
using AbstractFactory;
using UnityEngine;

public class CrazyEnemyFactory : MonoBehaviour
{
    private List<ICrazyEnemyFactory> _factories =new List<ICrazyEnemyFactory>();
    // Start is called before the first frame update
    void Awake()
    {
        foreach (var factory in gameObject.GetComponents<ICrazyEnemyFactory>())
        {
            _factories.Add(factory);
            Debug.Log("a");
        }
    }

    public CrazyEnemy GetEnemy<T>()
    {
        //T tester = new T();
        //if(tester is ICrazyEnemy)
            return _factories.Find(x => x.CheckType<T>()).CreateEnemy();
        Debug.LogWarning("The Type T Must Be An ICrazyEnemy");
        return null;
    }
}
