using Unity.Netcode;
using UnityEngine;

[DefaultExecutionOrder(1000)]
public class NetcodeTransformSimple : NetworkBehaviour
{
    [Space]
    public Transform target;
    [Space]
    public bool syncPosition = true;
    public bool syncRotation = true;
    public float smooth = 100f;
    public float teleportThreshold = 3f;
    [Space]
    public bool syncLocal = true;
    [Space]
    public bool canUpdate = true;
    
    //-----------------------------------------------------------------------------------------------------------------//
    private NetworkVariable<Vector3> _position = new NetworkVariable<Vector3>(new Vector3(float.MaxValue, float.MaxValue, float.MaxValue),
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner);

    private NetworkVariable<Quaternion> _rotation = new NetworkVariable<Quaternion>(new Quaternion(float.MaxValue, float.MaxValue, float.MaxValue, float.MaxValue),
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner);

    //-----------------------------------------------------------------------------------------------------------------//
    private Vector3 _posCur;
    private Quaternion _rotCur;
    private Vector3 _posDest;
    private Quaternion _rotDest;

    //-----------------------------------------------------------------------------------------------------------------//
    void Awake()
    {
        if (target == null)
        {
            target = transform;
        }

        _posCur = _posDest = syncLocal ? target.localPosition : target.position;
        _rotCur = _rotDest = syncLocal ? target.localRotation : target.rotation;

        _position.OnValueChanged += (prev, cur) =>
        {
            _posDest = cur;

            if (IsOwner)
            {
                // На случай смены владельца 
                _posCur = cur;
            }
        };

        _rotation.OnValueChanged += (prev, cur) =>
        {
            _rotDest = cur;

            if (IsOwner)
            {
                _rotCur = cur;
            }
        };
    }

    //-----------------------------------------------------------------------------------------------------------------//
    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            if (_position.Value.x != float.MaxValue)
            { 
                _posDest = _position.Value;
            }

            if (_rotation.Value.x != float.MaxValue)
            {
                _rotDest = _rotation.Value;
            }
        }
    }

    //-----------------------------------------------------------------------------------------------------------------//
    void LateUpdate()
    {
        if (!IsSpawned)
        {
            return;
        }

        if (IsOwner)
        {
            if (syncPosition)
            {
                _position.Value = syncLocal ? target.localPosition : target.position;
            }

            if (syncRotation)
            {
                _rotation.Value = syncLocal ? target.localRotation : target.rotation;
            }
        }
        else
        {
            if (syncPosition)
            {
                if ((smooth > 0f) && (Vector3.Distance(_posCur, _posDest) < teleportThreshold))
                {
                    _posCur = Vector3.Lerp(_posCur, _posDest, Time.deltaTime * smooth);
                }
                else
                {
                    _posCur = _posDest;
                }
            }

            if (syncRotation)
            {
                if (smooth > 0f)
                {
                    _rotCur = Quaternion.Slerp(_rotCur, _rotDest, Time.deltaTime * smooth);
                }
                else
                {
                    _rotCur = _rotDest;
                }
            }

            if (canUpdate)
            {
                if (syncPosition)
                {
                    if (syncLocal)
                    {
                        target.localPosition = _posCur;
                    }
                    else
                    {
                        target.position = _posCur;
                    }
                }

                if (syncRotation)
                {
                    if (syncLocal)
                    {
                        target.localRotation = _rotCur;
                    }
                    else
                    {
                        target.rotation = _rotCur;
                    }
                }
            }
        }
    }
}
