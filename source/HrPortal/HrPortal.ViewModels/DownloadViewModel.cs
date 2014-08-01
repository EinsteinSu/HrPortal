using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using DevExpress.Xpf.Core.Commands;
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
      service.BeginGetDownloads(ThreadHelper.SyncContextCallback(ar =>
      {
        try
        {
          var result = service.EndGetDownloads(ar);
          Collection = result.Load<EsuInfoCollection<Links>>();
          foreach (var link in collection)
          {
            link.ClickCommand = new DelegateCommand<Links>(links =>
            {
              MessageBox.Show(links.Name);
              //open download uri from ftp link
            });
          }
        }
        catch (Exception ex)
        {
          MessageBox.Show("获取列表错误:" + ex.Message);
        }
        finally
        {
          HomeViewModel.Instance.Progress.HideProgress();
        }

      }), null);
    }
  }
}
