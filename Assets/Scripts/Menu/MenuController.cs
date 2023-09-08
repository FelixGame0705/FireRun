using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : Singleton<MenuController>
{
    [SerializeField] private GameObject _soundSetting;
    [SerializeField] private Slider _musicSlider, _sfxSlider;

    public void OnClickSoundSetting()
    {
        _soundSetting.SetActive(true);
    }

    public void OnCloseSoundSetting()
    {
        _soundSetting.SetActive(false);
    }

    public void OnClickStart()
    {
        SceneManager.LoadScene("GamePlay");
        AudioManager.Instance.PlayMusic("ThemeGamePlay");
    }

    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }

    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();
    }

    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(_musicSlider.value);
    }

    public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(_sfxSlider.value);
    }
}
