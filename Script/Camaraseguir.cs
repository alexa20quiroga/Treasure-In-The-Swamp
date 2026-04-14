using UnityEngine;

public class Camaraseguir : MonoBehaviour
{
   public Transform target;

    public Vector3 offset = new Vector3(0, 10, -10);

    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        // Rotate offset with player
        Vector3 rotatedOffset = target.rotation * offset;

        Vector3 desiredPosition = target.position + rotatedOffset;

        // Smooth movement
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Look at player
        transform.LookAt(target);
    }
}
