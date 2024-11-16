using Core;
using ManagerScripts;
using UnityEngine;

public class ComabatTutorialOverlayScript : MonoBehaviour
{
    [SerializeField] private GameObject overlay;
    void Start()
    {
        ServiceLocator.Instance.GetService<EventManager>().CombatStart += HandleCombatStart;
        ServiceLocator.Instance.GetService<EventManager>().CombatEnd += HandleCombatEnd;
    }

    private void HandleCombatStart()
    {
        ServiceLocator.Instance.GetService<EventManager>().Pause += HandlePause;
        ServiceLocator.Instance.GetService<EventManager>().Resume += HandelResume;
    }
    private void HandleCombatEnd()
    {
        ServiceLocator.Instance.GetService<EventManager>().Pause -= HandlePause;
        ServiceLocator.Instance.GetService<EventManager>().Resume -= HandelResume;
    }

    private void HandlePause()
    {
        overlay.SetActive(true);
    }

    private void HandelResume()
    {
        overlay.SetActive(false);
    }
}
