using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RadzenDb.Models.TpcH
{
  public class Cust{
    public const string Title="顾客表";
    public const string id="顾客号";
    public const string id_err=Common.epre+id;
    public const string name="顾客姓名";
    public const string addr="顾客地址";
    public const string nation="顾客国籍";
    public const string phone="顾客电话";
    public const string acc="顾客余额";
    public const string mkn="市场名称";
    public const string comment="顾客备注";
  }
  [Table("customer")]
  public partial class Customer
  {

    [Key]
    public int c_custkey
    {
      get;
      set;
    }

    public ICollection<Order> Orders { get; set; }
    public int c_nationkey
    {
      get;
      set;
    }
    public Nation Nation { get; set; }
    public string c_name
    {
      get;
      set;
    }
    public string c_address
    {
      get;
      set;
    }
    public string c_phone
    {
      get;
      set;
    }
    public decimal? c_acctbal
    {
      get;
      set;
    }
    public string c_mktsegment
    {
      get;
      set;
    }
    public string c_comment
    {
      get;
      set;
    }
  }
}
