using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSliderManager : MonoBehaviour
{
    // https://docs.unity3d.com/Manual/AudioMixer.html
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider MasterSlider;
    [SerializeField] Slider SFX_Slider;
    [SerializeField] Slider MusicSlider;

    const string MIXER_MASTER = "MasterVolume";
    const string MIXER_SFX = "SFX_Volume";
    const string MIXER_MUSIC = "MusicVolume";

    private void Awake()
    {
        MasterSlider.onValueChanged.AddListener(SetMasterVolume);
        SFX_Slider.onValueChanged.AddListener(SetSFXVolume); 
        MusicSlider.onValueChanged.AddListener(SetMusicVolume);
    }

    void SetMasterVolume(float volume)
    {
        mixer.SetFloat(MIXER_MASTER, Mathf.Log10(volume) * 20);
    }
    void SetSFXVolume(float volume)
    {
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(volume) * 20);
    }
    void SetMusicVolume(float volume)
    {
        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(volume) * 20);
    }
}
