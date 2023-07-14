using System.Collections.Generic;
using ReactiveUI;

namespace DataDomain
{
    /// <summary>
    /// Заключенный
    /// </summary>
    public class Prisoner : ReactiveObject
    {
        private int _id;
        private bool _isNoteFound;
        private List<Box> _openBoxes;

        public Prisoner(int id)
        {
            _id = id;
            _openBoxes = new List<Box>();
        }

        /// <summary>
        /// Добавить открытую коробку
        /// </summary>
        /// <param name="box"></param>
        public void OpenBox(Box box)
        {
            _openBoxes.Add(box);

            if (!box.PrisonerId.Equals(Id))
                return;

            box.IsPrisonerFound = true;
            IsNoteFound = true;
        }

        /// <summary>
        /// ID
        /// </summary>
        public int Id
        {
            get => _id;
            private set => this.RaiseAndSetIfChanged(ref _id, value);
        }

        /// <summary>
        /// Открытые коробки
        /// </summary>
        public List<Box> OpenBoxes
        {
            get => _openBoxes;
            private set => this.RaiseAndSetIfChanged(ref _openBoxes, value);
        }

        /// <summary>
        /// Number
        /// </summary>
        public int NumberOfOpenBoxes => _openBoxes.Count;

        /// <summary>
        /// Статус о найденном номере
        /// </summary>
        public bool IsNoteFound
        {
            get => _isNoteFound;
            set => this.RaiseAndSetIfChanged(ref _isNoteFound, value);
        }
    }
}