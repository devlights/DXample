using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace DXample.XPOSample02.Models
{

  public partial class Music
  {
    public Music(Session session) : base(session) { }
    public override void AfterConstruction() { base.AfterConstruction(); }
  }

}
