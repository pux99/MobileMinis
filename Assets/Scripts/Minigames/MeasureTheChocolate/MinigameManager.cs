using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Minigames.MeasureTheChocolate
{
    
    public class MinigameManager : MonoBehaviour
    {
        public static MinigameManager Instance { get; private set; }

        private void Awake()
        {
            // Ensure only one instance of MinigameManager exists
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        public event Action WinMinigame;
        public event Action LostMinigame;

        [SerializeField] private ChocolateFactory factory;
        [SerializeField] public int cantChocolates;

        [SerializeField] public Transform goalContainer;
        [SerializeField] public TMP_Text taskText;

        private List<Chocolate> _generatedChocolates;
        private List<Chocolate> _goalChocolatesOrder;

        private enum SortCriteria
        {
            Size,
            Rows,
            Columns
        }

        private SortCriteria _chosenCriteria;

        private enum SortOrder
        {
            Ascending,
            Descending
        }

        private SortOrder _chosenOrder;
        
        public void StartMinigame()
        {
            _generatedChocolates = factory.GenerateRandomChocolate(cantChocolates);
            _goalChocolatesOrder = new List<Chocolate>();
            ChooseRandomCriteria();
        }

        public void ResetMinigame()
        {
            foreach (Transform child in goalContainer)
            {
                Destroy(child.gameObject);
            }

            _goalChocolatesOrder.Clear();
            _generatedChocolates.Clear();
        }

        public void OnChocolateMovedToGoal(Chocolate chocolate)
        {
            if (!_goalChocolatesOrder.Contains(chocolate))
            {
                _goalChocolatesOrder.Add(chocolate);
            }

            if (goalContainer.childCount == cantChocolates)
            {
                CheckForVictory();
            }
        }

        private void CheckForVictory()
        {
            var extractedValues = ExtractCriteriaValues();

            var sortedValues = new List<int>(extractedValues);

            bool ascending = _chosenOrder == SortOrder.Ascending;
            QuickSort(sortedValues, 0, sortedValues.Count - 1, ascending);
            for (int i = 0; i < extractedValues.Count; i++)
            {
                Debug.Log("Ex value: " + extractedValues[i]);
                Debug.Log("Sort value: " + sortedValues[i]);
            }


            bool isVictory = extractedValues.SequenceEqual(sortedValues);

            if (isVictory)
            {
                WinMinigame?.Invoke();
                Debug.Log("Victory! The chocolates are correctly sorted.");
            }
            else
            {
                LostMinigame?.Invoke();
                Debug.Log("Defeat! The chocolates are not sorted as required.");
            }
        }

        private void ChooseRandomCriteria()
        {
            _chosenCriteria = (SortCriteria)Random.Range(0, System.Enum.GetValues(typeof(SortCriteria)).Length);
            _chosenOrder = (SortOrder)Random.Range(0, System.Enum.GetValues(typeof(SortOrder)).Length);

            DisplaySortingTask();
        }

        private void DisplaySortingTask()
        {
            string criteriaText = _chosenCriteria.ToString();
            string orderText = _chosenOrder == SortOrder.Ascending ? "smallest to largest" : "largest to smallest";

            taskText.text = _chosenCriteria == SortCriteria.Size
                ? $"Sort the chocolate bars by the <b>amount of pieces</b>, from {orderText}."
                : $"Sort the chocolate bars by <b>{criteriaText}</b>, from {orderText}.";

        }

        private List<int> ExtractCriteriaValues()
        {
            List<int> extractedValues = null;

            switch (_chosenCriteria)
            {
                case SortCriteria.Size:
                    extractedValues = _goalChocolatesOrder.Select(chocolate => chocolate.Size).ToList();
                    break;
                case SortCriteria.Rows:
                    extractedValues = _goalChocolatesOrder.Select(chocolate => chocolate.Rows).ToList();
                    break;
                case SortCriteria.Columns:
                    extractedValues = _goalChocolatesOrder.Select(chocolate => chocolate.Columns).ToList();
                    break;
            }

            return extractedValues;
        }


        //Quicksort
        private void QuickSort(List<int> list, int left, int right, bool ascending)
        {
            if (left < right)
            {
                int pivotIndex = Partition(list, left, right, ascending);
                QuickSort(list, left, pivotIndex - 1, ascending);
                QuickSort(list, pivotIndex + 1, right, ascending);
            }
        }

        private int Partition(List<int> list, int left, int right, bool ascending)
        {
            int pivot = list[right];
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (ascending ? list[j] <= pivot : list[j] >= pivot)
                {
                    i++;
                    Swap(list, i, j);
                }
            }

            Swap(list, i + 1, right);
            return i + 1;
        }

        private void Swap(List<int> list, int i, int j)
        {
            int temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}
