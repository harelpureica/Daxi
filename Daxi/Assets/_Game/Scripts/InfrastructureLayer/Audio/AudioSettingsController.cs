using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace Daxi.InfrastructureLayer.Audio
{
    public class AudioSettingsController
    {
        #region Injects
        [Inject(Id ="Music")]
        private AudioMixer musicMixer;

        [Inject(Id = "Sfx")]
        private AudioMixer sfxMixer;

        public enum MixerType { Sfx,Music}

        private float sfxVolume=1;

        private float musicVolume=1;


        #endregion

        #region Methods
        public float GetVolume(MixerType mixer)
        {
            AudioMixer selectedMixer = null;
            if (mixer == MixerType.Music)
            {
                return musicVolume;
            }
            else
            {
                return sfxVolume;
            }

        }
        public void SetMixerVolume(MixerType mixer,float volume)
        {
            volume = Mathf.Clamp01(volume);
            
            AudioMixer selectedMixer = null;
            if(mixer== MixerType.Music)
            {
                selectedMixer = musicMixer;
                musicVolume = volume;
            }
            else
            {
                selectedMixer = sfxMixer;
                sfxVolume= volume;
            }
            selectedMixer.SetFloat("Volume", Mathf.Lerp(-80,20,volume));
        }

        #endregion
    }
}
