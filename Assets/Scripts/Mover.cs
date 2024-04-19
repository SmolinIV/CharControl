using System;
using UnityEngine;

[RequireComponent (typeof(CharacterController))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _movingSpeed = 5f;
    [SerializeField] private float _lookingSpeed = 5f;
    [SerializeField] private float _verticalLookingSpeed = 5f;
    [SerializeField] private float _gravityScale = 100f;
    [SerializeField] private Transform _cameraTransform;

    private CharacterController _characterController;

    private Vector2 _movingDirection;
    private Vector2 _lookingDirection;

    private float _minVerticalLookAngle = -89f;
    private float _maxVerticalLookAngle = 89f;
    private float _cuurentVerticalLookAngle = 0;


    public void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _cuurentVerticalLookAngle = _cameraTransform.localRotation.eulerAngles.x;
    }

    private void FixedUpdate()
    {
        Move();
        Look();
    }

    public void UpdateMovingDirection(Vector2 movingDirection)
    {
        _movingDirection = movingDirection;
    }

    public void UpdateLookingDirection(Vector2 lookingDirection)
    {
        _lookingDirection = lookingDirection;
    }

    private void Move()
    {
        float scaledMovingSpeed = _movingSpeed * Time.deltaTime;

        Vector3 forward = Vector3.ProjectOnPlane(_cameraTransform.forward, Vector3.up).normalized * _movingDirection.y;
        Vector3 right = Vector3.ProjectOnPlane(_cameraTransform.right, Vector3.up).normalized * _movingDirection.x;

        Vector3 offset;

        if (_characterController.isGrounded)
            offset = (forward + right) * scaledMovingSpeed + Vector3.down;
        else
            offset = Physics.gravity * _gravityScale * Time.deltaTime;

        _characterController.Move(offset);
    }

    private void Look()
    {
        LookVertical(); 
        float scaledLookingSpeed = _lookingSpeed * Time.deltaTime;
        Vector3 offset = new Vector3(0, _lookingDirection.x, 0) * scaledLookingSpeed;

        transform.Rotate(offset);

    }

    private void LookVertical()
    {
        _cuurentVerticalLookAngle -= _lookingDirection.y;
        _cuurentVerticalLookAngle = Mathf.Clamp(_cuurentVerticalLookAngle, _minVerticalLookAngle, _maxVerticalLookAngle);
        _cameraTransform.localEulerAngles =  Vector3.right * _cuurentVerticalLookAngle * _lookingSpeed * Time.deltaTime;
    }
}
