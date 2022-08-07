using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    BoxCollider2D playerFeet;
    CapsuleCollider2D playerBody;
    SpriteRenderer playerSprite;
    Vector2 moveInput;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] GameObject laser;
    [SerializeField] GameObject upLaser;
    [SerializeField] Transform standingGun;
    [SerializeField] Transform crouchingGun;
    [SerializeField] Transform upGun;
    [SerializeField] AudioClip grunt;
    GameObject pause;
    GameObject ingame;
    SettingPersist setting;
    Animator animator;
    bool movingHorizontally;
    bool isCrouched;
    bool lookingUp;
    bool canShoot = true;
    bool isHit = false;
    bool isAlive = true;
    bool isPaused = false;
    float moveCooldown = 0.4f;
    float shotCooldown = 0.4f;
    int playerLives = 3;

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerFeet = GetComponent<BoxCollider2D>();
        playerBody = GetComponent<CapsuleCollider2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        setting = FindObjectOfType<SettingPersist>();
        playerSprite.color = setting.GetInstance().playerColor;
        pause = GameObject.Find("PauseCanvas");
        ingame = GameObject.Find("IngameCanvas");
        pause.SetActive(false);
    }

    void Update()
    {
        if(!isAlive) {return;}
        if(!isHit)
        Run();
        if(moveCooldown > 0)
        moveCooldown -= Time.deltaTime;
        else
        isHit = false;
        if(shotCooldown > 0)
        shotCooldown -= Time.deltaTime;
        else
        canShoot = true;

        if(!playerFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) && !isHit)
        {
            animator.SetBool("isInAir", true);
        }
        else
        animator.SetBool("isInAir", false);

        if(moveInput.y > 0 && moveInput.x == 0)
        lookingUp = true;
        else if(moveInput.y < 0 && moveInput.x == 0)
        isCrouched = true;
        else if(moveInput.y == 0)
        {
            lookingUp = false;
            isCrouched = false;
            animator.SetBool("shootUp", false);
            animator.SetBool("isCrouched", false);
        }

    }

    void Run()
    {
        if(!isAlive) {return;}
        myRigidbody.velocity = new Vector2(moveInput.x * moveSpeed, myRigidbody.velocity.y);
        movingHorizontally = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        animator.SetBool("isRunning", movingHorizontally);
        if(movingHorizontally)
        transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1);
        
    }

    void OnMove(InputValue value)
    {
        if(!isAlive) {return;}
        if(isPaused) {return;}
        moveInput = value.Get<Vector2>();
        if(Mathf.Abs(moveInput.y) > Mathf.Epsilon)
        {
            if(moveInput.x < 0)
            moveInput.x -= 0.29f;
            else if(moveInput.x > 0)
            moveInput.x += 0.29f;
        }
    }

    void OnJump()
    {
        if(!isAlive) {return;}
        if(playerFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 10f);
    }

    void OnFire(InputValue value)
    {
        if(!isAlive) {return;}
        if(animator.GetBool("isInAir")) {return;}
        if(canShoot)
        {
            if(lookingUp)
            {
                animator.SetBool("shootUp", true);
                Instantiate(upLaser, upGun.position, transform.rotation);
            }
            else if(isCrouched)
            {
                animator.SetBool("isCrouched", true);
                Instantiate(laser, crouchingGun.position, transform.rotation);
            }
            else
            Instantiate(laser, standingGun.position, transform.rotation);
        
            if(!animator.GetBool("isInAir"))
            animator.SetTrigger("onFire");
            canShoot = false;
            shotCooldown = 0.4f;
        }
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if(!isAlive) {return;}
        if(other.gameObject.tag == "Crab" || other.gameObject.tag == "Jumper" || other.gameObject.tag == "Octopus")
        {
            isHit = true;
            moveCooldown = 0.4f;
            animator.SetTrigger("onHit");
            AudioSource.PlayClipAtPoint(grunt, transform.position);
            myRigidbody.velocity = new Vector2(transform.localScale.x * -9f, 4f);
            DamagePlayerHealth();
        }
    }

    void DamagePlayerHealth()
    {
        if(playerLives > 0)
        playerLives--;
        FindObjectOfType<GameSession>().GetComponent<GameSession>().ProcessPlayerDeath(playerLives);
        if(playerLives == 0)
        {
            isAlive = false;
            animator.SetBool("isAlive", false);
            playerBody.enabled = false;
            playerFeet.enabled = false;
        }
    }

    void OnPause()
    {
        isPaused = !isPaused;
        if(isPaused)
        {
            Time.timeScale = 0f;
            pause.SetActive(true);
            ingame.SetActive(false);
        }
        else
        {
            Time.timeScale = 1f;
            pause.SetActive(false);
            ingame.SetActive(true);
        }
    }
}
