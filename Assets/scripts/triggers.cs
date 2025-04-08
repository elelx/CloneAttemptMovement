using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class triggers : MonoBehaviour
{
    public TextMeshProUGUI displayText;
    public Image displayImage;

    private bool hasTriggeredPolice = false;  
    private followPlayer policeFollowScript;

    private AudioSource audioSource;
    public AudioClip talking;

    private void Start()
    {
        displayText.gameObject.SetActive(false);
        displayImage.gameObject.SetActive(false);

        audioSource = GetComponent<AudioSource>();


        GameObject policeNPC = GameObject.FindGameObjectWithTag("Police"); 
        if (policeNPC != null)
        {
            policeFollowScript = policeNPC.GetComponent<followPlayer>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (CompareTag("PoliceTrigger") && !hasTriggeredPolice)
            {

                displayText.gameObject.SetActive(true);
                displayImage.gameObject.SetActive(true);

                hasTriggeredPolice = true;

                if (policeFollowScript != null)
                {
                    policeFollowScript.StartFollowing();
                }
            }
            else
            {
                displayText.gameObject.SetActive(true);
                displayImage.gameObject.SetActive(true);



                if (audioSource != null && talking != null)
                {
                    audioSource.PlayOneShot(talking);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            displayText.gameObject.SetActive(false);
            displayImage.gameObject.SetActive(false);

        }
    }

    public void ResetPoliceTrigger()
    {
        hasTriggeredPolice = false;
    }

    public void HideUI()
    {
        displayText.gameObject.SetActive(false);
        displayImage.gameObject.SetActive(false);
    }

}
