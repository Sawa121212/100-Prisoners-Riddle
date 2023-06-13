using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BusinessLogic.Views;

public partial class GamesView : UserControl
{
    public GamesView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}