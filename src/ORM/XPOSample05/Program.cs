/////////////////////////////////////////////////////////////////////
//
// XPOSample05 -- Relation One-to-Many
// 
/////////////////////////////////////////////////////////////////////
namespace DXample.XPOSample05
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  using DevExpress.Xpo;
  using DevExpress.Xpo.DB;

  using XPOSample05.Models;

  class Program
  {
    static void Main()
    {
      //
      // XPOには、ORMとして関連を扱う機能がある。
      // 今回は1対多、つまり、One-to-Manyの場合を記述する。
      //
      // XPOにて、関連を記述する場合、対象となるプロパティに
      //   AssociationAttribute
      // を付与する必要がある。
      //
      // One-to-Manyの場合は以下のようにする。
      // 今回のサンプルでは、Customerが1側、Orderが多側となる。
      // 1側の方に定義するのは、多側のコレクションを取得するプロパティ。
      // これはreadonlyでよい。
      // 以下のようになる。
      //
      // [例]
      // public XPCollection<Order> Orders { get{ GetCollection<Order>("Orders"); }}
      // 
      // GetCollectionの引数に渡すのは、プロパティ名である。今回の場合だと、Ordersとなる。
      //
      // さらに、最後の仕上げとしてAssociationAttributeを付与する。関連名は任意のものでよい。
      // ただし、関連するもの同士の関連名は会わせておく必要がある。
      //
      // [例]
      // [Association("Customer-Orders")] public XPCollection<Order> Orders { get{ GetCollection<Order>("Orders"); }}
      //
      // 次に、多側であるが、こちらは1側のオブジェクトを保持するようにする。
      // プロパティは、通常のXPOプロパティを定義し、Associationを付与する。
      //
      // [例]
      // [Association("Customer-Orders")]
      // public Customer Customer
      // {
      //   get { return _customer; }
      //   set { SetPropertyValue("Customer", ref _customer, value); }
      // }
      //
      // これで関連が定義できた。後は、実行時にCustomerオブジェクトを取得すると
      // Ordersプロパティから、紐付くOrderが取得できる。
      // また、Orderオブジェクトからは、紐付くCustomerオブジェクトが取得できる。
      //
      // [参考URL]
      // http://documentation.devexpress.com/#XPO/CustomDocument2257
      //
      XpoDefault.DataLayer = new SimpleDataLayer(new InMemoryDataStore());
      XpoDefault.Session   = null;

      using (UnitOfWork uow = new UnitOfWork())
      {
        var newCustomer = new Customer(uow) { Name = "Customer-1", Age = 33 };

        for (int i = 0; i < 3; i++)
        {
          newCustomer.Orders.Add(
            new Order(uow) { ProductName = string.Format("Product-{0}", i), OrderDate = DateTime.Now }
          );
        }

        uow.CommitChanges();
      }

      using (UnitOfWork uow = new UnitOfWork())
      {
        foreach (var theCustomer in uow.Query<Customer>())
        {
          Console.WriteLine(new { Name = theCustomer.Name, Age = theCustomer.Age });

          foreach (var theOrder in theCustomer.Orders.OrderBy(_ => _.ProductName))
          {
            Console.WriteLine("\t{0}", new { Product = theOrder.ProductName, Parent = theOrder.Customer });
          }
        }
      }
    }
  }
}
