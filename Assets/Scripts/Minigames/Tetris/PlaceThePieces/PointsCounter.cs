using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Minigames.Tetris.PlaceThePieces
{
    public class PointsCounter : MonoBehaviour
    {
        public static PointsCounter Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        private int i;

        public TMP_Text counterText;
        public Image img;
        public int currentCount = 0;
        
        void Start()
        {
            img = GetComponent<Image>();
            counterText.text = currentCount.ToString();
        }

        private void Update()
        {
            if (currentCount > 0)
            {
                img.color = Color.green;
            }
        }

        public void AddPoints(int size)
        {
            currentCount += size + i;
            counterText.text = currentCount.ToString();
            i++;
        }

        public void CleanCounter()
        {
            i = 0;
            currentCount = 0;
            img.color = Color.white;
            counterText.text = currentCount.ToString();
        }
    }
}
