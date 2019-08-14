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






