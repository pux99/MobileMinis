using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
                DontDestroyOnLoad(gameObject);
            }
        }

        public TMP_Text counterText;
        public int currentCount = 0;

        void Start()
        {
            counterText.text = currentCount.ToString();
        }

        private void Count(int size)
        {
            currentCount += size;
            counterText.text = currentCount.ToString();
        }
    }
}
