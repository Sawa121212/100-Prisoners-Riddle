using ReactiveUI;

namespace DataDomain;

public class Prisoner : ReactiveObject
{
    private int _id;
    private bool _isNoteFound;
    private int _numberOfCheckedBoxes;

    public Prisoner(int id)
    {
        _id = id;
    }

    /// <summary>
    /// Number
    /// </summary>
    public int Id
    {
        get => _id;
        private set => this.RaiseAndSetIfChanged(ref _id, value);
    }

    /// <summary>
    /// Number
    /// </summary>
    public int NumberOfCheckedBoxes
    {
        get => _numberOfCheckedBoxes;
        private set => this.RaiseAndSetIfChanged(ref _numberOfCheckedBoxes, value);
    }


    public bool IsNoteFound
    {
        get => _isNoteFound;
        set => this.RaiseAndSetIfChanged(ref _isNoteFound, value);
    }
}