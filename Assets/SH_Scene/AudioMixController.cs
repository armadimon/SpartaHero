using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider BGMSlider;	// 볼륨을 조절할 Slider


    private void Awake()
    {
        BGMSlider.onValueChanged.AddListener(SetBGMVolume);
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            BGMSlider.value = PlayerPrefs.GetFloat("Volume");
        }
        else
            BGMSlider.value = 0.5f;

        audioMixer.SetFloat("BGM", Mathf.Log10(BGMSlider.value) * 20);
    }

    // Slider를 통해 걸어놓은 이벤트
    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Volume", BGMSlider.value);
    }
}
