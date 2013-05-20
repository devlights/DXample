///////////////////////////////////////////////////////
//
// XPOSample01 -- 基本的なXPOクラスの作成
// 
///////////////////////////////////////////////////////
namespace XPOSample01
{
  //
  // Framework namespace.
  //
  using System;
  using System.Collections.Generic;
  using System.Linq;
  //
  // DevExpress namespace.
  //
  // XPOの基本名前空間.
  //   XPOを利用する上で、最低限必要なアセンブリは以下のものとなる。
  //     DevExpress.Data.vX.Y.dll
  //     DevExpress.XPO.vX.Y.dll
  //   (http://documentation.devexpress.com/#XPO/CustomDocument3130)
  //
  //   DevExpressのオンラインドキュメント(http://documentation.devexpress.com/#XPO/CustomDocument4060)を見ると
  //   DevExpress.Xpo.Linq名前空間をusingしているが
  //   最新のバージョンでは、必要無い。
  //
  // 代わりに最近のバージョンでは
  //   DevExpress.XPO.vX.Y.Extensions.dll
  // というアセンブリが存在する。
  // このアセンブリは XPO OData Service を利用する際に必要となる。
  // (http://documentation.devexpress.com/#XPO/CustomDocument3130)
  //
  // 同じような感覚で、WebアプリでXPOを利用する場合は
  //   DevExpress.XPO.vX.Y.Web.dll
  // というアセンブリが必要となる。
  // (http://documentation.devexpress.com/#XPO/CustomDocument3130)
  //
  using DevExpress.Xpo;
  using DevExpress.Xpo.DB;
  //
  // User namespace.
  //
  using XPOSample01.Models;

  class Program
  {
    static void Main()
    {
      //
      // XPOにて処理を行うためには、以下のものを設定する必要がある。
      // (http://documentation.devexpress.com/#XPO/CustomDocument2123)
      // (http://help.devexpress.com/#XPO/CustomDocument2022)
      //
      //   ・データストア： 実際のデータを保持するストア (DB, インメモリDBなど)
      //   ・データレイヤ： データストアにアクセスするレイヤ
      //
      // このほかにも, オブジェクトレイヤやキャッシュレイヤなどが存在するが割愛する。
      //
   
      //
      // データストアの構築
      //   本サンプルでは、XPO付属のインメモリデータストアを利用する
      //
      var dataStore = new InMemoryDataStore();

      //
      // XPOにて初期処理を行う場合、データにアクセスするためのデータレイヤーを
      // 構築する必要がある。何も設定せずに、利用するとMDBがデータストアとして
      // 利用される。
      //
      // データレイヤークラスには、派生クラスがいくつか存在するが
      // よく利用するのは、SimpleDataLayerクラス。
      //
      var dataLayer = new SimpleDataLayer(dataStore);

      //
      // 最後にデータを取得するためのセッションを構築する.
      // XPOではSessionクラスという名前のセッションを構築するクラスが
      // あるが、UnifOfWorkクラスを利用するのが一般的。
      // (Unit of Workデザインパターンの実装）
      //
      // Session, UnitOfWorkの両方ともIDisposableを実装しているので
      // usingブロック内で利用するのが一般的.
      //
      using (var uow = new UnitOfWork(dataLayer))
      {
        //
        // まだデータが存在しないので、ここで作る.
        //
        var savior    = new Music(uow) { Title = "savior", ArtistName = "Rise Against" };
        var satellite = new Music(uow) { Title = "satellite", ArtistName = "Rise Against" };
        var hereIAm   = new Music(uow) { Title = "Here I am", ArtistName = "Dragon Ash" };

        //
        // 変更を確定.
        //
        uow.CommitChanges();
      }

      //
      // 作ったデータを取得.
      //
      using (var uow = new UnitOfWork(dataLayer))
      {
        //
        // データを取得するのに、最も基本的なやり方は
        //   ・XPCollection
        //   ・XPView
        // を利用して、データを取得するか
        //    ・Session
        //    ・UnitOfWork
        // に存在するメソッドを利用してデータを取得するか
        //    ・Linq to XPO
        // を用いて、データを取得するかになる.
        //
        // 今回は最もよく利用する LINQ to XPO を用いて
        // データを取得する.
        //
        var query = from   item in uow.Query<Music>()
                    where  item.ArtistName == "Rise Against"
                    select item;

        foreach (var item in query)
        {
          Console.WriteLine(item);
        }
      }
    }
  }  
}
