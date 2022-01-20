using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private CharacterController _controller;
    [Header("Controller Settings")]
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _jumpHeight = 8.0f;
    [SerializeField] private float _gravity = 20.0f;
    private float _yVelocity;

    private Camera _camera;
    [Header("Camera Settings")]
    [SerializeField]
    public float mouseSensitivity = 2.0f;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        if (_controller == null)
        {
            Debug.LogError("Character controller is NULL!!");
        }
        _camera = Camera.main;
        if (_camera == null)
        {
            Debug.LogError("Camera Is NULL!!");
        }

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Movement();
        CameraController();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical);
        Vector3 velocity = direction * _speed;

        if (_controller.isGrounded == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
            }
        }
        else
        {
            _yVelocity -= _gravity * Time.deltaTime;
        }

        velocity.y = _yVelocity;

        velocity = transform.TransformDirection(velocity);

        _controller.Move(velocity * Time.deltaTime);
    }
    private void CameraController()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        float xMin = 0f;
        float xMax = 26f;


        //look left and right
        Vector3 currentRotation = transform.localEulerAngles;
        currentRotation.y += mouseX * mouseSensitivity;
        transform.localRotation = Quaternion.AngleAxis(currentRotation.y, Vector3.up);

        //look up and down
        Vector3 currentCameraRotation = _camera.gameObject.transform.localEulerAngles;
        currentCameraRotation.x -= mouseY * mouseSensitivity;
        //currentCameraRotation.x = Mathf.Clamp(currentCameraRotation.x, xMin, xMax);
        _camera.gameObject.transform.localRotation = Quaternion.AngleAxis(currentCameraRotation.x, Vector3.right);
    }
}
