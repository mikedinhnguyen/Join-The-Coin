using UnityEngine;

public class LockHolePlacement : MonoBehaviour
{
    public float minAngle, maxAngle;
    int direction = 1;

    void Start()
    {
        minAngle = 90;
        maxAngle = 250; 
        ChangeLocks();
    }

    public void ChangeLocks()
    {
        transform.Rotate(0, 0, Random.Range(minAngle, maxAngle)* direction);
        direction *= -1;
    }

}
