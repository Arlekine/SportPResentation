using UnityEngine;

public class FollowPlayerHead : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _moveSpeed = 1f;

    private Transform _target;

    public void SetHead(Transform head)
    {
        _target = head;
    }

    private void Update()
    {
        if (_target == null)
            return;

        var localOffset = _target.InverseTransformPoint(_offset);
        var targetPosition = (_target.position + _target.forward * _offset.z);
        var directionToCamera = (_target.position - transform.position).normalized;

        transform.rotation = Quaternion.LookRotation(-directionToCamera, Vector3.up);
        transform.position = Vector3.Lerp(transform.position, targetPosition, _moveSpeed * Time.deltaTime);
    }
}