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
  /// 
  /// 親クラスに、XPObjectを指定している場合
  /// Primary keyはOIDというプロパティ名で自動的に定義されている。
  /// このプロパティには、KeyAttributeが付与されているので
  /// 子クラス側では、新たにPrimary Keyを作成する事ができない。
  /// (一意な値のみを許すプロパティを定義することは出来る。 (Unique Index))
  /// 
  /// XPObjectは、データベース毎、最初から作成できるパターンの場合に有効。
  /// 既にデータベースが存在するようなプロジェクトの場合は, XPLiteObjectを
  /// 親クラスにして、永続クラスを作成する。
  /// 
  /// XPLiteObjectには、OIDのようなPrimary Keyプロパティが事前に定義されていないので
  /// ユーザ側で自由にキー項目を作成することが出来る。
  /// </remarks>
  public class Music : XPObject
  {
    private string _artistName;
    private string _title;

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

    /// <summary>
    /// 曲のタイトル
    /// </summary>
    /// <remarks>
    /// XPOでは、プロパティを作成する際に以下のようにXPO専用のやり方がある。
    /// これを行わずに、普通に自動プロパティなどを利用しても問題はない。
    /// ただし、UI側とデータバインディングを行う場合やセッションからリロードする際
    /// などにいろいろと面倒な事になるので、できる限りこの形式でプロパティを定義する方が良い。
    /// 
    /// 単純にデータを取得して、永続化するだけなら普通のプロパティでも問題はない。
    /// 
    /// CodeRushを利用している場合、"xp[型パラメータ(s, i, d8など)]" でテンプレートが展開される。
    /// </remarks>
    public string Title
    {
      get
      {
        return _title;
      }
      set
      {
        SetPropertyValue("Title", ref _title, value);
      }
    }

    /// <summary>
    /// アーティスト名
    /// </summary>
    public string ArtistName
    {
      get
      {
        return _artistName;
      }
      set
      {
        SetPropertyValue("ArtistName", ref _artistName, value);
      }
    }

    /// <summary>
    /// オブジェクトの文字列表現を取得します。
    /// </summary>
    /// <returns>文字列表現</returns>
    public override string ToString()
    {
      return string.Format("Title=[{0}], ArtistName=[{1}]", Title, ArtistName);
    }
  }
}
