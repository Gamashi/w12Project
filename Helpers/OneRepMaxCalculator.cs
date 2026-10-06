using System.Collections.Generic;
using w12.Models;

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

        public static OneRepMaxCardSummary GetCardSummary(List<ExecutionExercise> executions)
        {
            // 1. Filtra registros nulos ou órfãos (onde o BaseExercise foi deletado)
            var validExecutions = executions?
                .Where(e => e.BaseExercise != null)
                .ToList();

            if (validExecutions == null || validExecutions.Count == 0)
            {
                return new OneRepMaxCardSummary
                {
                    HasData = false,
                    ExerciseName = "Cadastre seu primeiro treino!"
                };
            }

            var today = DateTime.Today;

            var todayExecutions = validExecutions
                .Where(e => e.ExecutionDate.Date == today)
                .ToList();

            if (todayExecutions.Count > 0)
            {
                var bestToday = todayExecutions
                    .Select(e => new
                    {
                        Execution = e,
                        RM = EstimateOneRepMax(e.Weight, e.RepetitionCount)
                    })
                    .OrderByDescending(x => x.RM)
                    .First();

                return new OneRepMaxCardSummary
                {
                    HasData = true,
                    Title = "MAIOR 1RM DE HOJE 🔥",
                    // Protegido contra null com fallback amigável
                    ExerciseName = bestToday.Execution.BaseExercise?.Name ?? "Exercício Desconhecido",
                    Estimated1RM = bestToday.RM
                };
            }

            var bestAllTime = validExecutions
                .Select(e => new
                {
                    Execution = e,
                    RM = EstimateOneRepMax(e.Weight, e.RepetitionCount)
                })
                .OrderByDescending(x => x.RM)
                .First();

            return new OneRepMaxCardSummary
            {
                HasData = true,
                Title = "RECORDE PESSOAL (PR)",
                ExerciseName = bestAllTime.Execution.BaseExercise?.Name ?? "Exercício Desconhecido",
                Estimated1RM = bestAllTime.RM
            };
        }
    }
}