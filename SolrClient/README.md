# 「Solr クライアント」プロジェクト



## 開発環境
* VS Code : バージョン: 1.37.0 (system setup)

プロジェクトのルートパスをPRJ_HOMEと記す。

### Utils\JsonUtilクラスの参照を設定

```
PRJ_HOME > dotnet add reference ..\Utils\Utils.csproj
```

### System.Windows.Forms を使用可能にする

[Windows Forms デスクトップ アプリを .NET Core に移植する](https://docs.microsoft.com/ja-jp/dotnet/core/porting/winforms)より

プロジェクトファイル（.csproj）の```<PropertyGroup>```タグに```<UseWindowsForms>true</UseWindowsForms>```を追加する。

```dotnet build```すると、

```
MainWindow.xaml.cs(34,13): error CS0104: 'Button' は、'System.Windows.Controls.Button' と 'System.Windows.Forms.Button' 間のあいまいな参照です
```
が多発した。

あいまいな参照を解決する必要がある。<br/>
⇒ 今回```System.Windows.Forms```の参照が不要となった。


## リリースモジュールの作成

```
PRJ_HOME > dotnet restore
  SLN_HOME\Utils\Utils.csproj の復元が 611.72 ms で完了しました。
  SLN_HOME\SolrClient\SolrClient.csproj の復元が 56.28 sec で完了しました。
```

```
PRJ_HOME > dotnet publish -c Release -r win10-x64
.NET Core 向け Microsoft (R) Build Engine バージョン 16.3.0-preview-19329-01+d31fdbf01
Copyright (C) Microsoft Corporation.All rights reserved.

  SLN_HOME\SolrClient\SolrClient.csproj の復元が 390.53 ms で完了しました。
  SLN_HOME\Utils\Utils.csproj の復元が 399.06 ms で完了しました。
  You are using a preview version of .NET Core. See: https://aka.ms/dotnet-core-preview
  Utils -> SLN_HOME\Utils\bin\Release\netstandard2.0\Utils.dll
  SolrClient -> SLN_HOME\SolrClient\bin\Release\netcoreapp3.0\win10-x64\SolrClient.dll
  SolrClient -> SLN_HOME\SolrClient\bin\Release\netcoreapp3.0\win10-x64\publish\
```





