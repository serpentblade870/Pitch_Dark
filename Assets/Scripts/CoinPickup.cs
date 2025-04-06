using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public GameObject pickupEffect; // Optional visual effect
    public AudioClip pickupSound; // Optional sound effect
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddCoins(1);
                
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }

            Destroy(gameObject);
                
         }
    }
}