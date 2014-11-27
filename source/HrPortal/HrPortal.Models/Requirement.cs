using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Media;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core.Commands;
using Newtonsoft.Json;
using Supeng.Silverlight.Common.Entities;
using Supeng.Silverlight.Common.Entities.ObserveCollection;

namespace HrPortal.Models
{
  public class Requirement : EsuInfoBase
  {
    private AppType app;
    private Category category;
    private EsuInfoCollection<Comment> comments;
    private string createUserID;
    private string createUserName;
    private int id1;
    private string number;
    private Priority priority;
    private bool risk;
    private Status status;
    private string subject;
    private int workload;
    private DelegateCommand<Requirement> addCommentCommand;
    private DelegateCommand<Requirement> updateCommentCommand;
    private DelegateCommand<Requirement> deleteCommentCommand;
    private DateTime updateTime;

    [Display(AutoGenerateField = false)]
    public string Number
    {
      get { return number; }
      set
      {
        if (value == number) return;
        number = value;
        NotifyOfPropertyChange(() => Number);
      }
    }

    [Display(AutoGenerateField = false)]
    public EsuInfoCollection<Comment> Comments
    {
      get { return comments; }
      set
      {
        if (Equals(value, comments)) return;
        comments = value;
        NotifyOfPropertyChange(() => Comments);
      }
    }

    #region properties

    [Display(AutoGenerateField = false)]
    public int Id
    {
      get { return id1; }
      set
      {
        if (value == id1) return;
        id1 = value;
        NotifyOfPropertyChange(() => Id);
      }
    }

    [Display(AutoGenerateField = false)]
    public AppType App
    {
      get { return app; }
      set
      {
        if (value == app) return;
        app = value;
        NotifyOfPropertyChange(() => App);
      }
    }

    [Display(AutoGenerateField = false)]
    public Category Category
    {
      get { return category; }
      set
      {
        if (value == category) return;
        category = value;
        NotifyOfPropertyChange(() => Category);
      }
    }

    [Display(AutoGenerateField = false)]
    public string Subject
    {
      get { return subject; }
      set
      {
        if (value == subject) return;
        subject = value;
        NotifyOfPropertyChange(() => Subject);
      }
    }

    [Display(AutoGenerateField = false)]
    public Priority Priority
    {
      get { return priority; }
      set
      {
        if (value == priority) return;
        priority = value;
        NotifyOfPropertyChange(() => Priority);
      }
    }

    [Display(AutoGenerateField = false)]
    public bool Risk
    {
      get { return risk; }
      set
      {
        if (value.Equals(risk)) return;
        risk = value;
        NotifyOfPropertyChange(() => Risk);
      }
    }

    [Display(AutoGenerateField = false)]
    public Status Status
    {
      get { return status; }
      set
      {
        if (value == status) return;
        status = value;
        NotifyOfPropertyChange(() => Status);
      }
    }

    [Display(AutoGenerateField = false)]
    public int Workload
    {
      get { return workload; }
      set
      {
        if (value == workload) return;
        workload = value;
        NotifyOfPropertyChange(() => Workload);
      }
    }

    [Display(AutoGenerateField = false)]
    public string CreateUserID
    {
      get { return createUserID; }
      set
      {
        if (value == createUserID) return;
        createUserID = value;
        NotifyOfPropertyChange(() => CreateUserID);
      }
    }

    [ReadOnly(true)]
    [Display(Name = "创建人")]
    public string CreateUserName
    {
      get { return createUserName; }
      set
      {
        if (value == createUserName) return;
        createUserName = value;
        NotifyOfPropertyChange(() => CreateUserName);
      }
    }

    [Display(AutoGenerateField = false)]
    public DateTime UpdateTime
    {
      get { return updateTime; }
      set
      {
        if (value.Equals(updateTime)) return;
        updateTime = value;
        NotifyOfPropertyChange(() => UpdateTime);
      }
    }

    #endregion

    #region commands
    [JsonIgnore]
    [Display(AutoGenerateField = false)]
    public DelegateCommand<Requirement> AddCommentCommand
    {
      get { return addCommentCommand; }
      set
      {
        if (Equals(value, addCommentCommand)) return;
        addCommentCommand = value;
        NotifyOfPropertyChange(() => AddCommentCommand);
      }
    }

    [JsonIgnore]
    [Display(AutoGenerateField = false)]
    public DelegateCommand<Requirement> UpdateCommentCommand
    {
      get { return updateCommentCommand; }
      set
      {
        if (Equals(value, updateCommentCommand)) return;
        updateCommentCommand = value;
        NotifyOfPropertyChange(() => UpdateCommentCommand);
      }
    }

    [JsonIgnore]
    [Display(AutoGenerateField = false)]
    public DelegateCommand<Requirement> DeleteCommentCommand
    {
      get { return deleteCommentCommand; }
      set
      {
        if (Equals(value, deleteCommentCommand)) return;
        deleteCommentCommand = value;
        NotifyOfPropertyChange(() => DeleteCommentCommand);
      }
    }
    #endregion

    [JsonIgnore]
    [Display(AutoGenerateField = false)]
    public SolidColorBrush Background
    {
      get
      {
        if (status == Status.新建)
          return new SolidColorBrush(Color.FromArgb(255, 192, 255, 255));
        if (status == Status.接受)
          return new SolidColorBrush(Color.FromArgb(255, 255, 255, 192));
        if (status == Status.处理中)
          return new SolidColorBrush(Color.FromArgb(255, 255, 224, 192));
        if (status == Status.完成)
          return new SolidColorBrush(Color.FromArgb(255, 192, 255, 192));
        return new SolidColorBrush(Colors.Transparent);
      }
    }
  }

  public enum AppType
  {
    Hrb,
    Hrc,
    Wlc,
    Hrp,
    Hro,
    Portal,
  }

  public enum Category
  {
    系统缺陷,
    功能调整,
    功能增强,
    业务逻辑变更,
    数据修改
  }

  public enum Priority
  {
    低,
    中,
    高,
    紧急,
  }

  public enum Status
  {
    新建,
    接受,
    处理中,
    完成
  }
}