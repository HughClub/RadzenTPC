using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RadzenDb.Models.TpcH
{
  public class Reg{
    public const string Title="区域";
    public const string id="地区编号";
    public const string id_err=Common.epre+id;
    public const string name="地区名";
    public const string comment="地区备注";
  }
  [Table("region")]
  public partial class Region
  {
    [Key]
    public int r_regionkey
    {
      get;
      set;
    }

    public ICollection<Nation> Nations { get; set; }
    public string r_name
    {
      get;
      set;
    }
    public string r_comment
    {
      get;
      set;
    }
  }
}
