using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabBehaviour : MonoBehaviour
{
    Rigidbody2D enemyRigidbody;
    Animator enemyAnimator;
    CircleCollider2D enemyCollider;
    [SerializeField] float enemyMoveSpeed;
    [SerializeField] int goldValue = 100;
    [SerializeField] AudioClip deathSound;
    float startSpeed;
    bool isAlive = true;
    bool isDead = false;
    [SerializeField] int enemyLives = 3;
    void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        enemyCollider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if(isAlive)
        MoveEnemy();
        else if(!isAlive && !isDead)
        Die();
    }

    void Die()
    {
        isDead = true;
        enemyRigidbody.velocity = Vector2.zero;
        enemyCollider.enabled = false;
        enemyAnimator.SetTrigger("isDead");
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        FindObjectOfType<GameSession>().GetComponent<GameSession>().IncreaseScore(goldValue);
        Destroy(gameObject, 0.42f);
    }

    void MoveEnemy()
    {
        enemyRigidbody.velocity = new Vector2(enemyMoveSpeed, enemyRigidbody.velocity.y);
    }


    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag == "Ground")
        {
            startSpeed = enemyMoveSpeed;
            enemyMoveSpeed = 0f;
            enemyAnimator.SetBool("isStanding", true);
            StartCoroutine("ChangeDirection");
        }

    }

    IEnumerator ChangeDirection()
    {
        yield return new WaitForSecondsRealtime(1f);

        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        enemyMoveSpeed = -startSpeed;
        enemyAnimator.SetBool("isStanding", false);
    }

    public void ReduceEnemyHealth(int damage)
    {
        if(enemyLives > 0)
        enemyLives -= damage;
        if(enemyLives <= 0)
        isAlive = false;
    }
}
