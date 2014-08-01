using System.Collections.Generic;
using Supeng.Silverlight.Common.Entities;

namespace HrPortal.Models
{
  public class Requirement : EsuInfoBase
  {
    private AppType app;
    private Category category;
    private List<Comment> comments;
    private string createUserID;
    private string createUserName;
    private int id1;
    private Priority priority;
    private bool risk;
    private Status status;
    private string subject;
    private int workload;

    #region properties

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

    #endregion

    public List<Comment> Comments
    {
      get { return comments; }
      set
      {
        if (Equals(value, comments)) return;
        comments = value;
        NotifyOfPropertyChange(() => Comments);
      }
    }
  }

  public enum AppType
  {
    Hrc,
    Wlc,
    Hrp,
    Hro,
    Portal,
    Hrb
  }

  public enum Category
  {
    Defect,
    Story,
    Enhancement,
    BussinessLogical
  }

  public enum Priority
  {
    High,
    Medium,
    Low
  }

  public enum Status
  {
    Create,
    Accept,
    Processing,
    Done,
    All
  }
}