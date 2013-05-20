///////////////////////////////////////////////////////
//
// XPOSample01 -- 基本的なXPOクラスの作成
// 
///////////////////////////////////////////////////////
namespace XPOSample01
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  //
  // XPOの基本名前空間.
  //   XPOを利用する上で、最低限必要なアセンブリは以下のものとなる。
  //     DevExpress.Data.vX.Y.dll
  //     DevExpress.XPO.vX.Y.dll
  //   (http://documentation.devexpress.com/#XPO/CustomDocument3130)
  //
  //   DevExpressのオンラインドキュメント(http://documentation.devexpress.com/#XPO/CustomDocument4060)を見ると
  //   DevExpress.Xpo.Linq名前空間をusingしているが
  //   最新のバージョンでは、必要無い。
  //
  // 代わりに最近のバージョンでは
  //   DevExpress.XPO.vX.Y.Extensions.dll
  // というアセンブリが存在する。
  // このアセンブリは XPO OData Service を利用する際に必要となる。
  // (http://documentation.devexpress.com/#XPO/CustomDocument3130)
  //
  // 同じような感覚で、WebアプリでXPOを利用する場合は
  //   DevExpress.XPO.vX.Y.Web.dll
  // というアセンブリが必要となる。
  // (http://documentation.devexpress.com/#XPO/CustomDocument3130)
  //
  using DevExpress.Xpo;

  class Program
  {
    static void Main()
    {
    }
  }  
}
