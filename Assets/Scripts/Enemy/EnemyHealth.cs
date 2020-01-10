using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public float health = 100;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReceiveDamage(float amount)
    {
        health -= amount;

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
