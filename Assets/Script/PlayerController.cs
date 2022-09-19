using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _turnSpeed = 360;
    private Vector3 _input;
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private Transform _model;

    private void Update() {
        GatherInput();
        Look();
    }

    private void FixedUpdate() {
        Move();
    }

    private void GatherInput() {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    private void Look()
    {
        if (_input == Vector3.zero) return;

        Quaternion rot = Quaternion.LookRotation(_input.ToIso(), Vector3.up);
        _model.rotation = Quaternion.RotateTowards(_model.rotation, rot, _turnSpeed * Time.deltaTime);
    }

    private void Move()
    {
        _rigidBody.MovePosition(transform.position + _input.ToIso() * _input.normalized.magnitude * _speed * Time.deltaTime);
    }
}

