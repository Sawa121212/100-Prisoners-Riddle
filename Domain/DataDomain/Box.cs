using ReactiveUI;

namespace DataDomain;

public class Box : ReactiveObject
{
    private int _id;
    private int _prisonerId;
    private bool _isPrisonerFound;

    public Box(int id, int prisonerId)
    {
        _id = id;
        _prisonerId = prisonerId;
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
    /// Prisoner Id
    /// </summary>
    public int PrisonerId
    {
        get => _prisonerId;
        private set => this.RaiseAndSetIfChanged(ref _prisonerId, value);
    }
    
    public bool IsPrisonerFound
    {
        get => _isPrisonerFound;
        set => this.RaiseAndSetIfChanged(ref _isPrisonerFound, value);
    }
}