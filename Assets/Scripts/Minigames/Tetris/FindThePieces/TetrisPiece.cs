using System.Collections;
using System.Collections.Generic;
using Minigames.Tetris.General;
using Tetris_Minigame.Scripts.UI;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TetrisPiece : MonoBehaviour
{
    public PieceData data;
    public Color color;

    public void Initialize(Color newColor,float sizeMod ,Canvas gameCanvas)
    {
        Image pieceImage = GetComponent<Image>();
        Image pieceColor = transform.GetChild(0).GetComponent<Image>();
        pieceImage.sprite = data.sprite;
        pieceColor.sprite = data.ColorSprite;
        color = newColor;
        pieceColor.color = color;
        pieceImage.rectTransform.sizeDelta = new Vector2(
            data.size.x * Screen.currentResolution.width*sizeMod,
            data.size.y * Screen.currentResolution.width*sizeMod);
        GetComponent<Drag>().Canvas = gameCanvas;
    }
}
