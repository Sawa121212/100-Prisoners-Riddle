using System.Collections.ObjectModel;
using ReactiveUI;

namespace DataDomain;

/// <summary>
/// Игра
/// </summary>
public class Game : ReactiveObject
{
    private Room _room;
    private ObservableCollection<Prisoner> _prisoners;
    private int _id;
    private bool _isSuccess;

    public Game(int id)
    {
        Id = id;
        Prisoners = new ObservableCollection<Prisoner>();
    }

    public int Id
    {
        get => _id;
        private set => this.RaiseAndSetIfChanged(ref _id, value);
    }

    public string Description => $"Game {_id}";

    /// <summary>
    /// Собрать вместе
    /// </summary>
    /// <param name="count"></param>
    public void Build(int count)
    {
        if (count != 0)
        {
            _prisoners = new ObservableCollection<Prisoner>();

            for (int i = 0; i < count; i++)
            {
                _prisoners.Add(new Prisoner(i + 1));
            }

            _room = new Room(_prisoners);
        }
    }

    public void CheckSuccess()
    {
        IsSuccess = _prisoners.FirstOrDefault(p => !p.IsNoteFound) == null;
    }

    /// <summary>
    /// Комната
    /// </summary>
    public bool IsSuccess
    {
        /*get
        {
            var c = _prisoners.FirstOrDefault(p => !p.IsNoteFound);
            return  c == null;
        }*/
        get => _isSuccess;
        private set => this.RaiseAndSetIfChanged(ref _isSuccess, value);
    }

    /// <summary>
    /// Заключенные
    /// </summary>
    public ObservableCollection<Prisoner> Prisoners
    {
        get => _prisoners;
        private set => this.RaiseAndSetIfChanged(ref _prisoners, value);
    }

    /// <summary>
    /// Комната
    /// </summary>
    public Room Room
    {
        get => _room;
        private set => this.RaiseAndSetIfChanged(ref _room, value);
    }
}