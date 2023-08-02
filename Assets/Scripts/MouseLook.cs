using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        float MouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
        playerBody.Rotate(Vector3.up * MouseX);
        
        xRotation -= MouseY;
        transform.localRotation = Quaternion.Euler(xRotation, 0 ,0);
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
    }
}
