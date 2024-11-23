using UnityEngine;

namespace Minigames.CrazyControls
{
    public class CrazyWin : MonoBehaviour
    {
        private CrazyControlsMinigame _minigameController;

        private void Start()
        {
            _minigameController = transform.parent.parent.parent.GetComponent<CrazyControlsMinigame>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Win");
            _minigameController.ChangeState(CrazyControlsMinigame.States.Win);
        }
    }
}
