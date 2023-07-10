using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BusinessLogic.Views.Report
{
    public partial class ReportView : UserControl
    {
        public ReportView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
