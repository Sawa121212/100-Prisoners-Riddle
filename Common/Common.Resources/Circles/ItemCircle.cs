using System.Collections.ObjectModel;
using Avalonia.Controls;
using ReactiveUI;

namespace Common.Resources.Circles;

public class ItemCircle : ReactiveObject
{
    private ObservableCollection<CircleSubject> _items;

    public ItemCircle()
    {
        ItemsSource = new ObservableCollection<CircleSubject>();
    }

    public ObservableCollection<CircleSubject> ItemsSource
    {
        get => _items;
        set => this.RaiseAndSetIfChanged(ref _items, value);
    }
}

public class CircleSubject : ReactiveObject
{
    private int _x;
    private int _y;
    private object _content;
    private int _width;
    private int _height;

    public object Content
    {
        get => _content;
        set => this.RaiseAndSetIfChanged(ref _content, value);
    }

    public int X
    {
        get => _x;
        set => this.RaiseAndSetIfChanged(ref _x, value);
    }

    public int Y
    {
        get => _y;
        set => this.RaiseAndSetIfChanged(ref _y, value);
    }
    public int Width
    {
        get => _width;
        set => this.RaiseAndSetIfChanged(ref _width, value);
    }

    public int Height
    {
        get => _height;
        set => this.RaiseAndSetIfChanged(ref _height, value);
    }
}