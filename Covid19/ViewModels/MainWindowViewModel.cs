﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Covid19.Infrastructure.Commands;
using Covid19.Models;
using Covid19.Models.Decanat;
using Covid19.ViewModels.Base;

namespace Covid19.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        /*----------------------------------------------------------------------------------------------------------------------------------------------------*/


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
            set
            {
                if (!Set(ref _SelectedGroup, value)) return;
                _SelectedGroupStudents.Source = value?.Students;
                OnPropertyChanged(nameof(SelectedGroupStudents));
                    
            }
        }

        #endregion

        #region StudentFilterTest : String

        private string _StudentFilterText;

        public string StudentFilterText
        {
            get => _StudentFilterText;
            set
            {
                if (!Set(ref _StudentFilterText, value)) return;
                _SelectedGroupStudents.View.Refresh();
            }
        }
        #endregion

        #region SelectedGroupStudents
        private readonly CollectionViewSource _SelectedGroupStudents = new CollectionViewSource();

        private void OnStudentFiltred(object sender, FilterEventArgs e)
        {

            if (!(e.Item is Student student))
            {
                e.Accepted = false;
                return;
            }

            var filter_text = _StudentFilterText;
            if (string.IsNullOrEmpty(filter_text))
                return;


            if (student.Name is null || student.Surname is null || student.Patronymic is null)
            {
                e.Accepted = false;
                return;
            }

            if (student.Name.Contains(filter_text)) return;
            if (student.Surname.Contains(filter_text)) return;
            if (student.Patronymic.Contains(filter_text)) return;

            e.Accepted = false;

        }

        public ICollectionView SelectedGroupStudents => _SelectedGroupStudents?.View;
        #endregion


        #region PageIndex : int - номер вкладки
        /// <summary>
        /// Номер вкладки
        /// </summary>
        private int _SelectedPageIndex = 1;

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





        public IEnumerable<Student> TestStudents => Enumerable.Range(1, App.IsDesignMode ? 10 : 100000).Select(i => new Student
        {
            Name = $"Имя {i}",
            Surname = $"Фамилия {i}"
        });

        public DirectoryViewModel DiskRootDir { get; } = new DirectoryViewModel("C:\\");

        #region Выбранная директория
        public DirectoryViewModel _SelectedDirectory;

        public DirectoryViewModel SelectedDirectory
        {
            get => _SelectedDirectory;
            set => Set(ref _SelectedDirectory, value);
        }
        #endregion
        /*----------------------------------------------------------------------------------------------------------------------------------------------------*/

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
        #region CreateNewGroupp
        public ICommand CreateNewGroupCommand { get; }

        private bool CanCreateGroupCommandExecute(object p) => true;
        
        private void OnCreateGroupCommandExecuted(object p)
        {
            var group_max_index = Groups.Count + 1;
            var new_group = new Group
            {
                Name = $"Группа {group_max_index}",
                Students = new ObservableCollection<Student>()
            };
            Groups.Add(new_group);
        }
        #endregion
        #region DeleteGroupCommand
        public ICommand DeleteGroupCommand { get; }

        private bool CanDeleteGroupCommandExeucte(object p) => p is Group group && Groups.Contains(group);
        private void OnDeleteGroupCommandExecuted(object p)
        {
            if (!(p is Group group)) return;
            var group_index = Groups.IndexOf(group);
            Groups.Remove(group);
            if (group_index < Groups.Count)
                SelectedGroup = Groups[group_index];

        }
        #endregion
        #endregion

        /*----------------------------------------------------------------------------------------------------------------------------------------------------*/

        public MainWindowViewModel()
        {
            #region Команды

            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecuted);
            ChangeTabIndexCommand = new LambdaCommand(OnChangeTabIndexCommandExecuted, CanChangeTabIndexCommandExecute);
            CreateNewGroupCommand = new LambdaCommand(OnCreateGroupCommandExecuted, CanCreateGroupCommandExecute);
            DeleteGroupCommand = new LambdaCommand(OnDeleteGroupCommandExecuted, CanDeleteGroupCommandExeucte);
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

            _SelectedGroupStudents.Filter += OnStudentFiltred;

        }

        
        /*----------------------------------------------------------------------------------------------------------------------------------------------------*/

    }
}
