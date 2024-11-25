using UnityEngine;
using UnityEngine.Events;

namespace Factory
{
    public interface IButtonFactory
    {
        GameObject CreateButton<T>(string buttonText, Vector2 position,T scriptsAttach);
        GameObject CreateButton<T,T1>(string buttonText, Vector2 position,T scriptsAttach,T1 scriptsAttach1);
        GameObject CreateButton<T,T1,T2>(string buttonText, Vector2 position, T scriptsAttach,T1 scriptsAttach1,T2 scriptsAttach2);
        GameObject CreateButton<T,T1,T2,T3>(string buttonText, Vector2 position, T scriptsAttach,T1 scriptsAttach1,T2 scriptsAttach2,T3 scriptsAttach3);
    }
}
