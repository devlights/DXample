/////////////////////////////////////////////////////////////////////
//
// XPOSample04 -- XpoProviderTypeString
// 
/////////////////////////////////////////////////////////////////////
namespace XPOSample05
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
