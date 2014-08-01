using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using HrPortal.Service.Core.Entities;
using Supeng.Common.Strings;
using Supeng.Data.Sql;

namespace HrPortal.Service.Core.Storages
{
  public interface ISecurityStorage
  {
    User Login(string userName, string password);
  }

  public class SecurityStorage : SqlDataStorage, ISecurityStorage
  {
    public SecurityStorage()
      : base(Globals.ConnectionString, TaskScheduler.Current)
    {
    }

    public User Login(string userName, string password)
    {
      const string sql = "select guid, username from hrcommon.dbo.enterpriseusers where username = @username and password = @password";
      var encryptPassword = new StringSecurity().EncryptString(password);
      var parameters = new IDataParameter[2];
      parameters[0] = new SqlParameter("@username", userName);
      parameters[1] = new SqlParameter("@password", encryptPassword);
      return ReadSingleRecord(sql, new UserCreator(), parameters);
    }
  }
}
