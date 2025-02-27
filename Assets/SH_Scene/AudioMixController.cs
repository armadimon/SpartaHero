using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider BGMSlider;
    [SerializeField] private Slider SFXSlider;


    private void Awake()
    {
        BGMSlider.onValueChanged.AddListener(SetBGMVolume);
        SFXSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    void Start()
    {
        // BGM
        if (PlayerPrefs.HasKey("BGMVolume"))
        {
            BGMSlider.value = PlayerPrefs.GetFloat("BGMVolume");
        }
        else
            BGMSlider.value = 0.5f;

        audioMixer.SetFloat("BGM", Mathf.Log10(BGMSlider.value) * 20);

        // SFX
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        }
        else
            SFXSlider.value = 0.5f;

        audioMixer.SetFloat("SFX", Mathf.Log10(SFXSlider.value) * 20);

    }

    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("BGMVolume", BGMSlider.value);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", SFXSlider.value);
    }

}
