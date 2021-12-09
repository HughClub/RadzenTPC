using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RadzenDb.Models.TpcH
{
  public class LI{
    public const string Title="订单详细";
    public const string oid="订单号";
    public const string lid="订单明细号";
    public const string id_err=Common.epre+lid;
    public const string pid="零件编号";
    public const string sid="供应商号";
    public const string quantity="订单数量";
    public const string price="总金额";
    public const string discount="折扣";
    public const string tax="赋税";
    public const string retflag="是否退货";
    public const string status="明细状态";
    public const string trans_date="运输日期";
    public const string commit_date="交付日期";
    public const string receipt_date="收货日期";
    public const string trans_depart="运输单位";
    public const string trans_mode="运输方式";
    public const string comment="备注";
  }
  [Table("lineitem")]
  public partial class Lineitem
  {
    [Key]
    public int l_orderkey
    {
      get;
      set;
    }
    public Order Order { get; set; }
    [Key]
    public int l_linenumber
    {
      get;
      set;
    }
    public int l_partkey
    {
      get;
      set;
    }
    public Partsupp Partsupp { get; set; }
    public int l_suppkey
    {
      get;
      set;
    }
    public decimal? l_quantity
    {
      get;
      set;
    }
    public decimal? l_extendedprice
    {
      get;
      set;
    }
    public decimal? l_discount
    {
      get;
      set;
    }
    public decimal? l_tax
    {
      get;
      set;
    }
    public string l_returnflag
    {
      get;
      set;
    }
    public string l_linestatus
    {
      get;
      set;
    }
    public DateTime? l_shipdate
    {
      get;
      set;
    }
    public DateTime? l_commitdate
    {
      get;
      set;
    }
    public DateTime? l_receiptdate
    {
      get;
      set;
    }
    public string l_shipinstruct
    {
      get;
      set;
    }
    public string l_shipmode
    {
      get;
      set;
    }
    public string l_comment
    {
      get;
      set;
    }
  }
}
