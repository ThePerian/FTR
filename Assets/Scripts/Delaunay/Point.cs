using System.Collections.Generic;
using UnityEngine;

namespace Delaunay
{
    public class Point
    {
        public Vector2 position;
        public HashSet<Triangle> AdjacentTriangles { get; } = new HashSet<Triangle>();
        public HashSet<Point> neighbourPoints = new HashSet<Point>();

        public Point(float x, float y)
        {
            position = new Vector2(x, y);
        }

        public Point(Vector2 pos)
        {
            position = pos;
        }
    }
}