using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperBehaviour : MonoBehaviour
{
    Rigidbody2D enemyRigidbody;
    Animator enemyAnimator;
    BoxCollider2D enemyCollider;
    [SerializeField] float jumpHeight = 6.5f;
    [SerializeField] float jumpLength = 3f;
    [SerializeField] AudioClip deathSound;
    [SerializeField] int goldValue = 100;
    bool isAlive = true;
    bool isDead = false;
    bool coroutineStarted = false;
    [SerializeField] int enemyLives = 3;
    void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        enemyCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if(isAlive)
        {
            if(enemyCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                enemyAnimator.SetBool("isJumping", false);
                if(!coroutineStarted)
                StartCoroutine("MoveEnemy");
            } 
        }
        else if(!isAlive && !isDead)
        {
            Die();
            enemyRigidbody.velocity = Vector2.zero;
        }
    }

    void Die()
    {
        isDead = true;
        enemyCollider.enabled = false;
        enemyRigidbody.gravityScale = 0;
        enemyAnimator.SetTrigger("isDead");
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        FindObjectOfType<GameSession>().GetComponent<GameSession>().IncreaseScore(goldValue);
        Destroy(gameObject, 0.42f);
    }

    IEnumerator MoveEnemy()
    {
        coroutineStarted = true;
        yield return new WaitForSecondsRealtime(2.5f);
        enemyAnimator.SetBool("isJumping", true);
        enemyRigidbody.velocity = new Vector2(jumpLength, jumpHeight);
        jumpLength = -jumpLength;
        coroutineStarted = false;
    }

    public void ReduceEnemyHealth(int damage)
    {
        if(enemyLives > 0)
        enemyLives -= damage;
        if(enemyLives <= 0)
        isAlive = false;
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Ground")
        {
            enemyRigidbody.velocity = new Vector2(0f, enemyRigidbody.velocity.y);
        }   
    }
}
