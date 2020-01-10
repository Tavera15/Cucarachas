using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public GameObject gun;
    public GameObject gunBarrel;
    public GameObject bullet;                               // Bullet to instatiate
    public GameObject crosshair;
    public Camera cam;
    public float shootForce = 10;                           // The amount of force the bullet will travel
    public float raycastMaxDistance = 100000.0f;            // How far the raycast will travel
    public float maxDegreesToRotate = 25.0f;                // How far to rotate gun vertically
    public float attackDamage = 10;                         // How much damage the bullet will inflict

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GunVerticalMovement();
        SpawnBullet();
    }

    void GunVerticalMovement()
    {
        // Moves camera vertically within x and -x degrees.
        float mouseY = (Input.mousePosition.y / Screen.height) - 0.5f;
        gun.transform.localRotation = Quaternion.Euler(new Vector3(-10, 90, -1f * (mouseY * maxDegreesToRotate)));
    }

    void SpawnBullet()
    {
        // Setup Raycast using crosshair on screen
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(crosshair.transform.position);

        // Change crosshair color if it's hovering over an enemy to red - else to white
        crosshair.GetComponent<Image>().color = (Physics.Raycast(ray, out hit, raycastMaxDistance) 
            && hit.collider.tag == "Enemy" ? Color.red : Color.white);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Instantiate bullet and apply implusive force to launch it towards the crosshair
            var bulletInstance = Instantiate(bullet, gunBarrel.transform.position, transform.rotation);
            Vector3 direction = (ray.GetPoint(raycastMaxDistance) - bulletInstance.transform.position).normalized;
            bulletInstance.GetComponent<Rigidbody>().AddForce(direction * shootForce, ForceMode.Impulse);

            // Apply damage to enemies
            if (hit.collider.tag == "Enemy")
            {
                hit.collider.gameObject.GetComponent<EnemyHealth>().ReceiveDamage(attackDamage);
            }

            // Destroy bullet instance
            Destroy(bulletInstance, 2f);
        }
    }
}
