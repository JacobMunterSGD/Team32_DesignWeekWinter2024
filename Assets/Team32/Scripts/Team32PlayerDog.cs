using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Team32PlayerDog : MicrogameInputEvents
{
    public Sprite sprite1; // Sprite 1��״̬�ܷ���ʱʹ��
    public Sprite sprite2; // Sprite 2��״̬���ܷ���ʱʹ��
    public Sprite sprite3; // Sprite 3������סʱʹ��

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private float moveSpeed = 4f;
    private float score = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite1; // ��ʼ״̬Ϊ״̬�ܷ���ʱ��sprite
    }

    private void FixedUpdate()
    {
        float moveBy = stick.x * Time.deltaTime * moveSpeed;
        if (Mathf.Abs(rb.position.x + moveBy) > 5.5f) return; // �����ƶ���Χ

        // �����ƶ�ʱ����sprite�Ĵ�ֱ����
        if (moveBy < 0)
            spriteRenderer.flipX = true; // ����ֱ��ת
        else if (moveBy > 0)
            spriteRenderer.flipX = false; // ��������

        rb.MovePosition(rb.position + new Vector2(moveBy, 0));
    }

    void Update()
    {
        // �л�sprite2��״̬���ܷ���ʱʹ�ã���sprite3������סʱʹ�ã�
        if (stick.y == 1)
            spriteRenderer.sprite = sprite2; // �л�Ϊ״̬���ܷ���ʱ��sprite
        else
            spriteRenderer.sprite = sprite1; // �л�Ϊ״̬�ܷ���ʱ��sprite
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ��ײʱ���ӵ÷�
        score++;
        Debug.Log("Score: " + score);
    }

    protected override void OnTimesUp()
    {
        Debug.Log("Final Score: " + score);
    }
}
