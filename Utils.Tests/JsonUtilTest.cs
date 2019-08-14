using NUnit.Framework;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace JsonUtil.Tests
{
    [DataContract]
    public class TestJsonFormat01
    {
        /// <summary>レスポンスヘッダー.</summary>
        [DataMember]
        public ResponseHeader responseHeader { get; set; }
        [DataMember(Name = "response")]
        public ResponseData responseData { get; set; }
    }

    /// <summary>レスポンスヘッダー.</summary>
    [DataContract]
    public class ResponseHeader
    {
        [DataMember]
        public int status { get; set; }
        /// <summary>処理時間.</summary>
        [DataMember]
        public int QTime { get; set; }
        /// <summary>検索パラメータ.</summary>
        [DataMember(Name = "params")]
        public SearchParameters parameters { get; set; }
    }

    /// <summary>検索パラメータ.</summary>
    [DataContract]
    public class SearchParameters
    {
        /// <summary>検索文字列.</summary>
        [DataMember]
        public string q { get; set; }
        /// <summary>取得項目.</summary>
        [DataMember]
        public string fl { get; set; }
        /// <summary>並び替え条件.</summary>
        [DataMember]
        public string sort { get; set; }
    }

    /// <summary>レスポンスデータ.</summary>
    [DataContract]
    public class ResponseData
    {
        /// <summary>検索結果総件数.</summary>
        [DataMember]
        public int numFound { get; set; }
        /// <summary>検索結果データ開始インデックス.</summary>
        [DataMember]
        public int start { get; set; }
        /// <summary>検索結果データ一覧.</summary>
        [DataMember]
        public SearchDocument[] docs { get; set; }
    }

    /// <summary>検索結果データ.</summary>
    [DataContract]
    public class SearchDocument
    {
        /// <summary>サマリ.</summary>
        [DataMember]
        public string[] summar { get; set; }
        /// <summary>タイトル.</summary>
        [DataMember]
        public string[] title { get; set; }
        /// <summary>id.</summary>
        [DataMember]
        public string id { get; set; }
        /// <summary>id.</summary>
        [DataMember]
        public string[] og_url { get; set; }
    }

    public class JsonUtilTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            json.util.JsonUtil util = new json.util.JsonUtil();
            TestJsonFormat01 result = util.paeseJson<TestJsonFormat01>("{}");
            Debug.Assert(result != null, "戻り値がnull");
        }

        [Test]
        public void Test2()
        {
            string jsonStr = "{";
            jsonStr += "\"responseHeader\":{";
            jsonStr += "  \"status\":0,";
            jsonStr += "  \"QTime\":3,";
            jsonStr += "  \"params\":{";
            jsonStr += "      \"q\":\"PC\",";
            jsonStr += "      \"fl\":\"id,og_url,title,summary\",";
            jsonStr += "      \"sort\":\"pubdateiso desc\"}},";
            jsonStr += "\"response\":{\"numFound\":342,\"start\":0,\"docs\":[";
            jsonStr += "    {";
            jsonStr += "      \"summary\":[\"キャリア事業に参入する楽天は、5Gではどんなサービスを考えているのだろうか。「Rakuten Optimism 2019」で、その一端を見ることができる。サッカーと野球のプロ球団を所有する楽天ならではのコンテンツに期待したい。\"],";
            jsonStr += "      \"title\":[\"楽天が見据える5Gの世界、スポーツとのコラボに期待\"],";
            jsonStr += "      \"id\":\"https://www.itmedia.co.jp/mobile/articles/1908/02/news131.html\",";
            jsonStr += "      \"og_url\":[\"https://www.itmedia.co.jp/mobile/articles/1908/02/news131.html\"]},";
            jsonStr += "    {";
            // jsonStr += "      \"summary\":[\"\n                    Image:ギズモード・ジャパン/YouTubeiPhoneからコンテンツビジネスへ。遅ればせながら、ギズモードもAppleの新OSパブリックベータ版を触りました。発表が行なわれた6月のWWDCから随分と経ってしまったので、もう一度おさらい。WWDCのキーノートで発表された新OSは「iOS13」「macOSCatalina」「watchOS6」「tvOS」、そして期待のiPad向けの新OS「iPa\n                \"],";
            jsonStr += "      \"summary\":[\"\\n                    Image:ギズモード・ジャパン/YouTubeiPhoneからコンテンツビジネスへ。遅ればせながら、ギズモードもAppleの新OSパブリックベータ版を触りました。発表が行なわれた6月のWWDCから随分と経ってしまったので、もう一度おさらい。WWDCのキーノートで発表された新OSは「iOS13」「macOSCatalina」「watchOS6」「tvOS」、そして期待のiPad向けの新OS「iPa\\n                \"],";
            jsonStr += "      \"title\":[\"今年の新OSから読み取れること：iOS 13/iPadOS/macOS パブリックベータ・ハンズオン【動画】\"],";
            jsonStr += "      \"id\":\"https://www.gizmodo.jp/2019/08/apple-2019-public-beta-hands-on.html\",";
            jsonStr += "      \"og_url\":[\"https://www.gizmodo.jp/2019/08/apple-2019-public-beta-hands-on.html\"]}]";
            jsonStr += "}}";

            json.util.JsonUtil util = new json.util.JsonUtil();
            TestJsonFormat01 result = util.paeseJson<TestJsonFormat01>(jsonStr);
            Debug.Assert(result != null, "戻り値がnull");
            Debug.Assert(result.responseHeader != null, "レスポンスヘッダーがない");
            Debug.Assert(result.responseHeader.status == 0, "ステータスが不正");
            Debug.Assert(result.responseHeader.QTime == 3, "処理時間が不正");
            Debug.Assert(result.responseHeader.parameters != null, "検索時のパラメータがない");
            Debug.Assert(result.responseHeader.parameters.q == "PC", "検索キーワードが不正");
            Debug.Assert(result.responseHeader.parameters.fl == "id,og_url,title,summary", "検索結果の項目が不正");
            Debug.Assert(result.responseHeader.parameters.sort == "pubdateiso desc", "並び替え条件が不正");

            Debug.Assert(result.responseData != null, "レスポンスデータがない");
            Debug.Assert(result.responseData.numFound == 342, "検索結果総件数が不正");
            Debug.Assert(result.responseData.start == 0, "検索結果データ開始インデックスが不正");
            Debug.Assert(result.responseData.docs.Length == 2, "検索結果データ数が不正");
            SearchDocument doc = result.responseData.docs[0];
            Debug.Assert(doc.id == "https://www.itmedia.co.jp/mobile/articles/1908/02/news131.html", "+0 : docs id 不正");
            Debug.Assert(doc.og_url[0] == "https://www.itmedia.co.jp/mobile/articles/1908/02/news131.html", "+0 : docs url 不正");
        }
    }
}