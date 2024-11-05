using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointsCounter : MonoBehaviour
{
    public TMP_Text counterText;
    public int currentCount = 0;
    void Start()
    {
        DragManager.Instance.OnPiecePlaced += Count;
        counterText.text = currentCount.ToString();
    }

    private void Count()
    {
        currentCount = currentCount+4;
        counterText.text = currentCount.ToString();
    }
}
