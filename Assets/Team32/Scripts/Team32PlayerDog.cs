using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Team32PlayerDog : MicrogameInputEvents
{
    public Sprite sprite1; // Sprite 1，状态能反弹时使用
    public Sprite sprite2; // Sprite 2，状态不能反弹时使用
    public Sprite sprite3; // Sprite 3，被困住时使用

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private float moveSpeed = 4f;
    private float score = 0f;

        public float leftBoundary = -5.5f; // 左边界
    public float rightBoundary = 5.5f; // 右边界

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite1; // 初始状态为状态能反弹时的sprite
    }

    private void FixedUpdate()
    {
        float moveBy = stick.x * Time.deltaTime * moveSpeed;
        float newXPosition = Mathf.Clamp(rb.position.x + moveBy, leftBoundary, rightBoundary);
        //if (Mathf.Abs(rb.position.x + moveBy) > 5.5f) return; // 限制移动范围

        // 左右移动时调整sprite的垂直方向
        if (moveBy < 0)
            spriteRenderer.flipX = true; // 向左水平反转
        else if (moveBy > 0)
            spriteRenderer.flipX = false; // 向右正常

        rb.MovePosition(new Vector2(newXPosition, rb.position.y));
    }

    void Update()
    {
        // 切换sprite2（状态不能反弹时使用）和sprite3（被困住时使用）
        if (stick.y == 1)
            spriteRenderer.sprite = sprite2; // 切换为状态不能反弹时的sprite
        else
            spriteRenderer.sprite = sprite1; // 切换为状态能反弹时的sprite
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 碰撞时增加得分
        score++;
        Debug.Log("Score: " + score);
    }

    protected override void OnTimesUp()
    {
        Debug.Log("Final Score: " + score);
    }
}
