using System.Collections.Generic;
using System.Windows;
using DevExpress.Xpf.Core.Commands;
using HrPortal.Models;
using HrPortal.ViewModels.EidtWindows;
using HrPortal.ViewModels.ReadService;
using HrPortal.ViewModels.WriteService;
using Supeng.Silverlight.Common;
using Supeng.Silverlight.Common.Entities;
using Supeng.Silverlight.Common.Entities.ObserveCollection;
using Supeng.Silverlight.Common.Interfaces;
using Supeng.Silverlight.Common.Strings;
using Supeng.Silverlight.Common.Threads;
using Supeng.Silverlight.Common.Webs;
using Supeng.Silverlight.Controls.Extensions;
using Supeng.Silverlight.Controls.ViewModels;

namespace HrPortal.ViewModels
{
  public class RequirementViewModel : EsuInfoBase, IDataLoad
  {
    private CrudCommandBase<Requirement> command;
    private EsuInfoCollection<Requirement> requirements;
    protected Dictionary<int, EsuInfoCollection<Comment>> Cache;

    public RequirementViewModel()
    {
      Cache = new Dictionary<int, EsuInfoCollection<Comment>>();
      command = new RequirementCrud(null, Load);
      NotifyOfPropertyChange(() => Command);
      NotifyOfPropertyChange(() => Visibility);
    }

    public Visibility Visibility
    {
      get
      {
        return HomeViewModel.Instance.CurrentUser.Name.Equals("系统管理员") ? Visibility.Visible : Visibility.Collapsed;
      }
    }

    public EsuInfoCollection<Requirement> Requirements
    {
      get { return requirements; }
      set
      {
        if (Equals(value, requirements)) return;
        requirements = value;
        NotifyOfPropertyChange(() => Requirements);
      }
    }

    public CrudCommandBase<Requirement> Command
    {
      get { return command; }
    }

    public void Load()
    {
      IReadService service = new WcfClientAddressBase<ReadServiceClient>("../ReadService.svc").GetService();
      HomeViewModel.Instance.Progress.ShowProgress("正在获取问题记录...");
      service.BeginGetRequirements(0, ThreadHelper.SyncContextCallback(ar => Globals.ProcessAsyncServiceData(() =>
      {
        Cache = new Dictionary<int, EsuInfoCollection<Comment>>();
        string result = service.EndGetRequirements(ar);
        Requirements = result.Load<EsuInfoCollection<Requirement>>();
        Requirements.CurrentItemChanged = CurrentItemChanged;
        Requirements.AcceptChanges();
      })), null);
    }

    private void CurrentItemChanged(Requirement requirement)
    {
      if (requirement != null)
      {
        requirement.AddCommentCommand = new DelegateCommand<Requirement>(AddComment);
        requirement.UpdateCommentCommand = new DelegateCommand<Requirement>(UpdateComment);
        requirement.DeleteCommentCommand = new DelegateCommand<Requirement>(DeleteComment);
        if (Cache.ContainsKey(requirement.Id))
          requirement.Comments = Cache[requirement.Id];
        else
          LoadComment();
      }
      command = new RequirementCrud(requirement, Load);
      NotifyOfPropertyChange(() => Command);
      NotifyOfPropertyChange(() => Visibility);
    }

    #region comment crud
    private void LoadComment()
    {
      if (requirements.CurrentItem == null)
        return;
      if (Cache.ContainsKey(requirements.CurrentItem.Id))
        Cache.Remove(requirements.CurrentItem.Id);
      HomeViewModel.Instance.Progress.ShowProgress("正在加载评论...");
      IReadService service = new WcfClientAddressBase<ReadServiceClient>("../ReadService.svc").GetService();
      service.BeginGetComments(requirements.CurrentItem.Id, ThreadHelper.SyncContextCallback(ar => Globals.ProcessAsyncServiceData(() =>
      {
        var result = service.EndGetComments(ar);
        var collection = result.Load<EsuInfoCollection<Comment>>();
        requirements.CurrentItem.Comments = collection;
        Cache.Add(requirements.CurrentItem.Id, collection);
      })), null);
    }

    private void AddComment(Requirement requirement)
    {
      if (requirement == null)
        return;
      var c = new Comment
      {
        CreateUserID = HomeViewModel.Instance.CurrentUserID,
        CreateUserName = HomeViewModel.Instance.CurrentUser.Name
      };
      var viewModel = new CommentEditWindow(c);
      DialogWindowHelper.ShowDialogWindow(viewModel, result =>
      {
        if (result)
        {
          #region save to service
          IWriteService service = new WcfClientAddressBase<WriteServiceClient>("../WriteService.svc").GetService();
          service.BeginInsertComment(viewModel.Data.ToString(), requirement.Id,
            ThreadHelper.SyncContextCallback(ar => Globals.ProcessAsyncServiceData(() =>
            {
              string message = service.EndInsertComment(ar);
              if (string.IsNullOrEmpty(message))
                LoadComment();
              else
                MessageBox.Show(message);
            })), null);
          #endregion
        }
      });
    }

    private void UpdateComment(Requirement requirement)
    {
      var comment = requirement.Comments.CurrentItem;
      if (comment == null || requirements.CurrentItem == null)
        return;
      if (!comment.CreateUserID.Equals(HomeViewModel.Instance.CurrentUserID))
      {
        MessageBox.Show("不是你的评论不能修改及删除！");
        return;
      }
      var c = comment.ToString().Load<Comment>();
      var viewModel = new CommentEditWindow(c);
      DialogWindowHelper.ShowDialogWindow(viewModel, result =>
      {
        if (result)
        {
          IWriteService service = new WcfClientAddressBase<WriteServiceClient>("../WriteService.svc").GetService();
          service.BeginUpdateComment(viewModel.Data.ToString(), requirements.CurrentItem.Id,
            ThreadHelper.SyncContextCallback(ar => Globals.ProcessAsyncServiceData(() =>
            {
              string message = service.EndUpdateComment(ar);
              if (string.IsNullOrEmpty(message))
                LoadComment();
              else
                MessageBox.Show(message);
            })), null);
        }
      });
    }

    private void DeleteComment(Requirement requirement)
    {
      var comment = requirement.Comments.CurrentItem;
      if (comment == null || requirements.CurrentItem == null)
        return;
      if (!comment.CreateUserID.Equals(HomeViewModel.Instance.CurrentUserID))
      {
        MessageBox.Show("不是你的评论不能修改及删除！");
        return;
      }
      if (Utils.DeleteMessage("评论及意见"))
      {
        IWriteService service = new WcfClientAddressBase<WriteServiceClient>("../WriteService.svc").GetService();
        service.BeginDeleteComment(comment.ToString(), requirements.CurrentItem.Id,
          ThreadHelper.SyncContextCallback(ar => Globals.ProcessAsyncServiceData(() =>
          {
            string message = service.EndDeleteComment(ar);
            if (string.IsNullOrEmpty(message))
              LoadComment();
            else
              MessageBox.Show(message);
          })), null);
      }
    }
    #endregion

  }
}