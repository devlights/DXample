namespace DXample.XPOSample06
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  using DXample.XPOSample06.Models;

  using DevExpress.Data.Filtering;
  using DevExpress.Xpo;
  using DevExpress.Xpo.DB;

  class Program
  {
    static void Main()
    {
      //
      // XPOには、LINQ to XPOの他にCriteriaOperatorを利用したクエリ方法がある。
      // CriteriaOperator系のクラスは、DevExpress.Data.Filterling名前空間に配置されている。
      //
      // CriteriaOperatorで構築した抽出条件は、XPCollectionやXPViewなどのXPOコレクションクラスにて
      // 絞り込み条件として指定できる。
      //
      // 使用方法としては、以下の2種類のやり方がある。
      //   ・BinaryOperatorなどの各Operatorクラスをインスタンス化して条件を構築.
      //   ・CriteriaOperator.Parseメソッドに対して、文字列で条件を指定して構築.
      //
      // 参考リソース
      //   http://documentation.devexpress.com/#XPO/CustomDocument2258
      //   http://documentation.devexpress.com/#XPO/CustomDocument2132
      //   http://documentation.devexpress.com/#XPO/CustomDocument2047
      //   http://documentation.devexpress.com/#XPO/CustomDocument2129
      //
      var dataStore = new InMemoryDataStore();
      var dataLayer = new SimpleDataLayer(dataStore);

      //
      // 初期データ設定
      //
      using (var uow = new UnitOfWork(dataLayer))
      {
        for (int i = 0; i < 10; i++)
        {
          new Customer(uow) { Name = string.Format("Customer-{0}", i), Age = i + 20 };
        }

        uow.CommitChanges();
      }

      //
      // Operatorクラスを利用して条件を構築.
      //
      var opGreater = new BinaryOperator("Age", 25, BinaryOperatorType.GreaterOrEqual);
      using (var uow = new UnitOfWork(dataLayer))
      {
        foreach (var item in new XPCollection<Customer>(uow){ Criteria = opGreater })
        {
          Console.WriteLine(item.Name);
        }
      }

      //
      // CriteriaOperator.Parseメソッドを利用して条件を構築.
      //
      var criteria = CriteriaOperator.Parse("Age >= 25");
      using (var uow = new UnitOfWork(dataLayer))
      {
        foreach (var item in new XPCollection<Customer>(uow) { Criteria = criteria })
        {
          Console.WriteLine(item.Name);
        }
      }

      //
      // Operatorクラスのチェイン (複数条件の指定)
      //   条件同士をand, orするには、GroupOperatorの静的メソッドを利用する.
      //
      var opLike = new BinaryOperator("Name", "%7%", BinaryOperatorType.Like);
      var opAnd = GroupOperator.And(opGreater, opLike);

      using (var uow = new UnitOfWork(dataLayer))
      {
        foreach (var item in new XPCollection<Customer>(uow){ Criteria = opAnd })
        {
          Console.WriteLine(new { Name = item.Name, Age = item.Age});
        }
      }

      //
      // CriteriaOperator.Parseメソッドで複数条件の指定.
      //   条件同士をand, orするには、そのまま文字列中にand, orと記述すればよい。
      //
      criteria = CriteriaOperator.Parse("Age >= 25 AND Name like '%7%'");
      using (var uow = new UnitOfWork(dataLayer))
      {
        foreach (var item in new XPCollection<Customer>(uow) { Criteria = criteria })
        {
          Console.WriteLine(new { Name = item.Name, Age = item.Age });
        }
      }
    }
  }
}
