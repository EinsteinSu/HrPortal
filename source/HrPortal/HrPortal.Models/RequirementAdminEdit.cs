using System.ComponentModel.DataAnnotations;

namespace HrPortal.Models
{
  public class RequirementAdminEdit : Requirement
  {
    private string comment;

    [Display(Name = "危险与否", Order = 0)]
    public new bool Risk
    {
      get { return base.Risk; }
      set { base.Risk = value; }
    }

    [Display(Name = "危险与否", Order = 1)]
    public new Status Status
    {
      get { return base.Status; }
      set { base.Status = value; }
    }

    [Display(Name = "预计时间(小时)", Order = 2)]
    public new int Workload
    {
      get { return base.Workload; }
      set { base.Workload = value; }
    }

    [DataType(DataType.MultilineText)]
    [Display(Name = "评论及意见", Order = 3)]
    public string Comment
    {
      get { return comment; }
      set
      {
        if (value == comment) return;
        comment = value;
        NotifyOfPropertyChange(() => Comment);
      }
    }
  }
}