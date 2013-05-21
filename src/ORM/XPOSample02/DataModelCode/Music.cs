using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace XPOSample02.Models
{

  public partial class Music
  {
    public Music() : base(Session.DefaultSession) { }
    public Music(Session session) : base(session) { }
    public override void AfterConstruction() { base.AfterConstruction(); }
  }

}
