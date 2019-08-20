using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Web;

using json.util;

namespace solr.client.Service
{
    public class SolrService
    {
        private static readonly string HTTP_SCHEMA = "http://";
        private static readonly string HOST_NAME = "localhost:8983";
        private static readonly string URL_SERVICE = "/solr/web_test/select";
        // private static readonly string SEARCH_OPTIONS = "&fl=id,og_url,title,summary&sort=pubdateiso desc";
        private static readonly Hashtable SEARCH_OPTIONS = new Hashtable
        {
              ["fl"] = "id,og_url,title,summary"    // 取得する項目
            , ["sort"] = "pubdateiso desc"          // 並び替え
        };
        public SolrService()
        {
            Console.WriteLine("SolrService !");
        }
        public T searchString<T>(String keyWord, int start, int rows) where T : new()
        {
            //Console.WriteLine("searchString : key word = " + keyWord);
            // URLを編集
            string getOpts = "q=description" + HttpUtility.UrlEncode(":" + keyWord) + this.makeOptions();
            string url = HTTP_SCHEMA + HOST_NAME + URL_SERVICE + '?' + getOpts;
            url += "&start=" + start.ToString() + "&rows=" + rows.ToString();
            Console.WriteLine("searchString : uri = " + url);

            WebRequest request = WebRequest.Create(url);
            request.Headers.Add("Accept-Language:ja,en-us;q=0.7,en;q=0.3");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Console.WriteLine("searchString : Response　Status = " + response.StatusDescription);

            Stream dataStream = response.GetResponseStream ();
            StreamReader reader = new StreamReader(dataStream);
            string responseData = reader.ReadToEnd();
            // Console.WriteLine("searchString : Response　response data = " + responseData);

            reader.Close ();
            dataStream.Close();
            response.Close();

            JsonUtil util = new JsonUtil();
            return util.paeseJson<T>(responseData);
        }
        private string makeOptions()
        {
            string opts = "";
            foreach (DictionaryEntry entry in SEARCH_OPTIONS)
            {
                opts += "&" + entry.Key + "=" + HttpUtility.UrlEncode(entry.Value.ToString());
            }
            return opts;
        }
    }
}