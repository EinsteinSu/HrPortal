using System;
using HrPortal.Models;
using Supeng.Silverlight.Controls.ViewModels.DialogWindows;

namespace HrPortal.ViewModels.EidtWindows
{
  public class RequirementEditViewModel : EntityEditViewModelBase<RequirementClientEdit>
  {
    public RequirementEditViewModel()
      : base(500)
    {
    }

    public RequirementEditViewModel(RequirementClientEdit data)
      : base(data)
    {
    }

    public override string Title
    {
      get { return "问题编辑"; }
    }

    protected override string TemplateName
    {
      get { return "RequirementEditWindow"; }
    }

    protected override string DataCheck()
    {
      string errMsg = string.Empty;
      if (string.IsNullOrEmpty(Data.Subject))
        errMsg += "主题不能为空" + Environment.NewLine;
      if (string.IsNullOrEmpty(Data.Description))
        errMsg += "问题描述不能为空" + Environment.NewLine;
      return errMsg;
    }
  }
}