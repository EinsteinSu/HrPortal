using System.Data;
using Supeng.Common.DataOperations;
using Supeng.Common.Entities;

namespace HrPortal.Service.Core.Entities
{
  public class User : EsuInfoBase
  {
    private string name;

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
  }

  public sealed class UserCreator : IDataCreator<User>
  {
    public User CreateData(IDataReader reader)
    {
      var data = new User();
      data.ID = reader["guid"].ToString();
      data.Name = reader["userName"].ToString();
      return data;
    }
  }
}
