using System;
using HealthSystem;
using UnityEngine;

namespace Effects
{
    //TODO: Rename into Effect
    public abstract class IEffect : MonoBehaviour 
    {
        //TODO: Make this into an abstract method
        public virtual void ApplyEffect(UHealth receiver,float value)
        {
        
        }
    }
}
