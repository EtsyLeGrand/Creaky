using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private GameObject eyes;
    [SerializeField] private PlayerFeet feet;
    private GameObject currentWatchedObject;

    [Header("Movement")]
    private Vector3 velocity;
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float sprintSpeed = 1.5f;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float distanceBeforeCreak = 10.0f;
    private float walkedDistance = 0.0f;

    [Header("Mouse look")]
    [SerializeField] private float mouseSensitivity = 100.0f;
    [SerializeField] private float clampAngle = 80.0f;
    [SerializeField] private float maxRaycastDistance = 10.0f;

    [Header("Input")]
    [SerializeField] private KeyCode[] inspectKeys;
    [SerializeField] private KeyCode[] interactKeys;

    private int ignoredMask;

    private float pitch = 0.0f; // rotation around the up/y axis
    private float yaw = 0.0f; // rotation around the right/x axis

    private bool isLocked = false;

    public GameObject Eyes { get => eyes; }

    private void Start()
    {
        // Lock cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        ignoredMask = 1 << LayerMask.NameToLayer("Prop");

        GetComponent<Animator>().Play("Intro");
        isLocked = true;
    }

    void Update()
    {
        if (!isLocked)
        {
            #region Mouse Input
            yaw = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            transform.Rotate(0, yaw, 0);

            pitch = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            pitch = ClampPitchAngle(eyes.transform.rotation.eulerAngles.x - pitch, -clampAngle, clampAngle);

            eyes.transform.localEulerAngles = new Vector3(pitch, 0f, 0f);
            #endregion

            #region Movement
            if (Input.GetKey(KeyCode.LeftShift))
            {
                float horizontal = Input.GetAxisRaw("Horizontal") * sprintSpeed;
                float vertical = Input.GetAxisRaw("Vertical") * sprintSpeed;
                Vector3 move = transform.right * horizontal + transform.forward * vertical;
                characterController.Move(sprintSpeed * Time.deltaTime * move);
                walkedDistance += sprintSpeed * Time.deltaTime * move.magnitude;
            }
            else
            {
                float horizontal = Input.GetAxisRaw("Horizontal") * speed;
                float vertical = Input.GetAxisRaw("Vertical") * speed;
                Vector3 move = transform.right * horizontal + transform.forward * vertical;
                characterController.Move(speed * Time.deltaTime * move);
                walkedDistance += sprintSpeed * Time.deltaTime * move.magnitude;
            }
            #endregion

            #region Gravity
            velocity.y += gravity * Time.deltaTime;
            characterController.Move(velocity * Time.deltaTime);

            #endregion

            #region Raycast
            LookForInteractableInspectable();
            #endregion

            #region Inspect Management
            if (IsInspectKeyDown() && currentWatchedObject != null)
            {
                if (currentWatchedObject.TryGetComponent(out Inspectable inspectable))
                {
                    inspectable.Inspect();
                    currentWatchedObject = null;
                    LookForInteractableInspectable();
                }
            }
            else if (IsInteractKeyDown() && currentWatchedObject != null)
            {
                if (currentWatchedObject.TryGetComponent(out Interactable interactable))
                {
                    interactable.Interact();
                    currentWatchedObject = null;
                    if (interactable.DestroyOnUse) EventManager.TriggerEvent("OnNotLookingAtInteractable", new Dictionary<string, object>());
                }
            }
            #endregion

            #region Creak
            if (walkedDistance >= distanceBeforeCreak)
            {
                walkedDistance = 0.0f;
                feet.Step();
            }
            #endregion
        }
    }

    private float ClampPitchAngle(float pitch, float minAngle, float maxAngle)
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

    private bool IsInteractKeyDown()
    {
        foreach (KeyCode key in interactKeys)
        {
            if (Input.GetKeyDown(key)) return true;
        }
        return false;
    }

    private bool IsInspectKeyDown()
    {
        foreach (KeyCode key in inspectKeys)
        {
            if (Input.GetKeyDown(key)) return true;
        }
        return false;
    }

    private void LookForInteractableInspectable()
    {
        RaycastHit hit;
        if (Physics.Raycast(eyes.transform.position, eyes.transform.forward, out hit, maxRaycastDistance, ignoredMask))
        {
            // Set watched obj
            if (currentWatchedObject != hit.collider.gameObject)
            {
                currentWatchedObject = hit.collider.gameObject;
                if (currentWatchedObject.TryGetComponent(out Inspectable inspectable))
                {
                    EventManager.TriggerEvent("OnLookingAtInspectable", new Dictionary<string, object>());

                    foreach (Outline outline in inspectable.OutlineObject)
                    {
                        outline.eraseRenderer = false;
                    }

                    if (currentWatchedObject.TryGetComponent(out Interactable interactable))
                    {
                        if (interactable.CanBeInteracted()) EventManager.TriggerEvent("OnLookingAtInteractable", new Dictionary<string, object>());
                    }
                }
            }
        }
        else if (currentWatchedObject != null)
        {
            // Clear watched obj
            if (currentWatchedObject.TryGetComponent(out Inspectable inspectable))
            {
                foreach (Outline outline in inspectable.OutlineObject)
                {
                    outline.eraseRenderer = true;
                }
            }
            currentWatchedObject = null;

            EventManager.TriggerEvent("OnNotLookingAtInspectable", new Dictionary<string, object>());
            EventManager.TriggerEvent("OnNotLookingAtInteractable", new Dictionary<string, object>());
        }
    }

    public void Lock()
    {
        isLocked = true;
    }

    public void Unlock()
    {
        isLocked = false;
    }

    public void AfterIntro()
    {
        Unlock();
        Destroy(GetComponent<Animator>());
    }
}