namespace w12.Helpers
{
    public static class OneRepMaxCalculator
    {
        public static double EstimateOneRepMax(double weight, int reps)
        {
            if (reps <= 0) return 0;
            if (reps == 1) return weight;

            // Fórmula de Brzycki
            return weight / (1.0278 - (0.0278 * reps));
        }
    }
}