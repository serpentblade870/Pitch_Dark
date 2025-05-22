using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    private bool isChasing = false;
    
    // Distance at which the enemy is considered to have "touched" the player
    public float touchDistance = 1.0f;
    
    // Maximum distance the enemy will chase the player
    public float maxChaseDistance = 15.0f;
    
    // Original position to return to when not chasing
    private Vector3 originalPosition;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component missing!");
        }
        
        if (player == null)
        {
            Debug.LogError("Player reference not set!");
        }
        
        // Store the enemy's original position
        originalPosition = transform.position;
    }

    public void StartChasing() 
    {
        if (agent != null && player != null)
        {
            Debug.Log("Starting to chase player");
            isChasing = true;
            agent.SetDestination(player.position);
        }
    }

    void Update()
    {
        if (player == null)
            return;
            
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        if (isChasing && agent != null)
        {
            // Check if player is too far away
            if (distanceToPlayer > maxChaseDistance)
            {
                Debug.Log("Player too far away! Stopping chase.");
                StopChasing();
            }
            else
            {
                // Continue chasing
                agent.SetDestination(player.position);
                
                // Check if enemy has touched the player
                if (distanceToPlayer <= touchDistance)
                {
                    Debug.Log("Enemy touched player! Destroying player.");
                    // Destroy(gameObject);
                    //Destroy(player.gameObject);
                    SceneManager.LoadScene("GameOver");

                }
            }
        }
    }
    
    void StopChasing()
    {
        isChasing = false;
        
        // Return to original position
        if (agent != null)
        {
            agent.SetDestination(originalPosition);
            Debug.Log("Returning to original position: " + originalPosition);
        }
    }
}