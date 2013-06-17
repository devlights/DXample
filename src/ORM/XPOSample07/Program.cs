namespace XPOSample07
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  using DXample.XPOSample07.Models;

  using DevExpress.Xpo;
  using DevExpress.Xpo.DB;
  using DevExpress.Data.Filtering;

  class Program
  {
    static void Main()
    {
      //
      // XPOには、NestedUnitOfWorkというクラスが存在する。
      // このクラスは、文字通りネストしたUOWを示す。
      //（サブトランザクションのようなイメージ）
      // 
      // NestedUnitOfWork内でコミットされた変更は
      // 親のUOWにてコミットされない限り、確定しない。
      // これは、別のUOWからは変更が見えないという意味であり
      // 親のUOWからはNestedUnitOfWorkの変更は見える。
      //
      // 概念的には、DBのトランザクションやTransactionScopeと同じ。
      //
      // 画面にて別画面を開いて、データを編集してまた戻ってくる場合などに利用できる。
      // (Tutorial 4はそのパターンを実装してくれている。）
      //
      // NestedUnitOfWork内では、オブジェクトを親のUOWと明確に分けて取得することが出来る。
      //   ・GetNestedObject
      // また、NestedUnitOfWork内で親側のUOWからオブジェクトを取得することも出来る。
      //   ・GetParentObject
      //
      // [参考リソース]
      //   http://documentation.devexpress.com/#XPO/CustomDocument2260
      //   http://documentation.devexpress.com/#XPO/CustomDocument2113
      //
      XpoDefault.DataLayer = XpoDefault.GetDataLayer(MSSqlCEConnectionProvider.GetConnectionString("XPOSample07.sdf"), AutoCreateOption.DatabaseAndSchema);
      XpoDefault.Session   = null;

      //
      // 初期データ生成.
      //
      using (var uow = new UnitOfWork())
      {
        uow.Delete(new XPCollection<Customer>(uow));

        for (int i = 0; i < 10; i++)
        {
          new Customer(uow){ Name = string.Format("Customer-[{0}]", i), Age = i + 30 };
        }

        uow.CommitChanges();
      }

      //
      // NestedUnitOfWorkを作成して親UOWがコミットしていない状態での値を確認.
      //
      var criteria = CriteriaOperator.Parse("Age = 33");

      using (var uow = new UnitOfWork())
      {
        using (var nuow = uow.BeginNestedUnitOfWork())
        {
          var nuowCustomer = nuow.FindObject<Customer>(criteria);
          nuowCustomer.Name = "Modified";

          nuow.CommitChanges();
        }

        var theCustomer = uow.FindObject<Customer>(criteria);
        Console.WriteLine(theCustomer.Name);

        //
        // わざと親のUOWではCommitChangesを呼ばずに処理終了.
        //
        // 以下のコメントを外すと、変更が確定され、別のUOWでも変更が見えるようになる.
        //uow.CommitChanges();
      }

      //
      // 別のUOWで再度同じ条件を指定して値を確認.
      //
      using (var uow = new UnitOfWork())
      {
        var theCustomer = uow.FindObject<Customer>(criteria);
        Console.WriteLine(theCustomer.Name);
      }

      using (var uow = new UnitOfWork())
      {
        var parentCustomer = uow.FindObject<Customer>(criteria);

        using (var nuow = uow.BeginNestedUnitOfWork())
        {
          var nestedCustomer  = nuow.GetNestedObject<Customer>(parentCustomer);
          nestedCustomer.Name = "Modified 2";

          nuow.CommitChanges();
        }

        //
        // NestedUnitOfWork側でコミット（つまり子のトランザクション）を行う事により
        // 親側のオブジェクトの値も変更状態となる。
        // しかし、この変更は親側のトランザクションにて未コミットとなっているので
        // リロードするか、そのままUOWをコミットせずに終了することにより変更が破棄される。
        //
        // 強制的に親オブジェクトの値を元に戻すには、リロード処理を行う必要がある。
        //   Session.Reload, Session.DropIdentityMap
        // もしくは、再度同じ条件でオブジェクトを取得し直す.
        //
        Console.WriteLine(parentCustomer.Name);
        uow.Reload(parentCustomer);
        Console.WriteLine(parentCustomer.Name);
        Console.WriteLine(uow.FindObject<Customer>(criteria).Name);
      }

      //
      // 別のUOWで再度同じ条件を指定して値を確認.
      //
      using (var uow = new UnitOfWork())
      {
        var theCustomer = uow.FindObject<Customer>(criteria);
        Console.WriteLine(theCustomer.Name);
      }

    }
  }
}
