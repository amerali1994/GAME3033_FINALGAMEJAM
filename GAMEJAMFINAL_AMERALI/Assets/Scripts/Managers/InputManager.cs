using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerInputSystem playerControls;
    PlayerAnimationManager animatorManager;
    PlayerAttacker playerAttacker;
    PlayerInventory playerInventory;

    [SerializeField]
    public Vector2 movementInput;
    public Vector2 cameraInput;
    public GameObject pauseScreen;

    public float cameraInputX;
    public float cameraInputY;

    public float verticalInput;
    public float horizontalInput;

    public bool LightAttackPerformed;
    public bool HeavyAttackPerformed;

    public float MoveAmount;

    

    private void Awake()
    {
        animatorManager = GetComponent<PlayerAnimationManager>();
        playerAttacker = GetComponent<PlayerAttacker>();
        playerInventory = GetComponent<PlayerInventory>();
    }
    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerInputSystem();

            playerControls.Player.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.Player.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            playerControls.Player.Pause.performed += PauseGame;

        }

        playerControls.Enable();
    }

    private void PauseGame(InputAction.CallbackContext obj)
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0f;
       
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void HandleAllInputs(float delta)
    {
        HandleMovementInput(delta);
        AttackInput(delta);
    }

    private void HandleMovementInput(float delta)
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;


        MoveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, MoveAmount);
    }

    private void AttackInput(float delta)
    {
        playerControls.Player.LightAttack.performed += i => LightAttackPerformed = true;
        playerControls.Player.HeavyAttack.performed += i => HeavyAttackPerformed = true;

        if(LightAttackPerformed)
        {
            playerAttacker.HandleLightAttack(playerInventory.rightHandweapon);
        }
        if (HeavyAttackPerformed)
        {
            playerAttacker.HandleHeavyAttack(playerInventory.rightHandweapon);
        }
    }
}