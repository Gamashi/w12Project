using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using w12.Models;
using w12.Services;

namespace w12.ViewModels
{
    public partial class AddNewBaseExerciseViewModel : ObservableObject
    {
        private readonly Database _dataBase;
        [ObservableProperty]
        public ObservableCollection<Category> categories = new ObservableCollection<Category>();
        [ObservableProperty]
        public BaseExercise baseExercise = new BaseExercise();
        private List<Category> _categories = new List<Category>();
        public AddNewBaseExerciseViewModel(Database database)
        {
            this._dataBase = database;
            GetCategories();
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
        [RelayCommand]
        public void AddNewBaseExercise()
        {
            if (string.IsNullOrEmpty(BaseExercise.Name) || string.IsNullOrEmpty(BaseExercise.Description))
            {
                return;
            }
            if (BaseExercise.Category == null)
            {
                return;
            }
            if (BaseExercise.Image == null)
            {
                return;
            }
            if (BaseExercise.Video == null)
            {
                return;
            }
        }
        private void VaidateBaseExercise()
        {

        }
    }
}
