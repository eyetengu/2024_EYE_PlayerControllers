using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBCC_PlayerController : MonoBehaviour
{
    //This particular script is from the GDHQ 2.5D Platformer course section
    //Controls the controller movement, jump and double jump

    private CharacterController _controller;

    private float _speed = 5.0f;
    private float _gravity = 1.0f;
    private float _jumpHeight = 15.0f;
    private float _yVelocity;

    private bool _canDoubleJump;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(0,0,verticalInput);
        Vector3 velocity = direction * _speed;

        if (_controller.isGrounded == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
                _canDoubleJump = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_canDoubleJump)
                {
                    _yVelocity += _jumpHeight;
                    _canDoubleJump = false;
                }
            }

            _yVelocity -= _gravity;
        }

        velocity.y = _yVelocity;

        _controller.Move(velocity * Time.deltaTime);
    }
}
