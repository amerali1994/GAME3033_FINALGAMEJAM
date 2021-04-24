using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    EnemyLocomotionManager enemyLocomotionManager;
    public bool isPerformingAction;

    public float detectionRadius = 20.0f;
    public float maxDetectionAngle = 50.0f;
    public float minDetectionAngle = -50.0f;
    private void Awake()
    {
        enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
    }

    private void Update()
    {
       
    }
    private void FixedUpdate()
    {
        HandleCurrentAction();
        
    }
    private void HandleCurrentAction()
    {
        if(enemyLocomotionManager.currentTarget == null)
        {
            enemyLocomotionManager.HandleDetection();
        }

        else
        {
            enemyLocomotionManager.HandleMoveToEnemy();
        }
    }

 
}
