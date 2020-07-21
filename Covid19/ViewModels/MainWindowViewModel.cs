using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Covid19.Infrastructure.Commands;
using Covid19.Models;
using Covid19.ViewModels.Base;

namespace Covid19.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        #region TestDataPoints : IEnumerable<DataPoint>

        /// <summary>
        ///  Тестовый набор данных для графиков
        /// </summary>
        private IEnumerable<DataPoint> _TestDataPoints;

        /// <summary>
        ///  Тестовый набор данных для графиков
        /// </summary>
        public IEnumerable<DataPoint> TestDataPoint { get => _TestDataPoints; set => Set(ref _TestDataPoints, value); }

        #endregion

        #region Заголовок Окна
        private string _Title = "Анализ статистики Covid19";

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion


        #region Status : string - DESCRIPTION
        ///<summary>Статус программы(Перменная)</summary>
        private string _Status = "Готов!";

        ///<summary>Статус Программы(Свойство)</summary>
        public string Status
        {
          get => _Status;
          set => Set(ref _Status, value);
        }
        #endregion

        #region Команды

        #region CloseApplicationCommand
        public ICommand CloseApplicationCommand { get; }

        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }
        private bool CanCloseApplicationCommandExecuted(object p) => true;
        #endregion

        #endregion


        public MainWindowViewModel()
        {
            #region Команды

            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecuted);

            #endregion


            var data_points = new List<DataPoint>((int)(360/0.1));
            for(var x = 0d; x <= 360; x+= 0.1)
            {
                const double to_rad = Math.PI / 180;
                var y = Math.Sin(x * to_rad);

                data_points.Add(new DataPoint { XValue = x, YValue = y });
            }
            TestDataPoint = data_points;
        }
    }
}
