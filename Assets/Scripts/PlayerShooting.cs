using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject gun;
    public GameObject gunBarrel;
    public GameObject bullet;
    public GameObject crosshair;
    public Camera cam;
    public float shootForce = 10;
    public float rotateSpeed = 70;
    public float raycastMaxDistance = 100000.0f;
    public float maxDegreesToRotate = 25.0f;


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
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // Setup Raycast using crosshair on screen
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(crosshair.transform.position);
            
            // Instantiate bullet and apply implusive force to launch it towards the crosshair
            var bulletInstance = Instantiate(bullet, gunBarrel.transform.position, transform.rotation);
            Vector3 direction = (ray.GetPoint(raycastMaxDistance) - bulletInstance.transform.position).normalized;
            bulletInstance.GetComponent<Rigidbody>().AddForce(direction * shootForce, ForceMode.Impulse);

            // Apply damage to enemies
            if (Physics.Raycast(ray, out hit, raycastMaxDistance))
            {
                Debug.Log(hit.collider.gameObject.name);
            }

            // Destroy bullet if it doesn't hit anything
            Destroy(bulletInstance, 2f);
        }
    }
}
