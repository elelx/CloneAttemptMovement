//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerMovement : MonoBehaviour
//{
//    [SerializeField] private float speed = 5f;
//    [SerializeField] private float jumpForce = 10f;

//    private Rigidbody rb;
//    //private bool isGrounded = false;
//    public bool canMove = true;

//    //--- arm animations------------
//    private Animator animator;


//    //private Collider leftFistCollider;

//    private void Start()
//    {
//        rb = GetComponent<Rigidbody>();
//        animator = GetComponent<Animator>();


//        ////  colliders to fists
//        //leftFistCollider = transform.Find("leftArm/leftFist")?.GetComponent<Collider>();

//        //// Ensure colliders are disabled initially
//        //if (leftFistCollider != null) leftFistCollider.enabled = false;

//    }

//    //this is for train code
//    public void SetCanMove(bool canMoveState)
//    {
//        canMove = canMoveState;
//    }

//    private void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.Space))
//        {
//            Debug.Log("Jump Trigger Activated");
//            animator.SetTrigger("jump");
//        }

//        if (Input.GetKeyDown(KeyCode.E))
//        {
//            animator.SetTrigger("punch");
//        }
//    }


//    private void FixedUpdate()
//    {
//        float moveX = Input.GetAxis("Horizontal");
//        float moveZ = Input.GetAxis("Vertical");

//        // Get camera direction
//        Vector3 forward = Camera.main.transform.forward;


//        forward.y = 0; // Keep movement horizontal
//        forward.Normalize(); //so it deosnt shake

//        Vector3 right = Camera.main.transform.right;


//        right.y = 0;
//        right.Normalize();

//        Vector3 moveDirection = (forward * moveZ + right * moveX).normalized;

//        rb.linearVelocity = new Vector3(moveDirection.x * speed, rb.linearVelocity.y, moveDirection.z * speed);

//        animator.SetFloat("speedofplayer", moveDirection.magnitude);


//        // Rotate player to face movement direction (fixes spinning issues)
//        if (moveDirection.magnitude > 0.1f)
//        {
//            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
//            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * 10f);
//        }
//    }


//    //private void OnCollisionEnter(Collision collision)
//    //{
//    //    if (collision.gameObject.CompareTag("ground"))
//    //    {
//    //        Debug.Log("Landed on ground"); 
//    //        isGrounded = true;
//    //    }
//    //}



//    //private void OnCollisionExit(Collision collision)
//    //{
//    //    if (collision.gameObject.CompareTag("ground"))
//    //    {
//    //        isGrounded = false;
//    //    }
//    //}

//    //IEnumerator EnablePunchCollider(Collider punchCollider)
//    //{
//    //    punchCollider.enabled = true;
//    //    yield return new WaitForSeconds(0.2f); // Active for 0.2s (adjust based on animation timing)
//    //    punchCollider.enabled = false;
//    //}
//}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;

    private Rigidbody rb;
    private bool isGrounded = false;
    public bool canMove = true;

    //--- arm animations------------
    private Animator animator;
    private bool isPunching = false;

    public Animator gateAnimator;

    private AudioSource audioSource; 
    public AudioClip punchSFX;
    public AudioClip jump;

    public AudioClip gatFalling;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); 
    }

    //this is for train code
    public void SetCanMove(bool canMoveState)
    {
        canMove = canMoveState;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            animator.SetTrigger("jump");

            if (audioSource != null && jump != null)
            {
                audioSource.PlayOneShot(jump);
            }

            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce * 1.8f, rb.linearVelocity.z);
            rb.AddTorque(Random.onUnitSphere * 10f, ForceMode.Impulse);



            isGrounded = false;

        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetTrigger("punch"); 
            isPunching = true;

            // Play punch sound effect
            if (audioSource != null && punchSFX != null)
            {
                audioSource.PlayOneShot(punchSFX); 
            }
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            isPunching = false;
        }



        if (Input.GetKeyDown(KeyCode.E))
        {
            gateAnimator.SetTrigger("fall");

            if (audioSource != null && gatFalling != null)
            {
                audioSource.PlayOneShot(gatFalling);
            }
        }
    }

    private void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Get camera direction
        Vector3 forward = Camera.main.transform.forward;
        forward.y = 0; // Keep movement horizontal
        forward.Normalize(); //so it doesn't shake

        Vector3 right = Camera.main.transform.right;
        right.y = 0;
        right.Normalize();

        Vector3 moveDirection = (forward * moveZ + right * moveX).normalized;

        rb.linearVelocity = new Vector3(moveDirection.x * speed, rb.linearVelocity.y, moveDirection.z * speed);

        animator.SetFloat("speedofplayer", moveDirection.magnitude);

        // Rotate player to face movement direction
        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * 10f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = false;
        }
    }

    public bool IsPunching()
    {
        return isPunching;
    }
}

