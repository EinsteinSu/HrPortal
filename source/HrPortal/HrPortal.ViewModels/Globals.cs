using System;
using System.Windows;

namespace HrPortal.ViewModels
{
  public class Globals
  {
    public static void ProcessAsyncServiceData(Action action)
    {
      try
      {
        if (action != null)
          action();
      }
      catch (Exception ex)
      {
        MessageBox.Show("获取数据错误：" + ex.Message);
      }
      finally
      {
        HomeViewModel.Instance.Progress.HideProgress();
      }
    }
  }
}