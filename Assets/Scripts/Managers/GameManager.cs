//Written by Gabriel Tupy 11-28-2020
//Last modified by Gabriel Tupy 11-28-2020
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private int currentScore = 0;
    public Text scoreText = null;

    [Range(0,1)] private float MusicVolume;
    [SerializeField] private Sound[] musicSounds;
    [Range(0, 1)] private float SFXVolume;
    [SerializeField] private Sound[] SFXSounds;

    //Establishing singleton pattern
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (currentScore > 0)
        {
            scoreText.text = currentScore.ToString();
        }

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetMultipleVolumes(SFXSounds, SFXVolume);
            AudioManager.Instance.SetMultipleVolumes(musicSounds, MusicVolume);
        }
    }

    public void ChangeMusicVolume(Slider slider)
    {
        MusicVolume = Mathf.Clamp(slider.value, 0, 1);
    }
    public void ChangeSFXVolume(Slider slider)
    {
        SFXVolume = Mathf.Clamp(slider.value, 0, 1);
    }

    public void ResetScore()
    {
        currentScore = 0;
    }

    public void ChangeScore(int amount)
    {
        currentScore += amount;
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
