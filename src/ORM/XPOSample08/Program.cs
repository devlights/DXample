namespace DXample.XPOSample08
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  using DXample.XPOSample08.Models;

  using DevExpress.Xpo;
  using DevExpress.Xpo.DB;
  using DevExpress.Data.Filtering;

  class Program
  {
    static void Main()
    {
      //
      // XPView
      //
      XpoDefault.DataLayer = XpoDefault.GetDataLayer(MSSqlCEConnectionProvider.GetConnectionString("XPOSample08.sdf"), AutoCreateOption.DatabaseAndSchema);
      XpoDefault.Session   = null;
    }
  }
}
