using System.Collections.ObjectModel;
using DataDomain;

namespace BusinessLogic.Infrastructure.Interfaces
{
    public interface ISearchService
    {
        /// <summary>
        /// История попыток
        /// </summary>
        public ObservableCollection<Search> Searches { get; }

        /// <summary>
        /// Войти в комнату
        /// </summary>
        /// <param name="prisoner"></param>
        /// <param name="room"></param>
        /// <param name="maxSearchAttempt"></param>
        void ComeIntoTheRoom(Prisoner prisoner, Room room, int maxSearchAttempt);
    }
}