using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class map : MonoBehaviour
{
    public AudioClip pickupSound;
    public GameObject mapUI;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
                mapUI.SetActive(true);
            }

            Destroy(gameObject);

        }
    }
}