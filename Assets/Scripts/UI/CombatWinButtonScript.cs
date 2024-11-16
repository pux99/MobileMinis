using Core;
using ManagerScripts;
using UnityEngine;

public class CombatWinButtonScript : MonoBehaviour
{
    public void ButtonPressAction()
    {
        ServiceLocator.Instance.GetService<EventManager>().OnCombatEnd();
    }
}
