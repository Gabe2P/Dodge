//Written by Gabriel Tupy 11-28-2020
//Last modified by Gabriel Tupy 11-28-2020

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewOrbType", menuName = "Types/Orb")]
public class OrbType : ScriptableObject
{
    [SerializeField] private int score = 10;
    [SerializeField] private Sprite sprite = null;
    [SerializeField] private Color color = Color.clear;

    public int GetScore()
    {
        return score;
    }

    public void SetScore(int newScore)
    {
        score = newScore;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    public void SetSprite(Sprite newSprite)
    {
        sprite = newSprite;
    }

    public Color GetColor()
    {
        return color;
    }

    public void SetColor(Color newColor)
    {
        color = newColor;
    }
}
