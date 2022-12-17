using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private GameObject eyes;
    private Vector3 velocity;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float jump = 10f;
    [SerializeField] private float gravity = -9.8f;

    // Mouse look variables
    [SerializeField] private float mouseSensitivity = 100.0f;
    [SerializeField] private float clampAngle = 80.0f;

    private float pitch = 0.0f; // rotation around the up/y axis
    private float yaw = 0.0f; // rotation around the right/x axis

    private void Start()
    {
        // Lock cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Handle mouse input
        yaw = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        transform.Rotate(0, yaw, 0);

        pitch = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        pitch = ClampPitchAngle(eyes.transform.rotation.eulerAngles.x -pitch, -clampAngle, clampAngle);

        eyes.transform.localEulerAngles = new Vector3(pitch, 0f, 0f);

        // Movement
        float horizontal = Input.GetAxisRaw("Horizontal") * speed;
        float vertical = Input.GetAxisRaw("Vertical") * speed;
        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        characterController.Move(speed * Time.deltaTime * move);

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    float ClampPitchAngle(float pitch, float minAngle, float maxAngle)
    {
        // convert angle to -180 to 180 range
        if (pitch > 180)
        {
            pitch -= 360;
        }

        // clamp pitch angle to specified range
        pitch = Mathf.Clamp(pitch, minAngle, maxAngle);

        // return clamped pitch angle
        return pitch;
    }
}