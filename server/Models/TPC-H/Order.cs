using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RadzenDb.Models.TpcH
{
  public class Ord{
    public const string Title="订单表";
    public const string name="订单";
    public const string id="订单号";
    public const string id_err=Common.epre+id;
    public const string cust_id="订单顾客";
    public const string status="订单状态";
    public const string price="订单金额";
    public const string date="订单日期";
    public const string ord_prior="订单优先级";
    public const string clerk="制单员";
    public const string trans_prior="运输优先级";
    public const string comment="订单备注";
    public static string[] DefaultOrderPriority=new string[]{"高","中","低"};
  }
  [Table("orders")]
  public partial class Order
  {
    [Key]
    public int o_orderkey
    {
      get;
      set;
    }

    public ICollection<Lineitem> Lineitems { get; set; }
    public int o_custkey
    {
      get;
      set;
    }
    public Customer Customer { get; set; }
    public string o_orderstatus
    {
      get;
      set;
    }
    public decimal? o_totalprice
    {
      get;
      set;
    }
    public DateTime? o_orderdate
    {
      get;
      set;
    }
    public string o_orderpriority
    {
      get;
      set;
    }
    public string o_clerk
    {
      get;
      set;
    }
    public int? o_shippriority
    {
      get;
      set;
    }
    public string o_comment
    {
      get;
      set;
    }
  }
}
