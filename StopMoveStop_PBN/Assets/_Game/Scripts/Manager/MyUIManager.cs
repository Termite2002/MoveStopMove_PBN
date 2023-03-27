using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyUIManager : Singleton<MyUIManager>
{
    public GameObject LoseUI;
    public GameObject WinUI;
    public GameObject SettingUI;

    [SerializeField] Text aliveText;

    public void OpenWinUI()
    {
        WinUI.SetActive(true);
    }
    public void OpenSettingUI()
    {
        //SoundManager.Instance.PlaySFX(5);
        SoundManager.Instance.PlaySound(5);

        SettingUI.SetActive(true);
    }
    public void OpenLoseUI()
    {
        LoseUI.SetActive(true);
    }

    public void SetAlive(int alive)
    {
        aliveText.text = "Alive: " + alive.ToString();
    }
    public void NextButton()
    {
        //SoundManager.Instance.PlaySFX(5);
        SoundManager.Instance.PlaySound(5);

        LevelManager.Instance.NextLevel();
        WinUI.SetActive(false);
    }
    public void RetryButton()
    {
        //SoundManager.Instance.PlaySFX(5);
        SoundManager.Instance.PlaySound(5);

        LevelManager.Instance.LoadLevel();
        LoseUI.SetActive(false);
    }
    public void ReturnButton()
    {
        //SoundManager.Instance.PlaySFX(5);
        SoundManager.Instance.PlaySound(5);

        SettingUI.SetActive(false);
    }
}
