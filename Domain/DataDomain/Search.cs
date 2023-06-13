using System.Collections.ObjectModel;
using ReactiveUI;

namespace DataDomain;

/// <summary>
/// Поиск
/// </summary>
public class Search : ReactiveObject
{
    private ObservableCollection<Attempt> _attempts;
    private Prisoner _prisoner;

    public Search(Prisoner prisoner)
    {
        _attempts = new ObservableCollection<Attempt>();
        _prisoner = prisoner;
    }

    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id => _prisoner.Id;

    /// <summary>
    /// Описание
    /// </summary>
    public string Description => $"#{Id} prisoner search list";

    /// <summary>
    /// Заключенный
    /// </summary>
    public Prisoner Prisoner
    {
        get => _prisoner;
        private set => this.RaiseAndSetIfChanged(ref _prisoner, value);
    }
    
    /// <summary>
    /// Попытки заключенный
    /// </summary>
    public ObservableCollection<Attempt> Attempts
    {
        get => _attempts;
        private set => this.RaiseAndSetIfChanged(ref _attempts, value);
    }

    /// <summary>
    /// попытки
    /// </summary>
    public bool OpenBox(Box box)
    {
        _attempts.Add(new Attempt(box));
        _prisoner.AddOpenedBox(box);
        
        if (box.PrisonerId.Equals(Id))
        {
            box.IsPrisonerFound = true;
            _prisoner.IsNoteFound = true;
            return true;
        }

        return false;
    }
}