using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Covid19.Infrastructure.Commands;
using Covid19.ViewModels.Base;

namespace Covid19.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
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
        }
    }
}
