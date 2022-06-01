using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuEvents : MonoBehaviour
{
    public Slider volumeSlider;
    public Slider soundSlider;
    public AudioMixer mixer;
    public AudioMixer soundMixer;
    private float value;

    private void Start()
    {
        Time.timeScale = 1;
        mixer.GetFloat("volume", out value);
        volumeSlider.value = value;
        soundMixer.GetFloat("sound", out value);
        soundSlider.value = value;
    }

    public void SetVolume()
    {
        mixer.SetFloat("volume", volumeSlider.value);
    }

    public void SetSound()
    {
        soundMixer.SetFloat("sound", soundSlider.value);
    }

    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
