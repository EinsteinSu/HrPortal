using Supeng.Silverlight.Common.Entities;

namespace HrPortal.Models
{
  public class Comment : EsuInfoBase
  {
    private string createUserID;
    private string createUserName;

    #region properties

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
  }
}