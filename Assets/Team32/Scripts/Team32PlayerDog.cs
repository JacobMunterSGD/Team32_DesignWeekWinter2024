using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class Team32PlayerDog : MicrogameInputEvents
{
    public Sprite sprite1; // 玩家未激活雨伞时的Sprite
    public Sprite sprite2; // 玩家激活雨伞时的Sprite
    public Sprite sprite3;
    public PolygonCollider2D umbrellaCollider; // 雨伞的PolygonCollider2D
    public BoxCollider2D playerCollider;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private float moveSpeed = 4f;

    public float leftBoundary = -5.5f;
    public float rightBoundary = 5.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite1;

        // 游戏开始时雨伞碰撞体默认禁用
        umbrellaCollider.enabled = false;
        playerCollider.enabled = true;
    }

    private void FixedUpdate()
    {
        float moveBy = stick.x * Time.deltaTime * moveSpeed;
        float newXPosition = Mathf.Clamp(rb.position.x + moveBy, leftBoundary, rightBoundary);

        if (moveBy < 0)
            spriteRenderer.flipX = true;
        else if (moveBy > 0)
            spriteRenderer.flipX = false;

        rb.MovePosition(new Vector2(newXPosition, rb.position.y));
    }

    protected override void OnButton1Pressed(InputAction.CallbackContext context)
    {
        // 如果按钮1被按下，切换雨伞碰撞体的激活状态
        if (!umbrellaCollider.enabled)
        {
            spriteRenderer.sprite = sprite2; // 切换到激活雨伞的Sprite
            umbrellaCollider.enabled = true; // 激活雨伞碰撞体
            playerCollider.enabled = false;
        }
        else
        {
            spriteRenderer.sprite = sprite1; // 切换到未激活雨伞的Sprite
            umbrellaCollider.enabled = false; // 禁用雨伞碰撞体
            playerCollider.enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 如果雨伞碰撞体激活并且发生了碰撞
        if (umbrellaCollider.enabled)
        {
            umbrellaCollider.enabled = false; // 禁用雨伞碰撞体
            playerCollider.enabled = true;
            spriteRenderer.sprite = sprite1; // 切换到未激活雨伞的Sprite
        }
    }
}