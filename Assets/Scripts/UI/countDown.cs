using Effects;
using Minigamas.GeneralUse;
using TMPro;
using UnityEngine;

namespace UI
{
    public class countDown : MonoBehaviour
    {
        [SerializeField]private  TextMeshProUGUI timer;
        [SerializeField]private float _timer = 5;
        [SerializeField]private  float attackCooldown;
        [SerializeField] private PlayerUIManager playerUIManager;
        [SerializeField] private EnemyUIManager enemyUIManager;
        [SerializeField] private DealDamage _dealDamage;
        public void ResetTimer(int time)
        {
            _timer = time;
        }
        public void addTime(int time)
        {
            _timer += time;
        }
        public void removeTime(int time)
        {
            _timer -= time;
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
            _dealDamage.ApplyEffect(playerUIManager.PlayerManager.PlayerHealth);
            //playerUIManager.PlayerManager.PlayerHealth.TakeDamage(50);
            enemyUIManager.PlayAttackAnimation();
        }
    }
}
