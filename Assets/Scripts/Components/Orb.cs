//Written by Gabriel Tupy 11-28-2020
//Last modified by Gabriel Tupy 11-30-2020

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ToggleGameObjectComponent))]
public class Orb : MonoBehaviour, ICollectable
{
    public OrbType orbType = null;
    public Animator anim = null;
    public GameObject CollectionEffect = null;
    public Spawner spawner = null;
    private ToggleGameObjectComponent toggleScript;
    public AnimationCurve AliveTimer = null;
    [SerializeField] private float curTimer = 0f;
    private bool haveCalledGameOver = false;
    public float AcceptedDistance = 5f;

    private void Awake()
    {
        toggleScript = GetComponent<ToggleGameObjectComponent>();
    }

    private void Update()
    {
        if (curTimer > AliveTimer.Evaluate(GameManager.Instance.GetCurrentScore()) || Vector3.Distance(this.transform.position, GameManager.Instance.Player.transform.position) >= AcceptedDistance)
        {
            if (!haveCalledGameOver)
            {
                GameManager.Instance.GameOver();
                haveCalledGameOver = true;
            }
        }
        else
        {
            curTimer += Time.deltaTime;

            if (curTimer > AliveTimer.Evaluate(GameManager.Instance.GetCurrentScore()) * .75)
            {
                anim.SetBool("Disappearing", true);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
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
        AudioManager.Instance.Play("OrbSpawned");
        curTimer = 0f;
        anim.SetBool("Disappearing", false);
    }
}
