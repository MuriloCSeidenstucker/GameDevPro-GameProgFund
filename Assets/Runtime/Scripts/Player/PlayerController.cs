using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerAudioController audioController;
    [SerializeField] private GameMode gameMode;
    [SerializeField] private float horizontalSpeed = 15;
    [SerializeField] private float laneDistanceX = 4;

    [Header("Jump")]
    [SerializeField] private float jumpDistanceZ = 5;
    [SerializeField] private float jumpHeightY = 2;

    [SerializeField] private float jumpLerpSpeed = 10;

    [Header("Roll")]

    [SerializeField] private float rollDistanceZ = 5;
    [SerializeField] private Collider regularCollider;
    [SerializeField] private Collider rollCollider;


    Vector3 initialPosition;

    float targetPositionX;

    public bool IsJumping { get; private set; }

    private float rollStartZ;
    public bool IsRolling { get; private set; }

    public float JumpDuration => jumpDistanceZ / ForwardSpeed;

    public float RollDuration => rollDistanceZ / ForwardSpeed;
    float jumpStartZ;

    private float LeftLaneX => initialPosition.x - laneDistanceX;
    private float RightLaneX => initialPosition.x + laneDistanceX;

    private bool CanJump => !IsJumping;
    private bool CanRoll => !IsRolling;

    public float TravelledDistance => transform.position.z - initialPosition.z;
    public float ForwardSpeed;

    void Awake()
    {
        initialPosition = transform.position;
        StopRoll();
        enabled = false;
    }

    void Update()
    {
        if (!gameMode.IsGamePaused)
        {
            ProcessInput();
        }

        Vector3 position = transform.position;

        position.x = ProcessLaneMovement();
        position.y = ProcessJump();
        position.z = ProcessForwardMovement();
        ProcessRoll();

        transform.position = position;
    }

    private void ProcessInput()
    {
        if (gameMode.IsGameOver) return;

        if (Input.GetKeyDown(KeyCode.D))
        {
            targetPositionX += laneDistanceX;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            targetPositionX -= laneDistanceX;
        }
        if (Input.GetKeyDown(KeyCode.W) && CanJump)
        {
            StartJump();
        }
        if (Input.GetKeyDown(KeyCode.S) && CanRoll)
        {
            StartRoll();
        }

        targetPositionX = Mathf.Clamp(targetPositionX, LeftLaneX, RightLaneX);
    }

    private float ProcessLaneMovement()
    {
        return Mathf.Lerp(transform.position.x, targetPositionX, Time.deltaTime * horizontalSpeed);
    }

    private float ProcessForwardMovement()
    {
        return transform.position.z + ForwardSpeed * Time.deltaTime;
    }

    private void StartJump()
    {
        IsJumping = true;
        jumpStartZ = transform.position.z;
        StopRoll();
        audioController.PlayJumpSFX();
    }

    private void StopJump()
    {
        IsJumping = false;
    }

    private float ProcessJump()
    {
        float deltaY = 0;
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

    private void ProcessRoll()
    {
        if (IsRolling)
        {
            float percent = (transform.position.z - rollStartZ) / rollDistanceZ;
            if (percent >= 1)
            {
                StopRoll();
            }
        }
    }

    private void StartRoll()
    {
        rollStartZ = transform.position.z;
        IsRolling = true;
        regularCollider.enabled = false;
        rollCollider.enabled = true;

        StopJump();
        audioController.PlayRollSFX();
    }

    private void StopRoll()
    {
        IsRolling = false;
        regularCollider.enabled = true;
        rollCollider.enabled = false;
    }

    public void Die()
    {
        ForwardSpeed = 0;
        IsJumping = false;
        IsRolling = false;
        rollCollider.enabled= false;
        regularCollider.enabled = false;
    }
}
