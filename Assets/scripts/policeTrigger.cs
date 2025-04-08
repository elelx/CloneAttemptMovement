using UnityEngine;

public class policeTrigger : MonoBehaviour
{
    public followPlayer policeScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            policeScript.StartFollowing();
        }
    }
}
