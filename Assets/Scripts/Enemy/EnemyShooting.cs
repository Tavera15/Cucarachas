using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public float reloadTime = 1.5f;             // Time for bullets fired in between
    public GameObject enemyLaser;               // Bullet to instantiate and fire
    public Transform playerCam;                 // Where the enemy will target at
    public float shootForce = 1350f;            // Force that the bullet will launch at
    public float attackDamage = 10;

    private float lastShotDur = 0;              // Timer of last time when enemy fired
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        lastShotDur += Time.deltaTime;
    }

    public void Shoot()
    {
        if (lastShotDur >= reloadTime)
        {
            anim.SetTrigger("attack");

            // Instantiate bullet/laser
            var laser = Instantiate(enemyLaser, transform.position, transform.rotation);
            laser.GetComponent<EnemyLaser>().damageAmount = attackDamage;

            // Calculate the direction between enemy and camera position, and launch the bullet using shoot force
            Vector3 direction = (playerCam.position - laser.transform.position).normalized;
            laser.GetComponent<Rigidbody>().AddForce(direction * shootForce, ForceMode.Force);

            Destroy(laser, 2f);         // Destory bullet after a certain time
            lastShotDur = 0;            // Reset timer for enemy so the enemy can't shoot for a duration
        }
    }

}
