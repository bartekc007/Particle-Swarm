using System;

namespace ParticleSworm
{
    class Solver : BaseModel
    {
        public static double[] Solve(int dimension, int numParticles, double minX, double maxX, int maxEpochs, double exitError, string[] args)
        {
            Random rnd = new Random();

            Particle[] swarm = new Particle[numParticles];
            double[] bestGlobalPosition = new double[dimension];
            double bestGlobalError = double.MaxValue;

            for (int j = 0; j < swarm.Length; j++)
            {
                // Initialized random possitions
                double[] randomPosition = new double[dimension];
                
                if(args.Length > 0)
                {
                    for (int i = 0; i < randomPosition.Length; i++)
                    {
                        randomPosition[i] = (maxX - minX) * rnd.NextDouble() + minX;
                    }
                }
                else
                {
                    randomPosition[0] = (maxX - minX) * rnd.NextDouble() + minX;
                }
                
                
                double error = Error(randomPosition, args);

                // Initialized random velocity
                double[] randomVelocity = new double[dimension];
                if (args.Length > 0)
                {
                    for (int i = 0; i < randomVelocity.Length; i++)
                    {
                        double lo = minX * 0.1;
                        double hi = maxX * 0.1;
                        randomVelocity[i] = (hi - lo) * rnd.NextDouble() + lo;
                    }
                }
                else
                {
                    double loX = minX * 0.1;
                    double hiX = maxX * 0.1;
                    randomVelocity[0] = (hiX - loX) * rnd.NextDouble() + loX;
                }
                   
                swarm[j] = new Particle(randomVelocity, error, randomVelocity, randomPosition, error);

                if(swarm[j].Error < bestGlobalError)
                {
                    bestGlobalError = swarm[j].Error;
                    swarm[j].Position.CopyTo(bestGlobalPosition, 0);
                }
            }

            double w = 0.729; // inertia
            double c1 = 1.49445; // cognitive weight
            double c2 = 1.49445; // social weight
            double r1, r2; // cognitive, social randomizations
            double probDeath = 0.05;
            int epoch = 0;

            double[] newVelocity = new double[dimension];
            double[] newPosition = new double[dimension];
            double newError;

            while(epoch < maxEpochs)
            {
                for (int i = 0; i < swarm.Length; i++)
                {
                    Particle currentParticle = swarm[i];

                    // New velocity for current particle
                    for (int j = 0; j < currentParticle.Velocity.Length; j++)
                    {
                        r1 = rnd.NextDouble();
                        r2 = rnd.NextDouble();

                        newVelocity[j] = (w * currentParticle.Velocity[j]) +
                          (c1 * r1 * (currentParticle.BestPosition[j] - currentParticle.Position[j])) +
                          (c2 * r2 * (bestGlobalPosition[j] - currentParticle.Position[j]));
                    }
                    newVelocity.CopyTo(currentParticle.Velocity, 0);

                    // New Position for current particle
                    if(args.Length > 0)
                    {
                        for (int j = 0; j < currentParticle.Position.Length; j++)
                        {
                            newPosition[j] = currentParticle.Position[j] + newVelocity[j];
                            if (newPosition[j] < minX)
                                newPosition[j] = minX;
                            else if (newPosition[j] > maxX)
                                newPosition[j] = maxX;
                        }
                    }
                    else
                    {
                        newPosition[0] = currentParticle.Position[0] + newVelocity[0];
                        newPosition[1] = currentParticle.Position[1] + newVelocity[1];
                        if (newPosition[0] < minX)
                            newPosition[0] = minX;
                        if (newPosition[0] > maxX)
                            newPosition[0] = maxX;
                    }
                    
                    newPosition.CopyTo(currentParticle.Position, 0);

                    newError = Error(newPosition, args);
                    currentParticle.Error = newError;

                    if (newError < currentParticle.BestError)
                    {
                        newPosition.CopyTo(currentParticle.BestPosition, 0);
                        currentParticle.BestError = newError;
                    }

                    if (newError < bestGlobalError)
                    {
                        newPosition.CopyTo(bestGlobalPosition, 0);
                        bestGlobalError = newError;
                    }

                    // death

                    double die = rnd.NextDouble();
                    if(die < probDeath)
                    {
                        if(args.Length > 0)
                        {
                            for (int j = 0; j < currentParticle.Position.Length; j++)
                            {
                                currentParticle.Position[j] = (maxX - minX) * rnd.NextDouble() + minX;
                            }
                        }
                        else 
                        {
                            currentParticle.Position[0] = (maxX - minX) * rnd.NextDouble() + minX;
                        }
                        
                        currentParticle.Error = Error(currentParticle.Position, args);
                        currentParticle.Position.CopyTo(currentParticle.BestPosition, 0);
                        currentParticle.BestError = currentParticle.Error;

                        if(currentParticle.Error < bestGlobalError)
                        {
                            bestGlobalError = currentParticle.Error;
                            currentParticle.Position.CopyTo(bestGlobalPosition, 0);
                        }
                    }
                } // for each particle
                epoch++;
            } // swarm

            // show final swarm
            Console.WriteLine("\nProcessing complete");
            Console.WriteLine("\nFinal swarm:\n");
            for (int i = 0; i < swarm.Length; ++i)
                Console.WriteLine(swarm[i].ToString());

            double[] result = new double[dimension];
            bestGlobalPosition.CopyTo(result, 0);
            return result;
        }
    }
}
