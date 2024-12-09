using System;
using System.Collections;
using Core;
using Effects;
using Enemies;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ManagerScripts
{
    public class ExploringManager : MonoBehaviour
    {
        /* Legacy
        [SerializeField] private SoEnemy startingEnemy;
        [SerializeField] private SoEnemy rightEnemy;
        [SerializeField] private SoEnemy leftEnemy; 
        */
        [SerializeField] private DungeonManager dungeonManager;
        [SerializeField] private SwipeControl swipe;
        [SerializeField] private int dungeonHeight;
        [SerializeField] private SoEnemy[] enemies;
        [SerializeField] private GameObject leftEnemyPreview;
        [SerializeField] private GameObject rightEnemyPreview;

        public bool waitingForNextCombat=false;
        private ABB dungeon;
        private NodoABB currentRoom;

        private IEnumerator Start()
        {
            ServiceLocator.Instance.GetService<EventManager>().CombatEnd += ReturnToExporting;
            swipe.SwipeToTheLeft += LeftCombat;
            swipe.SwipeToTheRight += RightCombat;
            CreateDungeon(dungeonHeight);
            yield return 5;
            StartFirstCombat();
        }

        private void CreateDungeon(int height)
        {
            dungeon = new ABB();
            dungeon.InicializarArbol();
            for (int i = 0; i < (MathF.Pow(2, height) - 1); i++)
            {
                dungeon.AgregarElem(i, enemies[UnityEngine.Random.Range(0,enemies.Length)]);
            }
        }

        [ContextMenu("startCombat")]
        private void StartFirstCombat()
        {
            currentRoom = dungeon.Raiz();
            dungeonManager.NextCombat(currentRoom.enemigo);
        }

        private void ReturnToExporting()
        {
            if (CheckIfCombatAreAvailable())
            {
                Start5SecondCourutine();
                rightEnemyPreview.GetComponent<Image>().sprite = currentRoom.hijoDer.Raiz().enemigo.enemyPreview;
                leftEnemyPreview.GetComponent<Image>().sprite = currentRoom.hijoIzq.Raiz().enemigo.enemyPreview;
            }
            else
            {
                ServiceLocator.Instance.GetService<EventManager>().OnDungeonWin();
            }
        }

        [ContextMenu("MoveToTheLeft")]
        private void LeftCombat()
        {

            if (waitingForNextCombat) 
            {
                currentRoom = currentRoom.hijoIzq.Raiz();
                dungeonManager.NextCombat(currentRoom.enemigo);
            }
            waitingForNextCombat = false;
        }

        [ContextMenu("MoveToTheRight")]
        private void RightCombat()
        {

            if (waitingForNextCombat)
            {
                currentRoom = currentRoom.hijoDer.Raiz();
                dungeonManager.NextCombat(currentRoom.enemigo);
            }
            waitingForNextCombat = false;
        }

        private bool CheckIfCombatAreAvailable()
        {
            if (currentRoom.hijoDer.Raiz() == null && currentRoom.hijoDer.Raiz() == null)
            {
                return false;
            }

            return true;
        }

        private void Start5SecondCourutine()
        {
            StartCoroutine(WaitFor1Secodns());
        }
        private IEnumerator WaitFor1Secodns()
        {
            yield return new WaitForSeconds(1);
            waitingForNextCombat = true;
            ServiceLocator.Instance.GetService<EventManager>().OnExploringUI();
        }
    }
}
