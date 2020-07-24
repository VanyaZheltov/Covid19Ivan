using Covid19.Models.Decanat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Covid19.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();

        private void GroupsCollection_OnFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Group group)) return;
            if (group.Name is null) return;


            var filter_text = GroupNameFilterText.Text;
            if (filter_text.Length == 0) return;


            if (group.Name.ToLower().Contains(filter_text) || group.Name.Contains(filter_text)) return;
            if (group.Description != null && group.Description.Contains(filter_text)) return;

            e.Accepted = false;
        }

        private void GroupNameFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            var text_box = (TextBox) sender;
            var collection = (CollectionViewSource)text_box.FindResource("GroupsCollection");
            collection.View.Refresh();
        }
    }
}
