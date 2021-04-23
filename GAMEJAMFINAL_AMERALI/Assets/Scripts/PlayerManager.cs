using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    PlayerController playerController;
    CameraManager cameraManager;
    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        cameraManager = FindObjectOfType<CameraManager>();
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        float delta = Time.deltaTime;
        inputManager.HandleAllInputs(delta);
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
    }
}
