/////////////////////////////////////////////////////////////////////
//
// XPOSample03 -- XpoDefault.DataLayerでデフォルトデータレイヤを設定
// 
/////////////////////////////////////////////////////////////////////
namespace XPOSample03
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  using DevExpress.Xpo;
  using DevExpress.Xpo.DB;

  using XPOSample03.Models;
  
  class Program
  {
    static void Main()
    {
      var connString = MSSqlCEConnectionProvider.GetConnectionString("SampleDB.sdf");
      var dataLayer  = XpoDefault.GetDataLayer(connString, AutoCreateOption.DatabaseAndSchema);

      XpoDefault.DataLayer = dataLayer;
      XpoDefault.Session   = null;

      using (var uow = new UnitOfWork())
      {
        for (int i = 0; i < 5; i++)
        {
          new Music(uow){ Title = string.Format("Title-{0}", i), ArtistName = string.Format("ArtistName-{0}", i) };
        }

        uow.CommitChanges();
      }

      using (var uow = new UnitOfWork())
      {
        var query = from   _ in uow.Query<Music>()
                    select new { Title = _.Title, ArtistName = _.ArtistName };

        foreach (var item in query)
        {
          Console.WriteLine(item);
        }
      }
    }
  }
}
