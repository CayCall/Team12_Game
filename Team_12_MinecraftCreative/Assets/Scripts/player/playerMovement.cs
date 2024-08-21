using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class playerMovement : MonoBehaviour
{
     #region MovementVariables
        [Header("Movement")]
        public float walkSpeed = 6f;
        public float runSpeed = 12f;
        public float JumpPower = 7f;
        public float creativeSpeed = 10f;
        public float gravity = 10f;

        [SerializeField] private bool isWalking = false;
        public bool isCreativeMode = false;

        private Vector3 initialPosition;
        private Vector3 velocity = Vector3.zero; // For SmoothDamp
    #endregion

    #region MouseAndCameraVariables
        [Header("Mouse look & Camera")]
        public float lookSpeed = 2f;
        public float lookYLimit = 45f;
        public Camera playerCamera;
        public float amplitude = 0.1f;  // Height of bobbing effect
        public float frequency = 1.0f;  // Speed of bobbing effect
        public float smoothTime = 0.1f; // Time for smooth transitions
    #endregion

    #region ShootVariables
    [Header("Shoot")]
        public bool Shoot;
    #endregion

    #region PrivateVariables
        private Vector3 moveDirection = Vector3.zero;
        private float rotationX = 0;
        private bool canMove = true;
    #endregion

    #region PrivateScriptReferences
        private CharacterController _characterController;
    #endregion

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        // CAM bob effect
        initialPosition = playerCamera.transform.localPosition;
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleCreativeMovement();
    }

    private void HandleMovement()
    {
        if (!isCreativeMode)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            // Shift to run
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
            float movementDirectionY = moveDirection.y;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            isWalking = curSpeedX != 0 || curSpeedY != 0;

            // Jumping
            if (Input.GetButton("Jump") && canMove && _characterController.isGrounded)
            {
                moveDirection.y = JumpPower;
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }

            if (!_characterController.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }

            _characterController.Move(moveDirection * Time.deltaTime);
            Vector3 targetPosition = initialPosition;
            if (isWalking)
            {
                targetPosition = new Vector3(
                    initialPosition.x,
                    initialPosition.y + Mathf.Sin(Time.time * frequency) * amplitude,
                    initialPosition.z
                );
            }

            playerCamera.transform.localPosition = Vector3.SmoothDamp(
                playerCamera.transform.localPosition,
                targetPosition,
                ref velocity,
                smoothTime
            );
        }
    }

    private void HandleCreativeMovement()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleCreativeMode();
        }

        if (isCreativeMode)
        {
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            movement = transform.TransformDirection(movement) * creativeSpeed * Time.deltaTime;

            if (Input.GetKey(KeyCode.Space))
            {
                movement.y += creativeSpeed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                movement.y -= creativeSpeed * Time.deltaTime;
            }

            _characterController.Move(movement);
        }
    }

    private void ToggleCreativeMode()
    {
        isCreativeMode = !isCreativeMode;

        if (isCreativeMode)
        {
            gravity = 0;
        }
        else
        {
            gravity = 100f; 
        }
    }

    private void HandleRotation()
    {
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookYLimit, lookYLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
}