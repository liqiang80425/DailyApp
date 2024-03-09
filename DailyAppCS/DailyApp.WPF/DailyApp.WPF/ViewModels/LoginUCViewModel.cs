using DailyApp.WPF.DTOs;
using DailyApp.WPF.HttpClients;
using DailyApp.WPF.MsgEvents;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DailyApp.WPF.ViewModels
{
    /// <summary>
    /// 登录视图模型
    /// </summary>
    internal class LoginUCViewModel : BindableBase, IDialogAware
    {
        public string Title { get; set; } = "我的日常";

        public event Action<IDialogResult> RequestClose;

        private readonly HttpRestClient HttpClient;

        private readonly IEventAggregator Aggregator;

        /// <summary>
        /// 登录命令
        /// </summary>
        public DelegateCommand LoginCmm { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public LoginUCViewModel(HttpRestClient _HttpClient, IEventAggregator _Aggregator)
        {
            //登录命令
            LoginCmm = new DelegateCommand(Login);

            //显示注册内容命令
            ShowRegInfoCmm = new DelegateCommand(ShowRegInfo);

            //显示登录内容命令
            ShowLoginInfoCmm = new DelegateCommand(ShowLoginInfo);

            //登录命令
            RegCmm = new DelegateCommand(Reg);

            //实例化注册信息
            AccountInfoDTO = new AccountInfoDTO();

            //请求client
            HttpClient = _HttpClient;

            //发布订阅
            Aggregator = _Aggregator;
        }

        /// <summary>
        /// 登录方法
        /// </summary>
        private void Login()
        {
            //数据基本验证
            if (string.IsNullOrEmpty(Account) || string.IsNullOrEmpty(Pwd))
            {
                //发布消息
                Aggregator.GetEvent<MsgEvent>().Publish("登录信息不全");
                return;
            }

            //调用api
            ApiRequest apiRequest = new ApiRequest();
            apiRequest.Method = RestSharp.Method.GET;

            Pwd = Md5Hepler.GetMd5(Pwd);
            apiRequest.Route = $"Account/Login?account={Account}&pwd={Pwd}";
            ApiResponse reponse = HttpClient.Execute(apiRequest);
          
            if (reponse.ResultCode == 1)//登录成功
            {
                if (RequestClose != null)
                {
                    //将json格式反序列化成对象
                    AccountInfoDTO accountInfoDTO = JsonConvert.DeserializeObject<AccountInfoDTO>(reponse.ResultData.ToString());

                    DialogParameters paras = new DialogParameters();
                    paras.Add("LoginName", accountInfoDTO.Name);
                    RequestClose(new DialogResult(ButtonResult.OK, paras));
                }
            }
            else
            {
                Aggregator.GetEvent<MsgEvent>().Publish(reponse.Msg);
            }
        }

        #region 注册
        public DelegateCommand RegCmm { get; set; }

        /// <summary>
        /// 注册
        /// </summary>
        private void Reg()
        {
            //数据基本验证
            if (string.IsNullOrEmpty(AccountInfoDTO.Name) || string.IsNullOrEmpty(AccountInfoDTO.Account) || string.IsNullOrEmpty(AccountInfoDTO.Pwd) || string.IsNullOrEmpty(AccountInfoDTO.ConfirmPwd))
            {
                //发布消息
                Aggregator.GetEvent<MsgEvent>().Publish("注册信息不全");
                //MessageBox.Show("注册信息不全");
                return;
            }

            if (AccountInfoDTO.Pwd != AccountInfoDTO.ConfirmPwd)
            {
                //发布消息
                MessageBox.Show("两次密码输入不一致");
                Aggregator.GetEvent<MsgEvent>().Publish("两次密码输入不一致");
                return;
            }

            //调用API
            ApiRequest apiRequest = new ApiRequest();
            apiRequest.Method = RestSharp.Method.POST;
            apiRequest.Route = "Account/Reg";

            //对密码md5处理
            AccountInfoDTO.Pwd = Md5Hepler.GetMd5(AccountInfoDTO.Pwd);
            AccountInfoDTO.ConfirmPwd = Md5Hepler.GetMd5(AccountInfoDTO.ConfirmPwd);

            apiRequest.Parameters = AccountInfoDTO;

            ApiResponse response = HttpClient.Execute(apiRequest);//请求api
            if (response.ResultCode == 1)
            {
                //MessageBox.Show(response.Msg);
                Aggregator.GetEvent<MsgEvent>().Publish(response.Msg);
                SelectedIndex = 0;//注册成功，切换到登录
            }
            else
            {
                Aggregator.GetEvent<MsgEvent>().Publish(response.Msg);
            }
        }

        /// <summary>
        /// 注册信息
        /// </summary>
        private AccountInfoDTO _AccountInfoDTO;

        /// <summary>
        /// 注册信息
        /// </summary>

        public AccountInfoDTO AccountInfoDTO
        {
            get { return _AccountInfoDTO; }
            set
            {
                _AccountInfoDTO = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        /// <summary>
        /// 是否可以关闭对话框
        /// </summary>
        /// <returns></returns>
        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {

        }

        #region 显示内容
        /// <summary>
        /// 显示内容的索引
        /// </summary>
        private int _SelectedIndex;

        /// <summary>
        /// 显示内容的索引
        /// </summary>
        public int SelectedIndex
        {
            get { return _SelectedIndex; }
            set
            {
                _SelectedIndex = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 显示注册信息命令
        /// </summary>
        public DelegateCommand ShowRegInfoCmm { get; set; }

        /// <summary>
        /// 显示登录信息命令
        /// </summary>
        public DelegateCommand ShowLoginInfoCmm { get; set; }

        /// <summary>
        /// 显示注册信息
        /// </summary>
        private void ShowRegInfo()
        {
            SelectedIndex = 1;
        }

        /// <summary>
        /// 显示登录信息
        /// </summary>
        private void ShowLoginInfo()
        {
            SelectedIndex = 0;
        }
        #endregion

        #region 登录信息

        /// <summary>
        /// 账号
        /// </summary>
        private string _Account;

        /// <summary>
        /// 账号
        /// </summary>
        public string Account
        {
            get { return _Account; }
            set
            {
                _Account = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 密码
        /// </summary>
        private string _Pwd;

        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd
        {
            get { return _Pwd; }
            set
            {
                _Pwd = value;
                RaisePropertyChanged();
            }
        }
        #endregion


    }
}
