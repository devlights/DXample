namespace XPOSample05.Models
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  using DevExpress.Xpo;

  public class Customer : XPObject
  {
    private int _age;
    private string _name;

    public Customer(Session session) : base(session)
    {
    }

    public string Name
    {
      get
      {
        return _name;
      }
      set
      {
        SetPropertyValue("Name", ref _name, value);
      }
    }

    public int Age
    {
      get
      {
        return _age;
      }
      set
      {
        SetPropertyValue("Age", ref _age, value);
      }
    }

    // 多側のオブジェクトを取得
    [Association("Customer-Orders")]
    public XPCollection<Order> Orders
    {
      get
      {
        return GetCollection<Order>("Orders");
      }
    }
  }
}
