using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RadzenDb.Models.TpcH
{
  public class Nat{
    public const string Title="国家表";
    public const string id="国家编号";
    public const string id_err=Common.epre+id;
    public const string reg="所在地区";
    public const string reg_err=Common.epre+reg;
    public const string name="国家名";
    public const string comment="国家备注";

  }
  [Table("nation")]
  public partial class Nation
  {
    [Key]
    public int n_nationkey
    {
      get;
      set;
    }

    public ICollection<Customer> Customers { get; set; }
    public ICollection<Supplier> Suppliers { get; set; }
    public int n_regionkey
    {
      get;
      set;
    }
    public Region Region { get; set; }
    public string n_name
    {
      get;
      set;
    }
    public string n_comment
    {
      get;
      set;
    }
  }
}
