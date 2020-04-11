using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraComp : MonoBehaviour
{
    public float maxDegreesToRotate = 25.0f;
    
    void Start()
    {

    }

    void Update()
    {
        VerticalMovement();
    }

    // Moves camera vertically within x and -x degrees.
    void VerticalMovement()
    {
        float mouseY = (Input.mousePosition.y / Screen.height) - 0.5f;
        transform.localRotation = Quaternion.Euler(new Vector3(-1f * (mouseY * maxDegreesToRotate), 0, 0));
    }
}
