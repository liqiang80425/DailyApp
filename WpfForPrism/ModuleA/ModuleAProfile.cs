using ModuleA.ViewModels;
using ModuleA.Views;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleA
{
    public class ModuleAProfile : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
           
        }

        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="containerRegistry"></param>
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //注入导航
            containerRegistry.RegisterForNavigation<ViewA, ViewAViewModel>();

            //注入对话框服务
            containerRegistry.RegisterDialog<ViewC, ViewCViewModel>();
        }
    }
}
