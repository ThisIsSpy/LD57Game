using UnityEngine;

public class RotationLocker : MonoBehaviour
{
    void Update()
    {
        transform.rotation = Quaternion.identity;
    }
}
