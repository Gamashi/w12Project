using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using w12.Messages;
using w12.Models;
using w12.Services;
using w12.Views;

namespace w12.ViewModels
{
    public partial class ExerciseModelMagenimentViewModel : ObservableObject
    {
        private readonly Services.Database _dataBase;
        [ObservableProperty]
        public ObservableCollection<Category> categories = new ObservableCollection<Category>();
        private List<Category> _categories = new List<Category>();
        [ObservableProperty]
        public ObservableCollection<BaseExercise> baseExercises = new ObservableCollection<BaseExercise>();
        public List<BaseExercise> _baseExercises = new List<BaseExercise>();
        private BaseExercise baseExercise = new();
        public ExerciseModelMagenimentViewModel(Database database)
        {
            this._dataBase = database;
            WeakReferenceMessenger.Default.Register<BaseExerciseAddedMessage>(this, (r, m) =>
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    baseExercise = m.Value;
                    if (baseExercise != null)
                    {
                        GetBaseExercises();
                    }
                });
            });
            GetBaseExercises();
        }
        async void GetBaseExercises() 
        {
            if(BaseExercises.Count > 0)
            {
                BaseExercises.Clear();
            }   
            _baseExercises = await _dataBase.GetBaseExercisesAsync();
            if (_baseExercises != null && _baseExercises.Count > 0)
            {
                foreach (var baseExercise in _baseExercises)
                {
                    BaseExercises.Add(baseExercise);
                }
            }

        }
        [RelayCommand]
        async Task NavigateToAddNewBaseExercise()
        {
            await Shell.Current.GoToAsync(nameof(AddNewBaseExercise));
        }
        [RelayCommand]
        async Task NavigateToEditBaseExercise(BaseExercise exercise)
        {
            if (exercise == null) return;

            var navigationParameters = new Dictionary<string, object>
            {
                { "BaseExercise", exercise }
            };

            await Shell.Current.GoToAsync(nameof(AddNewBaseExercise), navigationParameters);
        }
    }
}
