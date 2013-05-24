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
      //
      // XPOにて、共通の設定を行う為のクラスはXpoDefaultというクラスになる.
      // このクラスには、デフォルトデータレイヤやデフォルトセッションなどを保持している。
      //
      // SessionやUnitOfWorkを作成する際に、コンストラクタにデータレイヤなどを
      // 明示的に渡さずに初期化した場合、XpoDefaultに設定されている内容が利用される。
      //
      // 接続に関して、重要なのは以下の２つのプロパティ.
      //    XpoDefault.DataLayer -- デフォルトデータレイヤ
      //    XpoDefault.Session  -- デフォルトセッション
      //
      // 何も設定していない場合、XPOではデフォルトでMSAccessデータベースが
      // 作成される。それは、XpoDefaultにて初期設定がそのようになっているからである。
      //
      // アプリを作成する際に、特定のデータベースに接続する必要があるが
      // 以下の手順で行う。
      //
      //   (1) 各データベース用のConnectionProviderクラスから接続文字列を作成する。(ex:MSSqlCEConnectionProvider)
      //   (2) XpoDefault.GetDataLayerメソッドに接続文字列を渡して、データレイヤを取得
      //   (3) XpoDefault.DataLayerに(2)で取得したデータレイヤを設定。これがデフォルトデータレイヤとなる。
      //   (4) XpoDefault.Sessionにnullを設定.
      //
      // (4)を行う事により、デフォルトセッションを利用できなくしている。(XPO Best Practice)
      // つまり、XPCollectionなどを利用する際に明示的にセッションを渡さないとエラーになるようにできる。
      // デフォルトセッションを利用していると、たまに意図しない接続先に繋がっていることもあるので
      // 可能な限り利用しない方がよい。（デフォルトデータレイヤはオッケイ）
      //
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
