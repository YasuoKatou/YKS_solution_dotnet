using System.Runtime.Serialization;

namespace solr.client.dto
{
    [DataContract]
    public class SolrSearchResponseDto
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
}