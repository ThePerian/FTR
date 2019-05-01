﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Delaunay;

public class GlobalMapController : MonoBehaviour
{
    public GameObject pointPrefab;
    public GameObject playerPrefab;
    public int numberOfPoints;
    public Collider2D spawnArea;
    public Canvas parentCanvas;
    public List<Collider2D> localAreas;
    public float playerSpeed = 1f;

    List<GameObject> existingPoints = new List<GameObject>();
    GameObject playerToken;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        playerToken = Instantiate(playerPrefab);
        float minDistance = pointPrefab.GetComponent<CircleCollider2D>().radius * 2;
        DelaunayTriangulator delaunay = new DelaunayTriangulator();
        var points = delaunay.GeneratePoints(numberOfPoints, spawnArea, minDistance);
        var triangulation = delaunay.BowyerWatson(points);
        SpawnPOIs(pointPrefab, points, parentCanvas);
        Vector2 southernPoint = FindStartingPosition(points);
        playerToken.transform.SetPositionAndRotation(southernPoint, Quaternion.identity);
    }
    
    void Update()
    {/*
        if (Input.GetMouseButtonDown(0))
        {
            playerToken.GetComponent<PlayerController>().destination = 
                Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }*/
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
            newPoint.GetComponent<PointController>().playerToken = playerToken;

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