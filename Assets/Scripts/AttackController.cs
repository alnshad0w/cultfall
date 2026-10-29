using UnityEngine;

public class AttackController : MonoBehaviour
{
    public LayerMask enemyLayers;
    public float attackRange = 1f;
    public Transform attackOrigin;

    private PlayerController player;
    private new CameraController camera;
    private Animator animator;


    private void Awake()
    {
        player = GetComponent<PlayerController>();
        camera = FindAnyObjectByType<CameraController>();
        animator = camera.GetComponent<Animator>();
    }

    public void CloseAttack(Vector2 direction)
    {
        Vector2 origin = (Vector2)attackOrigin.position + direction.normalized * attackRange * 0.5f;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(origin, attackRange * 0.5f, enemyLayers);

        bool isHit = false;

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log(enemy.name);
            enemy.GetComponent<EnemyController>().TakeDamage(player.Damage);
            isHit = true;
        }

        if (animator != null && isHit)
        {
            animator.SetTrigger("Shake");
        }
    }
}
