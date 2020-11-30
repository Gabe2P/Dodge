//Written by Gabriel Tupy 11-28-2020
//Last modified by Gabriel Tupy 11-29-2020

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ToggleGameObjectComponent))]
public class Orb : MonoBehaviour, ICollectable
{
    public OrbType orbType = null;
    public GameObject CollectionEffect = null;
    public Spawner spawner = null;
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

        CameraShake.ShakeCamera();

        if (GameManager.Instance != null &&  spawner != null)
        {
            GameObject clone = Instantiate(CollectionEffect, this.transform.position, Quaternion.identity);
            GameManager.Instance.ChangeScore(orbType.GetScore());
            AudioManager.Instance.Play("OrbCollected");
            this.transform.position = spawner.transform.position;
            Destroy(clone, .5f);
        }

        toggleScript.Toggle();
    }
}
