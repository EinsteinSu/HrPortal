using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Core.Commands;
using HrPortal.Models;
using Newtonsoft.Json;
using Supeng.Silverlight.Common.Entities;
using Supeng.Silverlight.Common.Entities.ObserveCollection;
using Supeng.Silverlight.Common.Interfaces;
using Supeng.Silverlight.Common.IOs;
using Supeng.Silverlight.Controls.ViewModels;

namespace HrPortal.ViewModels
{
  public class HomeViewModel : EsuInfoBase
  {
    private Dictionary<string, Type> childViewTypes;
    private readonly EsuProgressViewModel progress;
    private EsuInfoCollection<FunctionModel> functionCollection;

    private HomeViewModel()
    {
      progress = new EsuProgressViewModel();
      var uri = new Uri("Functionalities.xml", UriKind.Relative);
      functionCollection = uri.ReadXmlFromLocalSource<EsuInfoCollection<FunctionModel>>();
      foreach (FunctionModel model in functionCollection)
        model.ClickAction = new DelegateCommand<FunctionModel>(FunctionClick);
      TurnToPage(functionCollection.Count - 1);
    }

    [JsonIgnore]
    public Dictionary<string, Type> ChildViewTypes
    {
      get { return childViewTypes; }
      set
      {
        if (Equals(value, childViewTypes)) return;
        childViewTypes = value;
        NotifyOfPropertyChange(() => ChildViewTypes);
      }
    }

    private static HomeViewModel instance;

    public static HomeViewModel Instance
    {
      get { return instance ?? (instance = new HomeViewModel()); }
    }

    public EsuProgressViewModel Progress
    {
      get { return progress; }
    }

    public string CurrentUrl
    {
      get
      {
        if (functionCollection.CurrentItem == null)
          return string.Empty;
        return functionCollection.CurrentItem.Url;
      }
    }

    public EsuInfoCollection<FunctionModel> FunctionCollection
    {
      get { return functionCollection; }
      set
      {
        if (Equals(value, functionCollection)) return;
        functionCollection = value;
        NotifyOfPropertyChange(() => FunctionCollection);
      }
    }

    public string AppName
    {
      get { return "Test Application"; }
    }

    private User currentUser;
    public User CurrentUser
    {
      get { return currentUser; }
      set
      {
        if (Equals(value, currentUser)) return;
        currentUser = value;
        NotifyOfPropertyChange(() => CurrentUser);
      }
    }

    public string CurrentUserID
    {
      get { return currentUser.ID; }
    }

    private void TurnToPage(int index)
    {
      if (functionCollection.Any())
      {
        FunctionCollection.CurrentItem = FunctionCollection[index];
        NotifyOfPropertyChange(() => CurrentUrl);
      }
    }

    private void TurnToPage(string url)
    {
      var first = functionCollection.FirstOrDefault(f => f.Url.Equals(url, StringComparison.InvariantCultureIgnoreCase));
      if (first != null)
      {
        functionCollection.CurrentItem = first;
        NotifyOfPropertyChange(() => CurrentUrl);
      }
    }

    private FunctionModel originalModel;

    protected virtual void FunctionClick(FunctionModel model)
    {
      originalModel = null;
      if (currentUser == null && model.NeedLogin)
      {
        originalModel = model;
        MessageBox.Show("请先登录！");
        TurnToPage(functionCollection.Count - 1);
        return;
      }
      if (model.Title.Equals("退出系统") &&
          MessageBox.Show("是否退出该系统？", "退出系统", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
        return;
      TurnToPage(model.Url);
    }

    public virtual void FunctionBinding(Page page)
    {
      if (functionCollection.CurrentItem == null)
        return;
      switch (functionCollection.CurrentItem.Title)
      {
        case "登录系统":
          page.DataContext = GetLoginViewModel();
          break;
        case "退出系统":
          functionCollection.CurrentItem.Title = "登录系统";
          CurrentUser = null;
          page.DataContext = GetLoginViewModel();
          break;
        case "下载中心":
          page.DataContext = new DownloadViewModel();
          break;
      }
      if (page.DataContext != null)
      {
        var dataLoad = page.DataContext as IDataLoad;
        if (dataLoad != null)
          dataLoad.Load();
      }
    }

    #region get function view model

    protected virtual LoginViewModel GetLoginViewModel()
    {
      var viewModel = new LoginViewModel(result =>
      {
        if (!result)
        {
          MessageBox.Show("用户名密码错误！");
          return;
        }
        functionCollection.CurrentItem.Title = "退出系统";

        if (originalModel == null)
          TurnToPage(0);
        else
          TurnToPage(originalModel.Url);
        NotifyOfPropertyChange(() => CurrentUrl);
        Instance.Progress.HideProgress();
      });
      return viewModel;
    }

    #endregion
  }
}
