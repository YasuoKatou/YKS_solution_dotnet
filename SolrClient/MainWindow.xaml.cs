using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using solr.client.Service;
using solr.client.dto;

namespace solr.client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SolrClient : Window
    {
        private SolrService webService = new SolrService();

        public SolrClient()
        {
            InitializeComponent();

            // 検索ボタンのクリックイベントを設定
            this.btnSearch.Click += this.btnSearch_Click;
        }

        /// <summary>
        /// 検索ボタンクリックイベント
        /// </summary>        
        void btnSearch_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //Console.WriteLine("検索ワード : " + c.Text);
            string searchWord = this.txtSearchWord.Text;
            if (searchWord == "")
            {
                MessageBox.Show("検索文字列を入力して下さい.", "確認"
                            , System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Question);
                return;
            }
            SolrSearchResponseDto dto = this.webService.searchString<SolrSearchResponseDto>(searchWord);
            this.txtQTime.Text = String.Format("検索時間 : {0} ms", dto.responseHeader.QTime);

            ListView v = this.FindName("lstSearchResultView") as ListView;
            foreach (SearchDocument docItem in dto.responseData.docs)
            {
                // ListViewItem vi = 
                v.Items.Add(docItem.title[0]);
            }
        }
    }
}