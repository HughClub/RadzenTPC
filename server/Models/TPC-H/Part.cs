using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RadzenDb.Models.TpcH
{
  public class P{
    public const string Title="零件表";
    public const string id="零件编号";
    public const string id_err=Common.epre+id;
    public const string name="零件名";
    public const string quality="零件质量";
    public const string brand="零件品牌";
    public const string type="零件型号";
    public const string size="零件尺寸";
    public const string container="零件的容器";
    public const string price="零件售价";
    public const string comment="零件备注";
  }
  [Table("part")]
  public partial class Part
  {
    [Key]
    public int p_partkey
    {
      get;
      set;
    }

    public ICollection<Partsupp> Partsupps { get; set; }
    public string p_name
    {
      get;
      set;
    }
    public string p_mfgr
    {
      get;
      set;
    }
    public string p_brand
    {
      get;
      set;
    }
    public string p_type
    {
      get;
      set;
    }
    public int? p_size
    {
      get;
      set;
    }
    public string p_container
    {
      get;
      set;
    }
    public decimal? p_retailprice
    {
      get;
      set;
    }
    public string p_comment
    {
      get;
      set;
    }
  }
}
