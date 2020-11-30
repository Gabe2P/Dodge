//Written by Gabriel Tupy 11-28-2020
//Last modified by Gabriel Tupy 11-29-2020
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void StateChange(int amount);
    public event StateChange onScoreChange;

    public delegate void InputChange();
    public event InputChange onInputChange;

    public static GameManager Instance;
    public GameObject Player = null;
    public UIManager GUI = null;

    [SerializeField] private int currentScore = 0;
    [SerializeField] private Spawner OrbSpawner = null;
    public AnimationCurve EnemySpawnCurve = null;
    [SerializeField] private Spawner EnemySpawner = null;
    public GameObject[] enemyPrefabs = null;
    [SerializeField] private List<EnemyAI> listOfEnemies = new List<EnemyAI>();
    [Range(0,1)] private float MusicVolume = .5f;
    public string[] musicSounds = null;
    [Range(0, 1)] private float SFXVolume = .5f;
    public string[] SFXSounds = null;

    //Establishing singleton pattern
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
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
        if (GUI == null)
        {
            GUI = FindObjectOfType<UIManager>();
        }
        if (OrbSpawner == null)
        {
            Spawner[] listOfSpawners = FindObjectsOfType<Spawner>();
            foreach (Spawner spawner in listOfSpawners)
            {
                if (spawner.CompareTag("OrbSpawner"))
                {
                    OrbSpawner = spawner;
                    break;
                }
            }
        }
        if (EnemySpawner == null)
        {
            Spawner[] listOfSpawners = FindObjectsOfType<Spawner>();
            foreach (Spawner spawner in listOfSpawners)
            {
                if (spawner.CompareTag("EnemySpawner"))
                {
                    EnemySpawner = spawner;
                    break;
                }
            }
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

    public void SetMusicVolume(float amount)
    {
        MusicVolume = amount;
    }

    public float GetMusicVolume()
    {
        return MusicVolume;
    }

    public void SetSFXVolume(float amount)
    {
        SFXVolume = amount;
    }

    public float GetSFXVolume()
    {
        return SFXVolume;
    }

    public void ToggleInputType(bool condition)
    {
        if (Player != null)
        {
            Player.GetComponent<InputManager>()?.SetInputToKeyboard(condition);
        }
    }


    public void SpawnEnemy(Spawner spawner, GameObject[] enemies)
    {
        if (listOfEnemies.Count < EnemySpawnCurve.Evaluate(currentScore) && (enemies != null || enemies.Length == 0))
        {
            for (int idx = listOfEnemies.Count; idx <= EnemySpawnCurve.Evaluate(currentScore)/100; idx++)
            {
                listOfEnemies.Add(Instantiate(enemies[Mathf.RoundToInt(Random.Range(0,enemies.Length - 1))], spawner.transform.position, Quaternion.identity).GetComponent<EnemyAI>());
                spawner.RelocateObject(spawner.gameObject.transform);
            }
        }
    }

    public void RemoveEnemy(EnemyAI enemy)
    {
        listOfEnemies.Remove(enemy);
    }

    public void ResetScore()
    {
        currentScore = 0;
        onScoreChange?.Invoke(currentScore);
    }

    public void ChangeScore(int amount)
    {
        currentScore += amount;
        SpawnEnemy(EnemySpawner, enemyPrefabs);
        onScoreChange?.Invoke(currentScore);
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void GameOver()
    {
        listOfEnemies = new List<EnemyAI>();
        ResetScore();
        GUI.RestartGame();
    }

}
