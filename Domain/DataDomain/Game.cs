using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Common.Resources.Circles;
using ReactiveUI;

namespace DataDomain
{
    /// <summary>
    /// Игра
    /// </summary>
    public class Game : ReactiveObject
    {
        private Room _room;
        private List<Prisoner> _prisoners;
        private List<ItemCircle> _itemCircle;
        private int _id;
        private bool _isSuccess;

        public Game(int id)
        {
            _id = id;
            _prisoners = new List<Prisoner>();
            _itemCircle = new List<ItemCircle>();
        }

        /// <summary>
        /// Номер игры
        /// </summary>
        public int Id
        {
            get => _id;
            private set => this.RaiseAndSetIfChanged(ref _id, value);
        }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description => $"Game {_id}";

        /// <summary>
        /// Начать игру
        /// </summary>
        public async Task Play(int prisonersCount)
        {
            await Build(prisonersCount);
            await StartSearch();
            await CheckSuccess();
            await BuildCircles().ConfigureAwait(false);
        }

        /// <summary>
        /// Собрать вместе заключенных и комнату
        /// </summary>
        /// <param name="count"></param>
        private async Task Build(int count)
        {
            if (count != 0)
            {
                _prisoners = new List<Prisoner>();

                for (int i = 0; i < count; i++)
                {
                    _prisoners.Add(new Prisoner(i + 1));
                }

                _room = new Room(_prisoners);
            }
        }

        /// <summary>
        /// Начать поиск номерка по коробкам в комнате
        /// </summary>
        private async Task StartSearch()
        {
            int maxSearchAttempt = _prisoners.Count / 2;
            foreach (Prisoner prisoner in _prisoners)
            {
                _room.EnterTheRoom(prisoner, maxSearchAttempt);
            }
        }

        /// <summary>
        /// Проверить на успешное выполнение 
        /// </summary>
        private async Task CheckSuccess()
        {
            IsSuccess = _prisoners.FirstOrDefault(p => !p.IsNoteFound) == null;
        }


        /// <summary>
        /// Построить круги коробок
        /// </summary>
        private async Task BuildCircles()
        {
            List<Box> boxes = new(_room.Boxes);

            int boxIndex = 1;
            int circleIndex = 1;

            while (boxes.Any())
            {
                ItemCircle circle = new($"Circle #{circleIndex}");

                while (true)
                {
                    // получили коробку по номеру
                    Box? box = boxes.FirstOrDefault(box => box.Id.Equals(boxIndex));

                    // если нет такой коробки, значит он уже используется
                    if (box == null)
                    {
                        break;
                    }

                    // запоминаем номер след. открываемой коробки по PrisonerId
                    boxIndex = box.PrisonerId;

                    circle.Add(new CircleSubject()
                    {
                        Width = 96,
                        Height = 96,
                        Content = box
                    });
                    boxes.Remove(box);
                }

                // update
                circle.OnUpdatePositioning();


                // save circle
                _itemCircle.Add(circle);

                // next box index
                boxIndex = boxes.FirstOrDefault() is { } item ? item.Id : 0;
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
        public List<Prisoner> Prisoners
        {
            get => _prisoners;
            private set => this.RaiseAndSetIfChanged(ref _prisoners, value);
        }

        /// <summary>
        /// Комната
        /// </summary>
        public Room Room
        {
            get => _room;
            private set => this.RaiseAndSetIfChanged(ref _room, value);
        }

        /// <summary>
        /// Круги коробок
        /// </summary>
        public List<ItemCircle> ItemCircle
        {
            get => _itemCircle;
            private set => this.RaiseAndSetIfChanged(ref _itemCircle, value);
        }
    }
}