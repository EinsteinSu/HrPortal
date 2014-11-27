using System.Xml.Serialization;
using DevExpress.Xpf.Core.Commands;
using Newtonsoft.Json;
using Supeng.Silverlight.Common.Entities;

namespace HrPortal.Models
{
  public class FunctionModel : EsuInfoBase
  {
    private string title;

    public string Title
    {
      get { return title; }
      set
      {
        if (value == title) return;
        title = value;
        NotifyOfPropertyChange(() => Title);
      }
    }

    public string ImageName { get; set; }

    public string Url { get; set; }

    public bool NeedLogin { get; set; }

    [XmlIgnore]
    [JsonIgnore]
    public DelegateCommand<FunctionModel> ClickAction { get; set; }
  }
}