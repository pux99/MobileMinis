using System;
using UnityEngine;

namespace Core
{
    [Serializable]
    public struct InterfaceRef<T> : ISerializationCallbackReceiver {
        [SerializeField] private UnityEngine.Object reference;
        
        private UnityEngine.Object _oldReference;
        
        public T Ref => reference != null ? (T)(object)reference : default;
        
        public void OnBeforeSerialize()
        { 
            Validate();
        }

        public void OnAfterDeserialize() { }

        private void Validate()
        {
            if (reference != null && reference is not T)
            {
                Debug.LogError($"{reference.GetType().Name} does not implement {typeof(T)}");
                reference = _oldReference;
            }
            else
                _oldReference = reference;
        }
    }
}