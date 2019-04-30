using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Delaunay;

public class GlobalMapController : MonoBehaviour
{
    public GameObject pointPrefab;
    public int numberOfPoints;
    public Collider2D spawnArea;
    public Canvas parentCanvas;
    public List<Collider2D> localAreas;

    List<GameObject> existingPoints = new List<GameObject>();

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        float minDistance = pointPrefab.GetComponent<CircleCollider2D>().radius * 2;
        DelaunayTriangulator delaunay = new DelaunayTriangulator();
        var points = delaunay.GeneratePoints(numberOfPoints, spawnArea, minDistance);
        var triangulation = delaunay.BowyerWatson(points);
        SpawnPOIs(pointPrefab, points, parentCanvas);
    }
    
    void Update()
    {
        
    }

    public void SpawnPOIs(GameObject objectToSpawn, IEnumerable<Point> locations, Canvas parentCanvas)
    {
        foreach (var point in locations)
        {
            foreach (var triangle in point.AdjacentTriangles)
            {
                point.neighbourPoints.UnionWith(triangle.Vertices);
            }
            point.neighbourPoints.Remove(point);

            List<Vector2> neighbours = new List<Vector2>();
            foreach (var p in point.neighbourPoints)
                neighbours.Add(p.position);

            GameObject newPoint = Instantiate(
                objectToSpawn, 
                point.position,
                Quaternion.identity, 
                parentCanvas.transform);

            newPoint.GetComponent<PointController>().neighbourPoints = neighbours;

            foreach (var area in localAreas)
            {
                if (newPoint.GetComponent<Collider2D>().Distance(area).isOverlapped)
                {
                    newPoint.tag = area.tag;
                    break;
                }
            }

            existingPoints.Add(newPoint);
        }
    }
}
