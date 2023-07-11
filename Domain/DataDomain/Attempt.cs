using ReactiveUI;

namespace DataDomain
{

    /// <summary>
    /// Попытка просмотра коробки
    /// </summary>
    public class Attempt : ReactiveObject
    {
        private Box _box;

        public Attempt(Box box)
        {
            _box = box;
        }

        /// <summary>
        /// Коробка
        /// </summary>
        public Box Box
        {
            get => _box;
            private set => this.RaiseAndSetIfChanged(ref _box, value);
        }
    }
}
