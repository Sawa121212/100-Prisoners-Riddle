using System.Collections.ObjectModel;
using ReactiveUI;

namespace DataDomain;

/// <summary>
/// Комната с коробками
/// </summary>
public class Room : ReactiveObject
{
    private readonly Random _rng = new();
    private ObservableCollection<Box> _boxes;

    public Room()
    {
        Boxes = new ObservableCollection<Box>();
    }

    public Room(ObservableCollection<Prisoner> prisoners) : this()
    {
        CollectBoxes(prisoners);
    }

    /// <summary>
    /// Собрать коробки
    /// </summary>
    /// <param name="prisoners"></param>
    public void CollectBoxes(IList<Prisoner> prisoners)
    {
        if (prisoners.Count == 0)
            return;

        var randomPrisoners = Shuffle(prisoners);

        _boxes = new ObservableCollection<Box>();
        for (int i = 0; i < randomPrisoners.Count; i++)
        {
            _boxes.Add(new Box(i + 1, randomPrisoners[i].Id));
        }
    }

    /// <summary>
    /// Перемешать
    /// </summary>
    /// <param name="prisoners"></param>
    private List<Prisoner> Shuffle(IList<Prisoner> prisoners)
    {
        var newCollection = new List<Prisoner>(prisoners);
        var n = newCollection.Count;
        while (n > 1)
        {
            n--;
            var k = _rng.Next(n + 1);
            (newCollection[k], newCollection[n]) = (newCollection[n], newCollection[k]);
        }

        return newCollection;
    }

    /// <summary>
    /// Коробки
    /// </summary>
    public ObservableCollection<Box> Boxes
    {
        get => _boxes;
        private set => this.RaiseAndSetIfChanged(ref _boxes, value);
    }
}