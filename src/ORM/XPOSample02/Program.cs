///////////////////////////////////////////////////////
//
// XPOSample02 -- Data Model Wizardの利用
// 
///////////////////////////////////////////////////////
namespace DXample.XPOSample02
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  using DevExpress.Xpo;
  using DevExpress.Xpo.DB;

  using XPOSample02.Models;
  
  class Program
  {
    static void Main()
    {
      //
      // XPOには、永続クラスを簡単に作成できるよう
      // Data Model Wizardが付属している。
      //
      // XPOは、EntityFrameworkと同様に
      //  ・Model Wizardを使って作成： Model-First
      //  ・コードから作成： Code-First
      // という2通りの作成方法を用意している。
      //
      // Code-Firstは、前回のサンプル (XPOSample01) で示したように
      // 自分で永続クラスを定義して利用する方法である。
      //
      // Model-Firstは、今回のようにウィザードを利用する方法である。
      // ビギナーにはモデルデザイナを利用して勧めることが推奨されている模様。
      // (http://documentation.devexpress.com/#XPO/CustomDocument2256)
      // 
      // デザイナを利用するには、新規項目追加画面にて「DXpreience v12.2 ORM Data Model Wizard」を
      // 選択する。初期表示に若干時間がかかるが、少しするとデザイナ画面が表示される。
      //
      // 後は、Tutorial 1(http://documentation.devexpress.com/#XPO/CustomDocument2256)に記載されている
      // 手順通りにデザイナを操作すれば、モデルクラスが自動生成される。
      //
      // 個人的にデザイナにて設定しておいた方が良いプロパティを以下に記述する.
      // 以下のプロパティは、ORMデザイナにてデザイナ本体のプロパティである。
      //   Generate Default Constructor: False
      //   Field Name Case: withLowerCase
      //   Field Name Prefix: _
      //   Namespace: 初期状態の名前が気に入らない場合に修正する (プロジェクト基底名前空間.Modelsとか)
      // Field Name系のプロパティは、デフォルトのままにしておくと
      // 自動生成されるクラスのフィールド名の先頭に f が付いたり、大文字のままだったりするので
      // 個人的にはこの設定の方が好み。
      //
      var dataStore = new InMemoryDataStore();
      var dataLayer = new SimpleDataLayer(dataStore);

      //
      // 初期データ生成
      //
      using (var uow = new UnitOfWork(dataLayer))
      {
        for (int i = 0; i < 5; i++)
        {
          new Music(uow) 
          {
            Title = string.Format("Title-{0}", i), 
            ArtistName = string.Format("Artist-{0}", i) 
          };
        }

        uow.CommitChanges();
      }

      //
      // データ確認.
      //
      using (var uow = new UnitOfWork(dataLayer))
      {
        foreach (var item in uow.Query<Music>().OrderBy(_ => _.Title))
        {
          Console.WriteLine("Title={0}, ArtistName={1}", item.Title, item.ArtistName);
        }
      }
    }
  }
}
