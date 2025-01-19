using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] private Pickup pickup;

    public Pickup SpawnPickup()
    {
        pickup = Instantiate(pickup, transform.localPosition, Quaternion.identity, this.transform);
        return pickup;
    }

    public void SpawnPickups(int length, Vector3 distanceBetweenPickups, bool centralizedSpawn = false)
    {
        Vector3 initialPosition = centralizedSpawn ? ((length*distanceBetweenPickups-distanceBetweenPickups)*0.5f)*-1 : Vector3.zero;
        Vector3 lastPickupPosition = Vector3.zero;
        for (int i = 0; i < length; i++)
        {
            pickup = SpawnPickup();
            
            if (i == 0)
            {
                pickup.transform.localPosition = initialPosition;
            }
            else
            {
                pickup.transform.localPosition = lastPickupPosition + distanceBetweenPickups;
            }
            lastPickupPosition = pickup.transform.localPosition;
        }
    }
}
