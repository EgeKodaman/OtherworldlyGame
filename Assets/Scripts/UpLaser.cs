using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpLaser : MonoBehaviour
{
    [SerializeField] float laserSpeed = 15f;
    [SerializeField] AudioClip laserSound;
    int laserDamage;
    Rigidbody2D laserBody;
    Animator animator;
    SpriteRenderer laserSprite;

    void Awake()
    {
        laserBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        laserSprite = GetComponent<SpriteRenderer>();
        laserBody.velocity = new Vector2(0f, laserSpeed);
        AudioSource.PlayClipAtPoint(laserSound, transform.position);
        laserDamage = FindObjectOfType<GameSession>().GetComponent<GameSession>().laserDamage;
        if(FindObjectOfType<GameSession>().GetComponent<GameSession>().poweredUp)
        laserSprite.color = new Color32(255, 100, 100, 255);
    }


    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag != "Player")
        {
            laserBody.velocity = Vector2.zero;
            animator.SetBool("onHit", true);
            Destroy(gameObject, 0.4f);
        }
        if(other.tag == "Crab")
        other.gameObject.GetComponent<CrabBehaviour>().ReduceEnemyHealth(laserDamage);
        else if(other.tag == "Jumper")
        other.gameObject.GetComponent<JumperBehaviour>().ReduceEnemyHealth(laserDamage);
        else if(other.tag == "Octopus")
        other.gameObject.GetComponent<OctopusBehaviour>().ReduceEnemyHealth(laserDamage);
    }
}