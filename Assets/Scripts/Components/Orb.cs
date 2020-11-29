//Written by Gabriel Tupy 11-28-2020
//Last modified by Gabriel Tupy 11-28-2020

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ToggleGameObjectComponent))]
public class Orb : MonoBehaviour, ICollectable
{
    public OrbType orbType = null;
    public Transform spawnChecker = null;
    private ToggleGameObjectComponent toggleScript;

    private void Awake()
    {
        toggleScript = GetComponent<ToggleGameObjectComponent>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);

        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            Collect(this.gameObject);
        }
    }

    public void Collect(object source)
    {
        toggleScript.SetToggleObject(source as GameObject);
        toggleScript.Toggle();

        if (GameManager.Instance != null)
        {
            GameManager.Instance.ChangeScore(orbType.GetScore());
            SpawnManager.Instance.RelocateObject(this.transform);
        }

        toggleScript.Toggle();
    }
}
