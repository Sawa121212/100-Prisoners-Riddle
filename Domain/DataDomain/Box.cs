using ReactiveUI;

namespace DataDomain
{
    /// <summary>
    /// Коробка
    /// </summary>
    public class Box : ReactiveObject
    {
        public Box(int id, int prisonerId)
        {
            Id = id;
            PrisonerId = prisonerId;
        }

        /// <summary>
        /// Number
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Prisoner Id
        /// </summary>
        public int PrisonerId { get; }

        /// <summary>
        /// Флаг, что в коробке заключенный нашел свой номер
        /// </summary>
        public bool IsPrisonerFound { get; set; }
    }
}