using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer_RBForce : MonoBehaviour
{
    [SerializeField] private float _speed = 10;
    [SerializeField] private float _rotationSpeed = 10;
    private float _step;
    private float _rotationStep;
    [SerializeField] private Transform _target;
    private Rigidbody _rb;

    void Start()
    {
        _rb= GetComponent<Rigidbody>();
    }

    void Update()
    {
        _step = _speed * Time.deltaTime;
        _rotationStep = _rotationSpeed * Time.deltaTime;
        LookAtPlayer();
        ChasePlayer();
    }

    void LookAtPlayer()
    {
        Vector3 targetDirection = _target.position - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, _rotationStep, 0.0f);

        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    void ChasePlayer()
    {
        _rb.AddForce(transform.forward * _step);
    }

}
