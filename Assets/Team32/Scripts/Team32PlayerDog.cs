using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class Team32PlayerDog : MicrogameInputEvents
{
    public Sprite sprite1; // ���δ������ɡʱ��Sprite
    public Sprite sprite2; // ��Ҽ�����ɡʱ��Sprite
    public Sprite sprite3;
    public PolygonCollider2D umbrellaCollider; // ��ɡ��PolygonCollider2D
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

        // ��Ϸ��ʼʱ��ɡ��ײ��Ĭ�Ͻ���
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
        // �����ť1�����£��л���ɡ��ײ��ļ���״̬
        if (!umbrellaCollider.enabled)
        {
            spriteRenderer.sprite = sprite2; // �л���������ɡ��Sprite
            umbrellaCollider.enabled = true; // ������ɡ��ײ��
            playerCollider.enabled = false;
        }
        else
        {
            spriteRenderer.sprite = sprite1; // �л���δ������ɡ��Sprite
            umbrellaCollider.enabled = false; // ������ɡ��ײ��
            playerCollider.enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �����ɡ��ײ�弤��ҷ�������ײ
        if (umbrellaCollider.enabled)
        {
            umbrellaCollider.enabled = false; // ������ɡ��ײ��
            playerCollider.enabled = true;
            spriteRenderer.sprite = sprite1; // �л���δ������ɡ��Sprite
        }
    }
}