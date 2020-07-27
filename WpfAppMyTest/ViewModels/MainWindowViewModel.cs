using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfAppMyTest.ViewModels.Base;
using WpfAppMyTest.Infrastructure.Commands;
using System.Windows;

namespace WpfAppMyTest.ViewModels
{
    class MainWindowViewModel : ViewModel
    {

        #region Заголовок Окна
        private string _Title = "Анализ";

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        #endregion
        #region command
        
        #endregion
    }

}
