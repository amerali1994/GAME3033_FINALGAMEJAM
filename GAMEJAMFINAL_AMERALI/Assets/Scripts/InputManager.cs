using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerInputSystem playerControls;
    AnimationManager animatorManager;
    [SerializeField]
    public Vector2 movementInput;
    public Vector2 cameraInput;

    public float cameraInputX;
    public float cameraInputY;

    public float verticalInput;
    public float horizontalInput;

    private float MoveAmount;

    private void Awake()
    {
        animatorManager = GetComponent<AnimationManager>();
    }
    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerInputSystem();

            playerControls.Player.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.Player.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;


        MoveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, MoveAmount);
    }
}