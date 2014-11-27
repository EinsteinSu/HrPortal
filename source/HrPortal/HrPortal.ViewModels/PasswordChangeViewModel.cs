using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using HrPortal.ViewModels.WriteService;
using Supeng.Silverlight.Common.Threads;
using Supeng.Silverlight.Common.Webs;
using Supeng.Silverlight.Controls.ViewModels;

namespace HrPortal.ViewModels
{
  public class PasswordChangeViewModel : PasswordChangeBase
  {
    [Display(Name = "当前用户")]
    [ReadOnly(true)]
    public string CurrentUserName
    {
      get { return HomeViewModel.Instance.CurrentUser.Name; }
    }

    protected override void UpdatePassword()
    {
      IWriteService service = new WcfClientAddressBase<WriteServiceClient>("../WriteService.svc").GetService();
      service.BeginPasswordChange(HomeViewModel.Instance.CurrentUserID, NewPassword,
        ThreadHelper.SyncContextCallback(
          ar => Globals.ProcessAsyncServiceData(() => MessageBox.Show(service.EndPasswordChange(ar)))), null);
    }
  }
}