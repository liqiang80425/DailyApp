using Prism.Events;
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

namespace WpfForPrism.Views
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IEventAggregator EventAggregator;
        public MainWindow(IEventAggregator _EventAggregator)
        {
            InitializeComponent();
            EventAggregator= _EventAggregator;
        }

        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPubClick(object sender, RoutedEventArgs e)
        {
            EventAggregator.GetEvent<MsgEvent>().Publish("我想要去旅游");
        }

        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSubClick(object sender, RoutedEventArgs e)
        {
            EventAggregator.GetEvent<MsgEvent>().Subscribe(Sub);
        }

        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="obj">接收的订阅信息</param>
        private void Sub(string obj)
        {
            MessageBox.Show($"我收到订阅的信息:{obj}");
        }

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancelClick(object sender, RoutedEventArgs e)
        {
            EventAggregator.GetEvent<MsgEvent>().Unsubscribe(Sub);
        }
        
    }
}
