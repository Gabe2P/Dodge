//Written by Gabriel Tupy 11-29-2020
//Last modified by Gabriel Tupy 11-29-2020

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text scoreText = null;
    [SerializeField] private Slider musicSlider = null;
    [SerializeField] private Slider sfxSlider = null;

    void ChangeScore(int amount)
    {
        scoreText.text = amount.ToString();
    }

    private void Update()
    {
        musicSlider.value = GameManager.Instance.GetMusicVolume();
        sfxSlider.value = GameManager.Instance.GetSFXVolume();
    }

    private void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.onScoreChange += ChangeScore;
        }
    }
    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.onScoreChange -= ChangeScore;
        }
    }

    public void PlayButtonSound()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.Play("ButtonClick");
        }
    }

    public void ChangeMusicVolume(Slider slider)
    {
        musicSlider = slider;
        GameManager.Instance.SetMusicVolume(slider.value);
    }
    public void ChangeSFXVolume(Slider slider)
    {
        sfxSlider = slider;
        GameManager.Instance.SetSFXVolume(slider.value);
    }

    public void ToggleInput(Toggle toggle)
    {
        GameManager.Instance.ToggleInputType(toggle.isOn);
    }

    public void StartGame()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.Play("ThemeSong");
        }
        SceneManager.LoadScene("GameScene");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        AudioManager.Instance.UnPause("ThemeSong");
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        AudioManager.Instance.Pause("ThemeSong");
    }

    public void RestartGame()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.Stop("ThemeSong");
        }
        SceneManager.LoadScene("MainMenuScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
