using UnityEngine;

public class EnemyAI3D : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;
    
    private float lastAttackTime;

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            // Mira al jugador y avanza hacia él
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else if (Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    void Attack()
    {
        Debug.Log("¡Enemigo 3D atacando!");
    }
}