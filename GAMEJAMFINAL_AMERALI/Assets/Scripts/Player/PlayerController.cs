using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    InputManager inputManager;
    public Vector3 moveDirection;
    Vector3 targetPosition;
    Vector3 normalVector;
    Transform cameraObject;
    Rigidbody playerRigidBody;
    PlayerManager playerManager;
    PlayerAnimationManager animationManager;
    private Transform myTransform;
    private Collider myCollider;
    //private Rigidbody rigidbody;
    public float movementSpeed = 15;
    public float rotatonSpeed = 1;

    private AudioSource audioSource;
    public AudioClip swordSwing;


    [SerializeField]
    float groundDetectionRayStartPoint = 0.5f;
    [SerializeField]
    float minDistanceNeededtoStartFall = 1f;
    [SerializeField]
    float groundDirectionRayDistance = 0.5f;
    [SerializeField]
    float fallingSpeed = 45f;
    LayerMask ignoreForGroundCheck;
    public float inAirTimer;

    private void Awake()
    {
        myCollider = GetComponent<Collider>();
        playerManager = GetComponent<PlayerManager>();
        inputManager = GetComponent<InputManager>();
        animationManager = GetComponent<PlayerAnimationManager>();
        playerRigidBody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
    }

    void Start()
    {
        myCollider.enabled = true;
        myTransform = transform;
       // rigidbody = GetComponent<Rigidbody>();
        playerManager.isGrounded = true;
        ignoreForGroundCheck = ~(1 << 8 | 1 << 11);
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySwordSwing()
    {
        audioSource.clip = swordSwing;
        audioSource.Play();
    }
    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        if (playerManager.isInteracting)
            return;

        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;
        moveDirection = moveDirection * movementSpeed;

        Vector3 movementVelocity = moveDirection;
        playerRigidBody.velocity = moveDirection;

        
    }

    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
            targetDirection = transform.forward;
        
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRottion = Quaternion.Slerp(transform.rotation, targetRotation, rotatonSpeed * Time.deltaTime);

        transform.rotation = playerRottion;

    }

    public void HandleFalling(float delta, Vector3 moveDirection)
    {
        playerManager.isGrounded = false;
        RaycastHit hit;
        Vector3 origin = myTransform.position;
        origin.y += groundDetectionRayStartPoint;

        if(Physics.Raycast(origin, myTransform.forward,out hit, 0.4f))
        {
            moveDirection = Vector3.zero;
        }

        if(playerManager.isInAir)
        {
            playerRigidBody.AddForce(-Vector3.up * fallingSpeed);
            playerRigidBody.AddForce(moveDirection * fallingSpeed / 10f);
        }

        Vector3 dir = moveDirection;
        dir.Normalize();
        origin = origin + dir * groundDirectionRayDistance;

        targetPosition = myTransform.position;

        Debug.DrawRay(origin, -Vector3.up * minDistanceNeededtoStartFall, Color.red, 0.1f, false);
        if(Physics.Raycast(origin, -Vector3.up,out hit, minDistanceNeededtoStartFall, ignoreForGroundCheck))
        {
            normalVector = hit.normal;
            Vector3 tp = hit.point;
            playerManager.isGrounded = true;
            targetPosition.y = tp.y;

            if(playerManager.isInAir)
            {
                if(inAirTimer > 0.5f)
                {
                    
                    animationManager.PlayTargetAnimation("Landing", true);
                    inAirTimer = 0;
                }

                else 
                {
                    animationManager.PlayTargetAnimation("Blend Tree", false);
                    inAirTimer = 0;
                }

                playerManager.isInAir = false;
            }
        }
        else
        {
            if(playerManager.isGrounded)
            {
                playerManager.isGrounded = false;
            }

            if(playerManager.isInAir == false)
            {
                if (playerManager.isInteracting == false)
                {
                    animationManager.PlayTargetAnimation("FallingIdle", true);
                }

                Vector3 vel = playerRigidBody.velocity;
                vel.Normalize();
                playerRigidBody.velocity = vel * (movementSpeed / 2);
                playerManager.isInAir = true;
            }
         
        }

        if(playerManager.isGrounded)
        {
            if (playerManager.isInteracting || inputManager.MoveAmount > 0)
            {
                myTransform.position = Vector3.Lerp(myTransform.position, targetPosition, Time.deltaTime);
            }
            else
            {
                myTransform.position = targetPosition;
            }
        }
    }
}
