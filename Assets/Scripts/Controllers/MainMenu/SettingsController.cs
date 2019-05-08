using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public Text gameSettingsHeader;
    public Text videoSettingsHeader;
    public Text audioSettingsHeader;

    public void GameSettingsClick()
    {
        gameSettingsHeader.text = "ИГРА";
        videoSettingsHeader.text = "Видео";
        audioSettingsHeader.text = "Аудио";
    }

    public void VideoSettingsClick()
    {
        gameSettingsHeader.text = "Игра";
        videoSettingsHeader.text = "ВИДЕО";
        audioSettingsHeader.text = "Аудио";
    }

    public void AudioSettingsClick()
    {
        gameSettingsHeader.text = "Игра";
        videoSettingsHeader.text = "Видео";
        audioSettingsHeader.text = "АУДИО";
    }
}
