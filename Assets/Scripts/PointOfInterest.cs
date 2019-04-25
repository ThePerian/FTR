using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfInterest
{
    public Vector2 position;
    public string tag;
    public List<PointOfInterest> closestPoints = new List<PointOfInterest>();

    public PointOfInterest(float x, float y)
    {
        position = new Vector2(x, y);
    }

    public PointOfInterest(Vector2 pos)
    {
        position = pos;
    }

    public PointOfInterest() { }
}
