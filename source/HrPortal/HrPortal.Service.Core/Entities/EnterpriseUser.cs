using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using Supeng.Common.DataOperations;
using Supeng.Common.Entities;
using Supeng.Common.Types;

namespace HrPortal.Service.Core.Entities
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
  }

  public sealed class EnterpriseUserCreator : IDataCreator<EnterpriseUser>
  {
    public EnterpriseUser CreateData(IDataReader reader)
    {
      var data = new EnterpriseUser();
      data.ID = reader["guid"].ToString();
      data.Code = reader["code"].ToString();
      data.Name = reader["username"].ToString();
      data.Gender = reader["sex"].ToString();
      data.BirthDate = reader["birthdate"].ToString().ConvertToDateTime();
      data.IDCard = reader["idcard"].ToString();
      data.Tel = reader["tel"].ToString();
      data.Mobile = reader["mobile"].ToString();
      data.Email = reader["email"].ToString();
      data.QQ = reader["qq"].ToString();
      data.Photo = reader["photo"].ToString().ConvertData<byte[]>();
      return data;
    }
  }

  public sealed class EnterpriseUserSave : IDataSave<EnterpriseUser>
  {
    public IDataParameter[] MappingParameters(EnterpriseUser data)
    {
      var parameters = new IDataParameter[11];
      parameters[0] = new SqlParameter("@guid", data.ID);
      parameters[1] = new SqlParameter("@code", data.Code);
      parameters[2] = new SqlParameter("@username", data.Name);
      parameters[3] = new SqlParameter("@sex", data.Gender);
      parameters[4] = new SqlParameter("@birthdate", data.BirthDate);
      parameters[5] = new SqlParameter("@idcard", data.IDCard);
      parameters[6] = new SqlParameter("@tel", data.Tel);
      parameters[7] = new SqlParameter("@mobile", data.Mobile);
      parameters[8] = new SqlParameter("@email", data.Email);
      parameters[9] = new SqlParameter("@qq", data.QQ);
      parameters[10] = new SqlParameter("@photo", data.Photo);
      return parameters;
    }

    public string InsertSqlScript()
    {
      throw new NotImplementedException();
    }

    public string UpdateSqlScript()
    {
      return "Update hrcommon.dbo.EnterpriseUsers set code = @code,username = @username,sex = @sex,birthdate = @birthdate,idcard = @idcard,tel = @tel,mobile = @mobile,email = @email,qq = @qq,photo = @photo Where GUID = @guid";
    }

    public string DeleteSqlScript()
    {
      throw new NotImplementedException();
    }
  }
}
