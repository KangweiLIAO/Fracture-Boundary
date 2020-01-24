using UnityEngine;

public class Target : MonoBehaviour
{

    public float health = 50f;

    private float currentHealth;

    private void OnEnable()
    {
        currentHealth = health;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }
}