//Written by Gabriel Tupy 11-28-2020
//Last modified by Gabriel Tupy 11-28-2020

using UnityEngine;
using UnityEngine.UI;

public class UpdateText : MonoBehaviour
{
    [SerializeField] private Text scoreText = null;

    void ChangeScore(int amount)
    {
        scoreText.text = amount.ToString();
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
}
