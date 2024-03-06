using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3; 
    private int currentHealth; 

    void Start()
    {
        
        currentHealth = maxHealth;
    }

    
    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Tag0"))
        {
            
            currentHealth -= 1;
            Debug.Log("Player hit! Current health: " + currentHealth);

            
            if (currentHealth <= 0)
            {
                Debug.Log("Game Over!");
                
            }
        }
    }
}
