//Written by Gabriel Tupy 11-28-2020
//Last modified by Gabriel Tupy 11-28-2020
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

    private Rigidbody2D motor = null;

    private void Awake()
    {
        motor = GetComponent<Rigidbody2D>();
        input = GetComponent<InputManager>();
    }

    private void Update()
    {
        if (GameManager.Instance != null)
        {
            curSpeed = SpeedCurve.Evaluate(GameManager.Instance.GetCurrentScore());
        }
        else
        {
            curSpeed = 0;
        }

        motor.velocity = transform.up * curSpeed;
        motor.MoveRotation(motor.rotation + curRotationSpeed * Time.deltaTime);
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
    }

    private void OnDisable()
    {
        input.onDirectionChange += UpdateRotation;
    }
}
