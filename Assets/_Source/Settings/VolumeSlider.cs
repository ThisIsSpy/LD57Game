using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;

namespace Settings
{
    public class VolumeSlider : MonoBehaviour
    {
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private AudioMixMode mixMode;

        private void Awake()
        {
            OnChangeSlider(GetComponent<Slider>().value);
        }

        public void OnChangeSlider(float value)
        {
            switch (mixMode)
            {
                case AudioMixMode.LinearMixerVolume:
                    audioMixer.SetFloat("Volume", -80 + value * 100);
                    break;
                case AudioMixMode.LogrithmicMixerVolume:
                    audioMixer.SetFloat("Volume", Mathf.Log10(value) * 20);
                    break;
            }
        }
    }

    public enum AudioMixMode
    {
        LinearMixerVolume,
        LogrithmicMixerVolume
    }
}
