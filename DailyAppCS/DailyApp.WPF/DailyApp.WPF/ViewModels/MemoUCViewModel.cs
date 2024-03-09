using DailyApp.WPF.DTOs;
using DailyApp.WPF.HttpClients;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
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
    /// 备忘录视图模型
    /// </summary>
    internal class MemoUCViewModel : BindableBase
    {

        #region 备忘录事项数据
        private List<MemoInfoDTO> _MemoList;

        /// <summary>
        /// 客户端
        /// </summary>
        private readonly HttpRestClient HttpClient;

        /// <summary>
        /// 查询命令
        /// </summary>
        public DelegateCommand QueryMemoListCmm { get; set; }

        /// <summary>
        /// 备忘录事项数据
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

        /// <summary>
        /// 构造函数
        /// </summary>
        public MemoUCViewModel(HttpRestClient _HttpClient)
        {
            HttpClient = _HttpClient;

            QueryMemoList();//创建备忘录事项模拟数据

            ShowAddMemoCmm = new DelegateCommand(ShowAddMemo);//显示添加备忘录命令

            //查询命令
            QueryMemoListCmm = new DelegateCommand(QueryMemoList);

            //添加备忘录命令
            AddMemoCmm = new DelegateCommand(AddMemo);

            DelCmm = new DelegateCommand<MemoInfoDTO>(Del);//删除命令
        }

        /// <summary>
        /// 查询备忘录事项数据
        /// </summary>
        private void QueryMemoList()
        {
            ApiRequest apiRequest = new ApiRequest();
            apiRequest.Method = RestSharp.Method.GET;
            apiRequest.Route = $"Memo/QueryMemo?title={SearchTitle}";

            ApiResponse response = HttpClient.Execute(apiRequest);

            if (response.ResultCode == 1)//获取成功
            {
                MemoList = JsonConvert.DeserializeObject<List<MemoInfoDTO>>(response.ResultData.ToString());

                Visibility = (MemoList.Count > 0) ? Visibility.Hidden : Visibility.Visible;
            }
            else
            {
                MemoList = new List<MemoInfoDTO>();
            }
        }

        #region 显示添加备忘录
        private bool _IsShowAddMemo;

        /// <summary>
        /// 是否显示添加备忘录
        /// </summary>
        public bool IsShowAddMemo
        {
            get { return _IsShowAddMemo; }
            set
            {
                _IsShowAddMemo = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 显示添加备忘录
        /// </summary>
        private void ShowAddMemo()
        {
            IsShowAddMemo = true;
        }

        public DelegateCommand ShowAddMemoCmm { get; set; }//显示添加备忘录命令
        #endregion

        #region 备忘录查询显示

        /// <summary>
        /// 查询标题(模糊查询)
        /// </summary>
        public string SearchTitle { get; set; }
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

        #region 添加备忘录
        public MemoInfoDTO MemoInfoDTO { get; set; } = new MemoInfoDTO();

        /// <summary>
        /// 添加备忘录命令 
        /// </summary>
        public DelegateCommand AddMemoCmm { get; set; }

        /// <summary>
        /// 添加备忘录
        /// </summary>
        private void AddMemo()
        {
            if (string.IsNullOrEmpty(MemoInfoDTO.Title) || string.IsNullOrEmpty(MemoInfoDTO.Content))
            {
                MessageBox.Show("备忘录信息不全");
                return;
            }
            //调用api实现添加备忘录

            ApiRequest apiRequest = new ApiRequest();
            apiRequest.Method = RestSharp.Method.POST;
            apiRequest.Parameters = MemoInfoDTO;
            apiRequest.Route = "Memo/AddMomo";
            ApiResponse response = HttpClient.Execute(apiRequest);
            if (response.ResultCode == 1)//添加成功
            {
                MessageBox.Show(response.Msg);

                QueryMemoList();//刷新列表

                IsShowAddMemo = false;//隐藏
            }
            else
            {
                MessageBox.Show(response.Msg);
            }
        }
        #endregion

        #region 删除备忘录
        public DelegateCommand<MemoInfoDTO> DelCmm { get; set; }//删除命令

        /// <summary>
        /// 删除备忘录
        /// </summary>
        /// <param name="memoInfoDTO"></param>
        public void Del(MemoInfoDTO memoInfoDTO)
        {
            var selResult = MessageBox.Show("确定删除吗?","温馨提示", MessageBoxButton.OKCancel);
            if (selResult == MessageBoxResult.OK)
            {
                ApiRequest apiRequest = new ApiRequest();
                apiRequest.Method = RestSharp.Method.DELETE;
                apiRequest.Route = $"Memo/DelMemo?memoId={memoInfoDTO.MemoId}";

                ApiResponse response = HttpClient.Execute(apiRequest);

                if (response.ResultCode == 1)//删除成功
                {
                    MessageBox.Show(response.Msg);
                    QueryMemoList();// 刷新数据
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
