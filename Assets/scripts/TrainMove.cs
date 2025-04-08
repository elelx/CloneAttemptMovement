using UnityEngine;

public class TrainMove : MonoBehaviour
{

    [SerializeField] private float speed = 10f;

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveX, 0, moveZ);
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }
}

