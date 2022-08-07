using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopusBehaviour : MonoBehaviour
{
    Rigidbody2D enemyRigidbody;
    Animator enemyAnimator;
    CapsuleCollider2D enemyCollider;
    [SerializeField] float flySpeed = 3f;
    [SerializeField] int goldValue = 100;
    [SerializeField] AudioClip deathSound;
    bool isAlive = true;
    bool isDead = false;
    bool coroutineStarted = false;
    [SerializeField] int enemyLives = 3;
    void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        enemyCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        if(isAlive)
        {
            if(!coroutineStarted)
            StartCoroutine("MoveEnemy");
        }
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

    IEnumerator MoveEnemy()
    {
        coroutineStarted = true;
        yield return new WaitForSecondsRealtime(3f);
        enemyRigidbody.velocity = new Vector2(flySpeed, 0f);
        transform.localScale = new Vector3(Mathf.Sign(flySpeed), 1f, 1f);
        flySpeed = -flySpeed;
        coroutineStarted = false;
    }

    public void ReduceEnemyHealth(int damage)
    {
        if(enemyLives > 0)
        enemyLives -= damage;
        if(enemyLives <= 0)
        isAlive = false;
    }
}
