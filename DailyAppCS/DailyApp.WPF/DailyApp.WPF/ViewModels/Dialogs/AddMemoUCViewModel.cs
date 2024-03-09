using DailyApp.WPF.DTOs;
using DailyApp.WPF.Service;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DailyApp.WPF.ViewModels.Dialogs
{
   /// <summary>
   /// 添加备忘录视图模型
   /// </summary>
    internal class AddMemoUCViewModel : IDialogHostAware
    {
        /// <summary>
        /// 确定命令
        /// </summary>
        public DelegateCommand SaveCommand { get; set; }

        /// <summary>
        /// 取消命令
        /// </summary>
        public DelegateCommand CancelCommand { get; set; }

        /// <summary>
        /// 打开过程执行
        /// </summary>
        /// <param name="parameters"></param>

        public void OnDialogOpening(IDialogParameters parameters)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public AddMemoUCViewModel()
        {
            SaveCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(Cancel);
        }
        /// <summary>
        /// 确定方法
        /// </summary>
        private void Save()
        {
            if (string.IsNullOrEmpty(MemoInfoDTO.Title) || string.IsNullOrEmpty(MemoInfoDTO.Content))
            {
                MessageBox.Show("备忘录信息不全");
                return;
            }

            if (DialogHost.IsDialogOpen(DailogHostName))
            {
                DialogParameters paras = new DialogParameters();
                paras.Add("AddMemoInfo", MemoInfoDTO);
                DialogHost.Close(DailogHostName, new DialogResult(ButtonResult.OK, paras));
            }
        }

        /// <summary>
        /// 取消方法
        /// </summary>
        private void Cancel()
        {
            if (DialogHost.IsDialogOpen(DailogHostName))
            {
                DialogHost.Close(DailogHostName, new DialogResult(ButtonResult.No));
            }
        }

        /// <summary>
        /// 备忘录信息
        /// </summary>
        public MemoInfoDTO MemoInfoDTO { get; set; } = new MemoInfoDTO();

        /// <summary>
        /// 对话框主机唯一标识
        /// </summary>
        private const string DailogHostName = "RootDialog";
    }
}
