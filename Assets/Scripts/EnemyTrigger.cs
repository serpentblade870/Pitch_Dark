using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    public EnemyAI enemyAI;

    private void Start()
    {
        if (enemyAI == null)
        {
            Debug.LogError("Enemy AI reference not set on trigger!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger entered by: {other.gameObject.name} with tag: {other.tag}");
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger zone!");
            if (enemyAI != null)
            {
                enemyAI.StartChasing();
            }
        }
    }
}