using UnityEngine;

public class PushTrap : MonoBehaviour
{
    public float pushForce = 10f;     // How strong the push is
    public Vector3 pushDirection = Vector3.forward;  // Direction to push (local space)
    public float cooldownTime = 1f;   // Time before trap can trigger again
    
    private bool canTrigger = true;
    
    void OnTriggerEnter(Collider other)
    {
        if (!canTrigger) return;
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("Push trap triggered!");
            
            // Get the CharacterController if it exists
            CharacterController controller = other.GetComponent<CharacterController>();
            Rigidbody rb = other.GetComponent<Rigidbody>();
            
            // Convert local direction to world space
            Vector3 worldPushDirection = transform.TransformDirection(pushDirection.normalized);
            
            // Apply force based on available components
            if (controller != null)
            {
                // For CharacterController
                StartCoroutine(PushCharacter(controller, worldPushDirection));
            }
            else if (rb != null)
            {
                // For Rigidbody
                rb.AddForce(worldPushDirection * pushForce, ForceMode.Impulse);
            }
            else
            {
                // Fallback for simple transform movement
                other.transform.position += worldPushDirection * pushForce * Time.deltaTime;
            }
            
            // Start cooldown
            StartCooldown();
        }
    }
    
    System.Collections.IEnumerator PushCharacter(CharacterController controller, Vector3 direction)
    {
        float pushTime = 0.5f;  // How long to apply the push
        float elapsedTime = 0;
        
        while (elapsedTime < pushTime)
        {
            controller.Move(direction * pushForce * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    
    void StartCooldown()
    {
        canTrigger = false;
        Invoke("ResetTrap", cooldownTime);
    }
    
    void ResetTrap()
    {
        canTrigger = true;
    }
}