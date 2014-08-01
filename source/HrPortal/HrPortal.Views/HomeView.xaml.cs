using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
