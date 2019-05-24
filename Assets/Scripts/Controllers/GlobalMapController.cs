using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Delaunay;

public class GlobalMapController : MonoBehaviour
{
    public GameObject pointPrefab;
    public GameObject playerPrefab;
    public int numberOfPoints;
    public Collider2D spawnArea;
    public Canvas parentCanvas;
    public List<Collider2D> localAreas;

    public delegate void PlayerReady(Vector2 playerPosition);
    public static event PlayerReady OnPlayerReady;

    List<GameObject> existingPoints = new List<GameObject>();
    GameObject playerToken;

    private void Awake()
    {
        playerToken = Instantiate(playerPrefab);

    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        float minDistance = pointPrefab.GetComponent<CircleCollider2D>().radius * 2;
        DelaunayTriangulator delaunay = new DelaunayTriangulator();
        var points = delaunay.GeneratePoints(numberOfPoints, spawnArea, minDistance);
        var triangulation = delaunay.BowyerWatson(points);
        //remove junctions that are subjectively too long
        float maxDistanceBetweenPoints = Player.Instance.maxTravelDistance / 3;
        SpawnPOIs(pointPrefab, points, maxDistanceBetweenPoints, parentCanvas);
        Vector2 southernPoint = FindStartingPosition(points);

        playerToken.transform.SetPositionAndRotation(southernPoint, Quaternion.identity);
        playerToken.GetComponent<PlayerToken>().OnReachedDestination
            += GetComponent<RandomEventController>().StartEvent;

        OnPlayerReady?.Invoke(playerToken.transform.position);
    }
    
    void Update()
    {
    }

    private void OnDisable()
    {
        //playerToken.GetComponent<PlayerTokenController>().OnReachedDestination
        //    -= GetComponent<RandomEventController>().StartEvent;
    }

    public void SpawnPOIs(GameObject objectToSpawn, IEnumerable<Point> locations, float maxDistance, Canvas parentCanvas)
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
                if (Vector2.Distance(p.position, point.position) <= maxDistance)
                    neighbours.Add(p.position);

            GameObject newPoint = Instantiate(
                objectToSpawn, 
                point.position,
                Quaternion.identity, 
                parentCanvas.transform);
            
            newPoint.GetComponent<PointOfInterest>().neighbourPoints = neighbours;
            newPoint.GetComponent<PointOfInterest>().playerToken = playerToken;

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

    Vector2 FindStartingPosition(IEnumerable<Point> points)
    {
        Vector2 south = new Vector2(999, 999);

        foreach (var point in points)
        {
            if (point.position.y < south.y)
                south = point.position;
        }

        return south;
    }
}
