using System.Collections.ObjectModel;
using ReactiveUI;
using System;
using System.Linq;
using Avalonia;

namespace Common.Resources.Circles
{
    /// <summary>
    /// Круг, цикл
    /// </summary>
    public class ItemCircle : ReactiveObject
    {
        private ObservableCollection<CircleSubject> _items;

        public ItemCircle(string description = null)
        {
            _description = description;
            _items = new ObservableCollection<CircleSubject>();
        }

        /// <summary>
        /// Добавить элемент
        /// </summary>
        /// <param name="circleSubject"></param>
        public void Add(CircleSubject circleSubject)
        {
            if (circleSubject == null)
                return;

            _items.Add(circleSubject);
        }

        /// <summary>
        /// Обновить представление
        /// </summary>
        public void OnUpdatePositioning()
        {
            if (!_items.Any())
            {
                return;
            }


            double itemHeight = _items.OrderByDescending(item => item.Height).First().Height;
            double itemWidth = _items.OrderByDescending(item => item.Width).First().Width;

            if (itemHeight <= 0)
            {
                itemHeight = 1;
            }

            if (itemWidth <= 0)
            {
                itemWidth = 1;
            }

            int itemCount = _items.Count;

            _width = itemCount > 3 ? (itemCount * 0.5) * itemWidth : itemCount * itemWidth;
            _height = itemCount > 3 ? (itemCount * 0.5) * itemHeight : itemCount * itemHeight;

            // установим центр
            _center = new Point(_width / 2.0, _height / 2.0);

            // добавим, т.к. у объекта положение не по центру, а вниз-направо
            _width += itemWidth;
            _height += itemHeight;

            // выбираем минимальный радиус
            double radius = Math.Min(_center.X, _center.Y);

            for (int index = 0; index < itemCount; index++)
            {
                CircleSubject item = _items[index];

                double angleRadians = (2.0 / itemCount) * index * Math.PI;

                item.X = (radius - item.Width / 2.0) * Math.Sin(angleRadians) + radius;
                item.Y = (radius - item.Height / 2.0) * Math.Cos(angleRadians) + radius;
            }
        }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description
        {
            get => _description;
            set => this.RaiseAndSetIfChanged(ref _description, value);
        }

        /// <summary>
        /// Элементы круга
        /// </summary>
        public ObservableCollection<CircleSubject> ItemsSource
        {
            get => _items;
            private set => this.RaiseAndSetIfChanged(ref _items, value);
        }

        public double Width
        {
            get => _width;
            set => this.RaiseAndSetIfChanged(ref _width, value);
        }

        public double Height
        {
            get => _height;
            set => this.RaiseAndSetIfChanged(ref _height, value);
        }

        public Point Center
        {
            get => _center;
            private set => this.RaiseAndSetIfChanged(ref _center, value);
        }

        private string _description;
        private double _width;
        private double _height;
        private Point _center;
    }
}