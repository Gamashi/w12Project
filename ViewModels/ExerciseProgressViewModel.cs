using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Collections.ObjectModel;
using w12.Helpers;
using w12.Models;
using w12.Services;

namespace w12.ViewModels
{
    public partial class ExerciseProgressViewModel : ObservableObject
    {
        private readonly Database _database;
        private List<ExecutionExercise> _allExecutions = new();

        [ObservableProperty]
        private ObservableCollection<BaseExercise> _availableExercises = new();

        [ObservableProperty]
        private BaseExercise? _selectedExercise;

        [ObservableProperty]
        private ISeries[] _series = Array.Empty<ISeries>();

        [ObservableProperty]
        private Axis[] _xAxes = Array.Empty<Axis>();

        [ObservableProperty]
        private Axis[] _yAxes = Array.Empty<Axis>();

        [ObservableProperty]
        private bool _hasData;

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private bool _hasNoData;
        [ObservableProperty]
        private bool _isSingleEntry;


        public ExerciseProgressViewModel(Database database)
        {
            _database = database;
        }

        public async Task InitializeAsync()
        {
            IsLoading = true;
            try
            {
                var executions = await _database.GetExecutionExercisesListAsync();
                _allExecutions = executions?
                    .Where(e => e.BaseExercise != null)
                    .ToList() ?? new List<ExecutionExercise>();

                // Pega apenas os exercícios base que têm execuções registradas
                var distinctExercises = _allExecutions
                    .Select(e => e.BaseExercise!)
                    .GroupBy(b => b.BaseExerciseId)
                    .Select(g => g.First())
                    .OrderBy(b => b.Name)
                    .ToList();

                AvailableExercises = new ObservableCollection<BaseExercise>(distinctExercises);

                // Seleciona o primeiro por padrão, se houver
                if (AvailableExercises.Count > 0 && SelectedExercise == null)
                {
                    SelectedExercise = AvailableExercises.First();
                }
                if (AvailableExercises.Count == 0)
                {
                    HasData = false;
                    HasNoData = true;
                }
            }
            finally
            {
                IsLoading = false;
            }
        }

        partial void OnSelectedExerciseChanged(BaseExercise? value)
        {
            if (value == null) return;
            UpdateChartData(value.BaseExerciseId);
        }

        private void UpdateChartData(int baseExerciseId)
        {
            var exerciseExecutions = _allExecutions
                    .Where(e => e.BaseExerciseId == baseExerciseId)
                    .OrderBy(e => e.ExecutionDate)
                    .ToList();

            if (exerciseExecutions.Count == 0)
            {
                HasData = false;
                HasNoData = true;
                IsSingleEntry = false;
                Series = Array.Empty<ISeries>();
                return;
            }

            if (exerciseExecutions.Count == 1)
            {
                // Apenas 1 registro: marcamos como entrada única
                HasData = false;
                HasNoData = false;
                IsSingleEntry = true;
                Series = Array.Empty<ISeries>();
                return;
            }

            // 2 ou mais registros: traça o gráfico completo
            HasData = true;
            HasNoData = false;
            IsSingleEntry = false;

            HasData = true;
            HasNoData = false; // Esconde o aviso e mostra o gráfico

            // Se você cadastrou vários testes no mesmo dia, ordenamos por execução
            // Caso queira agrupar por dia, pode agrupar, mas para teste manter por sessão/execução garante que apareçam todos os pontos!
            var dataPoints = exerciseExecutions
                .Select(e => new
                {
                    // Se for no mesmo dia, mostra a hora para diferenciar; se forem dias distintos, mostra só a data
                    DateLabel = e.ExecutionDate.ToString("dd/MM"),
                    RM = Math.Round(OneRepMaxCalculator.EstimateOneRepMax(e.Weight, e.RepetitionCount), 1)
                })
                .ToList();

            var values = dataPoints.Select(d => d.RM).ToArray();
            var labels = dataPoints.Select(d => d.DateLabel).ToArray();

            // Cor do aplicativo (dourado/laranja vibrante)
            var primaryColor = SKColor.Parse("#FFAE52");

            Series = new ISeries[]
            {
        new LineSeries<double>
        {
            Name = "1RM (kg)",
            Values = values,
            Stroke = new SolidColorPaint(primaryColor, 3),
            GeometryStroke = new SolidColorPaint(primaryColor, 3),
            GeometryFill = new SolidColorPaint(SKColors.White),
            GeometrySize = 12, // Marcador redondo destacado
            LineSmoothness = 0.2, // Curva elegante e suave
            Fill = new LiveChartsCore.SkiaSharpView.Painting.LinearGradientPaint(
                new [] { primaryColor.WithAlpha(100), primaryColor.WithAlpha(10) },
                new SKPoint(0.5f, 0),
                new SKPoint(0.5f, 1))
        }
            };

            // Eixo X mapeando direto os rótulos de data
            XAxes = new Axis[]
            {
        new Axis
        {
            Labels = labels,
            LabelsPaint = new SolidColorPaint(SKColors.Black),
            TextSize = 13,
            LabelsRotation = 0,
            //SeparatorsPaint = new SolidColorPaint(SKColors.Gray.WithAlpha(40))
            SeparatorsPaint = new SolidColorPaint(SKColors.White)
        }
            };

            // Eixo Y ajustado para dar respiro nas cargas
            double minWeight = values.Length > 0 ? Math.Max(0, values.Min() - 5) : 0;
            double maxWeight = values.Length > 0 ? values.Max() + 5 : 50;

            YAxes = new Axis[]
            {
        new Axis
        {
            MinLimit = minWeight,
            MaxLimit = maxWeight,
            Labeler = val => $"{val:0} kg",
            LabelsPaint = new SolidColorPaint(SKColors.Black),
            TextSize = 13,
            //SeparatorsPaint = new SolidColorPaint(SKColors.Gray.WithAlpha(40))
            SeparatorsPaint = new SolidColorPaint(SKColors.White)
        }
            };
        }
    }
}