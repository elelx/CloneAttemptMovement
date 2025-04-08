using UnityEngine;

public class followPlayer : MonoBehaviour
{
    public GameObject target;
    public float Dis;
    [SerializeField] public float followingSpeed = 5f;
    public bool isFollowing = false;

    private PlayerMovement playerMovement;
    private Rigidbody rb;
    private Animator animator;

    private float lastQPressTime = -1f;
    private float doubleTapTimeWindow = 0.5f;
    private int doubleTapCount = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerMovement = target.GetComponent<PlayerMovement>();

        Collider policeCollider = GetComponent<Collider>();

        GameObject[] goThroObjects = GameObject.FindGameObjectsWithTag("GoThro");

        foreach (GameObject obj in goThroObjects)
        {
            Collider targetCollider = obj.GetComponent<Collider>();
            if (targetCollider != null)
            {
                Physics.IgnoreCollision(policeCollider, targetCollider);
            }
        }
    }

    public void StartFollowing()
    {
        isFollowing = true;
    }

    void Update()
    {
        if (!isFollowing || target == null) return;

        Dis = Vector3.Distance(transform.position, target.transform.position);

        if (Dis >= 3)
        {
            Vector3 moveDirection = target.transform.position - transform.position;

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * followingSpeed);

            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, followingSpeed * Time.deltaTime);

            animator.SetFloat("walking", moveDirection.magnitude);
        }
        else
        {
            animator.SetFloat("walking", 0f);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (Time.time - lastQPressTime <= doubleTapTimeWindow)
            {
                doubleTapCount++;

                if (doubleTapCount >= 2)
                {
                    StopFollowingAndHideUI();
                }

                PerformHitAndFly();
            }
            lastQPressTime = Time.time;
        }
    }

    void PerformHitAndFly()
    {
        animator.SetTrigger("Hit");

        if (rb != null)
        {
            rb.AddForce(Vector3.up * 5f + transform.forward * 3f, ForceMode.Impulse);
            rb.AddTorque(new Vector3(0, 5f, 0), ForceMode.Impulse);
        }
    }

    public void StopFollowingAndHideUI()
    {
        isFollowing = false;

        triggers triggerScript = GetComponent<triggers>();
        if (triggerScript != null)
        {
            triggerScript.HideUI();
        }

        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(Vector3.up * 5f + transform.forward * 3f, ForceMode.Impulse);
            rb.AddTorque(new Vector3(0, 5f, 0), ForceMode.Impulse);
        }

        Destroy(this, 3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.gameObject;
            StartFollowing();
        }
    }
}

