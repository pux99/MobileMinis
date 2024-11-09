using System;
using HealthSystem;
using UnityEngine;

namespace Effects
{
    [Serializable]
    public abstract class Effect : MonoBehaviour
    {
        public virtual void ApplyEffect(UHealth receiver)
        {
        
        }
    }
}
