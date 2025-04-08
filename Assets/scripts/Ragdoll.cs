using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        // Assign the Rigidbody at the start
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Calculate direction away from the player
            Vector3 pushDirection = (transform.position - collision.transform.position).normalized;

            // Add upward and directional force
            rb.AddForce(pushDirection * 5f + Vector3.up * 3f, ForceMode.Impulse);

            // Add a bit of torque for rotation
            rb.AddTorque(Random.insideUnitSphere * 5f, ForceMode.Impulse);
        }
    }
}
