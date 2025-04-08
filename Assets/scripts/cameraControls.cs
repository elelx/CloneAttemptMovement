//using UnityEngine;

//public class cameraControls : MonoBehaviour
//{
//    [SerializeField] private Transform player;  // Player position

//    [SerializeField] private float cameraSensitivity = 2f; // Mouse sensitivity
//    [SerializeField] private float distance = 5f; // Camera distance
//    [SerializeField] private float height = 2f;  // Camera height

//    private float rotationX = 0f; // Up/down rotation
//    private float rotationY = 0f; // Left/right rotation

//    private void Start()
//    {
//        Cursor.lockState = CursorLockMode.Locked;
//    }

//    private void LateUpdate()  // 
//    {
//        // Mouse input
//        float mouseX = Input.GetAxis("Mouse X") * cameraSensitivity;
//        float mouseY = Input.GetAxis("Mouse Y") * cameraSensitivity;

//        rotationY += mouseX; // Left/right camera rotation
//        rotationX -= mouseY; // Up/down camera rotation

//        rotationX = Mathf.Clamp(rotationX, -40f, 80f); // Prevents camera flipping

//        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0); // Camera rotation

//        Vector3 offset = rotation * new Vector3(0, 0, -distance); // Camera stays behind the player

//        // Set camera position and rotation
//        transform.position = player.position + offset + Vector3.up * height;
//        transform.LookAt(player.position + Vector3.up * height);
//    }
//}
using UnityEngine;

public class cameraControls : MonoBehaviour
{
    public Transform player;  // This will change between player and train
    [SerializeField] private float cameraSensitivity = 2f;
    [SerializeField] private float distance = 5f;
    [SerializeField] private float height = 2f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        if (player == null) return; // Avoid errors if player is null

        float mouseX = Input.GetAxis("Mouse X") * cameraSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * cameraSensitivity;

        rotationY += mouseX;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -40f, 80f);

        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);

        Vector3 offset = rotation * new Vector3(0, 0, -distance);

        transform.position = player.position + offset + Vector3.up * height;
        transform.LookAt(player.position + Vector3.up * height);
    }

    public void SetHeight(float newHeight)
    {
        height = newHeight; 
    }

    public void SetTarget(Transform newTarget, Vector3 offset)
    {
        player = newTarget;
        this.height = offset.y; 
    }
}












