
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
      var dataStore = new InMemoryDataStore();
      var dataLayer = new SimpleDataLayer(dataStore);

      //
      // 初期データ生成.
      //
      using (var uow = new UnitOfWork(dataLayer))
      {
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

      using (var uow = new UnitOfWork(dataLayer))
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
      using (var uow = new UnitOfWork(dataLayer))
      {
        var theCustomer = uow.FindObject<Customer>(criteria);
        Console.WriteLine(theCustomer.Name);
      }

      using (var uow = new UnitOfWork(dataLayer))
      {
        var parentCustomer = uow.FindObject<Customer>(criteria);

        using (var nuow = uow.BeginNestedUnitOfWork())
        {
          var nestedCustomer  = nuow.GetNestedObject<Customer>(parentCustomer);
          nestedCustomer.Name = "Modified 2";

          nuow.CommitChanges();
        }

        Console.WriteLine(parentCustomer.Name);
      }

      //
      // 別のUOWで再度同じ条件を指定して値を確認.
      //
      using (var uow = new UnitOfWork(dataLayer))
      {
        var theCustomer = uow.FindObject<Customer>(criteria);
        Console.WriteLine(theCustomer.Name);
      }

    }
  }
}
