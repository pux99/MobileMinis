using UnityEngine;

namespace UI
{
    public class EnemyUIManager : MonoBehaviour
    {
        [SerializeField] private Animator enemyAnimator;
        [SerializeField] private UIHealth enemyUIHealth;

        public void PlayAttackAnimation()
        {
            enemyAnimator.SetTrigger("ataque");
        }
    }
}
