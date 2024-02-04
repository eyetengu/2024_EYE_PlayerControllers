using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewController_A1 : MonoBehaviour
{
    [SerializeField] private float _playerSpeed = 3;
    [SerializeField] private float _playerTurnSpeed = 3;
    [SerializeField] private CharacterController _controller;
    [SerializeField] private float _jumpPower = 5f;
    [SerializeField] private float _gravity = 9.8f;
    private float _yDirection = 0;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        MovePlayerAround();
    }

    private void MovePlayerAround()
    {
        //setup the player movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //setup player rotation
        transform.Rotate(0, horizontalInput * _playerTurnSpeed * Time.deltaTime, 0);


        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        direction = transform.TransformDirection(direction);

        Vector3 velocity = direction * _playerSpeed * Time.deltaTime;

        if (_controller.isGrounded)
        {
            Debug.Log("Grounded");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yDirection = _jumpPower;
            }
        }
        else
        {
            Debug.Log("Not Grounded");
            _yDirection -= _gravity * Time.deltaTime;
        }

        direction.y = _yDirection;

        _controller.Move(velocity);

    }
}
