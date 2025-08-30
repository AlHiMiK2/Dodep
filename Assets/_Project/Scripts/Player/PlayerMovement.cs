using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _crouchHeight;

    private CharacterController _controller;
    private float _gravity;
    private Vector3 _velocity;
    private float _characterHeight;
    private Vector3 _characterCenter;
    
    public bool IsWalking { get; private set; }
    public Vector3 Velocity => _controller.velocity;
    
    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _characterHeight = _controller.height;
        _characterCenter = _controller.center;
    }

    private void Update()
    {
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")); 
        direction = Vector3.ClampMagnitude(direction, 1f);

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            float subtract = _characterHeight - _crouchHeight;
            _controller.height = _crouchHeight;
            _controller.center = new Vector3(_characterCenter.x, _characterCenter.y + subtract * 0.5f, _characterCenter.z);
            _controller.Move(Vector3.up * -subtract);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            float subtract = _characterHeight - _crouchHeight;
            _controller.Move(Vector3.up * subtract);
            _controller.height = _characterHeight;
            _controller.center = _characterCenter;
        }
        if (_gravity > 0f && _controller.velocity.y <= 0f)
        {
            _gravity = 0f;
        }
        if (_controller.isGrounded)
        {
            if (_gravity < 0)
                _gravity = 0f;
            if (Input.GetKeyDown(KeyCode.Space))
                _gravity = Mathf.Sqrt(_jumpHeight * -2.0f * Physics.gravity.y);
        }

        _gravity += Physics.gravity.y * Time.deltaTime;

        _velocity = transform.rotation * direction * _speed + Vector3.up * _gravity;
        _controller.Move(_velocity * Time.deltaTime);

        IsWalking = direction.magnitude > 0f;
    }
}
