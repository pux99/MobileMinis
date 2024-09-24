using UnityEngine;
using UnityEngine.UI;

namespace Tetris_Minigame.Scripts.UI
{
    public class RaycastTargetOffTest : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            Image a = this.GetComponent<Image>();
            a.raycastTarget = false;
        }
    }
}
