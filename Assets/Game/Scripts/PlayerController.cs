using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool IsJumping { get; private set; }
    public float JumpDuration => jumpDistanceZ / forwardSpeed;

    [SerializeField] private float horizontalSpeed = 15f;
    [SerializeField] private float forwardSpeed = 10f;
    [SerializeField] private float laneDistanceX = 1.5f;

    [Header("Jump")]
    [SerializeField] private float jumpDistanceZ = 5f;
    [SerializeField] private float jumpHeightY = 2f;

    private Vector3 initialPosition;
    private float jumpStartZ;
    private float targetPositionX;

    private float LeftLaneX => initialPosition.x - laneDistanceX;
    private float RightLaneX => initialPosition.x + laneDistanceX;

    private void Awake()
    {
        initialPosition = transform.position;
        enabled = false;
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
        if (Input.GetKeyDown(KeyCode.W) && !IsJumping)
        {
            IsJumping = true;
            jumpStartZ = transform.position.z;
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
        if (IsJumping)
        {
            float jumpCurrentProgress = transform.position.z - jumpStartZ;
            float jumpPercent = jumpCurrentProgress / jumpDistanceZ;
            if (jumpPercent >= 1)
            {
                IsJumping = false;
            }
            else
            {
                deltaY = Mathf.Sin(Mathf.PI * jumpPercent) * jumpHeightY;
            }
        }
        return initialPosition.y + deltaY;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Entrando no trigger -> {other.name}");
    }
}
