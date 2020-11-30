//Written by Gabriel Tupy 11-28-2020
//Last modified by Gabriel Tupy 11-29-2020
using UnityEngine;

[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(Rigidbody2D))]
public class Motor : MonoBehaviour
{
    public InputManager input = null;

    public AnimationCurve SpeedCurve;
    private float curSpeed = 0f;

    public float RotationSpeed;
    private float curRotationSpeed = 0f;

    public Rigidbody2D motor = null;

    private void Awake()
    {
        motor = GetComponent<Rigidbody2D>();
        input = GetComponent<InputManager>();
        curSpeed = SpeedCurve.Evaluate(0);
    }

    private void Update()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.onScoreChange += UpdateSpeed;
        }

        motor.velocity = transform.up * curSpeed;
        motor.MoveRotation(motor.rotation + curRotationSpeed * Time.deltaTime);
    }

    private void UpdateSpeed(int amount)
    {
        curSpeed = SpeedCurve.Evaluate(amount);
    }

    private void UpdateRotation(InputManager.inputDirection current)
    {
        switch (current)
        {
            case InputManager.inputDirection.None:
                curRotationSpeed = 0f;
                break;
            case InputManager.inputDirection.Left:
                curRotationSpeed = RotationSpeed;
                break;
            case InputManager.inputDirection.Right:
                curRotationSpeed = -RotationSpeed;
                break;
        }
    }

    private void OnEnable()
    {
        input.onDirectionChange += UpdateRotation;
        if (GameManager.Instance != null)
        {
            GameManager.Instance.onScoreChange += UpdateSpeed;
        }
    }

    private void OnDisable()
    {
        input.onDirectionChange += UpdateRotation;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.onScoreChange += UpdateSpeed;
        }
    }
}
