using UnityEngine;

public class punchDetect : MonoBehaviour
{
    public float punchForce = 10f;
    public float upwardForce = 5f;
    public float torqueForce = 10f;

    private Rigidbody rb;
    private AudioSource audioSource;
    public AudioClip punchSFX;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        rb = GetComponent<Rigidbody>(); // Get the Rigidbody of the player
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 2f); 
            foreach (var collider in colliders)
            {
                if (collider.CompareTag("NPC")) 
                {
                    Rigidbody npcRb = collider.GetComponent<Rigidbody>();

                    if (npcRb != null)
                    {
                        if (audioSource != null && punchSFX != null)
                        {
                            audioSource.PlayOneShot(punchSFX);
                        }

                        npcRb.isKinematic = false; 
                        npcRb.linearVelocity = Vector3.zero; 

                        Vector3 punchDirection = transform.forward;
                        Vector3 totalForce = punchDirection * punchForce + Vector3.up * upwardForce;

                        npcRb.AddForce(totalForce, ForceMode.Impulse); 

                        npcRb.AddTorque(Random.insideUnitSphere * torqueForce, ForceMode.Impulse); 
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            Rigidbody npcRb = other.GetComponent<Rigidbody>();

            if (npcRb != null)
            {

                if (audioSource != null && punchSFX != null)
                {
                    audioSource.PlayOneShot(punchSFX);
                }


                npcRb.isKinematic = false;

                Vector3 punchDirection = transform.forward;
                Vector3 totalForce = punchDirection * punchForce + Vector3.up * upwardForce;

                npcRb.AddForce(totalForce, ForceMode.Impulse); 
                npcRb.AddTorque(Random.insideUnitSphere * torqueForce, ForceMode.Impulse); 
            }
        }
    }
}
