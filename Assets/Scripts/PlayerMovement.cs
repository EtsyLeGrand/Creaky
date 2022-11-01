using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]

    [SerializeField] private float movementSpeed = 5f;
    private Rigidbody body;

    [Header("Rotation")]

    [SerializeField] private GameObject eyes;

    [Range(0.1f, 9f)] [SerializeField] float sensitivity = 2f;

    [Tooltip("Limits camera rotation. Prevents the flipping that happens when rotation goes above 90.")]
    [Range(0f, 90f)] [SerializeField] float yRotationLimit = 88f;
    private float xRotationEyesLimit = 40.0f;
    Vector2 rotation = Vector2.zero;
    const string xAxis = "Mouse X"; //Strings in direct code generate garbage, storing and re-using them creates no garbage
    const string yAxis = "Mouse Y";

    public float Sensitivity
    {
        get { return sensitivity; }
        set { sensitivity = value; }
    }

    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Rotate();
        Move();
    }

    private void Rotate()
    {

    }

    private void Move()
    {
        
    }

    
}