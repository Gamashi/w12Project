using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
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
            _baseExercises = await LinkCategories(_baseExercises);  
            if (_baseExercises != null && _baseExercises.Count > 0)
            {
                foreach (var baseExercise in _baseExercises)
                {                    
                    BaseExercises.Add(baseExercise);
                }
            }            
        }

        async Task <List<BaseExercise>> LinkCategories(List<BaseExercise> listBaseExercises) 
        {
            _categories = await _dataBase.GetCategoriesAsync();
            foreach(var bas in listBaseExercises)
            {
                if (bas.CategoryId != 0)
                {
                    bas.Category = _categories.FirstOrDefault(c => c.Id == bas.CategoryId);
                }
            }   
            return listBaseExercises;   
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
        [RelayCommand]
        async Task DeleteBaseExercise(BaseExercise exercise) 
        {
            if (exercise == null) return;

            // 1. Exibe a caixa de diálogo de confirmação
            bool confirm = await Shell.Current.DisplayAlert(
                    "Confirmar exclusão",
                    $"Tem certeza que deseja excluir o exercício \"{exercise.Name}\"? Todas as execuções e históricos vinculados a ele também serão excluídos.",
                    "Sim",
                    "Cancelar");
            // 2. Se o usuário cancelar, interrompe a execução
            if (!confirm) return;

            var response =  await _dataBase.DeleteBaseExerciseWithExecutionsAsync(exercise);
            if(response > 0)
            {
                BaseExercises.Remove(exercise);
                WeakReferenceMessenger.Default.Send(new ExerciseAddedMessage(null));
                ShowToast("Exercício deletado com sucesso");
            }
            else
            {
                ShowToast("Erro ao deletar exercício");
            }   
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
