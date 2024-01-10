using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 0.03f;
    [SerializeField] private float forwardSpeed = 0.1f;
    [SerializeField] private float laneDistance = 2f;

    private float targetPositionX;

    private void Update()
    {
        Vector3 currentPosition = transform.position;

        if (Input.GetKeyDown(KeyCode.A) && targetPositionX >= 0)
        {
            targetPositionX -= laneDistance;
        }
        if (Input.GetKeyDown(KeyCode.D) && targetPositionX <= 0)
        {
            targetPositionX += laneDistance;
        }

        currentPosition.x = Mathf.Lerp(currentPosition.x, targetPositionX, horizontalSpeed*Time.deltaTime);
        currentPosition += Vector3.forward * forwardSpeed * Time.deltaTime;

        transform.position = currentPosition;
    }
}
