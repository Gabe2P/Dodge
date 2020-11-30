//Written by Gabriel Tupy 11-28-2020
//Last modified by Gabriel Tupy 11-30-2020

using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Animator anim = null;
    public AnimationCurve DistanceCurve = null;
    private float curDistance = 0f;
    public float offset = 0f;
    [SerializeField] private GameObject player = null;
    private Vector3 curTarget = Vector3.zero;
    private Motor motor = null;
    public GameObject effect = null;
    public float inactiveTime = .5f;
    private float curTimer = 0f;
    private bool inactiveTimerDone = false;

    private void Start()
    {
        motor = this.GetComponent<Motor>();

        if (GameManager.Instance != null)
        {
            player = GameManager.Instance.Player;
            curDistance = DistanceCurve.Evaluate(GameManager.Instance.GetCurrentScore());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (curTimer < inactiveTime && !inactiveTimerDone)
        {
            curTimer += Time.deltaTime;
        }
        else
        {
            PolygonCollider2D Collider = GetComponent<PolygonCollider2D>();
            Collider.enabled = true;
            inactiveTimerDone = true;
            motor.shouldMove = true;
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.onScoreChange += UpdateDistance;
            player = GameManager.Instance.Player;
        }

        if (Vector3.Distance(this.transform.position, player.transform.position) > curDistance)
        {
            curTarget = player.transform.position;
        }
        else
        {
            if (curTarget == Vector3.zero || Vector3.Distance(this.transform.position, curTarget) <= .05f)
            {
                Vector3 newPosition = new Vector3(player.transform.position.x + Random.Range(-curDistance, curDistance), player.transform.position.y + Random.Range(-curDistance, curDistance), 0);
                curTarget = newPosition;
            }
        }

        motor.motor.MoveRotation(AngleOfTarget(curTarget));
    }

    private float AngleOfTarget(Vector3 target)
    {
        Vector2 diff = transform.position - target;
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        return rot_z + offset;
    }

    private void UpdateDistance(int amount)
    {
        curDistance = DistanceCurve.Evaluate(amount);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.GameOver();
            }
        }

        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Hazards")) || collision.gameObject.layer.Equals(LayerMask.NameToLayer("Collectables")))
        {
            Die();
        }
    }

    private void Die()
    {
        GameManager.Instance.RemoveEnemy(this);
        GameObject clone = Instantiate(effect, this.transform.position, Quaternion.identity);
        CameraShake.ShakeCamera();
        AudioManager.Instance.Play("EnemyKilled");
        Destroy(clone, .5f);
        Destroy(this.gameObject);
    }

    private void OnEnable()
    {
        PolygonCollider2D Collider = GetComponent<PolygonCollider2D>();
        Collider.enabled = false;
        motor = this.GetComponent<Motor>();
        motor.shouldMove = false;
        anim.SetTrigger("Spawning");
        AudioManager.Instance.Play("EnemySpawned");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.onScoreChange += UpdateDistance;
        }
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.onScoreChange -= UpdateDistance;
        }
    }
}
