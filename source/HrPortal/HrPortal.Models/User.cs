using Supeng.Silverlight.Common.Entities;

namespace HrPortal.Models
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
}