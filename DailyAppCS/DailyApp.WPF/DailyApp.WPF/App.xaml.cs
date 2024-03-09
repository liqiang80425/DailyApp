using DailyApp.WPF.HttpClients;
using DailyApp.WPF.Service;
using DailyApp.WPF.ViewModels;
using DailyApp.WPF.ViewModels.Dialogs;
using DailyApp.WPF.Views;
using DailyApp.WPF.Views.Dailogs;
using DryIoc;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Services.Dialogs;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DailyApp.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        /// <summary>
        /// 设置启动窗口
        /// </summary>
        /// <returns></returns>
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWin>();
        }

        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="containerRegistry"></param>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //登录
            containerRegistry.RegisterDialog<LoginUC, LoginUCViewModel>();

            //请求
            containerRegistry.GetContainer().Register<HttpRestClient>(made: Parameters.Of.Type<string>(serviceKey: "webUrl"));

            //导航页
            containerRegistry.RegisterForNavigation<HomeUC, HomeUCViewModel>();//首页
            containerRegistry.RegisterForNavigation<MemoUC, MemoUCViewModel>();//备忘录
            containerRegistry.RegisterForNavigation<SettingsUC, SettingsUCViewModel>();//设置
            containerRegistry.RegisterForNavigation<WaitUC, WaitUCViewModel>();//待办事项

            //设置左侧导航
            containerRegistry.RegisterForNavigation<PersonalUC, PersonalUCViewModel>();//个性化页面
            containerRegistry.RegisterForNavigation<AboutUsUC>();//关于更多页面
            containerRegistry.RegisterForNavigation<SysSetUC>();//系统设置页面

            //添加待办事项
            containerRegistry.RegisterForNavigation<AddWaitUC, AddWaitUCViewModel>();

            //自定义对话框服务
            containerRegistry.Register<DialogHostService>();

            //编辑待办事项
            containerRegistry.RegisterForNavigation<EditWaitUC, EditWaitUCViewModel>();

            //添加备忘录
            containerRegistry.RegisterForNavigation<AddMemoUC, AddMemoUCViewModel>();

            //编辑备忘录
            containerRegistry.RegisterForNavigation<EditMemoUC, EditMemoUCViewModel>();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        protected override void OnInitialized()
        {
            var dialog = Container.Resolve<IDialogService>();
            dialog.ShowDialog("LoginUC", callback =>
            {
                if (callback.Result != ButtonResult.OK)
                {
                    Environment.Exit(0);
                    return;
                }

                var mainVM = Current.MainWindow.DataContext as MainWinViewModel;//主界面数据上下文
                if (mainVM != null)
                {
                    if (callback.Parameters.ContainsKey("LoginName"))
                    {
                        string name = callback.Parameters.GetValue<string>("LoginName");

                        mainVM.SetDefaultNav(name);
                    }
                }

                base.OnInitialized();
            });

        }
    }
}
