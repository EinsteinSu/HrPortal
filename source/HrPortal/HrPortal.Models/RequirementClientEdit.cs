using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HrPortal.Models
{
  public class RequirementClientEdit : Requirement
  {
    #region properties

    [Display(Name = "系统类型", Order = 0)]
    public new AppType App
    {
      get { return base.App; }
      set { base.App = value; }
    }

    [Display(Name = "问题类型", Order = 1)]
    public new Category Category
    {
      get { return base.Category; }
      set { base.Category = value; }
    }

    [Display(Name = "主题", Order = 2)]
    public new string Subject
    {
      get { return base.Subject; }
      set { base.Subject = value; }
    }

    [DataType(DataType.MultilineText)]
    [Display(Name = "问题描述", Order = 3)]
    public new string Description
    {
      get { return base.Description; }
      set { base.Description = value; }
    }

    [Display(Name = "优先级", Order = 4)]
    public new Priority Priority
    {
      get { return base.Priority; }
      set { base.Priority = value; }
    }

    [ReadOnly(true)]
    [Display(Name = "创建人")]
    public new string CreateUserName
    {
      get { return base.CreateUserName; }
      set { base.CreateUserName = value; }
    }

    #endregion
  }
}