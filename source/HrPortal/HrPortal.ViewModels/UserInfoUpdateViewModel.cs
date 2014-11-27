using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Bars;
using HrPortal.Models;
using HrPortal.ViewModels.ReadService;
using HrPortal.ViewModels.WriteService;
using Supeng.Silverlight.Common.Entities;
using Supeng.Silverlight.Common.Interfaces;
using Supeng.Silverlight.Common.Strings;
using Supeng.Silverlight.Common.Threads;
using Supeng.Silverlight.Common.Webs;

namespace HrPortal.ViewModels
{
  public class UserInfoUpdateViewModel : EsuInfoBase, IDataLoad
  {
    private EnterpriseUser user;
    private readonly DelegateCommand uploadPhotoCommand;
    private readonly DelegateCommand saveCommand;

    public UserInfoUpdateViewModel()
    {
      uploadPhotoCommand = new DelegateCommand(UpdatePhoto, () => true);
      saveCommand = new DelegateCommand(Save, () => true);
    }

    public EnterpriseUser User
    {
      get { return user; }
      set
      {
        if (Equals(value, user)) return;
        user = value;
        NotifyOfPropertyChange(() => User);
      }
    }

    public DelegateCommand UploadPhotoCommand
    {
      get { return uploadPhotoCommand; }
    }

    public DelegateCommand SaveCommand
    {
      get { return saveCommand; }
    }

    public void Load()
    {
      IReadService service = new WcfClientAddressBase<ReadServiceClient>("../ReadService.svc").GetService();
      HomeViewModel.Instance.Progress.ShowProgress("正在获取用户信息...");
      service.BeginGetEnterpriseUserJson(HomeViewModel.Instance.CurrentUserID,
        ThreadHelper.SyncContextCallback(ar =>
        {
          var result = service.EndGetEnterpriseUserJson(ar);
          User = result.Load<EnterpriseUser>();
          HomeViewModel.Instance.Progress.HideProgress();
        }), null);
    }

    private void UpdatePhoto()
    {
      var of = new OpenFileDialog();
      var showDialog = of.ShowDialog();
      if (showDialog != null && showDialog.Value)
      {
        string fileName = of.File.Name;
      }
    }

    private void Save()
    {
      IWriteService service = new WcfClientAddressBase<WriteServiceClient>("../WriteService.svc").GetService();
      HomeViewModel.Instance.Progress.ShowProgress("正在保存用户信息...");
      service.BeginUpdateEnterpriseUser(user.ToString(),
       ThreadHelper.SyncContextCallback(ar =>
       {
         var result = service.EndUpdateEnterpriseUser(ar);
         if (!string.IsNullOrEmpty(result))
           MessageBox.Show("用户信息保存失败：" + result);
         else
         {
           MessageBox.Show("用户信息保存成功！");
         }
         HomeViewModel.Instance.Progress.HideProgress();
       }), null);
    }
  }
}
