using System;
using HealthSystem;
using UnityEngine;

namespace Effects
{
    public abstract class IEffect : MonoBehaviour 
    {
        public virtual void ApplyEffect(UHealth receiver,float value)
        {
        
        }
    }
}
