using DailyApp.WPF.DTOs;
using DailyApp.WPF.HttpClients;
using DailyApp.WPF.Models;
using DailyApp.WPF.Service;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DailyApp.WPF.ViewModels
{
    internal class HomeUCViewModel : BindableBase, INavigationAware
    {
        #region 统计面板数据
        private List<StatPanelInfo> _StatPanelList;

        /// <summary>
        /// 统计面板数据
        /// </summary>
        public List<StatPanelInfo> StatPanelList
        {
            get { return _StatPanelList; }
            set
            {
                _StatPanelList = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 待办事项数据
        private List<WaitInfoDTO> _WaitList;

        /// <summary>
        /// 待办事项数据
        /// </summary>
        public List<WaitInfoDTO> WaitList
        {
            get { return _WaitList; }
            set
            {
                _WaitList = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 统计面板导航
        public DelegateCommand<StatPanelInfo> NavigateCmm { get; set; }

        private readonly IRegionManager RegionManager;//区域管理

        /// <summary>
        /// 统计面板导航
        /// </summary>
        /// <param name="statPanelInfo"></param>
        private void Navigate(StatPanelInfo statPanelInfo)
        {
            if (!string.IsNullOrEmpty(statPanelInfo.ViewName))
            {
                if (statPanelInfo.ItemName == "已完成")
                {
                    NavigationParameters paras = new NavigationParameters();
                    paras.Add("SelectedIndex", 2);
                    RegionManager.Regions["MainViewRegion"].RequestNavigate(statPanelInfo.ViewName, paras);
                }
                else
                {
                    RegionManager.Regions["MainViewRegion"].RequestNavigate(statPanelInfo.ViewName);
                }
            }
        }
        #endregion

        #region 备忘录数据
        private List<MemoInfoDTO> _MemoList;

        /// <summary>
        /// 备忘录数据
        /// </summary>
        public List<MemoInfoDTO> MemoList
        {
            get { return _MemoList; }
            set
            {
                _MemoList = value;
                RaisePropertyChanged();
            }
        }
        #endregion


        private readonly HttpRestClient HttpClient;//请求api的客户端

        //对话服务(自定义)
        private readonly DialogHostService DialogHostService;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HomeUCViewModel(HttpRestClient _HttpClient, DialogHostService _DialogHostService, IRegionManager _RegionManager)
        {
            CreateStatPaneList();//创建统计面板数据

            HttpClient = _HttpClient;//请求api的客户端

            GetWaitingList();//创建待办事项模拟数据

            //打开添加待办事项命令
            ShowAddWaitDialogCmm = new DelegateCommand(ShowAddWaitDialog);

            //对话服务
            DialogHostService = _DialogHostService;

            //修改待办状态命令
            ChangeWaitStatusCmm = new DelegateCommand<WaitInfoDTO>(ChangeWaitStatus);

            //打开编辑待办事项命令
            ShowEditWaitDialogCmm = new DelegateCommand<WaitInfoDTO>(ShowEditWaitDialog);

            //统计面板导航命令
            NavigateCmm = new DelegateCommand<StatPanelInfo>(Navigate);

            //区域管理
            RegionManager = _RegionManager;

            CallStatMemo();//统计备忘录总数

            ShowAddMemoDialogCmm = new DelegateCommand(ShowAddMemoDialog);

            GetMemoList();//获取备忘录数据

            ShowEditMemoDialogCmm = new DelegateCommand<MemoInfoDTO>(ShowEditMemoDialog);
        }

        /// <summary>
        /// 创建统计面板数据
        /// </summary>
        private void CreateStatPaneList()
        {
            StatPanelList = new List<StatPanelInfo>();

            StatPanelList.Add(new StatPanelInfo() { Icon = "ClockFast", ItemName = "汇总", BackColor = "#FF0CA0FF", ViewName = "WaitUC", Result = "9" });
            StatPanelList.Add(new StatPanelInfo() { Icon = "ClockCheckOutline", ItemName = "已完成", BackColor = "#FF1ECA3A", ViewName = "WaitUC", Result = "9" });
            StatPanelList.Add(new StatPanelInfo() { Icon = "ChartLineVariant", ItemName = "完成比例", BackColor = "#FF02C6DC", Result = "90%" });
            StatPanelList.Add(new StatPanelInfo() { Icon = "PlaylistStar", ItemName = "备忘录", BackColor = "#FFFFA000", ViewName = "MemoUC", Result = "20" });
        }

        /// <summary>
        /// 获取待办状态的待办事项数据
        /// </summary>
        private void GetWaitingList()
        {
            ApiRequest apiRequest = new ApiRequest();
            apiRequest.Method = RestSharp.Method.GET;
            apiRequest.Route = "Wait/GetWaiting";

            ApiResponse response = HttpClient.Execute(apiRequest);

            if (response.ResultCode == 1)//获取成功
            {
                WaitList = JsonConvert.DeserializeObject<List<WaitInfoDTO>>(response.ResultData.ToString());
            }
            else
            {
                WaitList = new List<WaitInfoDTO>();
            }
        }

        /// <summary>
        /// 创建备忘录数据
        /// </summary>
        private void GetMemoList()
        {
            ApiRequest apiRequest = new ApiRequest();
            apiRequest.Method = RestSharp.Method.GET;
            apiRequest.Route = "Memo/QueryMemo";

            ApiResponse response = HttpClient.Execute(apiRequest);

            if (response.ResultCode == 1)//获取成功
            {
                MemoList = JsonConvert.DeserializeObject<List<MemoInfoDTO>>(response.ResultData.ToString());
            }
            else
            {
                MemoList = new List<MemoInfoDTO>();
            }
        }

        #region 显示登录用户信息

        private string _LoginInfo;

        /// <summary>
        /// 登录用户信息
        /// </summary>
        public string LoginInfo
        {
            get { return _LoginInfo; }
            set
            {
                _LoginInfo = value;
                RaisePropertyChanged();
            }
        }


        /// <summary>
        /// 接收导航并处理
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.ContainsKey("LoginName"))
            {
                DateTime now = DateTime.Now;//当前时间
                string[] week = new string[7] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
                string loginName = navigationContext.Parameters.GetValue<string>("LoginName");

                LoginInfo = $"您好！{loginName}。今天是{now.ToString("yyyy-MM-dd")} {week[(int)now.DayOfWeek]}";

                CallStatWait();//统计待办事项
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
        #endregion

        #region 待办事项统计
        private StatWaitDTO StatWaitDTO { get; set; } = new StatWaitDTO();

        /// <summary>
        /// 调用api获取统计待办实现数据
        /// </summary>
        private void CallStatWait()
        {
            ApiRequest apiRequest = new ApiRequest();
            apiRequest.Method = RestSharp.Method.GET;
            apiRequest.Route = "Wait/StatWait";

            ApiResponse response = HttpClient.Execute(apiRequest);

            if (response.ResultCode == 1)
            {
                StatWaitDTO = JsonConvert.DeserializeObject<StatWaitDTO>(response.ResultData.ToString());

                RefreshWaitStat();
            }
        }

        /// <summary>
        /// 更新待办统计数据
        /// </summary>
        private void RefreshWaitStat()
        {
            StatPanelList[0].Result = StatWaitDTO.TotalCount.ToString();
            StatPanelList[1].Result = StatWaitDTO.FinishCount.ToString();
            StatPanelList[2].Result = StatWaitDTO.FinishPercent;
        }
        #endregion

        #region 添加待办事项处理
        public DelegateCommand ShowAddWaitDialogCmm { get; set; }

        /// <summary>
        /// 打开添加待办事项对话框
        /// </summary>
        private async void ShowAddWaitDialog()//async 异步
        {
            var result = await DialogHostService.ShowDialog("AddWaitUC", null);//await等待

            if (result.Result == ButtonResult.OK)
            {
                //接收数据
                if (result.Parameters.ContainsKey("AddWaitInfo"))
                {
                    var addModel = result.Parameters.GetValue<WaitInfoDTO>("AddWaitInfo");
                    //调用api实现添加待办事项

                    ApiRequest apiRequest = new ApiRequest();
                    apiRequest.Method = RestSharp.Method.POST;
                    apiRequest.Parameters = addModel;
                    apiRequest.Route = "Wait/AddWait";
                    ApiResponse response = HttpClient.Execute(apiRequest);
                    if (response.ResultCode == 1)//添加成功
                    {
                        MessageBox.Show(response.Msg);
                        //刷新统计数据
                        CallStatWait();
                    }
                    else
                    {
                        MessageBox.Show(response.Msg);
                    }
                }
            }
        }
        #endregion

        #region 修改待办事项的状态
        public DelegateCommand<WaitInfoDTO> ChangeWaitStatusCmm { get; set; }

        /// <summary>
        /// 修改待办事项的状态
        /// </summary>
        private void ChangeWaitStatus(WaitInfoDTO waitInfoDTO)
        {
            ApiRequest apiRequest = new ApiRequest();
            apiRequest.Method = RestSharp.Method.PUT;
            apiRequest.Parameters = waitInfoDTO;
            apiRequest.Route = "Wait/UpdateStatus";
            ApiResponse response = HttpClient.Execute(apiRequest);
            if (response.ResultCode == 1)//修改成功
            {
                GetWaitingList();//刷新列表
                CallStatWait();//刷统计数据
            }
            else
            {
                MessageBox.Show(response.Msg);
            }
        }
        #endregion

        #region 编辑待办事项

        public DelegateCommand<WaitInfoDTO> ShowEditWaitDialogCmm { get; set; }

        /// <summary>
        /// 编辑待办事项对话框
        /// </summary>
        private async void ShowEditWaitDialog(WaitInfoDTO waitInfoDTO)//async 异步
        {
            DialogParameters paras = new DialogParameters();
            paras.Add("OldWaitInfo", waitInfoDTO);

            var result = await DialogHostService.ShowDialog("EditWaitUC", paras);//await等待

            if (result.Result == ButtonResult.OK)
            {
                //接收数据
                if (result.Parameters.ContainsKey("NewWaitInfo"))
                {
                    var newModel = result.Parameters.GetValue<WaitInfoDTO>("NewWaitInfo");
                    //调用api实现编辑待办事项

                    ApiRequest apiRequest = new ApiRequest();
                    apiRequest.Method = RestSharp.Method.PUT;
                    apiRequest.Parameters = newModel;
                    apiRequest.Route = "Wait/EditWait";
                    ApiResponse response = HttpClient.Execute(apiRequest);
                    if (response.ResultCode == 1)//编辑成功
                    {
                        MessageBox.Show(response.Msg);

                        //刷新统计数据
                        CallStatWait();

                        GetWaitingList();//刷新列表
                    }
                    else
                    {
                        MessageBox.Show(response.Msg);
                    }
                }
            }
        }
        #endregion


        #region 备忘录
        /// <summary>
        /// 备忘录数据统计
        /// </summary>
        private void CallStatMemo()
        {
            ApiRequest apiRequest = new ApiRequest();
            apiRequest.Method = RestSharp.Method.GET;
            apiRequest.Route = "Memo/StatMomo";

            ApiResponse response = HttpClient.Execute(apiRequest);

            if (response.ResultCode == 1)
            {
                StatPanelList[3].Result = response.ResultData.ToString();
            }
        }

        //显示添加备忘录视图命令
        public DelegateCommand ShowAddMemoDialogCmm { get; set; }

        /// <summary>
        /// 显示添加备忘录视图
        /// </summary>
        private async void ShowAddMemoDialog()
        {
            var result = await DialogHostService.ShowDialog("AddMemoUC", null);//await等待

            if (result.Result == ButtonResult.OK)
            {
                //接收数据
                if (result.Parameters.ContainsKey("AddMemoInfo"))
                {
                    var addModel = result.Parameters.GetValue<MemoInfoDTO>("AddMemoInfo");

                    //调用api实现添加备忘录

                    ApiRequest apiRequest = new ApiRequest();
                    apiRequest.Method = RestSharp.Method.POST;
                    apiRequest.Parameters = addModel;
                    apiRequest.Route = "Memo/AddMomo";
                    ApiResponse response = HttpClient.Execute(apiRequest);
                    if (response.ResultCode == 1)//添加成功
                    {
                        MessageBox.Show(response.Msg);
                        //刷新备忘录统计数据
                        CallStatMemo();

                        //刷新列表
                        GetMemoList();//获取备忘录数据
                    }
                    else
                    {
                        MessageBox.Show(response.Msg);
                    }
                }
            }
        }

        /// <summary>
        /// 显示备忘录编辑界面
        /// </summary>
        public DelegateCommand<MemoInfoDTO> ShowEditMemoDialogCmm { get; set; }

        private async void ShowEditMemoDialog(MemoInfoDTO memoInfoDTO)
        {
            DialogParameters paras = new DialogParameters();
            paras.Add("OldMemoInfo", memoInfoDTO);

            var result = await DialogHostService.ShowDialog("EditMemoUC", paras);//await等待

            if (result.Result == ButtonResult.OK)
            {
                //接收数据
                if (result.Parameters.ContainsKey("NewMemoInfo"))
                {
                    var newModel = result.Parameters.GetValue<MemoInfoDTO>("NewMemoInfo");

                    //调用api实现编辑备忘录

                    ApiRequest apiRequest = new ApiRequest();
                    apiRequest.Method = RestSharp.Method.PUT;
                    apiRequest.Parameters = newModel;
                    apiRequest.Route = "Memo/EditMemo";
                    ApiResponse response = HttpClient.Execute(apiRequest);
                    if (response.ResultCode == 1)//编辑成功
                    {
                        MessageBox.Show(response.Msg);

                        GetMemoList();//刷新列表
                    }
                    else
                    {
                        MessageBox.Show(response.Msg);
                    }
                }
            }
        }


        #endregion
    }
}
