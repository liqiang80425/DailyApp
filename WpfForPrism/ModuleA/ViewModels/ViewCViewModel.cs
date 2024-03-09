using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleA.ViewModels
{
    internal class ViewCViewModel : IDialogAware
    {
        public string Title { get; set; } = "这是对话框标题";
        public string P1 { get; set; }

        public event Action<IDialogResult> RequestClose;

        /// <summary>
        /// 确定命令
        /// </summary>
        public DelegateCommand ConfirmCmm { get; set; }

        /// <summary>
        /// 取消命令
        /// </summary>
        public DelegateCommand ConcelCmm { get; set; }

        public ViewCViewModel()
        {
            ConfirmCmm = new DelegateCommand(Confirm);
            ConcelCmm=new DelegateCommand(Concel);
        }

        /// <summary>
        /// 确定
        /// </summary>
        private void Confirm()
        {
            if (RequestClose != null)
            {
                DialogParameters paras = new DialogParameters();
                paras.Add("result1","返回值1:张三");
                paras.Add("result2", "返回值2:李四");
                RequestClose(new DialogResult(ButtonResult.OK, paras));
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        private void Concel()
        {
            if (RequestClose != null)
            {
                RequestClose(new DialogResult(ButtonResult.No));
            }
        }

        /// <summary>
        /// 是否能够关闭对话框
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool CanCloseDialog()
        {
            return true;
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public void OnDialogClosed()
        {
            if (RequestClose != null)
            {
                RequestClose(new DialogResult(ButtonResult.No));
            }
        }

        /// <summary>
        /// 打开对话框
        /// </summary>
        /// <param name="parameters"></param>
        public void OnDialogOpened(IDialogParameters parameters)
        {
            Title = parameters.GetValue<string>("Title");
            P1 = parameters.GetValue<string>("para1");
            string p2 = parameters.GetValue<string>("para2");
        }
    }
}
