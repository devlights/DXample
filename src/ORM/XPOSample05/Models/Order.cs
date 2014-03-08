namespace DXample.XPOSample05.Models
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  using DevExpress.Xpo;

  public class Order : XPObject
  {
    private Customer _customer;
    private DateTime _orderDate;
    private string _productName;

    public Order(Session session) : base(session)
    {
    }

    public string ProductName
    {
      get
      {
        return _productName;
      }
      set
      {
        SetPropertyValue("ProductName", ref _productName, value);
      }
    }

    public DateTime OrderDate
    {
      get
      {
        return _orderDate;
      }
      set
      {
        SetPropertyValue("OrderDate", ref _orderDate, value);
      }
    }

    // 1側のオブジェクトを取得
    [Association("Customer-Orders")]
    public Customer Customer
    {
      get
      {
        return _customer;
      }
      set
      {
        SetPropertyValue("Customer", ref _customer, value);
      }
    }
  }
}
