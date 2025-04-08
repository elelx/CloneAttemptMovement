using UnityEngine;

public class trainTriggered : MonoBehaviour
{
    public GameObject player;
    public GameObject train;

    public GameObject playerCamera;  
    public GameObject trainCamera;  

    private PlayerMovement playerMovement;
    private trainCart trainMovement;

    private bool isInTrain = false;

    public Transform seatBlock; 

    private void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        trainMovement = train.GetComponent<trainCart>();

        trainMovement.enabled = false;  

        trainCamera.SetActive(false);  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isInTrain)
        {
            EnterTrain();
        }
    }

    private void Update()
    {
        if (isInTrain)
        {
            player.transform.position = seatBlock.position;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ExitTrain();
            }
        }
    }

    private void EnterTrain()
    {
        isInTrain = true;

        playerMovement.SetCanMove(false);
        trainMovement.enabled = true;

        player.transform.SetParent(train.transform);

        player.transform.position = seatBlock.position;

        playerCamera.SetActive(false);
        trainCamera.SetActive(true);
    }

    private void ExitTrain()
    {
        isInTrain = false;

        trainMovement.enabled = false;
        playerMovement.SetCanMove(true);

        player.transform.SetParent(null);

        Vector3 exitPosition = train.transform.position + new Vector3(2, 0, 0);
        exitPosition.y = train.transform.position.y; 
        player.transform.position = exitPosition;

        trainCamera.SetActive(false);
        playerCamera.SetActive(true);
    }
}