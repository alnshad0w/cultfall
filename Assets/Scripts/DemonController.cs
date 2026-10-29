using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DemonController : EnemyController
{
    private SceneController sceneController;

    protected override void Start()
    {
        base.Start();
        sceneController = FindFirstObjectByType<SceneController>();
    }
    protected override void Update()
    {
        if (Health <= 0)
        {
            if (!sceneController.WinPanel.activeSelf)
            {
                sceneController.ActivatePanel(sceneController.WinPanel);
            }
        }
        else
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = (player.transform.position - transform.position).normalized;

            direction.y = 0;
            rb.linearVelocity = direction;

            if (attackDistance < distanceToPlayer && distanceToPlayer > distance)
            {
                if (canThrow)
                {
                    StartCoroutine(ThrowOrbs());
                }
            }

            if (distance < distanceToPlayer)
            {
                Vector3 target = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, target, Speed * Time.deltaTime);
            }
            else
            {
                rb.linearVelocity = Vector2.zero;
            }

            animationController.PlayMovement(rb.linearVelocity.y, rb.linearVelocity.x);
        }
    }

    protected override IEnumerator ThrowOrbs()
    {
        Damage = Random.Range(6, 66 + 1);
        return base.ThrowOrbs();
    }
}

