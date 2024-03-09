using DailyApp.WPF.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyApp.WPF.ViewModels
{
    internal class SettingsUCViewModel : BindableBase
    {
		#region 左侧菜单
	private List<LeftMenuInfo> _LeftMenuList;

        /// <summary>
        /// 左侧菜单
        /// </summary>
        public List<LeftMenuInfo> LeftMenuList
        {
			get { return _LeftMenuList; }
			set { _LeftMenuList = value; }
		}

        /// <summary>
        /// 创建菜单数据
        /// </summary>
        void CreateMenuList()
        {
            LeftMenuList=new List<LeftMenuInfo>();

            LeftMenuList.Add(new LeftMenuInfo() { Icon = "Palette", MenuName = "个性化", ViewName = "PersonalUC" });
            LeftMenuList.Add(new LeftMenuInfo() { Icon = "Cog", MenuName = "系统设置" , ViewName="SysSetUC"});
            LeftMenuList.Add(new LeftMenuInfo() { Icon = "Information", MenuName = "关于更多", ViewName = "AboutUsUC" });
        }
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public SettingsUCViewModel(IRegionManager _RegionManager)
        {
            CreateMenuList();
            RegionManager=_RegionManager;

            //导航命令
            NavigateCmm = new DelegateCommand<LeftMenuInfo>(Navigate);
        }

        #region 区域 导航实现导航功能
        private readonly IRegionManager RegionManager;

        public DelegateCommand<LeftMenuInfo> NavigateCmm { get; set; }//导航命令

        /// <summary>
        /// 导航
        /// </summary>
        /// <param name="menu">菜单信息</param>
        private void Navigate(LeftMenuInfo menu)
        {
            if (menu == null || string.IsNullOrEmpty(menu.ViewName))
            {
                return;
            }

            //导航 区域
            RegionManager.Regions["SettingRegion"].RequestNavigate(menu.ViewName);
        }
        #endregion
    }
}
