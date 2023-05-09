using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    public GameObject levelPanel, settingPanel, playerspanel;

    public Animator Shop, Level;

    public void Play()
    {
        PlayerPrefs.SetInt("TimeForAd", 0);
        int unlockLevel = PlayerPrefs.GetInt("UnlockLevel");
        PlayerPrefs.SetInt("CurrentLevel", unlockLevel);
        SceneManager.LoadScene("Level");
    }

    public void ShowLevelSelector()
    {
        AudioManager.instance.buttonAudio.Play();
        levelPanel.SetActive(true);
    }

    public void ShowSetting()
    {
        AudioManager.instance.buttonAudio.Play();
        settingPanel.SetActive(true);
    }

    public void ShowShop()
    {
        AudioManager.instance.buttonAudio.Play();
        playerspanel.SetActive(true);
    }

    public void CloseSetting()
    {
        AudioManager.instance.buttonAudio.Play();
        settingPanel.SetActive(false);
    }

    private void CloseLevelSelectorTime()
    {
        levelPanel.SetActive(false);
    }

    public void CloseLevelSelector()
    {
        AudioManager.instance.buttonAudio.Play();
        Level.Play("LevelPanelExit");
        Invoke("CloseLevelSelectorTime", 0.5f);
    }

    private void CloseShopTIme()
    {
        playerspanel.SetActive(false);

    }

    public void CloseShop()
    {
        AudioManager.instance.buttonAudio.Play();
        Shop.Play("PlayerScinsExit");
        Invoke("CloseShopTIme", 0.5f);
    }
}
