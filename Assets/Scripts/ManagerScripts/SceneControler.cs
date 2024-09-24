using UnityEngine;
using UnityEngine.SceneManagement;

namespace ManagerScripts
{
    public class SceneControler : MonoBehaviour
    {

        public void sceneChange(int scene)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(scene);
            //SoundAudioClip.instance.Destroymusic();
            //SoundAudioClip.instance.DestroySounds();
        }
        public void restartTime()
        {
        
        }
    
    }
}
