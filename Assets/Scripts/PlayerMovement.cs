using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;

    private Vector3 PlayerVelocity;
    private bool isGrounded;
    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }


    void Update()
    {
        isGrounded = characterController.isGrounded;
    }

    public void ProcessMove(Vector2 Input)
    {
        Vector3 moveDir = Vector3.zero;
        moveDir.x = Input.x;
        moveDir.z = Input.y;
        characterController.Move(transform.TransformDirection(moveDir) * speed * Time.deltaTime);
        PlayerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && PlayerVelocity.y < 0) {
            PlayerVelocity.y = -2f;
        }
        characterController.Move(PlayerVelocity * Time.deltaTime);
    }

    public void jump()
    {
        if (isGrounded)
        {
            PlayerVelocity.y = Mathf.Sqrt(jumpHeight * -3f * gravity);
        }
    }
}
