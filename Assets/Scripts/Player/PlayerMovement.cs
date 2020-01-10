using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10;
    public float rotateSpeed = 7;

    private Animator anim;
    private float yaw = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerTurn();
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
}
