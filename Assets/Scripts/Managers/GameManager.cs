//Written by Gabriel Tupy 11-28-2020
//Last modified by Gabriel Tupy 11-28-2020
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public delegate void StateChange(int amount);
    public event StateChange onScoreChange;

    public static GameManager Instance;
    public GameObject Player = null;

    [SerializeField] private int currentScore = 0;
    [SerializeField] private List<Spawner> spawnerList = null;

    [Range(0,1)] private float MusicVolume;
    public Sound[] musicSounds = null;
    [Range(0, 1)] private float SFXVolume;
    public Sound[] SFXSounds = null;

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

    private void Start()
    {
        FindPlayer();
    }

    private void Update()
    {
        if (Player == null)
        {
            FindPlayer();
        }

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetMultipleVolumes(SFXSounds, SFXVolume);
            AudioManager.Instance.SetMultipleVolumes(musicSounds, MusicVolume);
        }
    }

    private void FindPlayer()
    {
        Motor[] listOfMotors = FindObjectsOfType<Motor>();
        foreach (Motor motor in listOfMotors)
        {
            if (motor.CompareTag("Player"))
            {
                Player = motor.gameObject;
                break;
            }
        }
    }

    public void CreateEnemy()
    { 
        
    }

    public void AddSpawner(Spawner spawner)
    {
        spawnerList.Add(spawner);
    }

    public void RemoveSpawner(Spawner spawner)
    {
        spawnerList.Remove(spawner);
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
        onScoreChange?.Invoke(currentScore);
    }

    public void ChangeScore(int amount)
    {
        currentScore += amount;
        onScoreChange?.Invoke(currentScore);
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
