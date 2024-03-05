using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3; // ��ҵ��������ֵ
    private int currentHealth; // ��ҵ�ǰ������ֵ

    void Start()
    {
        // ÿ����Ϸ��ʼʱ��ʼ������ֵ
        currentHealth = maxHealth;
    }

    // ��ײ��⺯��
    void OnCollisionEnter2D(Collision2D collision)
    {
        // �����ײ�������Ƿ��ǵ��˻�Σ������
        if (collision.gameObject.CompareTag("Tag0"))
        {
            // ��������ֵ
            currentHealth -= 1;
            Debug.Log("Player hit! Current health: " + currentHealth);

            // �������ֵ�Ƿ��Ѻľ�
            if (currentHealth <= 0)
            {
                Debug.Log("Game Over!");
                // ������������Ϸ�������߼����������¼��س�������ʾ��Ϸ���������
            }
        }
    }
}
