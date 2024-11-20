using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class EnemyUIManager : MonoBehaviour
    {
        [SerializeField] private Animator enemyAnimator;
        [SerializeField] private UIHealth enemyUIHealth;
        [SerializeField] private EnemyManager enemyManager;
        [SerializeField] private Image spriteImage;
        [SerializeField] private Animator animator;

        private void Start()
        {
            enemyManager.Health.OnLifeChange += enemyUIHealth.LifeChange;
            enemyManager.Health.OnShieldStateChange += enemyUIHealth.ShieldChange;
        }

        public void SetUpArt(Sprite sprite)
        {
            spriteImage.sprite = sprite;

        }

        public void PlayAttackAnimation()
        {
            enemyAnimator.SetTrigger("ataque");
        }
        public void PlayDamagedAnimation()
        {
            enemyAnimator.SetTrigger("damaged");
        }
    }
}
