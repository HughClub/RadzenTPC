using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RadzenDb.Models.TpcH
{
  public class Supp{
    public const string Title="供应商表";
    public const string id="供应商编号";
    public const string id_err=Common.epre+id;
    public const string name="供应商";
    public const string addr="供应商地址";
    public const string nat="所属国家";
    public const string phone="联系电话";
    public const string acc="账户余额";
    public const string comment="零件备注";
  }
  [Table("supplier")]
  public partial class Supplier
  {
    [Key]
    public int s_suppkey
    {
      get;
      set;
    }

    public ICollection<Partsupp> Partsupps { get; set; }
    public int s_nationkey
    {
      get;
      set;
    }
    public Nation Nation { get; set; }
    public string s_name
    {
      get;
      set;
    }
    public string s_address
    {
      get;
      set;
    }
    public string s_phone
    {
      get;
      set;
    }
    public decimal? s_acctbal
    {
      get;
      set;
    }
    public string s_comment
    {
      get;
      set;
    }
  }
}
