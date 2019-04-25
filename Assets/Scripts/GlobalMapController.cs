using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMapController : MonoBehaviour
{
    public GameObject pointOfInterest;
    public int numberOfPoints;
    public Collider2D spawnArea;
    public Canvas parentCanvas;
    public List<Collider2D> localAreas;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        float minDistance = pointOfInterest.GetComponent<CircleCollider2D>().radius * 2;
        SpawnPOIs(pointOfInterest, numberOfPoints, spawnArea, minDistance, parentCanvas);
    }
    
    void Update()
    {
        
    }

    public void SpawnPOIs(GameObject objectToSpawn, int numberOfPoints,
        Collider2D spawnArea, float minDistance, Canvas parentCanvas)
    {
        List<PointOfInterest> existingPoints = new List<PointOfInterest>();

        for (int i = 0; i < numberOfPoints; i++)
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
                    if (Vector2.Distance(randomPosition, point.position) < minDistance)
                        isTooClose = true;
                }
            } while (!spawnArea.OverlapPoint(randomPosition) || isTooClose);

            GameObject newPoint = 
                Instantiate(objectToSpawn, randomPosition, Quaternion.identity, parentCanvas.transform);

            foreach(var area in localAreas)
            {
                if (newPoint.GetComponent<Collider2D>().Distance(area).isOverlapped)
                {
                    newPoint.tag = area.tag;
                    break;
                }
            }

            existingPoints.Add(new PointOfInterest()
            {
                position = randomPosition,
                tag = newPoint.tag
            });
        }
    }
}
