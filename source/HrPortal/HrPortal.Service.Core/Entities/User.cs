using System.Data;
using Supeng.Common.DataOperations;
using Supeng.Common.Entities;

namespace HrPortal.Service.Core.Entities
{
  public class User : EsuInfoBase
  {
    private string name;
    private string email;

    public string Name
    {
      get { return name; }
      set
      {
        if (value == name) return;
        name = value;
        NotifyOfPropertyChange(() => Name);
      }
    }

    public string Email
    {
      get { return email; }
      set
      {
        if (value == email) return;
        email = value;
        NotifyOfPropertyChange(() => Email);
      }
    }
  }

  public sealed class UserCreator : IDataCreator<User>
  {
    public User CreateData(IDataReader reader)
    {
      var data = new User();
      data.ID = reader["guid"].ToString();
      data.Name = reader["userName"].ToString();
      data.Email = reader["email"].ToString();
      return data;
    }
  }
}