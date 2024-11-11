using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CountDown : MonoBehaviour
    {
        [SerializeField]private  TextMeshProUGUI timer;
        [SerializeField]private float _timer = 5;
        [SerializeField]private  float attackCooldown;
        public Action CountdownEnd;

        public void SetAttackCooldown(float cooldown)
        {
            attackCooldown = cooldown;
        }
        public void ResetTimer(float time)
        {
            _timer = time;
        }
        public void AddTime(float time)
        {
            _timer += time;
        }
        public void RemoveTime(float time)
        {
            _timer -= time;
        }

        public void TurnOffCountdown()
        {
            ResetTimer(attackCooldown);
            gameObject.SetActive(false);
        }
        void Update()
        {
            _timer-=Time.deltaTime;
            timer.text=Mathf.CeilToInt(_timer).ToString();
            if(_timer < 0) 
            {
                _timer = attackCooldown;
                CountdownEndEvent();
            }
        }
        private void CountdownEndEvent()
        {
            CountdownEnd?.Invoke();
        }
    }
}
