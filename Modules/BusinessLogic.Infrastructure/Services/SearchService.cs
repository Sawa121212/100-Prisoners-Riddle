using System.Collections.ObjectModel;
using DataDomain;
using ReactiveUI;

namespace BusinessLogic.Infrastructure.Services;

public class SearchService : ReactiveObject, ISearchService
{
    private ObservableCollection<Search> _searches;

    public SearchService()
    {
        _searches = new ObservableCollection<Search>();
    }

    public ObservableCollection<Search> Searches
    {
        get => _searches;
        private set => this.RaiseAndSetIfChanged(ref _searches, value);
    }

    /// <summary>
    /// Войти в комнату
    /// </summary>
    /// <param name="prisoner"></param>
    /// <param name="room"></param>
    /// <param name="maxSearchAttempt"></param>
    public void ComeIntoTheRoom(Prisoner prisoner, Room room, int maxSearchAttempt)
    {
        var boxes = room.Boxes;

        // новый поиск
        var search = new Search(prisoner);

        var index = prisoner.Id;
        for (int i = 0; i < maxSearchAttempt; i++)
        {
            var box = boxes[index-1];
            search.OpenBox(box);

            if (prisoner.IsNoteFound)
            {
                // нашли
                break;
            }
            else
            {
                index = box.PrisonerId;
            }
        }

        _searches.Add(search);
    }
}

public interface ISearchService
{
    public ObservableCollection<Search> Searches { get; }
    void ComeIntoTheRoom(Prisoner prisoner, Room room, int maxSearchAttempt);
}