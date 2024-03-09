using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfForPrism.Views;

namespace WpfForPrism.ViewModels
{
    internal class MainWindowViewModel : BindableBase
    {
        /// <summary>
        /// 导航记录
        /// </summary>
        private IRegionNavigationJournal Journal;

        public DelegateCommand<string> ShowContentCmm { get; set; }

        /// <summary>
        /// 后退命令
        /// </summary>
        public DelegateCommand BackCmm { get; set; }

        public DelegateCommand<string> ShowDialogCmm { get; set; }

        ///区域管理
        private readonly IRegionManager RegionManager;

        /// <summary>
        /// 对话框服务
        /// </summary>
        private readonly IDialogService DialogService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_RegionManager"></param>
        public MainWindowViewModel(IRegionManager _RegionManager, IDialogService _DialogService)
        {
            ShowContentCmm = new DelegateCommand<string>(ShowContentFunc);
            BackCmm = new DelegateCommand(Back);

            RegionManager = _RegionManager;

            //对话框服务
            DialogService = _DialogService;
            ShowDialogCmm = new DelegateCommand<string>(ShowDialogFunc);
        }

        /// <summary>
        /// 后退方法
        /// </summary>
        private void Back()
        {
            if (Journal != null && Journal.CanGoBack)
            {
                Journal.GoBack();
            }
        }

        /// <summary>
        /// 改变显示用户控件
        /// </summary>
        /// <param name="Name">用户控件名称</param>
        private void ShowContentFunc(string viewName)
        {
            NavigationParameters paras = new NavigationParameters();
            paras.Add("MsgA", "大家好，我是A");//键值对

            RegionManager.Regions["ContentRegion"].RequestNavigate(viewName, callback =>
            {
                Journal = callback.Context.NavigationService.Journal;
            }, paras);
        }

        /// <summary>
        /// 打开对话框
        /// </summary>
        /// <param name="ucName">用户控件名称</param>
        private void ShowDialogFunc(string ucName)
        {
            DialogParameters paras = new DialogParameters();
            paras.Add("Title", "动态传递的标题");
            paras.Add("para1", "业务参数值1");
            paras.Add("para2", "业务参数值2");

            DialogService.ShowDialog(ucName, paras,
                callback =>
                {
                    if (callback.Result == ButtonResult.OK)
                    {
                        if (callback.Parameters.ContainsKey("result1"))
                        {
                            string r1 = callback.Parameters.GetValue<string>("result1");
                        }
                        string r2 = callback.Parameters.GetValue<string>("result2");
                    }
                    //if (callback.Result == ButtonResult.No)
                    //{ }
                    //if (callback.Result == ButtonResult.None)
                    //{ }
                });
        }

        /// <summary>
        /// 显示的内容
        /// </summary>
        private UserControl _ShowContent;

        /// <summary>
        /// 显示的内容
        /// </summary>
        public UserControl ShowContent
        {
            get { return _ShowContent; }
            set
            {
                _ShowContent = value;
                RaisePropertyChanged();
            }
        }


    }
}
