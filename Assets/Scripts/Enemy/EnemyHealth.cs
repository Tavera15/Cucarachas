using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public float health = 100;
    public Image enemyHealthBar;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Adjust healthbar fill according to the amount of health
        enemyHealthBar.fillAmount = health / maxHealth;
    }

    public void ReceiveDamage(float amount)
    {
        health -= amount;

        if(health <= 0)
        {
            // TODO Find a death animation
            Destroy(gameObject, 2f);
        }
    }
}
