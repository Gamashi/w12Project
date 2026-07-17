using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
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
        public ExerciseModelMagenimentViewModel(Database database)
        {
            this._dataBase = database;
            GetBaseExercises();
        }

        async void GetCategories()
        {
            _categories = await _dataBase.GetCategoriesAsync();
            if (_categories != null && _categories.Count > 0)
            {
                foreach (var cat in _categories)
                {
                    Categories.Add(cat);
                }
            }
        }
        async void GetBaseExercises() 
        {
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
    }
}
