using System.Collections.ObjectModel;
using ReactiveUI;

namespace DataDomain;

public class Room : ReactiveObject
{
    private ObservableCollection<Box> _boxes;

    public Room(ObservableCollection<Box> boxes)
    {
        _boxes = boxes;
    }

    public ObservableCollection<Box> Boxes
    {
        get => _boxes;
        private set => this.RaiseAndSetIfChanged(ref _boxes, value);
    }
}