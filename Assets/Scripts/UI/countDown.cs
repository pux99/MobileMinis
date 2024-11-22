using System;
using Core;
using ManagerScripts;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CountDown : MonoBehaviour
    {
        [SerializeField]private  TextMeshProUGUI timer;
        [SerializeField]private float _timer = 5;
        [SerializeField]private  float attackCooldown;
        private bool _paused =false;
        public Action CountdownEnd;

        public bool IsPaused => _paused;

        private void Awake()
        {
            ServiceLocator.Instance.RegisterService(this);
        }

        private void Start()
        {
            ServiceLocator.Instance.GetService<EventManager>().Pause += PauseTime;
            ServiceLocator.Instance.GetService<EventManager>().Resume += ResumeTime;
        }

        public void SetAttackCooldown(float cooldown)
        {
            attackCooldown = cooldown;
            _timer = attackCooldown;
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
        public void PauseTime()
        {
            _paused = true;
        }

        public void ResumeTime()
        {
            _paused = false;
        }

        public void TurnOffCountdown()
        {
            ResetTimer(attackCooldown);
            gameObject.SetActive(false);
        }
        void Update()
        {
            if (!_paused)
            {
                _timer-=Time.deltaTime;
                timer.text=Mathf.CeilToInt(_timer).ToString();
                if(_timer < 0) 
                {
                    _timer = attackCooldown;
                    CountdownEndEvent();
                }
            }
        }
        public void CountdownEndEvent()
        {
            CountdownEnd?.Invoke();
        }
    }
}
