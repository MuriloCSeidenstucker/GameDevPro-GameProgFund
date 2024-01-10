using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 15f;
    [SerializeField] private float forwardSpeed = 10f;
    [SerializeField] private float laneDistanceX = 1.5f;

    private Vector3 initialPosition;
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
}
