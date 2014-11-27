using System;
using System.Threading.Tasks;
using HrPortal.Service.Core.Entities;
using Supeng.Common.DataOperations;
using Supeng.Data;
using Supeng.Data.Sql;

namespace HrPortal.Service.Core.Storages
{
  public interface IUserStorage
  {
    string GetUserEmail(string userID);

    EnterpriseUser GetEnterpriseUser(string userID);

    string UpdateEnterpriseUser(EnterpriseUser user);
  }

  public class UserStorage : SqlDataStorage, IUserStorage
  {
    public UserStorage()
      : base(Globals.ConnectionString, TaskScheduler.Current)
    {
    }

    public string GetUserEmail(string userID)
    {
      var creator = new SqlScriptCreator("hrcommon.dbo.enterpriseusers", new[] { string.Format("guid = '{0}'", userID) },
        "guid, userName, Email");
      var user = ReadSingleRecord(creator.GetSqlScript(), new UserCreator());
      return user.Email;
    }

    public EnterpriseUser GetEnterpriseUser(string userID)
    {
      var creator = new SqlScriptCreator("hrcommon.dbo.enterpriseusers", new[] { string.Format("guid = '{0}'", userID) });
      return ReadSingleRecord(creator.GetSqlScript(), new EnterpriseUserCreator());
    }

    public string UpdateEnterpriseUser(EnterpriseUser user)
    {
      try
      {
        user.Update(this, new EnterpriseUserSave());
        return string.Empty;
      }
      catch (Exception ex)
      {
        return ex.Message;
      }
    }
  }
}
