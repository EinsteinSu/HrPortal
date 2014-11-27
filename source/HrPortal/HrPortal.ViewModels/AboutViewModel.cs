using Supeng.Silverlight.Controls.ViewModels;

namespace HrPortal.ViewModels
{
  public class AboutViewModel : AboutViewModelBase
  {
    public override string ProductName
    {
      get { return HomeViewModel.Instance.AppName; }
    }

    public override string CustomerCompany
    {
      get { return "恒锐消防建设技术咨询股份有限公司"; }
    }

    public override string Version
    {
      get { return "1.0.0"; }
    }
  }
}