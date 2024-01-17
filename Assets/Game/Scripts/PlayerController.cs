using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool IsDead { get; private set; }
    public bool IsJumping { get; private set; }
    public bool IsRolling { get; private set; }
    public float JumpDuration => jumpDistanceZ / forwardSpeed;
    public float RollDuration => rollDistanceZ / forwardSpeed;

    [SerializeField] private float horizontalSpeed = 15f;
    [SerializeField] private float forwardSpeed = 10f;
    [SerializeField] private float laneDistanceX = 1.5f;

    [Header("Jump")]
    [SerializeField] private float jumpDistanceZ = 5f;
    [SerializeField] private float jumpHeightY = 2f;
    [SerializeField] private float jumpLerpSpeed = 10f;

    [Header("Roll")]
    [SerializeField] private Collider regularCollider;
    [SerializeField] private Collider rollCollider;
    [SerializeField] private float rollDistanceZ = 2f;

    private Vector3 initialPosition;
    private float jumpStartZ;
    private float rollStartZ;
    private float targetPositionX;

    private float LeftLaneX => initialPosition.x - laneDistanceX;
    private float RightLaneX => initialPosition.x + laneDistanceX;

    private void Awake()
    {
        initialPosition = transform.position;
        StopRoll();
        enabled = false;
    }

    private void Update()
    {
        if (!IsDead)
        {
            ProcessInput();
        }

        Vector3 currentPosition = transform.position;

        currentPosition.x = ProcessLaneMovement();
        currentPosition.y = ProcessJump();
        currentPosition.z = ProcessForwardMovement();
        ProcessRoll();

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
            StartJump();
        }
        if (Input.GetKeyDown(KeyCode.S) && !IsRolling)
        {
            StartRoll();
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

    private void StartJump()
    {
        IsJumping = true;
        jumpStartZ = transform.position.z;
        StopRoll();
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
                StopJump();
            }
            else
            {
                deltaY = Mathf.Sin(Mathf.PI * jumpPercent) * jumpHeightY;
            }
        }

        float targetPositionY = initialPosition.y + deltaY;
        return Mathf.Lerp(transform.position.y, targetPositionY, Time.deltaTime * jumpLerpSpeed);
    }

    private void StopJump()
    {
        IsJumping = false;
    }

    private void StartRoll()
    {
        IsRolling = true;
        regularCollider.enabled = false;
        rollCollider.enabled = true;
        rollStartZ = transform.position.z;
        StopJump();
    }

    private void ProcessRoll()
    {
        if (IsRolling)
        {
            float rollCurrentProgress = transform.position.z - rollStartZ;
            float rollPercent = rollCurrentProgress / rollDistanceZ;
            if (rollPercent >= 1)
            {
                StopRoll();
            }
        }
    }

    private void StopRoll()
    {
        IsRolling = false;
        regularCollider.enabled = true;
        rollCollider.enabled = false;
    }

    public void OnPlayerDeath()
    {
        IsDead = true;
        forwardSpeed = 0f;
        StopRoll();
        StopJump();
    }
}
