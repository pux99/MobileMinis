using System.Collections;
using Core;
using Effects;
using Enemies;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace ManagerScripts
{
    public class ExploringManager : MonoBehaviour
    {
        [SerializeField] private SoEnemy startingEnemy;
        [SerializeField] private SoEnemy rightEnemy;
        [SerializeField] private SoEnemy leftEnemy;
        [SerializeField] private DungeonManager dungeonManager;
        [SerializeField] private SwipeControl swipe;
        public bool waitingForNextCombat=false;

        private IEnumerator Start()
        {
            ServiceLocator.Instance.GetService<EventManager>().CombatEnd += ReturnToExporting;
            swipe.SwipeToTheLeft += LeftCombat;
            swipe.SwipeToTheRight += RightCombat;
            yield return 5;
            StartFirstCombat();
        }

        [ContextMenu("startCombat")]
        private void StartFirstCombat()
        {
            dungeonManager.NextCombat(startingEnemy);
        }

        private void ReturnToExporting()
        {
            Start5SecondCourutine();
        }

        private void LeftCombat()
        {
            if(waitingForNextCombat)
                dungeonManager.NextCombat(leftEnemy);
            waitingForNextCombat = false;
        }

        private void RightCombat()
        {
            if(waitingForNextCombat)
                dungeonManager.NextCombat(rightEnemy);
            waitingForNextCombat = false;
        }

        private void Start5SecondCourutine()
        {
            StartCoroutine(Waitfor5secodns());
        }
        private IEnumerator Waitfor5secodns()
        {
            yield return new WaitForSeconds(5);
            waitingForNextCombat = true;
        }
    }
}
