
namespace DXample.XPOSample03.Models
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  using DevExpress.Xpo;

  public class Music : XPObject
  {
    private string _artistName;
    private string _title;

    public Music(Session session) : base(session)
    { 
    }

    public string Title
    {
      get
      {
        return _title;
      }
      set
      {
        SetPropertyValue("Title", ref _title, value);
      }
    }

    public string ArtistName
    {
      get
      {
        return _artistName;
      }
      set
      {
        SetPropertyValue("ArtistName", ref _artistName, value);
      }
    }
  }
}
