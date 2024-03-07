using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Team32PlayerDog : MicrogameInputEvents
{
    public Sprite sprite1; // Sprite when the player has not activated the umbrella
    public Sprite sprite2; // Sprite when the player activates the umbrella
    public Sprite sprite3;
    public PolygonCollider2D umbrellaCollider; // Umbrella PolygonCollider2D
    public CapsuleCollider2D playerCollider;
    public Slider progressSlider;

    public SpriteRenderer umbrellaSpriteRenderer;

    public Animator animator;

    public Transform Goal;
    private float startDistance;

    public Slider stunDurationSlider; 

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private float moveSpeed = 4f;
    private float stunDuration = 5f;

    public float leftBoundary;
    public float rightBoundary;

    private bool isStunned = false;

    public float cooldownAmount;

    float umbrellaCooldown;

    public GameObject buttonAnim;
    private SpriteRenderer buttonAnimSpriteRenderer;

    private void Start()
    {

        buttonAnimSpriteRenderer = buttonAnim.GetComponent<SpriteRenderer>();
        SetButtonAnimVisibility(false);

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite1;

        // Umbrella colliders are disabled by default at the start of the game
        umbrellaCollider.enabled = false;
        playerCollider.enabled = true;
        stunDurationSlider.gameObject.SetActive(false);

        startDistance = Vector3.Distance(transform.position, Goal.position);
        progressSlider.maxValue = startDistance; 
        progressSlider.value = startDistance; 
    }

    protected override void OnGameStart()
    {
        

        umbrellaCooldown = 0;

    }


    void Update()
    {
        UpdateProgress();
        if (umbrellaCooldown > -1)
        {
            umbrellaCooldown -= Time.deltaTime;
        }
    }

    void UpdateProgress()
    {
        float currentDistance = Vector3.Distance(transform.position, Goal.position);
        progressSlider.value = startDistance - currentDistance; 
    }

    private void FixedUpdate()
    {
        if (!isStunned)
        {
            float moveBy = stick.x * Time.deltaTime * moveSpeed;
            float newXPosition = Mathf.Clamp(rb.position.x + moveBy, leftBoundary, rightBoundary);

            if (moveBy < 0)
                spriteRenderer.flipX = true;
            else if (moveBy > 0)
                spriteRenderer.flipX = false;

            rb.MovePosition(new Vector2(newXPosition, rb.position.y));
        }
            
    }

    protected override void OnButton1Pressed(InputAction.CallbackContext context)
    {

        if (isStunned)
        {
            ReduceStunTime(); // Reduce stun time when stunned and button 1 is pressed.
        }
        else if (!umbrellaCollider.enabled && umbrellaCooldown <= 0)
        {
            spriteRenderer.sprite = sprite2;
            umbrellaCollider.enabled = true;
            playerCollider.enabled = false;
            animator.SetTrigger("Open");
        }
        // Removed functionality to close the umbrella using button 1
    }


    private void ReduceStunTime()
    {
        if (isStunned)
        {
            stunDuration = Mathf.Max(0, stunDuration - 1); 
            UpdateStunDurationSlider(); 
        }
    }

    private IEnumerator GetStunned()
    {
        if (!isStunned)
        {
            isStunned = true;
            rb.velocity = Vector2.zero; 
            umbrellaCollider.enabled = false;
            playerCollider.enabled = false;
            spriteRenderer.sprite = sprite3;
            SetButtonAnimVisibility(true);

            if (umbrellaSpriteRenderer != null)
            {
                umbrellaSpriteRenderer.enabled = false; 
            }

            stunDurationSlider.gameObject.SetActive(true);

            while (stunDuration > 0)
            {
                stunDuration -= Time.deltaTime; 
                UpdateStunDurationSlider(); 
                yield return null; 
            }

            UnstunPlayer();
        }
    }


    private void UnstunPlayer()
    {
        if (isStunned)
        {
            isStunned = false;
            stunDuration = 5f; 
            spriteRenderer.sprite = sprite1; 
            umbrellaSpriteRenderer.enabled = true;
            umbrellaCollider.enabled = true;
            playerCollider.enabled = false;
            stunDurationSlider.gameObject.SetActive(false);
            animator.SetTrigger("Open");
            SetButtonAnimVisibility(false);
        }
            
    }


    private void SetButtonAnimVisibility(bool visible)
    {
        if (buttonAnimSpriteRenderer != null)
        {
            Color color = buttonAnimSpriteRenderer.color;
            color.a = visible ? 1f : 0f; // 根据visible变量设置透明度
            buttonAnimSpriteRenderer.color = color;
        }
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (playerCollider.IsTouching(collision.collider)) // Check if it is a player collision body that is hit
        {
            if (!isStunned) // If the player is not currently stationary, the stationary effect is activated
            {
                StartCoroutine(GetStunned());

            }
        }
        else if (umbrellaCollider.enabled && umbrellaCollider.IsTouching(collision.collider)) // If the umbrella collision body is active and collides with another object
        {
            umbrellaCollider.enabled = false; 
            umbrellaCooldown = cooldownAmount;
            playerCollider.enabled=true;
            spriteRenderer.sprite = sprite1; 
            animator.SetTrigger("Close");
        }
    }

    private void UpdateStunDurationSlider()
    {
        if (isStunned)
        {
            stunDurationSlider.gameObject.SetActive(true);
            stunDurationSlider.value = stunDurationSlider.maxValue - stunDuration;
        }
        else
        {
            stunDurationSlider.gameObject.SetActive(false);
            stunDurationSlider.value = 0; 
        }
    }


}