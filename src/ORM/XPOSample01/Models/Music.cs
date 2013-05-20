namespace XPOSample01.Models
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  using DevExpress.Xpo;

  /// <summary>
  /// XPObjectを継承した永続クラス
  /// </summary>
  /// <remarks>
  /// CodeRushを利用している場合 "xc" でテンプレートが展開される.
  /// </remarks>
  public class Music : XPObject
  {
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="session">セッション</param>
    /// <remarks>
    /// XPOでは、デフォルトコンストラクタを利用せず
    /// 明示的にセッションを受け取るコンストラクタを
    /// 定義することが推奨されている。
    /// (http://www.devexpress.com/Support/Center/Question/Details/A2944)
    /// </remarks>
    public Music(Session session) : base(session)
    { 
    }
  }
}
