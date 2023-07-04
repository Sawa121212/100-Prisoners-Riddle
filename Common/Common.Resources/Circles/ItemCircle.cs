using System.Collections.ObjectModel;
using Avalonia.Controls;
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

        public void Add(CircleSubject circleSubject)
        {
            if (circleSubject != null)
            {
                _items.Add(circleSubject);
                OnUpdatePositioning();
            }
        }

        private void OnUpdatePositioning()
        {
            double scale = 1.25;

            if (!_items.Any())
            {
                return;
            }


            var itemHeight = _items.OrderByDescending(item => item.Height).First().Height;
            var itemWidth = _items.OrderByDescending(item => item.Width).First().Width;

            if (itemHeight <= 0)
            {
                itemHeight = 1;
            }

            if (itemWidth <= 0)
            {
                itemWidth = 1;
            }

            var itemCount = _items.Count;

            _width = itemCount > 1 ? (itemCount * 0.5) * itemWidth * scale : itemCount * itemWidth * scale;
            _height = itemCount > 2 ? (itemCount * 0.5) * itemHeight * scale : itemCount * itemHeight * scale;

            // установим центр
            _center = new Point(_width / 2, _height / 2);

            _width += itemWidth;
            _height += itemHeight;

            // выбираем минимальный радиус
            double radius = itemCount == 2 ? _center.Y : Math.Min(_center.X, _center.Y);

            for (int index = 0; index < itemCount; index++)
            {
                var item = _items[index];

                double angleRadians = ((double)2 / itemCount) * index * Math.PI;

                item.X = (radius - item.Width / 2) * Math.Sin(angleRadians) + radius;
                item.Y = (radius - item.Height / 2) * Math.Cos(angleRadians) + radius;
            }
        }

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

    /// <summary>
    /// Объект круга
    /// </summary>
    public class CircleSubject : ReactiveObject
    {
        private object _content;
        private double _x;
        private double _y;
        private double _width;
        private double _height;

        public object Content
        {
            get => _content;
            set => this.RaiseAndSetIfChanged(ref _content, value);
        }

        public double X
        {
            get => _x;
            set => this.RaiseAndSetIfChanged(ref _x, value);
        }

        public double Y
        {
            get => _y;
            set => this.RaiseAndSetIfChanged(ref _y, value);
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
    }
}