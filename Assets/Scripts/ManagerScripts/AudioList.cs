using SoundManagerDemo;
using UnityEngine;

namespace ManagerScripts
{
    public class AudioList : MonoBehaviour
    {
        [SerializeField] AudioSource[] SoundAudioSources;

        [SerializeField] AudioSource[] MusicAudioSources;

        private void Start()
        {
            PersistToggleChanged(true);
        }

        public void PlaySound(int index)
        {
            SoundAudioSources[index].PlayOneShotSoundManaged(SoundAudioSources[index].clip);
        }

        public void PlayMusic(int index)
        {
            MusicAudioSources[index].PlayLoopingMusicManaged(1.0f, 1.0f, true);
        }

        public void PersistToggleChanged(bool isOn)
        {
            SoundManager.StopSoundsOnLevelLoad = !isOn;
        }
    }
}
