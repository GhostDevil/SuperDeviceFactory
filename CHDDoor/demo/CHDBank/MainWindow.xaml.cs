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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CHDBank
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Border boder = new Border();
            boder.BorderBrush = new SolidColorBrush(Colors.Green);
            boder.BorderThickness =new Thickness(0,0,0,1);

            TextBlock textBlock = new TextBlock();
            textBlock.Text = DateTime.Now.ToString();
            boder.Child = textBlock;
            this.stackMessageInfo.Children.Insert(0, boder);
          
           
        }
    }
}
