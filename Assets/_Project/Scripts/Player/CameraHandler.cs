using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private float _sensevity;
    [SerializeField, Range(0f, 90f)] private float _maxAngle;
    [SerializeField] private Transform _parent;

    private float _angleX;
    private float _angleY;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        _angleX += Input.GetAxis("Mouse X") * _sensevity;
        _angleY -= Input.GetAxis("Mouse Y") * _sensevity;
        _angleY = Mathf.Clamp(_angleY, -_maxAngle, _maxAngle);
        transform.localRotation = Quaternion.Euler(_angleY, 0, 0);
        _parent.rotation = Quaternion.Euler(0, _angleX, 0);
    }
}
