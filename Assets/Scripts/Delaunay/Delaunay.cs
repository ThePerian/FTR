
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Delaunay
{
    //taken from https://github.com/RafaelKuebler/DelaunayVoronoi
    //and adapted for Unity
    public class DelaunayTriangulator
    {
        private float MaxX { get; set; }
        private float MaxY { get; set; }

        public IEnumerable<Point> GeneratePoints(int amount, Collider2D spawnArea, float minDistance)
        {
            MaxX = spawnArea.bounds.max.x;
            MaxY = spawnArea.bounds.max.y;
            var points = new List<Point>();

            for (int i = 0; i < amount; i++)
            {
                Vector2 randomPosition;
                bool isTooClose;

                do
                {
                    float pointX = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
                    float pointY = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
                    randomPosition = new Vector2(pointX, pointY);

                    isTooClose = false;
                    foreach (var point in points)
                    {
                        if (Vector2.Distance(randomPosition, point.position)
                            < minDistance)
                            isTooClose = true;
                    }
                } while (!spawnArea.OverlapPoint(randomPosition) || isTooClose);

                points.Add(new Point(randomPosition));
            }

            return points;
        }

        public IEnumerable<Triangle> BowyerWatson(IEnumerable<Point> points)
        {
            var supraTriangle = GenerateSupraTriangle();
            var triangulation = new HashSet<Triangle>() { supraTriangle };

            foreach (var point in points)
            {
                var badTriangles = FindBadTriangles(point, triangulation);
                var polygon = FindHoleBoundaries(badTriangles);

                foreach (var triangle in badTriangles)
                {
                    foreach (var vertex in triangle.Vertices)
                    {
                        vertex.AdjacentTriangles.Remove(triangle);
                    }
                }
                triangulation.RemoveWhere(o => badTriangles.Contains(o));

                foreach (var edge in polygon)
                {
                    var triangle = new Triangle(point, edge.Point1, edge.Point2);
                    triangulation.Add(triangle);
                }
            }
            
            triangulation.RemoveWhere(t => t.Vertices.Intersect(supraTriangle.Vertices).Any());

            return triangulation;
        }

        private List<Edge> FindHoleBoundaries(ISet<Triangle> badTriangles)
        {
            var edges = new List<Edge>();
            foreach (var triangle in badTriangles)
            {
                edges.Add(new Edge(triangle.Vertices[0], triangle.Vertices[1]));
                edges.Add(new Edge(triangle.Vertices[1], triangle.Vertices[2]));
                edges.Add(new Edge(triangle.Vertices[2], triangle.Vertices[0]));
            }
            var grouped = edges.GroupBy(o => o);
            var boundaryEdges = edges.GroupBy(o => o).Where(o => o.Count() == 1).Select(o => o.First());
            return boundaryEdges.ToList();
        }

        private Triangle GenerateSupraTriangle()
        {
            //   1  -> maxX
            //  / \
            // 2---3
            // |
            // v maxY
            var margin = 500;
            var point1 = new Point(0.5f * MaxX, -2 * MaxX - margin);
            var point2 = new Point(-2 * MaxY - margin, 2 * MaxY + margin);
            var point3 = new Point(2 * MaxX + MaxY + margin, 2 * MaxY + margin);
            return new Triangle(point1, point2, point3);
        }

        private ISet<Triangle> FindBadTriangles(Point point, HashSet<Triangle> triangles)
        {
            var badTriangles = triangles.Where(o => o.IsPointInsideCircumcircle(point));
            return new HashSet<Triangle>(badTriangles);
        }
    }
}