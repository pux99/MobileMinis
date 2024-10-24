using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class GridManager_MG3 : MonoBehaviour
    {
        void Update()
        {
            // Check if the mouse is over a UI element
            if (IsPointerOverUIElement(out GameObject hoveredObject))
            {
                // If it is, print the name of the object
                Debug.Log("Hovering over: " + hoveredObject.name);
            }
        }

        // Helper method to check if the pointer is over a UI element
        private bool IsPointerOverUIElement(out GameObject hoveredObject)
        {
            hoveredObject = null;

            // Set up a PointerEventData instance
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            // Raycast to find UI elements under the mouse position
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            // If there is a hit, set the hovered object
            if (results.Count > 0)
            {
                hoveredObject = results[0].gameObject;
                return true;
            }

            return false;
        }
    }
