using System;

namespace TMS.NET06.Lesson16.PatternMatching
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Pattern Matching!");
            
            Console.WriteLine("Square:");
            Console.WriteLine(ComputeAreaModernSwitch(new Square(0)));            
            
            Console.WriteLine("Circle:");
            Console.WriteLine(ComputeAreaModernSwitch(new Circle(10)));
            
            Console.WriteLine("Rectangle:");
            Console.WriteLine(ComputeAreaModernSwitch(new Rectangle(10, 5)));
            
            try {                
                Console.WriteLine("Triangle:");
                Console.WriteLine(ComputeAreaModernSwitch(new Triangle(10, 5)));
            }
            catch(Exception ex) {
                Console.WriteLine(ex);
            }

            try {
                Console.WriteLine("object:");
                Console.WriteLine(ComputeAreaModernSwitch(new object()));
            }
            catch(Exception ex) {
                Console.WriteLine(ex);
            }

            try {
                Console.WriteLine("Null:");
                Console.WriteLine(ComputeAreaModernSwitch(null));
            }
            catch(Exception ex) {
                Console.WriteLine(ex);
            }
        }

        static string Display(object o) // for point
        {
            switch (o)
            {
                case Point p when p.X == 0 && p.Y == 0:
                    return "origin";
                case Point p:
                    return $"({p.X}, {p.Y})";
                default:
                    return "unknown";
            }
        }

        public static double ComputeAreaModernSwitch(object shape)
        {
            switch (shape)
            {
                case Square s:
                    return s.Side * s.Side;
                case Circle c:
                    return c.Radius * c.Radius * Math.PI;
                case Rectangle r:
                    return r.Height * r.Length;
                default:
                    throw new ArgumentException(
                        message: "shape is not a recognized shape",
                        paramName: nameof(shape));
            }
        }
    }    
}
