using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using w12.Helpers;
using w12.Models;
using w12.Services;

namespace w12.ViewModels
{
    public partial class HistoryViewModel : ObservableObject
    {
        private readonly Database _database;
        private List<ExecutionExercise> _allExecutions = new();

        [ObservableProperty]
        private ObservableCollection<ExecutionExercise> _filteredExecutions = new();

        [ObservableProperty]
        private string _searchText = string.Empty;

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private bool _isEmpty;

        public HistoryViewModel(Database database)
        {
            _database = database;
        }

        public async Task LoadHistoryAsync()
        {
            IsLoading = true;
            try
            {
                var list = await _database.GetExecutionExercisesListAsync();

                // Filtra registros válidos e ordena do mais recente para o mais antigo
                _allExecutions = list?
                    .Where(e => e.BaseExercise != null)
                    .OrderByDescending(e => e.ExecutionDate)
                    .ToList() ?? new List<ExecutionExercise>();

                ApplyFilter();
            }
            finally
            {
                IsLoading = false;
            }
        }

        partial void OnSearchTextChanged(string value)
        {
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredExecutions = new ObservableCollection<ExecutionExercise>(_allExecutions);
            }
            else
            {
                var query = SearchText.Trim().ToLowerInvariant();
                var filtered = _allExecutions.Where(e =>
                    (e.BaseExercise?.Name?.ToLowerInvariant().Contains(query) ?? false) ||
                    e.ExecutionDate.ToString("dd/MM/yyyy").Contains(query)
                );

                FilteredExecutions = new ObservableCollection<ExecutionExercise>(filtered);
            }

            IsEmpty = FilteredExecutions.Count == 0;
        }
    }
}