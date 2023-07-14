using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ReactiveUI;

namespace DataDomain
{

    /// <summary>
    /// Комната с коробками
    /// </summary>
    public class Room : ReactiveObject
    {
        private readonly Random _rng = new();
        private List<Box> _boxes;

        private Room()
        {
            Boxes = new List<Box>();
        }

        public Room(IList<Prisoner> prisoners) : this()
        {
            CollectBoxes(prisoners);
        }

        /// <summary>
        /// Собрать коробки
        /// </summary>
        /// <param name="prisoners"></param>
        private void CollectBoxes(IList<Prisoner> prisoners)
        {
            if (prisoners.Count == 0)
                return;

            List<Prisoner> randomPrisoners = Shuffle(prisoners);

            _boxes = new List<Box>();
            for (int i = 0; i < randomPrisoners.Count; i++)
            {
                _boxes.Add(new Box(i + 1, randomPrisoners[i].Id));
            }
        }

        /// <summary>
        /// Перемешать
        /// </summary>
        /// <param name="prisoners"></param>
        private List<Prisoner> Shuffle(IList<Prisoner> prisoners)
        {
            List<Prisoner> newCollection = new List<Prisoner>(prisoners);
            int n = newCollection.Count;
            while (n > 1)
            {
                n--;
                int k = _rng.Next(n + 1);
                (newCollection[k], newCollection[n]) = (newCollection[n], newCollection[k]);
            }

            return newCollection;
        }

        /// <summary>
        /// Войти в комнату
        /// </summary>
        /// <param name="prisoner">Заключенный</param>
        /// <param name="maxSearchAttempt">Количество попыток</param>
        public void EnterTheRoom(Prisoner prisoner, int maxSearchAttempt)
        {
            int index = prisoner.Id;
            for (int i = 0; i < maxSearchAttempt; i++)
            {
                Box box = _boxes[index - 1];
                prisoner.OpenBox(box);

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
        }

        /// <summary>
        /// Коробки
        /// </summary>
        public List<Box> Boxes
        {
            get => _boxes;
            private set => this.RaiseAndSetIfChanged(ref _boxes, value);
        }
    }
}