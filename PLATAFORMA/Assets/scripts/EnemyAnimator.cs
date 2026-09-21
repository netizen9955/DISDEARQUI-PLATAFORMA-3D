using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private static readonly int IsMovingParameter = Animator.StringToHash("IsMoving");
    private static readonly int AttackParameter = Animator.StringToHash("Attack");

    private void Awake()
    {
        if (animator == null) animator = GetComponent<Animator>();
    }

    public void SetMoving(bool isMoving)
    {
        if (animator != null) animator.SetBool(IsMovingParameter, isMoving);
    }

    public void TriggerAttack()
    {
        if (animator != null) animator.SetTrigger(AttackParameter);
    }
}