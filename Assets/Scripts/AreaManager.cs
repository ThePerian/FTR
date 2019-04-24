using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Collider2D spawnArea;
    public int numeberOfPoints;
    public Canvas parentCanvas;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        float minSpawnDistance = objectToSpawn.GetComponent<CircleCollider2D>().radius * 2;
        Debug.Log(minSpawnDistance);
        List<Vector2> existingPoints = new List<Vector2>();

        for (int i = 0; i < numeberOfPoints; i++)
        {
            Vector2 randomPosition;
            bool isTooClose;

            do
            {
                randomPosition = new Vector2(
                    Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
                    Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y));

                isTooClose = false;
                foreach (var point in existingPoints)
                {
                    if (Vector2.Distance(randomPosition, point) < minSpawnDistance)
                        isTooClose = true;
                }
            } while (!spawnArea.OverlapPoint(randomPosition) || isTooClose);

            existingPoints.Add(randomPosition);
            Instantiate(objectToSpawn, (Vector3)randomPosition, Quaternion.identity, parentCanvas.transform);
        }
    }
    
    void Update()
    {
        
    }
}
