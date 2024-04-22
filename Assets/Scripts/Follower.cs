using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Follower : MonoBehaviour
{
    [SerializeField] private Player _target;
    [SerializeField] private float _movingSpeed;
    [SerializeField] private float _minDistance;
    [SerializeField] private float _gravityScale = 10f;


    private Rigidbody _rigidbody;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _rigidbody.AddForce(Physics.gravity * _gravityScale, ForceMode.Acceleration);

        transform.LookAt(_target.transform);

        FollowTarget();
    }

    private void FollowTarget()
    {
        if (Vector3.Distance(transform.position, _target.transform.position) <= _minDistance)
            return;

        Vector3 forward = _target.transform.position - transform.position;
        forward = Vector3.ProjectOnPlane(forward, Vector3.up).normalized * _movingSpeed * Time.deltaTime;

        _rigidbody.MovePosition(transform.position + forward);
    }
}
