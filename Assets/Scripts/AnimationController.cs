using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void PlayMovement(float yValue, float xValue)
    {
        if (HasParameter("Vertical", animator) && HasParameter("Horizontal", animator))
        {
            animator.SetFloat("Vertical", yValue);
            animator.SetFloat("Horizontal", xValue);
        }
        animator.SetBool("IsMoving", xValue != 0 || yValue != 0);
    }


    public void PlayFalling()
    {
        if (HasParameter("IsDead", animator))
        {
            animator.SetTrigger("IsDead");
        }

        if (spriteRenderer.color.a == 0)
        {
            Destroy(gameObject);
        }
    }

    public string GetTriggerForAttack()
    {
        if (spriteRenderer.sprite.name.Contains("back"))
            return "IsAttackUp";
        else if (spriteRenderer.sprite.name.Contains("left"))
            return "IsAttackLeft";
        else if (spriteRenderer.sprite.name.Contains("right"))
            return "IsAttackRight";

        return "IsAttackDown";
    }

    public Vector2 GetAttackDirection(string trigger)
    {
        animator.SetTrigger(trigger);

        switch (trigger)
        {
            case "IsAttackUp":
                return Vector2.up;
            case "IsAttackDown":
                return Vector2.down;
            case "IsAttackLeft":
                return Vector2.left;
            case "IsAttackRight":
                return Vector2.right;
            default:
                return Vector2.up;
        }
    }

    public void ActivateAnimation(string trigger = null, string animation = null)
    {
        if (trigger != null)
        {
            animator.SetTrigger(trigger);
        }
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        if (animation != null)
        {
            animator.Play(animation, 0, 0f);
        }
    }

    public void DeactivateAnimation(string trigger)
    {
        animator.ResetTrigger(trigger);
        animator.updateMode = AnimatorUpdateMode.Normal;
    }

    private bool HasParameter(string paramName, Animator animator)
    {
        if (animator == null)
            return false;

        AnimatorControllerParameter[] parameters = animator.parameters;

        foreach (AnimatorControllerParameter param in parameters)
        {
            if (param.name == paramName)
                return true;
        }

        return false;
    }
}
