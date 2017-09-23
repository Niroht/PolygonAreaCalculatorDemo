using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonAreaCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Polygon area calculator");

            while (true)
            {
                try
                {
                    var area = GetPolygonArea();
                    Console.WriteLine($"Polyon area is {area} square units");
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }

                Console.WriteLine("Repeat? [Y/N]");
                var response = Console.ReadLine();
                
                if (response.ToUpperInvariant() != "Y")
                {
                    break;
                }
            }
        }

        private static double GetPolygonArea()
        {
            var sides = GetNumberOfSidesFromUser();
            var endpointCoordinates = GetEndpointCoordinatesFromUser(sides);

            var polygon = new Polygon(sides, endpointCoordinates.ToArray());
            return polygon.CalculateArea();
        }

        private static int GetNumberOfSidesFromUser()
        {
            var sides = 0;

            while (sides < 3)
            {
                // This is inconsistent UX. We should ask the user for number of sides or endpoints, but not both.
                Console.Write("Enter number of end points: ");
                sides = int.Parse(Console.ReadLine());

                if (sides < 3)
                {
                    Console.WriteLine("Number of Sides must be < 2. Try again.");
                }
            }

            return sides;
        }

        private static IEnumerable<Point>  GetEndpointCoordinatesFromUser(int sides)
        {
            var points = new List<Point>();

            for (int i = 0; i < sides; i++)
            {
                Console.Write($"Enter coordinates for point {i + 1} in 'X,Y' format");

                var userInputPoints = Console.ReadLine();
                var coordinates = userInputPoints.Split(',').Select(x => x.Trim());

                points.Add(new Point(int.Parse(coordinates.ElementAt(0)), int.Parse(coordinates.ElementAt(1))));
            }

            return points;
        }
    }
}
