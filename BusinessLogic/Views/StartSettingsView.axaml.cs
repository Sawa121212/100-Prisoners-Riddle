using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BusinessLogic.Views;

public partial class StartSettingsView : UserControl
{
    public StartSettingsView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}