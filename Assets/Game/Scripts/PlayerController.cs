using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 15f;
    [SerializeField] private float forwardSpeed = 10f;
    [SerializeField] private float laneDistanceX = 1.5f;

    [Header("Jump")]
    [SerializeField] private float jumpDistanceZ = 5f;
    [SerializeField] private float jumpHeightY = 2f;

    private Vector3 initialPosition;
    private bool isJumping;
    private float initialJumpPositionZ;
    private float targetPositionX;

    private float LeftLaneX => initialPosition.x - laneDistanceX;
    private float RightLaneX => initialPosition.x + laneDistanceX;

    private void Awake()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        ProcessInput();

        Vector3 currentPosition = transform.position;

        currentPosition.x = ProcessLaneMovement();
        currentPosition.y = ProcessJump();
        currentPosition.z = ProcessForwardMovement();

        transform.position = currentPosition;
    }

    private void ProcessInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            targetPositionX -= laneDistanceX;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            targetPositionX += laneDistanceX;
        }
        if (Input.GetKeyDown(KeyCode.W) && !isJumping)
        {
            isJumping = true;
            initialJumpPositionZ = transform.position.z;
        }

        targetPositionX = Mathf.Clamp(targetPositionX, LeftLaneX, RightLaneX);
    }

    private float ProcessForwardMovement()
    {
        return transform.position.z + forwardSpeed * Time.deltaTime;
    }

    private float ProcessLaneMovement()
    {
        return Mathf.Lerp(transform.position.x, targetPositionX, horizontalSpeed * Time.deltaTime);
    }

    private float ProcessJump()
    {
        float deltaY = 0f;
        if (isJumping)
        {
            float currentJumpProgress = transform.position.z - initialJumpPositionZ;
            float jumpPercent = currentJumpProgress / jumpDistanceZ;
            if (jumpPercent >= 1)
            {
                isJumping = false;
            }
            deltaY = Mathf.Sin(Mathf.PI * jumpPercent) * jumpHeightY;
        }

        return deltaY;
    }
}
