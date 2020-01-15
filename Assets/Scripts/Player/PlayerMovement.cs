using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10;
    public float rotateSpeed = 7;
    public bool isPulseEnabled = false;

    private Animator anim;
    private float yaw = 0;
    private float initialSpeed;
    private float buffTimer = 0;
    private float pulsePower = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        initialSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        // Basic player movement
        PlayerMove();
        PlayerTurn();

        // Decrease buff timer and reset speed
        if (buffTimer <= 0)
            speed = initialSpeed;

        buffTimer -= Time.deltaTime;

        // Pulse buff activation. Player can only launch enemies if they pick up the buff and press Spacebar
        if(isPulseEnabled)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                pulseBuff();
                isPulseEnabled = false;
            }
        }
    }

    void PlayerMove()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(h * Time.deltaTime * speed, 0, v * Time.deltaTime * speed));
        anim.SetFloat("forward", v);
    }

    // Rotate player horizontally using mouse
    void PlayerTurn()
    {
        float mouseXdelta = (Input.GetAxis("Mouse X") * rotateSpeed);
        yaw += mouseXdelta;

        transform.rotation = Quaternion.Euler(new Vector3(0, yaw, 0));
    }

    /// <summary>
    /// Player's movement speed will be increased by a specified amount and duration.
    /// </summary>
    /// <param name="amountToIncrementBy">Value to be added to the player's original speed value by.</param>
    /// <param name="duration">Specify the duration the buff will be active.</param>
    public void buffMovementSpeed(float amountToIncrementBy, float duration)
    {
        speed += amountToIncrementBy;
        buffTimer = duration;
    }

    public void ActivatePulses(float power)
    {
        pulsePower = power;
        isPulseEnabled = true;
    }

    // TODO Adjust numbers in pulseBuff function.
    void pulseBuff()
    {
        var explosionPos = transform.position;
        var radius = 20;

        Collider[] colliders = Physics.OverlapSphere(explosionPos, 10);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null && hit.gameObject.tag == "Enemy")
            {
                hit.gameObject.GetComponent<EnemyMovement>().EnemyStun(2);
                rb.AddExplosionForce(pulsePower, explosionPos, radius, 1f, ForceMode.Impulse);
            }
        }
    }
}
