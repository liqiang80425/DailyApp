using DailyApp.WPF.DTOs;
using DailyApp.WPF.HttpClients;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DailyApp.WPF.ViewModels
{
    /// <summary>
    /// 待办事项视图模型
    /// </summary>
    internal class WaitUCViewModel : BindableBase, INavigationAware
    {
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


        private HttpRestClient HttpClient;
        /// <summary>
        /// 构造函数
        /// </summary>
        public WaitUCViewModel(HttpRestClient _HttpClient)
        {
            ShowAddWaitCmm = new DelegateCommand(ShowAddWait);//显示添加待办命令

            QueryWaitListCmm = new DelegateCommand(QueryWaitList);//查询待办事项命令

            HttpClient = _HttpClient;

            AddWaitCmm = new DelegateCommand(AddWait);

            //删除命令
            DelCmm = new DelegateCommand<WaitInfoDTO>(Del);
        }

        #region 查询待办事项数据

        public string SearchWaitTitle { get; set; }//查询条件

        private int _SearchWaitIndex;

        public int SearchWaitIndex//查询条件
        {
            get { return _SearchWaitIndex; }
            set
            {
                _SearchWaitIndex = value;
                RaisePropertyChanged();
            }
        }

        public DelegateCommand QueryWaitListCmm { get; set; }//查询待办命令

        /// <summary>
        /// 查询待办事项数据
        /// </summary>
        private void QueryWaitList()
        {

            //通过调用API取出来
            int? status = null;
            if (SearchWaitIndex == 1)
            {
                status = 0;
            }
            if (SearchWaitIndex == 2)
            {
                status = 1;
            }

            ApiRequest apiRequest = new ApiRequest();
            apiRequest.Method = RestSharp.Method.GET;
            apiRequest.Route = $"Wait/QueryWait?title={SearchWaitTitle}&status={status}";

            ApiResponse response = HttpClient.Execute(apiRequest);

            if (response.ResultCode == 1)//获取成功
            {
                WaitList = JsonConvert.DeserializeObject<List<WaitInfoDTO>>(response.ResultData.ToString());

                Visibility = (WaitList.Count > 0) ? Visibility.Hidden : Visibility.Visible;
            }
            else
            {
                WaitList = new List<WaitInfoDTO>();
            }
        }
        #endregion


        #region 显示添加待办
        private bool _IsShowAddWait;

        /// <summary>
        /// 是否显示添加待办
        /// </summary>
        public bool IsShowAddWait
        {
            get { return _IsShowAddWait; }
            set
            {
                _IsShowAddWait = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 显示添加待办
        /// </summary>
        private void ShowAddWait()
        {
            IsShowAddWait = true;
        }

        /// <summary>
        /// 接收
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.ContainsKey("SelectedIndex"))
            {
                SearchWaitIndex = navigationContext.Parameters.GetValue<int>("SelectedIndex");
            }
            else
            {
                SearchWaitIndex = 0;
            }

            QueryWaitList();//查询待办事项数据
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public DelegateCommand ShowAddWaitCmm { get; set; }//显示添加待办命令
        #endregion

        #region 是否显示列表
        private Visibility _Visibility;

        public Visibility Visibility
        {
            get { return _Visibility; }
            set
            {
                _Visibility = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 添加待办事项
        public WaitInfoDTO WaitInfoDTO { get; set; } = new WaitInfoDTO();

        /// <summary>
        /// 添加待办事项命令 
        /// </summary>
        public DelegateCommand AddWaitCmm { get; set; }

        /// <summary>
        /// 添加待办事项
        /// </summary>
        private void AddWait()
        {
            if (string.IsNullOrEmpty(WaitInfoDTO.Title) || string.IsNullOrEmpty(WaitInfoDTO.Content))
            {
                MessageBox.Show("待办事项信息不全");
                return;
            }

            //调用api实现添加待办事项
            ApiRequest apiRequest = new ApiRequest();
            apiRequest.Method = RestSharp.Method.POST;
            apiRequest.Parameters = WaitInfoDTO;
            apiRequest.Route = "Wait/AddWait";
            ApiResponse response = HttpClient.Execute(apiRequest);
            if (response.ResultCode == 1)//添加成功
            {
                MessageBox.Show(response.Msg);

                QueryWaitList();//刷新列表

                IsShowAddWait = false;//隐藏
            }
            else
            {
                MessageBox.Show(response.Msg);
            }
        }
        #endregion

        #region 删除待办事项
        public DelegateCommand<WaitInfoDTO> DelCmm { get; set; }//删除命令

        /// <summary>
        /// 删除待办事项
        /// </summary>
        /// <param name="waitInfoDTO"></param>
        public void Del(WaitInfoDTO waitInfoDTO)
        {
            var selResult = MessageBox.Show("确定删除吗?", "温馨提示", MessageBoxButton.OKCancel);
            if (selResult == MessageBoxResult.OK)
            {
                ApiRequest apiRequest = new ApiRequest();
                apiRequest.Method = RestSharp.Method.DELETE;
                apiRequest.Route = $"Wait/DelWait?waitId={waitInfoDTO.WaitId}";

                ApiResponse response = HttpClient.Execute(apiRequest);

                if (response.ResultCode == 1)//删除成功
                {
                    MessageBox.Show(response.Msg);
                    QueryWaitList();// 刷新数据
                }
                else
                {
                    MessageBox.Show(response.Msg);
                }
            }
        }
        #endregion
    }
}
