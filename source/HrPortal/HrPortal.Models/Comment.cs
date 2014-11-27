using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core.Commands;
using Newtonsoft.Json;
using Supeng.Silverlight.Common.Entities;

namespace HrPortal.Models
{
  public class Comment : EsuInfoBase
  {
    private string createUserID;
    private string createUserName;
    private int id1;
    private DelegateCommand<Comment> updateCommand;
    private DelegateCommand<Comment> deleteCommand;
    private DateTime updateTime;

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

    #region properties

    [DataType(DataType.MultilineText)]
    [Display(Name = "评论及意见")]
    new public string Description
    {
      get { return base.Description; }
      set { base.Description = value; }
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

    #endregion

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

    [JsonIgnore]
    [Display(AutoGenerateField = false)]
    public DelegateCommand<Comment> UpdateCommand
    {
      get { return updateCommand; }
      set
      {
        if (Equals(value, updateCommand)) return;
        updateCommand = value;
        NotifyOfPropertyChange(() => UpdateCommand);
      }
    }

    [JsonIgnore]
    [Display(AutoGenerateField = false)]
    public DelegateCommand<Comment> DeleteCommand
    {
      get { return deleteCommand; }
      set
      {
        if (Equals(value, deleteCommand)) return;
        deleteCommand = value;
        NotifyOfPropertyChange(() => DeleteCommand);
      }
    }
  }
}