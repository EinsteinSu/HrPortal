using HrPortal.Models;
using HrPortal.ViewModels.ReadService;
using Supeng.Silverlight.Common.Entities;
using Supeng.Silverlight.Common.Entities.ObserveCollection;
using Supeng.Silverlight.Common.Interfaces;
using Supeng.Silverlight.Common.Strings;
using Supeng.Silverlight.Common.Threads;
using Supeng.Silverlight.Common.Webs;

namespace HrPortal.ViewModels
{
  public class DownloadViewModel : EsuInfoBase, IDataLoad
  {
    private EsuInfoCollection<Links> collection;

    public EsuInfoCollection<Links> Collection
    {
      get { return collection; }
      set
      {
        if (Equals(value, collection)) return;
        collection = value;
        NotifyOfPropertyChange(() => Collection);
      }
    }

    public void Load()
    {
      IReadService service = new WcfClientAddressBase<ReadServiceClient>("../ReadService.svc").GetService();
      HomeViewModel.Instance.Progress.ShowProgress("正在获取下载列表...");
      service.BeginGetDownloads(ThreadHelper.SyncContextCallback(ar => Globals.ProcessAsyncServiceData(() =>
      {
        string result = service.EndGetDownloads(ar);
        Collection = result.Load<EsuInfoCollection<Links>>();
      })), null);
    }
  }
}