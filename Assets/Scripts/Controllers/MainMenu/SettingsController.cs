using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsController : MonoBehaviour
{
    public Text gameHeader;
    public Text videoHeader;
    public Text audioHeader;
    public GameObject gameArea;
    public GameObject videoArea;
    public GameObject audioArea;
    public Dropdown resolutionSelector;
    public AudioMixer audioMixer;

    Resolution[] _resolutions;
    List<string> _resolutionStrings;

    private void Awake()
    {
        _resolutionStrings = new List<string>();
        _resolutions = Screen.resolutions;
        foreach (var r in _resolutions)
        {
            _resolutionStrings.Add(r.ToString());
        }
        resolutionSelector.ClearOptions();
        resolutionSelector.AddOptions(_resolutionStrings);
    }

    public void GameSettingsClick()
    {
        gameHeader.text = "ИГРА";
        videoHeader.text = "Видео";
        audioHeader.text = "Аудио";
        videoArea.SetActive(false);
        audioArea.SetActive(false);
        gameArea.SetActive(true);
    }

    public void VideoSettingsClick()
    {
        gameHeader.text = "Игра";
        videoHeader.text = "ВИДЕО";
        audioHeader.text = "Аудио";
        gameArea.SetActive(false);
        audioArea.SetActive(false);
        videoArea.SetActive(true);
    }

    public void AudioSettingsClick()
    {
        gameHeader.text = "Игра";
        videoHeader.text = "Видео";
        audioHeader.text = "АУДИО";
        gameArea.SetActive(false);
        videoArea.SetActive(false);
        audioArea.SetActive(true);
    }

    public void ToggleFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void ChangeResolution(int i)
    {
        Screen.SetResolution(_resolutions[i].width, _resolutions[i].height, Screen.fullScreen);
    }

    public void ChangeMasterVolume(float amount)
    {
        audioMixer.SetFloat("MasterVolume", amount);
    }

    public void ChangeSFXVolume(float amount)
    {
        audioMixer.SetFloat("SFXVolume", amount);
    }

    public void ChangeMusicVolume(float amount)
    {
        audioMixer.SetFloat("MusicVolume", amount);
    }
}
