using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace ModuleA.ViewModels
{
    internal class ViewAViewModel : BindableBase,IConfirmNavigationRequest
    {
        /// <summary>
        /// 绑定的内容
        /// </summary>
        private string _Msg;

        /// <summary>
        /// 绑定的内容
        /// </summary>
        public string Msg
        {
            get { return _Msg; }
            set
            {
                _Msg = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <param name="continuationCallback"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            bool result = true;
            if (MessageBox.Show("确定切换吗?", "温馨提示", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                result = false;
            }

            continuationCallback(result);
        }

        /// <summary>
        /// 是否重用实例
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        /// <summary>
        /// 接收参数
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.ContainsKey("MsgA"))
            {
                Msg = navigationContext.Parameters.GetValue<string>("MsgA");
            }
        }
    }
}
