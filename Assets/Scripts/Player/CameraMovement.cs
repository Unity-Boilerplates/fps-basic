using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] InputReader inputReader;

    [SerializeField] float mouseSensitivity = 100f;
    [SerializeField] Transform cameraTransform;

    float xRotation = 0f;
    Vector2 movementInput;


    private void OnEnable()
    {
        inputReader.lookEvent += moveCamera;


    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void moveCamera(Vector2 movement)
    {
        float movementX = movement.x * mouseSensitivity * Time.deltaTime;
        float movementY = movement.y * mouseSensitivity * Time.deltaTime;

        xRotation -= movementY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        cameraTransform.Rotate(Vector3.up * movementX);
    }
}
