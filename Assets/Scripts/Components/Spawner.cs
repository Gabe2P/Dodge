//Written by Gabriel Tupy 11-28-2020
//Last modified by Gabriel Tupy 11-28-2020

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public AnimationCurve DistanceCurve = null;
    private float curDistance = 0f;
    [SerializeField] private GameObject player = null;

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            player = GameManager.Instance.Player;
            curDistance = DistanceCurve.Evaluate(GameManager.Instance.GetCurrentScore());
        }
    }

    private void Update()
    {
        if (player == null && GameManager.Instance != null)
        {
            player = GameManager.Instance.Player;
        }
    }

    private void UpdateDistance(int amount)
    {
        curDistance = DistanceCurve.Evaluate(amount);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        RelocateObject(this.transform);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        RelocateObject(this.transform);
    }

    private void RelocateObject(Transform obj)
    {
        if (player != null)
        {
            Vector3 newPosition = new Vector3(player.transform.position.x + Random.Range(-curDistance, curDistance), player.transform.position.y + Random.Range(-curDistance, curDistance), 0);
            obj.position = newPosition;
        }
    }

    private void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.onScoreChange += UpdateDistance;
            GameManager.Instance.AddSpawner(this);
        }
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.onScoreChange -= UpdateDistance;
            GameManager.Instance.RemoveSpawner(this);
        }
    }
}
