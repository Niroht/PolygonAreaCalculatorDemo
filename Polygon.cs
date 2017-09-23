using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonAreaCalculator
{
    public class Polygon
    {
        public int NumberOfSides { get; set; }

        public Point[] EndpointCoordinates { get; set; }

        public Polygon(int numberOfSides, Point[] endpointCoordinates)
        {
            NumberOfSides = numberOfSides;
            EndpointCoordinates = endpointCoordinates;
        }

        public double CalculateArea()
        {
            // Indeterminate number of sides - sum the areas of triangles inside the polygon to determine the area
            var sum = 0.0;

            for (int i = 1; i < NumberOfSides - 1; i++)
            {
                var triangleArea = GetAreaOfTriangleInsidePolygon(i);
                sum += triangleArea;
            }

            return sum;
        }

        /// <summary>
        /// Determines the area of a triangle-sized area inside the polygon, made by drawing a line between the origin
        /// endpoint and another non-adjacent endpoint
        /// </summary>
        /// <param name="currentEndpoint">The current endpoint to check against</param>
        /// <returns></returns>
        private double GetAreaOfTriangleInsidePolygon(int currentEndpoint)
        {
            var sideOneLength = CalculateDistanceBetweenPoints(EndpointCoordinates[0], EndpointCoordinates[currentEndpoint]);
            var sideTwoLength = CalculateDistanceBetweenPoints(EndpointCoordinates[0], EndpointCoordinates[currentEndpoint + 1]);
            var sideThreeLength = CalculateDistanceBetweenPoints(EndpointCoordinates[currentEndpoint], EndpointCoordinates[currentEndpoint + 1]);

            return PerformHeronsFormula(sideOneLength, sideTwoLength, sideThreeLength);
        }

        private double CalculateDistanceBetweenPoints(Point pointOne, Point pointTwo)
        {
            var squareXDistance = (pointTwo.X - pointOne.X) * (pointTwo.X - pointOne.X);
            var squareYDistance = (pointTwo.Y - pointOne.Y) * (pointTwo.Y - pointOne.Y);

            return Math.Sqrt(squareXDistance + squareYDistance);
        }

        private double PerformHeronsFormula(double sideOne, double sideTwo, double sideThree)
        {
            var s = (sideOne + sideTwo + sideThree) / 2;
            var radicand = s * (s - sideOne) * (s - sideTwo) * (s - sideThree);
            if (radicand < 0)
            {
                throw new ArgumentException("Invalid Polygon");
            }
            return Math.Sqrt(radicand);
        }
    }
}
