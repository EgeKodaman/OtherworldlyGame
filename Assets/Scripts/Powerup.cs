using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] AudioClip powerupSound;

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            FindObjectOfType<GameSession>().GetComponent<GameSession>().poweredUp = true;
            AudioSource.PlayClipAtPoint(powerupSound, transform.position);
            FindObjectOfType<GameSession>().GetComponent<GameSession>().IncreaseDamage();
            Destroy(gameObject);
        }    
    }
}
