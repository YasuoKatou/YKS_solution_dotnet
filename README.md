# VS Codeで複数プロジェクトをソリューションとして管理する

各プロジェクトの解説は、各プロジェクトの解説を参照願います。

## 環境
* VS Code バージョン：バージョン: 1.36.1 (system setup)

## ソリューションを作成
```mkdir YKS_solution```でソリューションフォルダを新規に作成する。<br/>
以下、このフォルダ（YKS_solution）を```SLN_HOME```と記す。<br/>


```dotnet new sln```でソリューションを作成する。<br/>
```
SLN_HOME > dotnet new sln
The template "Solution File" was created successfully.
```

### Solrクライアントプロジェクトを作成
```SLN_HOME > mkdir SolrClient```でフォルダを作成する。<br/>
```SLN_HOME > cd SolrClient```でフォルダを移動する。<br/>
```dotnet new wpf```でこのプロジェクトを作成する。<br/>
```
SLN_HOME\SolrClient > dotnet new wpf
The template "WPF Application" was created successfully.

Processing post-creation actions...
Running 'dotnet restore' on SLN_HOME\SolrClient\SolrClient.csproj...
  SLN_HOME\SolrClient\SolrClient.csproj の復元が 278.09 ms で完了しました。

Restore succeeded.
```
プロジェクトの詳細は、プロジェクト内のREADMR.mdを参照<br/>
```SLN_HOME\SolrClient > cd ..```でソリューションフォルダに戻って<br/>
```SLN_HOME > dotnet sln add .\SolrClient\SolrClient.csproj```でプロジェクトをソリューションに追加する<br/>

#### Solrクライアントの起動
```SLN_HOME > cd SolrClient```でフォルダを移動する。<br/>
#### ビルド
```
SLN_HOME\SolrClient > dotnet build
.NET Core 向け Microsoft (R) Build Engine バージョン 16.3.0-preview-19329-01+d31fdbf01
Copyright (C) Microsoft Corporation.All rights reserved.

  SLN_HOME\SolrClient\SolrClient.csproj の復元が 69.21 ms で完了しました。
  You are using a preview version of .NET Core. See: https://aka.ms/dotnet-core-preview
  SolrClient -> SLN_HOME\SolrClient\bin\Debug\netcoreapp3.0\SolrClient.dll

ビルドに成功しました。
    0 個の警告
    0 エラー

経過時間 00:00:15.53
```
#### 起動
```
SLN_HOME > dotnet run -p .\SolrClient\SolrClient.csproj
```
または、<br/>
```
SLN_HOME > cd .\SolrClient\
SLN_HOME\SolrClient > dotnet run -p .\SolrClient.csproj
```

### ユーティリティプロジェクトを作成
```SLN_HOME > mkdir Utils```でフォルダを作成する。<br/>
```SLN_HOME > cd Utils```でフォルダを移動する。<br/>
```
SLN_HOME\Utils> dotnet new classlib
The template "Class library" was created successfully.

Processing post-creation actions...
Running 'dotnet restore' on SLN_HOME\Utils\Utils.csproj...
  SLN_HOME\Utils\Utils.csproj の復元が 558.2 ms で完了しました。

Restore succeeded.
```
でプロジェクトを作成する。<br/>
```SLN_HOME\Utils > cd ..```でソリューションフォルダに戻って<br/>
```SLN_HOME > dotnet sln add .\Utils\Utils.csproj```でプロジェクトをソリューションに追加する<br/>


### ユーティリティテストプロジェクトを作成
```SLN_HOME > mkdir Utils.Tests```でフォルダを作成する。<br/>
```SLN_HOME > cd Utils.Tests```でフォルダを移動する。<br/>

```
\Utils.Tests> dotnet new nunit
The template "NUnit 3 Test Project" was created successfully.

Processing post-creation actions...
Running 'dotnet restore' on SLN_HOME\Utils.Tests\Utils.Tests.csproj...
  SLN_HOME\Utils.Tests\Utils.Tests.csproj の復元が 7.16 sec で完了しました。

Restore succeeded.
```
でプロジェクトを作成する。<br/>
```
SLM_HOME\JsonUtil.Tests > dotnet add reference ..\Utils\Utils.csproj
```
でテスト対象のプロジェクトの参照を設定する。<br/>

```SLN_HOME\JsonUtil.Tests > cd ..```でソリューションフォルダに戻って<br/>
```SLN_HOME > dotnet sln add .\Utils.Tests\Utils.Tests.csproj```でプロジェクトをソリューションに追加する<br/>

#### テストの実行
```SLN_HOME > cd Utils.Tests```でフォルダを移動する。<br/>
```dotnet test```でNUnitを実行する。<br/>
```
SLN_HOME\Utils.Tests> dotnet test
SLN_HOME\Utils.Tests\bin\Debug\netcoreapp3.0\Utils.Tests.dll(.NETCoreApp,Version=v3.0) のテスト実行
Microsoft (R) Test Execution Command Line Tool Version 16.1.1
Copyright (c) Microsoft Corporation.  All rights reserved.

テスト実行を開始しています。お待ちください...

テストの実行に成功しました。
Total tests: 2
     Passed: 2
テスト実行時間: 3.7413 秒
```


