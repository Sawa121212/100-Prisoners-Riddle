using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using BusinessLogic.Infrastructure.Interfaces;
using Common.Core;
using DataDomain;
using OxyPlot;
using OxyPlot.Series;
using Prism.Commands;
using Prism.Regions;
using ReactiveUI;

namespace BusinessLogic.Views.Report
{
    public class ReportViewModel : ViewModelBase, INavigationAware
    {
        private readonly IMainService _mainService;
        private IRegionNavigationJournal _journal;
        private PlotModel _gamesWinLoseModel;
        private PlotController _plotController;
        private PlotModel _prisonerWinLoseModel;

        public ReportViewModel(IMainService mainService)
        {
            _mainService = mainService;
            MoveBackCommand = new DelegateCommand(OnGoBack);

            PlotController = new PlotController();

            // отменяем скролл по колесику мыши
            //PlotController.UnbindMouseWheel();

            Initialize();
        }

        private void Initialize()
        {
            ObservableCollection<Game>? games = _mainService.Games;
            if (games.Any())
            {
                // Win\Lose Games
                _gamesWinLoseModel = new PlotModel() {Title = @"Win\Lose Games"};
                int gamesCount = games.Count;
                double gamesWin = games.Count(g => g.IsSuccess);
                double gamesLose = gamesCount - gamesWin;

                dynamic seriesP1 = new PieSeries
                {
                    StrokeThickness = 2.0,
                    InsideLabelPosition = 0.8,
                    AngleSpan = 360,
                    StartAngle = 0
                };
                seriesP1.Slices.Add(new PieSlice($"Win ({gamesWin})", gamesWin) {IsExploded = true, Fill = OxyColors.Green});
                seriesP1.Slices.Add(new PieSlice($"Lose ({gamesLose})", gamesLose) {IsExploded = false, Fill = OxyColors.Red});

                _gamesWinLoseModel.Series.Add(seriesP1);


                // Win\Lose Games
                _prisonerWinLoseModel = new PlotModel() { Title = @"Win\Lose Prisoner" };

                List<int> prisonersWin = games.Select(g => g.Prisoners.Count(p => p.IsNoteFound)).ToList();
                List<int> prisonersLose = games.Select(g => g.Prisoners.Count(p => !p.IsNoteFound)).ToList();


                dynamic seriesPrisoners1 = new PieSeries
                {
                    StrokeThickness = 2.0,
                    InsideLabelPosition = 0.7,
                    AngleSpan = 360,
                    StartAngle = 0
                };
                seriesPrisoners1.Slices.Add(new PieSlice($"Win ({prisonersWin.Sum()})", prisonersWin.Sum()) { IsExploded = true, Fill = OxyColors.Green });
                seriesPrisoners1.Slices.Add(new PieSlice($"Lose ({prisonersLose.Sum()})", prisonersLose.Sum()) { IsExploded = false, Fill = OxyColors.Red });

                _prisonerWinLoseModel.Series.Add(seriesPrisoners1);
            }
        }


        public PlotModel GamesWinLoseModel
        {
            get => _gamesWinLoseModel;
            set => this.RaiseAndSetIfChanged(ref _gamesWinLoseModel, value);
        }

        public PlotModel PrisonerWinLoseModel
        {
            get => _prisonerWinLoseModel;
            set => this.RaiseAndSetIfChanged(ref _prisonerWinLoseModel, value);
        }

        public PlotController PlotController
        {
            get => _plotController;
            set => this.RaiseAndSetIfChanged(ref _plotController, value);
        }


        private void OnGoBack()
        {
            _journal.GoBack();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _journal = navigationContext.NavigationService.Journal;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public ICommand MoveBackCommand { get; }
    }
}