using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Team32PlayerDog : MicrogameInputEvents
{
    public Sprite sprite1; // 玩家未激活雨伞时的Sprite
    public Sprite sprite2; // 玩家激活雨伞时的Sprite
    public Sprite sprite3;
    public PolygonCollider2D umbrellaCollider; // 雨伞的PolygonCollider2D
    public CapsuleCollider2D playerCollider;
    public Slider progressSlider;

    public Animator animator;

    public Transform Goal;
    private float startDistance;

    public Slider stunDurationSlider; // 在 Inspector 中设置这个引用

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private float moveSpeed = 4f;
    private float stunDuration = 5f;

    public float leftBoundary;
    public float rightBoundary;

    private bool isStunned = false;

    public float cooldownAmount;

    float umbrellaCooldown;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite1;

        // 游戏开始时雨伞碰撞体默认禁用
        umbrellaCollider.enabled = false;
        playerCollider.enabled = true;
        stunDurationSlider.gameObject.SetActive(false);

        startDistance = Vector3.Distance(transform.position, Goal.position);
        progressSlider.maxValue = startDistance; // 设置 Slider 的最大值
        progressSlider.value = startDistance; // 初始时，玩家与终点的距离是最大的
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
        progressSlider.value = startDistance - currentDistance; // 更新 Slider，反映玩家离目标的剩余距离
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
        // 如果按钮1被按下，切换雨伞碰撞体的激活状态
        if (!umbrellaCollider.enabled && !isStunned && umbrellaCooldown <= 0)
        {
            spriteRenderer.sprite = sprite2; // 切换到激活雨伞的Sprite
            umbrellaCollider.enabled = true; // 激活雨伞碰撞体
            playerCollider.enabled = false;
            animator.SetTrigger("Open");
        }
        else
        {
            if ( !isStunned)
            {
                spriteRenderer.sprite = sprite1; // 切换到未激活雨伞的Sprite
                umbrellaCollider.enabled = false; // 禁用雨伞碰撞体
                playerCollider.enabled = true;
                animator.SetTrigger("Close");
            }
                
        }
    }

    protected override void OnButton2Pressed(InputAction.CallbackContext context)
    {
        if (isStunned)
        {
            ReduceStunTime(); // 减少静止时间
        }
    }

    private void ReduceStunTime()
    {
        if (isStunned)
        {
            stunDuration = Mathf.Max(0, stunDuration - 1); // 每次按下按钮2减少1秒静止时间
            UpdateStunDurationSlider(); // 更新进度条
        }
    }

    private IEnumerator GetStunned()
    {
        if (!isStunned)
        {
            isStunned = true;
            rb.velocity = Vector2.zero; // 停止玩家移动
            umbrellaCollider.enabled = false;
            playerCollider.enabled = false;
            spriteRenderer.sprite = sprite3; // 切换至被击中的Sprite

            stunDurationSlider.gameObject.SetActive(true);

            while (stunDuration > 0)
            {
                stunDuration -= Time.deltaTime; // 随着时间减少 stunDuration
                UpdateStunDurationSlider(); // 更新进度条
                yield return null; // 等待下一帧
            }

            UnstunPlayer();
        }
    }


    private void UnstunPlayer()
    {
        if (isStunned)
        {
            isStunned = false;
            stunDuration = 5f; // 重置静止时间
            spriteRenderer.sprite = sprite2; // 恢复到常规状态的Sprite
            umbrellaCollider.enabled = true;
            playerCollider.enabled = false;
            stunDurationSlider.gameObject.SetActive(false);
            animator.SetTrigger("Open");
        }
            
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (playerCollider.IsTouching(collision.collider)) // 检查是否是玩家碰撞体被击中
        {
            if (!isStunned) // 如果玩家当前不处于静止状态，则启动静止效果
            {
                StartCoroutine(GetStunned());

            }
        }
        else if (umbrellaCollider.enabled && umbrellaCollider.IsTouching(collision.collider)) // 如果雨伞碰撞体激活并且与其他对象碰撞
        {
            umbrellaCollider.enabled = false; // 禁用雨伞碰撞体
            umbrellaCooldown = cooldownAmount;
            playerCollider.enabled=true;
            spriteRenderer.sprite = sprite1; // 切换到未激活雨伞的Sprite
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
            stunDurationSlider.value = 0; // 如果玩家没有被晕眩，进度条应该为空
        }
    }


}