//Written by Gabriel Tupy 11-28-2020
//Last modified by Gabriel Tupy 11-28-2020

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newHazardType", menuName = "Types/Hazard")]
public class HazardType : ScriptableObject
{
    [SerializeField] private string gameName = null;
    private Transform currentTransform = null;
    [SerializeField] private Color hazardColor = Color.clear;

    public string GetGameName()
    {
        return gameName;
    }

    public Color GetHazardColor()
    {
        return hazardColor;
    }

    public void SetHazardColor(Color newColor)
    {
        hazardColor = newColor;
    }

    public void SetTransform(Transform newTransform)
    {
        currentTransform = newTransform;
    }

    public Transform GetTransform()
    {
        return currentTransform;
    }

    public virtual void Attack(InputManager currentInput)
    { 
        
    }
}
