using HealthSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIHealth : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider;
        [SerializeField] private GameObject shield;
        private UHealth _health;
        void Start()
        {
            healthSlider.value = 1;
        }
    
        public void LifeChange(int maxHp, int newHp)
        {
            if(maxHp>0)
                healthSlider.value = newHp / (float)maxHp;
        }
        public void ShieldChange(bool state)
        {
            shield.SetActive(state);
        }
    }
}
