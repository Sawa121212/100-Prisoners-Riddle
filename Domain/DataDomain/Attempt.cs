using ReactiveUI;

namespace DataDomain;

public class Attempt : ReactiveObject
{
    private Box _box;

    public Attempt(Box box)
    {
        _box = box;
    }

    public Box Box
    {
        get => _box;
        private set => this.RaiseAndSetIfChanged(ref _box, value);
    }
}