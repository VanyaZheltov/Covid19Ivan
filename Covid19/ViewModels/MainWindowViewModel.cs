using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Covid19.Infrastructure.Commands;
using Covid19.Models;
using Covid19.Models.Decanat;
using Covid19.ViewModels.Base;

namespace Covid19.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        /*                                                                       */


        public ObservableCollection<Group> Groups { get; set; }

        public object[] CompositeCollection {     get;    }

        #region SelectedCompositeValue - выбранное композитное значение

        private Group _SelectedCompositeValue;

        public Group SelectedCompositeValue
        {
            get => _SelectedCompositeValue;
            set => Set(ref _SelectedCompositeValue, value);
        }

        #endregion

        #region SelectedGroup : Group - выбранная группа

        private Group _SelectedGroup;

        public Group SelectedGroup
        {
            get => _SelectedGroup;
            set => Set(ref _SelectedGroup, value);
        }

        #endregion

        #region PageIndex : int - номер вкладки
        /// <summary>
        /// Номер вкладки
        /// </summary>
        private int _SelectedPageIndex;

        public int SelectedPageIndex
        {
            get => _SelectedPageIndex;
            set => Set(ref _SelectedPageIndex, value);
        }
        #endregion

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

        /*                                                                       */

        #region Команды




        #region CloseApplicationCommand
        public ICommand CloseApplicationCommand { get; }

        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }
        private bool CanCloseApplicationCommandExecuted(object p) => true;
        #endregion
        #region ChangeTabIndexCommand
        public ICommand ChangeTabIndexCommand { get; }

        private bool CanChangeTabIndexCommandExecute(object p)
        {
            if ((p != null)) return true;
            else return false;
        }


        private void OnChangeTabIndexCommandExecuted(object p)
        {

            SelectedPageIndex += Convert.ToInt32(p);
        }
        #endregion
        #endregion

        /*                                                                       */

        public MainWindowViewModel()
        {
            #region Команды

            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecuted);
            ChangeTabIndexCommand = new LambdaCommand(OnChangeTabIndexCommandExecuted, CanChangeTabIndexCommandExecute);

            #endregion


            var data_points = new List<DataPoint>((int)(360/0.1));
            for(var x = 0d; x <= 360; x+= 0.1)
            {
                const double to_rad = Math.PI / 180;
                var y = Math.Sin(x * to_rad);

                data_points.Add(new DataPoint { XValue = x, YValue = y });
            }
            TestDataPoint = data_points;



            var student_index = 1;

            var students = Enumerable.Range(1, 10).Select(i => new Student
            {
                Name = $"Name {student_index}",
                Surname = $"Surname {student_index}",
                Patronymic = $"Patronymic {student_index++}",
                Birthday = DateTime.Now,
                Rating = 0
            });

            var groups = Enumerable.Range(1, 20).Select(i => new Group()
            {
                Name = $"Группа {i}",
                Students = new ObservableCollection<Student>(students)

            });
            Groups = new ObservableCollection<Group>(groups);


            var group = Groups[1];
            var data_list = new List<object>();
            data_list.Add("Hello!");
            data_list.Add(42);
            data_list.Add(group);
            data_list.Add(group.Students[0]);

            CompositeCollection = data_list.ToArray();
        }
        /*                                                                       */

    }
}
