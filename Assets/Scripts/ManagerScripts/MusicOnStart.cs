using UnityEngine;

namespace ManagerScripts
{
    public class MusicOnStart : MonoBehaviour
    {
        [SerializeField] AudioList audioList;
        void Start()
        {
            audioList.PlayMusic(0);
        }

    }
}
