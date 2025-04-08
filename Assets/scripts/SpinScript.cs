using UnityEngine;

public class SpinScript : MonoBehaviour
{

    public float spinSpeed = 300f;
    public float bounceForce = 10f;

    void Update()
    {
        transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f, Space.Self);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();

            if (playerRb != null)
            {
                // Push away even if player is standing still
                Vector3 pushDir = (collision.transform.position - transform.position).normalized;
                playerRb.linearVelocity = pushDir * bounceForce;
            }
        }
    }
}

