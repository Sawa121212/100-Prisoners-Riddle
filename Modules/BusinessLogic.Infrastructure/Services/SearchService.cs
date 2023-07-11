using System.Collections.ObjectModel;
using BusinessLogic.Infrastructure.Interfaces;
using DataDomain;
using ReactiveUI;

namespace BusinessLogic.Infrastructure.Services
{
    public class SearchService : ReactiveObject, ISearchService
    {
        private ObservableCollection<Search> _searches;

        public SearchService()
        {
            _searches = new ObservableCollection<Search>();
        }

        /// <inheritdoc />
        public ObservableCollection<Search> Searches
        {
            get => _searches;
            private set => this.RaiseAndSetIfChanged(ref _searches, value);
        }

        /// <inheritdoc />
        public void ComeIntoTheRoom(Prisoner prisoner, Room room, int maxSearchAttempt)
        {
            ObservableCollection<Box> boxes = room.Boxes;

            // новый поиск
            Search search = new(prisoner);

            int index = prisoner.Id;
            for (int i = 0; i < maxSearchAttempt; i++)
            {
                Box box = boxes[index - 1];
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
}