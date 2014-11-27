using System;
using System.Windows.Controls;
using System.Windows.Navigation;
using DevExpress.Xpf.Core;

namespace HrPortal.Views
{
  public partial class HomeView
  {
    public HomeView()
    {
      InitializeComponent();

      ThemeManager.ApplicationThemeName = "Office2010Black";
    }

    public Action<Page> BindingAction { get; set; }

    private void Frame_OnNavigated(object sender, NavigationEventArgs e)
    {
      var page = e.Content as Page;
      if (page != null && BindingAction != null)
      {
        BindingAction(page);
      }
    }
  }
}