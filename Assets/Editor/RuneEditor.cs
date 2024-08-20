using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RuneEditor : EditorWindow
{
    [MenuItem("Modes/ConectionList")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(RuneEditor));
    }
    private void OnGUI()
    {
        GUILayout.Label("¡Mi Ventana de Editor!");
        if (GUILayout.Button("Haz Algo"))
        {
            // Aquí puedes agregar la funcionalidad que desees
            Debug.Log("Hiciste clic en el botón 'Haz Algo'");
        }
    }
}
