using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    protected PlayerController player;
    protected EnemyController enemy;
    public float Lifetime = 2f;

    private Vector2 direction;

    [SerializeField] private GameObject effect;

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Start()
    {
        Destroy(gameObject, Lifetime); 
    }

    private void Awake()
    {
        player = FindAnyObjectByType<PlayerController>();
        enemy = FindAnyObjectByType<EnemyController>();
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !gameObject.CompareTag("Weapon"))
        {
            if (effect != null)
            {
                Instantiate(effect, transform.position, Quaternion.identity);
            }

            other.GetComponent<EnemyController>().TakeDamage(player.Damage);

            Destroy(gameObject);
        }

        if (gameObject.CompareTag("Weapon") && other.CompareTag("Player"))
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            other.GetComponent<PlayerController>().TakeDamage(enemy.Damage);
            Debug.Log($"MC is attacked! Health: {other.GetComponent<PlayerController>().Health}");

            Destroy(gameObject);
        }
    }
}