using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            FindObjectOfType<GameSession>().GetComponent<GameSession>().hasKey = true;
            Destroy(gameObject);
        }
    }
}
