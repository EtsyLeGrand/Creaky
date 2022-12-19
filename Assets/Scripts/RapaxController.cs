using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapaxController : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private CharacterController characterController;
    private bool isWalkingForward = false;
    private void Update()
    {
        if (isWalkingForward)
        {
            float vertical = walkSpeed;
            Vector3 move = transform.forward * vertical;
            characterController.Move(walkSpeed * Time.deltaTime * move);
        }
    }

    public void WalkForward()
    {
        isWalkingForward = true;
    }
}
