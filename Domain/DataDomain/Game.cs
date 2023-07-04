using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Common.Resources.Circles;
using DynamicData;
using ReactiveUI;

namespace DataDomain
{

    /// <summary>
    /// Игра
    /// </summary>
    public class Game : ReactiveObject
    {
        private Room _room;
        private ObservableCollection<Prisoner> _prisoners;
        private ObservableCollection<ItemCircle> _itemCircle;
        private int _id;
        private bool _isSuccess;

        public Game(int id)
        {
            _id = id;
            _prisoners = new ObservableCollection<Prisoner>();
            _itemCircle = new ObservableCollection<ItemCircle>();
        }

        public int Id
        {
            get => _id;
            private set => this.RaiseAndSetIfChanged(ref _id, value);
        }

        public string Description => $"Game {_id}";

        /// <summary>
        /// Собрать вместе
        /// </summary>
        /// <param name="count"></param>
        public void Build(int count)
        {
            if (count != 0)
            {
                _prisoners = new ObservableCollection<Prisoner>();

                for (int i = 0; i < count; i++)
                {
                    _prisoners.Add(new Prisoner(i + 1));
                }

                _room = new Room(_prisoners);
            }
        }

        public void CheckSuccess()
        {
            IsSuccess = _prisoners.FirstOrDefault(p => !p.IsNoteFound) == null;
        }

        public void BuildCircles()
        {
            var boxes = new List<Box>(_room.Boxes);

            var boxIndex = 1;
            var circleIndex = 1;

            while (boxes.Any())
            {
                var circle = new ItemCircle($"Circle #{circleIndex}");

                while (true)
                {
                    // получили коробку по номеру
                    var box = boxes.FirstOrDefault(box=> box.Id.Equals(boxIndex));

                    // если нет такой коробки, значит он уже используется
                    if (box == null)
                    {
                        break;
                    }

                    // запоминаем номер след. открываемой коробки по PrisonerId
                    boxIndex = box.PrisonerId;

                    circle.Add(new CircleSubject() { Width = 64, Height = 64, Content = box });
                    boxes.Remove(box);
                }

                // если мы не нашли коробки, начнем сначала
                //if (!circle.ItemsSource.Any())
                //{
                //    continue;
                //}

                // save circle
                _itemCircle.Add(circle);

                // next box index
                boxIndex = boxes.FirstOrDefault() is Box item ? item.Id : 0;
                circleIndex++;
            }
        }

        /// <summary>
        /// Комната
        /// </summary>
        public bool IsSuccess
        {
            get => _isSuccess;
            private set => this.RaiseAndSetIfChanged(ref _isSuccess, value);
        }

        /// <summary>
        /// Заключенные
        /// </summary>
        public ObservableCollection<Prisoner> Prisoners
        {
            get => _prisoners;
            private set => this.RaiseAndSetIfChanged(ref _prisoners, value);
        }

        public ObservableCollection<ItemCircle> ItemCircle
        {
            get => _itemCircle;
            private set => this.RaiseAndSetIfChanged(ref _itemCircle, value);
        }

        /// <summary>
        /// Комната
        /// </summary>
        public Room Room
        {
            get => _room;
            private set => this.RaiseAndSetIfChanged(ref _room, value);
        }
    }
}