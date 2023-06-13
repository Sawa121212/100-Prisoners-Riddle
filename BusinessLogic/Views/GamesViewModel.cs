using System.Collections.ObjectModel;
using System.Windows.Input;
using BusinessLogic.Infrastructure.Services;
using Common.Core;
using DataDomain;
using ReactiveUI;

namespace BusinessLogic.Views;

public class GamesViewModel : ViewModelBase
{
    private readonly IMainService _mainService;
    private ObservableCollection<Game> _games;
    private Game _selectedGame;

    public GamesViewModel(IMainService mainService)
    {
        _mainService = mainService;
        _games = _mainService.Games;
        AddNewGameCommand = ReactiveCommand.Create(OnAddNewGame);
    }

    /// <summary>
    /// Добавить новую игру
    /// </summary>
    private void OnAddNewGame()
    {
        _mainService.StartNewGame();
    }

    /// <summary>
    /// Список игр
    /// </summary>
    public ObservableCollection<Game> Games
    {
        get => _games;
        set => this.RaiseAndSetIfChanged(ref _games, value);
    }
    
    /// <summary>
    /// Выбранная игра
    /// </summary>
    public Game SelectedGame
    {
        get => _selectedGame;
        set => this.RaiseAndSetIfChanged(ref _selectedGame, value);
    }

    public ICommand AddNewGameCommand { get; }
}