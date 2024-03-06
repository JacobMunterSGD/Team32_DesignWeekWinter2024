using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Team32PlayerDog : MicrogameInputEvents
{
    public Sprite sprite1; // ���δ������ɡʱ��Sprite
    public Sprite sprite2; // ��Ҽ�����ɡʱ��Sprite
    public Sprite sprite3;
    public PolygonCollider2D umbrellaCollider; // ��ɡ��PolygonCollider2D
    public CapsuleCollider2D playerCollider;
    public Slider progressSlider;

    public Animator animator;

    public Transform Goal;
    private float startDistance;

    public Slider stunDurationSlider; // �� Inspector �������������

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

        // ��Ϸ��ʼʱ��ɡ��ײ��Ĭ�Ͻ���
        umbrellaCollider.enabled = false;
        playerCollider.enabled = true;
        stunDurationSlider.gameObject.SetActive(false);

        startDistance = Vector3.Distance(transform.position, Goal.position);
        progressSlider.maxValue = startDistance; // ���� Slider �����ֵ
        progressSlider.value = startDistance; // ��ʼʱ��������յ�ľ���������
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
        progressSlider.value = startDistance - currentDistance; // ���� Slider����ӳ�����Ŀ���ʣ�����
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
        // �����ť1�����£��л���ɡ��ײ��ļ���״̬
        if (!umbrellaCollider.enabled && !isStunned && umbrellaCooldown <= 0)
        {
            spriteRenderer.sprite = sprite2; // �л���������ɡ��Sprite
            umbrellaCollider.enabled = true; // ������ɡ��ײ��
            playerCollider.enabled = false;
            animator.SetTrigger("Open");
        }
        else
        {
            if ( !isStunned)
            {
                spriteRenderer.sprite = sprite1; // �л���δ������ɡ��Sprite
                umbrellaCollider.enabled = false; // ������ɡ��ײ��
                playerCollider.enabled = true;
                animator.SetTrigger("Close");
            }
                
        }
    }

    protected override void OnButton2Pressed(InputAction.CallbackContext context)
    {
        if (isStunned)
        {
            ReduceStunTime(); // ���پ�ֹʱ��
        }
    }

    private void ReduceStunTime()
    {
        if (isStunned)
        {
            stunDuration = Mathf.Max(0, stunDuration - 1); // ÿ�ΰ��°�ť2����1�뾲ֹʱ��
            UpdateStunDurationSlider(); // ���½�����
        }
    }

    private IEnumerator GetStunned()
    {
        if (!isStunned)
        {
            isStunned = true;
            rb.velocity = Vector2.zero; // ֹͣ����ƶ�
            umbrellaCollider.enabled = false;
            playerCollider.enabled = false;
            spriteRenderer.sprite = sprite3; // �л��������е�Sprite

            stunDurationSlider.gameObject.SetActive(true);

            while (stunDuration > 0)
            {
                stunDuration -= Time.deltaTime; // ����ʱ����� stunDuration
                UpdateStunDurationSlider(); // ���½�����
                yield return null; // �ȴ���һ֡
            }

            UnstunPlayer();
        }
    }


    private void UnstunPlayer()
    {
        if (isStunned)
        {
            isStunned = false;
            stunDuration = 5f; // ���þ�ֹʱ��
            spriteRenderer.sprite = sprite2; // �ָ�������״̬��Sprite
            umbrellaCollider.enabled = true;
            playerCollider.enabled = false;
            stunDurationSlider.gameObject.SetActive(false);
            animator.SetTrigger("Open");
        }
            
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (playerCollider.IsTouching(collision.collider)) // ����Ƿ��������ײ�屻����
        {
            if (!isStunned) // �����ҵ�ǰ�����ھ�ֹ״̬����������ֹЧ��
            {
                StartCoroutine(GetStunned());

            }
        }
        else if (umbrellaCollider.enabled && umbrellaCollider.IsTouching(collision.collider)) // �����ɡ��ײ�弤���������������ײ
        {
            umbrellaCollider.enabled = false; // ������ɡ��ײ��
            umbrellaCooldown = cooldownAmount;
            playerCollider.enabled=true;
            spriteRenderer.sprite = sprite1; // �л���δ������ɡ��Sprite
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
            stunDurationSlider.value = 0; // ������û�б���ѣ��������Ӧ��Ϊ��
        }
    }


}