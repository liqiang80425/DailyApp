using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.WPF.ViewModes
{
    public class MainWindowViewModel : BindableBase, INavigationAware
    {
        #region
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {

        }
        #endregion

        private readonly HttpRestClient client;
        public DelegateCommand TestCmm { get; set; }
        public MainWindowViewModel(HttpRestClient _client)
        {
            client = _client;
            TestCmm = new DelegateCommand(TestFunc);
        }

        public void TestFunc()
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.GET;
            request.Route = $"api/Test/TestFunc";
            var r = client.ExecuteAsync(request).Result;
        }


    }
}
