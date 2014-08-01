using System;
using HrPortal.Models;
using HrPortal.ViewModels.ReadService;
using Supeng.Silverlight.Common.Strings;
using Supeng.Silverlight.Common.Threads;
using Supeng.Silverlight.Common.Webs;
using Supeng.Silverlight.Controls.ViewModels;

namespace HrPortal.ViewModels
{
  public class LoginViewModel : LoginBase
  {
    private readonly Action<bool> loginDone;

    public LoginViewModel()
    {

    }

    public LoginViewModel(Action<bool> loginDone)
    {
      this.loginDone = loginDone;
      LoadLogin<LoginViewModel>();
    }

    public string Title
    {
      get { return "登录"; }
    }

    protected override void Login()
    {
      if (!ShowCheckLoginError())
        return;
      HomeViewModel.Instance.Progress.ShowProgress("正在登录...");
      IReadService service = new WcfClientAddressBase<ReadServiceClient>("../ReadService.svc").GetService();
      service.BeginLogin(UserName, Password, ThreadHelper.SyncContextCallback(ar =>
      {
        var data = service.EndLogin(ar);
        bool result = !string.IsNullOrEmpty(data);
        if (result)
        {
          SaveLogin();
          HomeViewModel.Instance.CurrentUser = data.Load<User>();
        }
        if (loginDone != null)
          loginDone(result);
      }), null);
    }
  }
}
