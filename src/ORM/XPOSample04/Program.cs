/////////////////////////////////////////////////////////////////////
//
// XPOSample04 -- XpoProviderTypeString
// 
/////////////////////////////////////////////////////////////////////
namespace XPOSample04
{
  using System;
  using System.Collections.Generic;
  using System.Data.Common;
  using System.Linq;

  using DevExpress.Xpo;
  using DevExpress.Xpo.DB;

  class Program
  {
    static void Main()
    {
      //
      // XPOでの接続文字列の構築方法について
      // 
      // XPOは、通常の接続文字列を指定しても接続できない.
      // (例外が発生する -- DevExpress.Xpo.Exceptions.CannotFindAppropriateConnectionProviderException)
      //
      // これは、XPO自体が多くのデータベースに対応しているため
      // 接続文字列だけを指定されても、それがどのデータベースの接続文字列なのかが
      // 判別できないからである。
      //
      // なので、XPOでは接続文字列にXPO専用の識別子を付けることになっている。
      // 識別子の名前は、「XpoProviderName」であり、値は各データベース毎に異なる。
      //
      // 各データベース毎に決まっている識別子の値は、共通のプロパティから取得できる。
      //    XpoProviderTypeString
      // 例を挙げると、SQL Server CEの場合は以下のようにして取得できる。
      //    DevExpress.Xpo.DB.MSSqlCEConnectionProvider.XpoProviderTypeString
      // 値は、MSSqlServerCEとなっている。
      //
      // 上記の内容に関しては、KBが存在し、そちらに詳しく記述されている。
      //    http://www.devexpress.com/Support/Center/Question/Details/K18445
      //

      //
      // ADO.NETを利用して普通に接続
      //
      var connString = @"data source=SampleDB.sdf;";

      using (var conn = DbProviderFactories.GetFactory("System.Data.SqlServerCe.4.0").CreateConnection())
      {
        conn.ConnectionString = connString;
        conn.Open();

        using (var command = conn.CreateCommand())
        {
          command.CommandText = "SELECT Name, Age FROM Customer";

          using (var reader = command.ExecuteReader())
          {
            while (reader.Read())
            {
              Console.WriteLine(new { Name = reader[0], Age = reader[1] });
            }
          }
        }
      }

      //
      // ADO.NETで利用できる接続文字列をXPOにそのまま渡して接続してみる.
      //
      try
      {
        XpoDefault.Session   = null;
        XpoDefault.DataLayer = XpoDefault.GetDataLayer(connString, AutoCreateOption.DatabaseAndSchema);

        using (UnitOfWork uow = new UnitOfWork())
        {
          uow.Connect();
        }
      }
      catch (DevExpress.Xpo.Exceptions.CannotFindAppropriateConnectionProviderException notFoundProviderEx)
      {
        //
        // 特定のコネクションプロバイダ（つまりデータソース）が見つからなかったという例外
        // 接続文字列に「XpoProviderName」が付与されていない場合に発生する.
        //
        Console.WriteLine(notFoundProviderEx);
      }

      //
      // 接続文字列に「XpoProviderName」を付与して、再度接続してみる.
      //
      var xpoConnString = string.Format("XpoProvider={0};{1}", MSSqlCEConnectionProvider.XpoProviderTypeString, connString);

      XpoDefault.DataLayer = XpoDefault.GetDataLayer(xpoConnString, AutoCreateOption.DatabaseAndSchema);
      using (UnitOfWork uow = new UnitOfWork())
      {
        //
        // 今回は問題なく接続できる.
        //
        uow.Connect();

        foreach (var row in uow.ExecuteQuery("SELECT Name, Age FROM Customer").ResultSet.First().Rows)
        {
          Console.WriteLine(new { Name = row.Values[0], Age = row.Values[1] });
        }
      }
    }
  }
}
