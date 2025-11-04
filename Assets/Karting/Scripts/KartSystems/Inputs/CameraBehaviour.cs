using UnityEngine;
using UnityEngine.InputSystem;

public sealed class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _cameraParent;
    [SerializeField] private Transform _mainCamera;

    public InputActionProperty _rotateAction;
    [SerializeField] private float _rotationSpeed = 90f; 
    [SerializeField] private float _smoothTime = 0.1f;

    private float _targetYRotation = 0f;
    private float _currentYRotation = 0f;
    private float _rotationVelocity;

    private void Update()
    {
        _cameraParent.position = _player.position;

        var rotate = _rotateAction.action?.ReadValue<Vector2>() ?? Vector2.zero;
        var xRotate = rotate.x;

        if (Mathf.Abs(xRotate) > 0.1f)
        {
            _targetYRotation += xRotate * _rotationSpeed * Time.deltaTime;
            _targetYRotation = Mathf.Repeat(_targetYRotation, 360f);
        }

        _currentYRotation = Mathf.SmoothDampAngle(
            _currentYRotation,
            _targetYRotation,
            ref _rotationVelocity,
            _smoothTime
        );

        _cameraParent.rotation = Quaternion.Euler(0f, _currentYRotation, 0f);
    }

    private void OnEnable()
    {
        _rotateAction.action?.Enable();
    }

    private void OnDisable()
    {
        _rotateAction.action?.Disable();
    }
}