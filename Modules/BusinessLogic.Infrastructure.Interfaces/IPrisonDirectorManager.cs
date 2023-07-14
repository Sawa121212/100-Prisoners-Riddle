using System.Collections.Generic;
using DataDomain;

namespace BusinessLogic.Infrastructure.Interfaces
{
    public interface IPrisonDirectorManager
    {
        /// <summary>
        /// Текущая игра
        /// </summary>
        public Game Game { get; }

        /// <summary>
        /// Список игр
        /// </summary>
        public List<Game> Games { get; }

        /// <summary>
        /// Начать новую игру
        /// </summary>
        /// <param name="prisonersCount">Количество заключенных в игре</param>
        public void StartNewGame(int prisonersCount);

        /// <summary>
        /// Начать новую игру
        /// </summary>
        public void StartNewGame();
    }
}