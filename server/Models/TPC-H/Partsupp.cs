using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RadzenDb.Models.TpcH
{
  public class PS{
    public const string Title="零件供应表";
    public const string pid="零件编号";
    public const string sid="供应编号";
    public const string id_err=$"{Common.epre}{pid}和{sid}";
    public const string quantity="剩余数量";
    public const string price="供应价格";
    public const string comment="零件备注";
  }
  [Table("partsupp")]
  public partial class Partsupp
  {
    [Key]
    public int ps_partkey
    {
      get;
      set;
    }

    public ICollection<Lineitem> Lineitems { get; set; }
    public Part Part { get; set; }
    [Key]
    public int ps_suppkey
    {
      get;
      set;
    }
    public Supplier Supplier { get; set; }
    public int? ps_availqty
    {
      get;
      set;
    }
    public decimal? ps_supplycost
    {
      get;
      set;
    }
    public string ps_comment
    {
      get;
      set;
    }
  }
}
