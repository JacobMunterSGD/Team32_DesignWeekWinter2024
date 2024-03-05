using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3; // 玩家的最大生命值
    private int currentHealth; // 玩家当前的生命值

    void Start()
    {
        // 每次游戏开始时初始化生命值
        currentHealth = maxHealth;
    }

    // 碰撞检测函数
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 检查碰撞的物体是否是敌人或危险物体
        if (collision.gameObject.CompareTag("Tag0"))
        {
            // 减少生命值
            currentHealth -= 1;
            Debug.Log("Player hit! Current health: " + currentHealth);

            // 检查生命值是否已耗尽
            if (currentHealth <= 0)
            {
                Debug.Log("Game Over!");
                // 这里可以添加游戏结束的逻辑，比如重新加载场景、显示游戏结束画面等
            }
        }
    }
}
