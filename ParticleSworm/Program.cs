using System;

namespace ParticleSworm
{
    class Program
    {
        static void Main(string[] args)
        {
            int dim = 1; // problem dimensions
            int numParticles = 20;
            int maxEpochs = 1000;
            double exitError = 0.000; // exit early if reach this error
            double minX = 0; // problem-dependent
            double maxX = 5000;


            if (args.Length>0)
            {
                if (args[0] == "-t1" || args[0] == "-T1")
                {
                    Console.WriteLine("Goal is to minimize f(x0,x1) = -20 * Exp(-0.2 * Sqrt(0.5 * (x0^2 * x1^2)))- Exp(0.5 * Cos2PIx0 + Cos2PIx1) + e + 20");
                    Console.WriteLine("Known solution is at x0 = 0, x1 = 0 and f(0,0) = 0");

                    dim = 2; // problem dimensions
                    numParticles = 5;
                    maxEpochs = 1000;
                    exitError = 0.0; // exit early if reach this error
                    minX = -5; // problem-dependent
                    maxX = 5;
                }

                if (args[0] == "-t2" || args[0] == "-T2" )
                {
                    Console.WriteLine("Goal is to minimize f(x0,x1,x2) = A*N + Suma[xi^2 - A * Cos2PIxi];");
                    Console.WriteLine("Known solution is at x0 = 0, x1 = 0, x2 = 0 and f(0,0,0) = 0");

                    dim = 3; // problem dimensions
                    numParticles = 10;
                    maxEpochs = 1000;
                    exitError = 0.0; // exit early if reach this error
                    minX = -5.12; // problem-dependent
                    maxX = 5.12;
                }

                if ((args[0] == "-t3" || args[0] == "-T3") && args.Length == 2)
                {

                    dim = Int32.Parse(args[1]); // problem dimensions
                    numParticles = 15;
                    maxEpochs = 1000;
                    exitError = 0.0; // exit early if reach this error
                    minX = -5; // problem-dependent
                    maxX = 5;

                    Console.WriteLine($"Goal is to minimize f(x0,...,x{dim-1}) = A*N + Suma[xi^2 - A * Cos2PIxi];");
                    Console.Write("Known solution is at ");
                    for (int i = 0; i < dim; i++)
                    {
                        Console.Write($"x{i} = -2.903534, ");
                    }
                    Console.WriteLine($"\n and f(-2.903534,...,-2.903534) = {-39.16617 * dim} ");
                   
                }
                if (args[0] == "-t4" || args[0] == "-T4")
                {
                    Console.WriteLine("Goal is to minimize cost");
                    Console.WriteLine("Real life problem: \n A power line needs to be run from point A to point B, through point C");
                    Console.WriteLine("It costs $50/ft. to run from point A to Poin C, and $130/ft. to run from point C to Point B.");
                    Console.WriteLine("Point A and Point C are at same level, Horizontal distance between point A and point B is equal 5000 ft. \\n And Vertical distance between point A and Point B is equal 1000 ft.");
                    Console.WriteLine("What should be the distance between point A and point C to minimize the overal Cost? \\n");
                    Console.WriteLine("Cost function: f(x) = 50(5000-x) + 130 * sqrt(x^2 + 1000^2)");
                    Console.WriteLine("Known solution is at x = 416.67 and f(416.67) = 370000");

                    dim = 1; // problem dimensions
                    numParticles = 20;
                    maxEpochs = 1000;
                    exitError = 0.0; // exit early if reach this error
                    minX = 0; // problem-dependent
                    maxX = 5000;
                }
            }
            else
            {
                Console.WriteLine("Goal is to minimize f(x0,x1,x2,x3) = A*N + Suma[xi^2 - A * Cos2PIxi];");
                Console.WriteLine("Known solution is at x0 = -2.903534, x1 = -2.903534, x2 = -2.903534, x3= -2.903534 and f(-2.903534, -2.903534, -2.903534, -2.903534) = -156.664");

                dim = 2; // problem dimensions
                numParticles = 15;
                maxEpochs = 1000;
                exitError = 0.0; // exit early if reach this error
                minX = 0.2; // problem-dependent
                maxX = 1;
            }
            

            Console.WriteLine("\nSetting problem dimension to " + dim);
            Console.WriteLine("Setting numParticles = " + numParticles);
            Console.WriteLine("Setting maxEpochs = " + maxEpochs);
            Console.WriteLine("Setting early exit error = " + exitError.ToString("F4"));
            Console.WriteLine("Setting minX, maxX = " + minX.ToString("F1") + " " + maxX.ToString("F1"));

            double[] bestPosition = Solver.Solve(dim, numParticles, minX, maxX, maxEpochs, exitError, args);
            double bestError = Solver.Error(bestPosition, args);

            Console.WriteLine("Best position/solution found:");
            for (int i = 0; i < bestPosition.Length; ++i)
            {
                Console.Write("x" + i + " = ");
                Console.WriteLine(bestPosition[i].ToString("F6") + " ");
            }
            Console.Write("Function value at best position: ");
            Console.WriteLine(Solver.GetFunctionResult(bestPosition, args));
            Console.WriteLine("");
            Console.Write("Final best error = ");
            Console.WriteLine(bestError.ToString("F5"));

            Console.ReadLine();
        }
    }
}
