using HrPortal.Models;
using Supeng.Silverlight.Controls.ViewModels.DialogWindows;

namespace HrPortal.ViewModels.EidtWindows
{
  public class RequirementProcessEditViewModel : EntityEditViewModelBase<RequirementAdminEdit>
  {
    public RequirementProcessEditViewModel(RequirementAdminEdit data)
    {
      Data = data;
    }

    public override string Title
    {
      get { return "问题处理"; }
    }

    protected override string TemplateName
    {
      get { return "RequirementAdminEditWindow"; }
    }

    protected override string DataCheck()
    {
      return string.Empty;
    }
  }
}