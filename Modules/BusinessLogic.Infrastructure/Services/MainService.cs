using System.Collections.ObjectModel;
using DataDomain;
using ReactiveUI;

namespace BusinessLogic.Infrastructure.Services;

public class MainService : ReactiveObject, IMainService
{
    private readonly ISearchService _searchService;
    private ObservableCollection<Prisoner> _prisoners;
    private readonly Random _rng = new();
    private Room _room;
    private int _maxSearchAttempt;

    public MainService(ISearchService searchService)
    {
        _searchService = searchService;
    }

    public Room Room
    {
        get => _room;
        private set => this.RaiseAndSetIfChanged(ref _room, value);
    }

    public ObservableCollection<Prisoner> Prisoners
    {
        get => _prisoners;
        private set => this.RaiseAndSetIfChanged(ref _prisoners, value);
    }

    /// <summary>
    /// Собрать комнатау
    /// </summary>
    /// <param name="prisonersCount"></param>
    public void AssembleARoom(int prisonersCount)
    {
        if (prisonersCount != 0)
        {
            UniteThePrisoners(prisonersCount);
            Shuffle();

            var boxes = new ObservableCollection<Box>();
            for (int i = 0; i < prisonersCount; i++)
            {
                boxes.Add(new Box(i + 1, _prisoners[i].Id));
            }

            MaxSearchAttempt = boxes.Count / 2;
            _room = new Room(boxes);

            StartSearch();
        }
    }

    public int MaxSearchAttempt
    {
        get => _maxSearchAttempt;
        private set => this.RaiseAndSetIfChanged(ref _maxSearchAttempt, value);
    }

    /// <summary>
    /// Собрать заключенных вместе
    /// </summary>
    /// <param name="count"></param>
    private void UniteThePrisoners(int count)
    {
        if (count != 0)
        {
            _prisoners = new ObservableCollection<Prisoner>();

            for (int i = 0; i < count; i++)
            {
                _prisoners.Add(new Prisoner(i + 1));
            }
        }
    }


    /// <summary>
    /// Перемешать
    /// </summary>
    private void Shuffle()
    {
        var n = _prisoners.Count;
        while (n > 1)
        {
            n--;
            var k = _rng.Next(n + 1);
            (_prisoners[k], _prisoners[n]) = (_prisoners[n], _prisoners[k]);
        }
    }

    public void StartSearch()
    {
        foreach (var prisoner in _prisoners)
        {
            _searchService.ComeIntoTheRoom(prisoner, _room, _maxSearchAttempt);
        }
    }
}

public interface IMainService
{
    public Room Room { get; }
    public ObservableCollection<Prisoner> Prisoners { get; }
    public void AssembleARoom(int prisonersCount);
    public void StartSearch();
}