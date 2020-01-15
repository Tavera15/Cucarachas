using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public float speed = .65f;                  // .65 is a decent number
    public float shootDitance = 7;              // Distance between enemy and player before enemy starts shooting

    private EnemyShooting enemyShooting;
    private Animator anim;
    private bool canMove = true;
    private float stunTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        enemyShooting = GetComponent<EnemyShooting>();
    }

    // Update is called once per frame
    void Update()
    {
        // Enemy will always face the player and approach them. 
        // If enemy is within shooting range, enemy will attack player.
        LookAtPlayer();
        bool isEnemyInRange = Vector3.Distance(transform.position, player.position) <= shootDitance;

        if (isEnemyInRange && canMove)
        {
            anim.SetBool("isMoving", false);
            enemyShooting.Shoot();
        }
        else if(canMove == true)
        {
            MoveTowardsPlayer();
        }

        // If enemy is stun, decrease the timer to reset enemy movement
        if (stunTimer <= 0)
            canMove = true;

        stunTimer -= Time.deltaTime;
    }

    // Update the rotation to face the player
    void LookAtPlayer()
    {
        Vector3 relativePos = player.position - transform.position;
        transform.rotation = Quaternion.LookRotation(relativePos, Vector3.up);
    }

    void MoveTowardsPlayer()
    {
        transform.position = Vector3.Lerp(transform.position, player.position, speed * Time.deltaTime);
        anim.SetBool("isMoving", true);
    }

    // Enemies won't move nor attack for a specified duration
    public void EnemyStun(float duration)
    {
        canMove = false;
        stunTimer = duration;
    }
}
