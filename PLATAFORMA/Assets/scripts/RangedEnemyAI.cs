using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class RangedEnemyAI : MonoBehaviour
{
    [Header("Referencias")]
    public Transform player;
    public Animator animator;
    public GameObject projectilePrefab; // Prefab del proyectil o torpedo
    public Transform spawnPoint;        // Punto de origen desde donde sale el disparo
    public Collider zoneBoundary;       // Collider tipo Trigger que delimita la zona permitida

    [Header("Ataque a Distancia")]
    public float attackRadius = 12f;
    public float attackCooldown = 2f;
    public float projectileSpeed = 15f;

    private NavMeshAgent agent;
    private float lastAttackTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) player = playerObj.transform;
        }

        if (animator == null) animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;

        // Comprobar si el jugador está dentro de la zona delimitada
        bool isPlayerInZone = zoneBoundary == null || zoneBoundary.bounds.Contains(player.position);
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (isPlayerInZone && distanceToPlayer <= attackRadius)
        {
            // Detener el desplazamiento para apuntar y disparar
            agent.isStopped = true;
            UpdateAnimationState(false);

            // Orientar al enemigo hacia el jugador
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0;
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 5f);
            }

            // Disparar según el cooldown
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                ShootProjectile();
                lastAttackTime = Time.time;
            }
        }
        else
        {
            // Si el jugador sale de la zona o está muy lejos, el enemigo permanece dentro de su área
            agent.isStopped = true;
            UpdateAnimationState(false);
        }
    }

    void UpdateAnimationState(bool isMoving)
    {
        if (animator != null)
        {
            animator.SetBool("IsMoving", isMoving);
        }
    }

    void ShootProjectile()
    {
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        if (projectilePrefab != null && spawnPoint != null)
        {
            // Instanciar el proyectil/torpedo orientándolo hacia el jugador
            GameObject proj = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.LookRotation(player.position - spawnPoint.position));
            Rigidbody rb = proj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = (player.position - spawnPoint.position).normalized * projectileSpeed;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}