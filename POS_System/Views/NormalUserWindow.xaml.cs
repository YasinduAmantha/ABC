using POS_System.ViewModels;
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

namespace POS_System.Views
{
    /// <summary>
    /// Interaction logic for NormalUserWindow.xaml
    /// </summary>
    public partial class NormalUserWindow : Window
    {
        public NormalUserWindow()
        {
            InitializeComponent();
            NormalUserVM vM = new NormalUserVM();
            DataContext = vM;
            vM.closeWindow = () => Close();
        }

    }
}
