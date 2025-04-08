using UnityEngine;

public class regFollowPlayer : MonoBehaviour
{

    public GameObject target;

    public float Dis;

    [SerializeField] public float followingSpeed = 5f;

    public bool isFollowing = false;


  
    void Start() //to ignore certain objects and go thro stuff
    {
     
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isFollowing = true;

        }
    }


    // Update is called once per frame
    void Update()
    {
        if (!isFollowing || target == null) return; //doesnt start till triggered

        Dis = Vector3.Distance(transform.position, target.transform.position); // this is a constant follow

        if (Dis >= 3)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, followingSpeed * Time.deltaTime); ;
        }
    }
}

