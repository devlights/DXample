namespace DXample.XPOSample06.Models
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  using DevExpress.Xpo;

  public class Customer : XPObject
  {
    private int _age;
    private string _name;

    public Customer(Session sess) : base(sess)
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
  }
}
