using System;
using System.Windows;
using DevExpress.Xpf.Bars;
using HrPortal.Models;
using HrPortal.ViewModels.EidtWindows;
using HrPortal.ViewModels.WriteService;
using Supeng.Silverlight.Common;
using Supeng.Silverlight.Common.Strings;
using Supeng.Silverlight.Common.Threads;
using Supeng.Silverlight.Common.Webs;
using Supeng.Silverlight.Controls.Extensions;
using Supeng.Silverlight.Controls.ViewModels;

namespace HrPortal.ViewModels
{
  public sealed class RequirementCrud : CrudCommandBase<Requirement>
  {
    private readonly DelegateCommand processCommand;
    private readonly Action reload;
    private readonly Requirement requirement;

    public RequirementCrud(Requirement requirement, Action reload)
    {
      this.requirement = requirement;
      this.reload = reload;
      processCommand = new DelegateCommand(Process, () => true);
    }

    #region cruds

    protected override void Add()
    {
      var r = new RequirementClientEdit
      {
        CreateUserID = HomeViewModel.Instance.CurrentUserID,
        CreateUserName = HomeViewModel.Instance.CurrentUser.Name
      };
      var viewModel = new RequirementEditViewModel(r);
      DialogWindowHelper.ShowDialogWindow(viewModel, result =>
      {
        if (result)
        {
          IWriteService service = new WcfClientAddressBase<WriteServiceClient>("../WriteService.svc").GetService();
          service.BeginInsertRequirement(viewModel.Data.ToString(),
            ThreadHelper.SyncContextCallback(ar => Globals.ProcessAsyncServiceData(() =>
            {
              string message = service.EndInsertRequirement(ar);
              if (string.IsNullOrEmpty(message))
              {
                if (reload != null)
                  reload();
              }
              else
                MessageBox.Show(message);
            })), null);
        }
      });
    }

    protected override void Modify()
    {
      if (requirement == null)
        return;
      if (!requirement.CreateUserID.Equals(HomeViewModel.Instance.CurrentUserID) && !HomeViewModel.Instance.CurrentUser.Name.Equals("系统管理员"))
      {
        MessageBox.Show("该问题不是由你创建，你不能修改！");
        return;
      }
      var r = requirement.ToString().Load<RequirementClientEdit>();
      var viewModel = new RequirementEditViewModel(r);
      DialogWindowHelper.ShowDialogWindow(viewModel, result =>
      {
        if (result)
        {
          IWriteService service = new WcfClientAddressBase<WriteServiceClient>("../WriteService.svc").GetService();
          service.BeginUpdateRequirement(viewModel.Data.ToString(), "",
            ThreadHelper.SyncContextCallback(ar => Globals.ProcessAsyncServiceData(() =>
            {
              string message = service.EndUpdateRequirement(ar);
              if (string.IsNullOrEmpty(message))
              {
                if (reload != null)
                  reload();
              }
              else
                MessageBox.Show(message);
            })), null);
        }
      });
    }

    protected override void Delete()
    {
      if (requirement == null)
        return;
      if (!requirement.CreateUserID.Equals(HomeViewModel.Instance.CurrentUserID))
      {
        MessageBox.Show("该问题不是由你创建，你不能删除！");
        return;
      }
      if (Utils.DeleteMessage("问题"))
      {
        IWriteService service = new WcfClientAddressBase<WriteServiceClient>("../WriteService.svc").GetService();
        service.BeginDeleteRequirement(requirement.ToString(),
          ThreadHelper.SyncContextCallback(ar => Globals.ProcessAsyncServiceData(() =>
          {
            string message = service.EndDeleteRequirement(ar);
            if (string.IsNullOrEmpty(message))
            {
              if (reload != null)
                reload();
            }
            else
              MessageBox.Show(message);
          })), null);
      }
    }

    #endregion

    public DelegateCommand ProcessCommand
    {
      get { return processCommand; }
    }

    private void Process()
    {
      if (requirement == null)
        return;
      if (!HomeViewModel.Instance.CurrentUser.Name.Equals("系统管理员"))
      {
        MessageBox.Show("您无此权限！");
        return;
      }
      var r = requirement.ToString().Load<RequirementAdminEdit>();
      var viewModel = new RequirementProcessEditViewModel(r);
      DialogWindowHelper.ShowDialogWindow(viewModel, result =>
      {
        if (result)
        {
          IWriteService service = new WcfClientAddressBase<WriteServiceClient>("../WriteService.svc").GetService();
          service.BeginUpdateRequirement(viewModel.Data.ToString(), viewModel.Data.Comment,
            ThreadHelper.SyncContextCallback(ar => Globals.ProcessAsyncServiceData(() =>
            {
              string message = service.EndUpdateRequirement(ar);
              if (string.IsNullOrEmpty(message))
              {
                if (reload != null)
                  reload();
              }
              else
                MessageBox.Show(message);
            })), null);
        }
      });
    }
  }
}