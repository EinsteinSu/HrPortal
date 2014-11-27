using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using DevExpress.Xpf.Bars;
using Newtonsoft.Json;
using Supeng.Silverlight.Common.Entities;

namespace HrPortal.Models
{
  public class EnterpriseUser : EsuInfoBase
  {
    private string code;
    private string name;
    private string gender;
    private DateTime birthDate;
    private string idCard;
    private string tel;
    private string mobile;
    private string email;
    private string qq;
    private byte[] photo;

    #region properties
    [ReadOnly(true)]
    [Display(Name = "用户代码")]
    public string Code
    {
      get { return code; }
      set
      {
        if (value == code) return;
        code = value;
        NotifyOfPropertyChange(() => Code);
      }
    }

    [Display(Name = "姓名")]
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

    [Display(Name = "性别")]
    public string Gender
    {
      get { return gender; }
      set
      {
        if (value == gender) return;
        gender = value;
        NotifyOfPropertyChange(() => Gender);
      }
    }

    [Display(Name = "出生日期")]
    public DateTime BirthDate
    {
      get { return birthDate; }
      set
      {
        if (value.Equals(birthDate)) return;
        birthDate = value;
        NotifyOfPropertyChange(() => BirthDate);
      }
    }

    [Display(Name = "身份证号")]
    public string IDCard
    {
      get { return idCard; }
      set
      {
        if (value == idCard) return;
        idCard = value;
        NotifyOfPropertyChange(() => IDCard);
      }
    }

    [Display(Name = "电话号码")]
    public string Tel
    {
      get { return tel; }
      set
      {
        if (value == tel) return;
        tel = value;
        NotifyOfPropertyChange(() => Tel);
      }
    }

    [Display(Name = "手机")]
    public string Mobile
    {
      get { return mobile; }
      set
      {
        if (value == mobile) return;
        mobile = value;
        NotifyOfPropertyChange(() => Mobile);
      }
    }

    [Display(Name = "Email")]
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

    [Display(Name = "QQ号")]
    public string QQ
    {
      get { return qq; }
      set
      {
        if (value == qq) return;
        qq = value;
        NotifyOfPropertyChange(() => QQ);
      }
    }

    [Display(AutoGenerateField = false)]
    public byte[] Photo
    {
      get { return photo; }
      set
      {
        if (Equals(value, photo)) return;
        photo = value;
        NotifyOfPropertyChange(() => Photo);
      }
    }
    #endregion

    [JsonIgnore]
    public IList<string> GenderCollection
    {
      get
      {
        return new List<string> { "男", "女" };
      }
    }
  }
}
