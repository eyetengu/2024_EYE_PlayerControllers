using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeslerController : MonoBehaviour
{
    //from a conversation with Thom Kesler on 9/11/23

    private CharacterController _controller;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _rotationSpeed = 1.0f;
    [SerializeField] private float _jumpPower = 5f;
    [SerializeField] private float _gravity = 9.8f;
    private float _yDirection = 0;

    private void Start()
    {
        _controller= GetComponent<CharacterController>();
    }

    private void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        float hMovement = Input.GetAxis("Horizontal");
        float vMovement = Input.GetAxis("Vertical");
        float yRotation = Input.GetAxis("Mouse X");
        //use this commented code to rotate an fps camera on its x axis
        //float xRotation = Input.GetAxis("Mouse Y");

        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.y += yRotation * _rotationSpeed;
        //rotation.x += xRotation * _rotationSpeed;
        transform.rotation = Quaternion.Euler(rotation);

        Vector3 direction = new Vector3(hMovement, 0, vMovement);
        direction = transform.TransformDirection(direction);

        if(_controller.isGrounded)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                _yDirection = _jumpPower;
            }
        }
        else
        {
            _yDirection -= _gravity * Time.deltaTime;
        }

        direction.y = _yDirection;

        _controller.Move(direction * (_speed * Time.deltaTime));       
    }
}
