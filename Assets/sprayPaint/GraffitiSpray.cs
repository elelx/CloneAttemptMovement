using UnityEngine;

public class GraffitiSpray : MonoBehaviour
{

    public GameObject[] graffitiPrefabs;     
    public float sprayDistance = 10f;        
    public KeyCode sprayKey = KeyCode.P;     

    void Update()
    {
        if (Input.GetKeyDown(sprayKey))
        {
            SprayGraffiti();
        }
    }

    void SprayGraffiti()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, sprayDistance))
        {
            Vector3 spawnPos = hit.point + hit.normal * 0.01f; 
            Quaternion spawnRot = Quaternion.LookRotation(-hit.normal);

            GameObject prefab = graffitiPrefabs[Random.Range(0, graffitiPrefabs.Length)];
            GameObject graffiti = Instantiate(prefab, spawnPos, spawnRot);

            Destroy(graffiti, 5f); 
        }
    }


    
}
