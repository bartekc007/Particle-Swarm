using System;

namespace ParticleSworm
{
    class BaseModel
    {
        public static double Error(double[] x, string[] args)
        {
            double trueMin = 0.04202;

            if(args.Length>0)
            {
                // Functon 1
                if (args[0] == "-t1" || args[0] == "-T1")
                    trueMin = 0;

                // Function 2
                if (args[0] == "-t2" || args[0] == "-T2")
                    trueMin = 0;

                // Function 3
                if (args[0] == "-t3" || args[0] == "-T3")
                    trueMin = -39.166165 * x.Length;

                // Function 4
                if (args[0] == "-t4" || args[0] == "-T4")
                    trueMin = 370000;
            }
            else
            {

            }
            
            double z = GetFunctionResult(x,args);
            double result = (z - trueMin) * (z - trueMin);
            return result;
        }

        public static double GetFunctionResult(double[] x, string[] args)
        {
            double result = 0;


            if (args.Length > 0)
            {
                // Function 1
                if (args[0] == "-t1" || args[0] == "-T1")
                    result = -20 * Math.Exp(-0.2 * Math.Sqrt(0.5 * (Math.Pow(x[0], 2) * Math.Pow(x[1], 2))))
                    - Math.Exp(0.5 * (Math.Cos(2 * Math.PI * x[0]) + Math.Cos(2 * Math.PI * x[1]))) + Math.E + 20;

                // Function 2
                if (args[0] == "-t2" || args[0] == "-T2")
                {
                    double a = 10;
                    result = a * x.Length;
                    for (int i = 0; i < x.Length; i++)
                    {
                        result += Math.Pow(x[i], 2) - a * Math.Cos(2 * Math.PI * x[i]);
                    }
                }

                // Function 3
                if (args[0] == "-t3" || args[0] == "-T3")
                {
                    result = 0;
                    for (int i = 0; i < x.Length; i++)
                    {
                        result += Math.Pow(x[i], 4) - 16 * Math.Pow(x[i], 2) + 5 * x[i];
                    }
                    result = result * 0.5;
                    return result;
                }

                // Function 4
                if (args[0] == "-t4" || args[0] == "-T4")
                {
                    result = 50 * (5000 - x[0]) + 130 * Math.Sqrt(Math.Pow(x[0], 2) + Math.Pow(1000, 2));
                    return result;
                }
            }
            else
            {
                double P = 1000;
                double E = 207 * Math.Pow(10, 9);
                result = (64 * P * Math.Pow(x[0], 3)) / (3 * E * Math.PI * Math.Pow(x[1], 4));
            }

            return result;
        }
    }
}
