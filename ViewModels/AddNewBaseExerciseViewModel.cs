using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
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
        public async Task AddNewBaseExercise()
        {
            if (string.IsNullOrEmpty(BaseExercise.Name))
            {
                ShowToast("Nome é um campo obrigatório.");
                return;
            }
            if (string.IsNullOrEmpty(BaseExercise.Description))
            {
                ShowToast("Descrição é um campo obrigatório.");
                return;
            }
            if (BaseExercise.Category == null)
            {
                ShowToast("Selecione uma categoria.");
                return;
            }
            if (!string.IsNullOrEmpty(BaseExercise.Video))
            {
                if (!YoutubeLinkValidade(BaseExercise.Video))
                {
                    ShowToast("Link do vídeo inválido.");
                    return;
                }
            }
            await _dataBase.SaveBaseExercise(BaseExercise);
            await Shell.Current.GoToAsync("..");
        }

        private bool YoutubeLinkValidade(string link)
        {
            if (!link.Contains("youtube.com") || !link.Contains("youtu.be"))
            {
                return false;
            }
            return true;
        }

        private void ShowToast(string message)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            string text = message;
            ToastDuration duration = ToastDuration.Short;
            double fontSize = 14;
            var toast = Toast.Make(text, duration, fontSize);
            toast.Show(cancellationTokenSource.Token);
        }
    }
}
