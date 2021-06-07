namespace ParticleSworm
{
    class Particle : BaseModel
    {
        public double[] Position;
        public double Error;
        public double[] Velocity;
        public double[] BestPosition;
        public double BestError;

        public Particle(double[] position, double error, double[] velocity, double[] bestPosition, double bestError)
        {
            this.Position = new double[position.Length];
            position.CopyTo(this.Position, 0);
            this.Error = error;
            this.Velocity = new double[velocity.Length];
            velocity.CopyTo(this.Velocity, 0);
            this.BestPosition = new double[bestPosition.Length];
            bestPosition.CopyTo(this.BestPosition, 0);
            this.BestError = bestError;
        }

        public override string ToString()
        {
            string s = "";
            s += "==========================\n";
            s += "Position: ";
            for (int i = 0; i < this.Position.Length; ++i)
                s += this.Position[i].ToString("F4") + " ";
            s += "\n";
            s += "Error = " + this.Error.ToString("F4") + "\n";
            s += "Velocity: ";
            for (int i = 0; i < this.Velocity.Length; ++i)
                s += this.Velocity[i].ToString("F4") + " ";
            s += "\n";
            s += "Best Position: ";
            for (int i = 0; i < this.BestPosition.Length; ++i)
                s += this.BestPosition[i].ToString("F4") + " ";
            s += "\n";
            s += "Best Error = " + this.BestError.ToString("F4") + "\n";
            s += "==========================\n";
            return s;
        }
    }
}
