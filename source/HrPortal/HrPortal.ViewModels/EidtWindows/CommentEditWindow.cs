using HrPortal.Models;
using Supeng.Silverlight.Controls.ViewModels.DialogWindows;

namespace HrPortal.ViewModels.EidtWindows
{
  public class CommentEditWindow : EntityEditViewModelBase<Comment>
  {
    public CommentEditWindow()
      : base(600)
    {

    }

    public CommentEditWindow(Comment comment)
      : base(comment)
    {

    }

    public override string Title
    {
      get { return "评论及意见"; }
    }

    protected override string TemplateName
    {
      get { return "CommentEditWindow"; }
    }

    protected override string DataCheck()
    {
      if (string.IsNullOrEmpty(Data.Description))
        return "评论不能为空！";
      return string.Empty;
    }
  }
}
