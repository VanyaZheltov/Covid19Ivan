using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfAppMyTest.Infrastructure.Commands.Base;
using WpfAppMyTest.Test;

namespace WpfAppMyTest.Infrastructure.Commands.Base
{
    internal class ApplicationCommad : Command
    {
        public override bool CanExecute(object parameter) => true;


        public override void Execute(object parameter) => test();

           public void test()
           {
            MessageBox.Show("");
           }
    }
}
