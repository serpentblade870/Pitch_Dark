using UnityEngine;

public class CandlePickup : MonoBehaviour
{
    public GameObject pickupEffect; // Optional visual effect
    public AudioClip pickupSound; // Optional sound effect
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            GameManager.Instance.AddScore(1);
            // Find the player's light system
            PlayerLightSystem lightSystem = other.GetComponent<PlayerLightSystem>();
            
            if (lightSystem != null)
            {
                // Add battery and increment candle count
                lightSystem.CollectCandle();
                
                // Play sound effect
                if (pickupSound != null)
                {
                    AudioSource.PlayClipAtPoint(pickupSound, transform.position);
                }
                
                // Play visual effect
                if (pickupEffect != null)
                {
                    Instantiate(pickupEffect, transform.position, Quaternion.identity);
                }
                
                // Destroy this pickup
                Destroy(gameObject);
            }
        }
    }
}