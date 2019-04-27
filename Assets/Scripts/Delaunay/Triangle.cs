using System.Collections.Generic;
using System.Linq;

namespace Delaunay
{
    public class Triangle
    {
        public Point[] Vertices { get; } = new Point[3];
        public Point Circumcenter { get; private set; }
        public double RadiusSquared;

        public IEnumerable<Triangle> TrianglesWithSharedEdge {
            get {
                var neighbors = new HashSet<Triangle>();
                foreach (var vertex in Vertices)
                {
                    var trianglesWithSharedEdge = vertex.AdjacentTriangles.Where(o =>
                    {
                        return o != this && SharesEdgeWith(o);
                    });
                    neighbors.UnionWith(trianglesWithSharedEdge);
                }

                return neighbors;
            }
        }

        public Triangle(Point point1, Point point2, Point point3)
        {
            if (!IsCounterClockwise(point1, point2, point3))
            {
                Vertices[0] = point1;
                Vertices[1] = point3;
                Vertices[2] = point2;
            }
            else
            {
                Vertices[0] = point1;
                Vertices[1] = point2;
                Vertices[2] = point3;
            }

            Vertices[0].AdjacentTriangles.Add(this);
            Vertices[1].AdjacentTriangles.Add(this);
            Vertices[2].AdjacentTriangles.Add(this);
            UpdateCircumcircle();
        }

        private void UpdateCircumcircle()
        {
            // https://codefound.wordpress.com/2013/02/21/how-to-compute-a-circumcircle/#more-58
            // https://en.wikipedia.org/wiki/Circumscribed_circle
            var p0 = Vertices[0];
            var p1 = Vertices[1];
            var p2 = Vertices[2];
            var dA = p0.position.x * p0.position.x + p0.position.y * p0.position.y;
            var dB = p1.position.x * p1.position.x + p1.position.y * p1.position.y;
            var dC = p2.position.x * p2.position.x + p2.position.y * p2.position.y;

            var aux1 = (dA * (p2.position.y - p1.position.y) 
                + dB * (p0.position.y - p2.position.y) 
                + dC * (p1.position.y - p0.position.y));
            var aux2 = -(dA * (p2.position.x - p1.position.x) 
                + dB * (p0.position.x - p2.position.x) 
                + dC * (p1.position.x - p0.position.x));
            var div = (2 * (p0.position.x * (p2.position.y - p1.position.y) 
                + p1.position.x * (p0.position.y - p2.position.y) 
                + p2.position.x * (p1.position.y - p0.position.y)));

            if (div == 0)
            {
                throw new System.Exception();
            }

            var center = new Point(aux1 / div, aux2 / div);
            Circumcenter = center;
            RadiusSquared = (center.position.x - p0.position.x) 
                * (center.position.x - p0.position.x) 
                + (center.position.y - p0.position.y) 
                * (center.position.y - p0.position.y);
        }

        private bool IsCounterClockwise(Point point1, Point point2, Point point3)
        {
            var result = (point2.position.x - point1.position.x) 
                * (point3.position.y - point1.position.y) 
                - (point3.position.x - point1.position.x) 
                * (point2.position.y - point1.position.y);
            return result > 0;
        }

        public bool SharesEdgeWith(Triangle triangle)
        {
            var sharedVertices = Vertices.Where(o => triangle.Vertices.Contains(o)).Count();
            return sharedVertices == 2;
        }

        public bool IsPointInsideCircumcircle(Point point)
        {
            var d_squared = (point.position.x - Circumcenter.position.x) 
                * (point.position.x - Circumcenter.position.x) 
                + (point.position.y - Circumcenter.position.y) 
                * (point.position.y - Circumcenter.position.y);
            return d_squared < RadiusSquared;
        }
    }
}