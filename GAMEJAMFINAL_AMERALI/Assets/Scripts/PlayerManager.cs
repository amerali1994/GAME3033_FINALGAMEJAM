using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    PlayerController playerController;
    CameraManager cameraManager;
    public Animator anim;

    public bool isInAir;
    public bool isGrounded;
    public bool isInteracting;
    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        cameraManager = FindObjectOfType<CameraManager>();
        playerController = GetComponent<PlayerController>();
        isInteracting = anim.GetBool("isInteracting");

    }

    void Update()
    {
        float delta = Time.deltaTime;
        inputManager.HandleAllInputs(delta);

        playerController.HandleAllMovement();
        playerController.HandleFalling(delta, playerController.moveDirection);
    }

    private void FixedUpdate()
    {
        playerController.HandleAllMovement();
    }

    private void LateUpdate()
    {
        inputManager.LightAttackPerformed = false;
        inputManager.HeavyAttackPerformed = false;

        cameraManager.HandleAllCameraMovement();

        if(isInAir)
        {
            playerController.inAirTimer = playerController.inAirTimer + Time.deltaTime;
        }
    }
}
