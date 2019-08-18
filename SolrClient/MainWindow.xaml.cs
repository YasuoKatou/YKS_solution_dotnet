using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Diagnostics;
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
    public class SearchInfo
    {
        public string keyWord { get; set; }
        public int startIndex { get; set; }
        public int numFound { get; set; }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SolrClient : Window
    {
        private SolrService webService = new SolrService();
        private SearchInfo searchInfo = null;

        public SolrClient()
        {
            InitializeComponent();

            // 検索ボタンのクリックイベントを設定
            this.btnSearch.Click += this.btnSearch_Click;
            // 検索結果一覧のダブルクリックイベントを設定
            this.lstSearchResultView.MouseDoubleClick += this.resultListDoubleClicked;
            // 前ページ表示イベントを設定
            this.btnPrev.Click += this.btnPrev_Click;
            // 次ページ表示イベントを設定
            this.btnNext.Click += this.btnNext_Click;
        }
        /// <summary>
        /// 検索結果一覧表示の行をダブルクリック
        /// </summary>
        void resultListDoubleClicked(object sender, RoutedEventArgs e)
        {
            if (sender is ListBox)
            {
                // Console.WriteLine("resultListDoubleClicked : " + (sender as ListBox).SelectedItem.ToString());
                if ((sender as ListBox).SelectedItem is SearchDocument)
                {
                    SearchDocument doc = ((sender as ListBox).SelectedItem as SearchDocument);
                    this.openDefaultWebBrowsew(doc.og_url[0]);
                    return;
                }
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// 指定のuriをWebブラウザで開く
        /// </summary>
        private void openDefaultWebBrowsew(string uri)
        {
            try {
                var app = new ProcessStartInfo(uri);
                app.UseShellExecute = true;
                Process.Start(app);
            } catch (System.ComponentModel.Win32Exception ex) {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.Message, "エラー"
                            , System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 検索ボタンクリックイベント
        /// </summary>        
        void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine("検索ワード : " + c.Text);
            string searchWord = this.txtSearchWord.Text;
            if (searchWord == "")
            {
                MessageBox.Show("検索文字列を入力して下さい.", "確認"
                            , System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Question);
                return;
            }
            this.doSearch(searchWord, 0, this.getRsultDocumentCount());
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        private void doSearch(string searchWord, int start, int rows)
        {
            SolrSearchResponseDto dto = this.webService.searchString<SolrSearchResponseDto>(searchWord, start, rows);
            var resp = dto.responseData;
            var num = rows + resp.start;
            if (num > resp.numFound) num = resp.numFound;
            this.txtNums.Text  = String.Format("{0} - {1} ({2})", resp.start + 1, num, resp.numFound);
            this.txtQTime.Text = String.Format("検索時間 : {0} ms", dto.responseHeader.QTime);

            Binding bind = new Binding();
            bind.Source = dto.responseData.docs.ToList();
            this.lstSearchResultView.SetBinding(ListBox.ItemsSourceProperty, bind);

            this.searchInfo = new SearchInfo();
            this.searchInfo.keyWord = searchWord;
            this.searchInfo.startIndex = start;
            this.searchInfo.numFound = resp.numFound;
        }
        /// <summary>
        /// 取得するドキュメント数を取得する.
        /// </summary>
        private int getRsultDocumentCount()
        {
            if (this.cbxResultRows.SelectedItem is ComboBoxItem)
            {
                return int.Parse((this.cbxResultRows.SelectedItem as ComboBoxItem).Tag.ToString());
            }
            return 10;
        }
        /// <summary>
        /// 前ページ表示ボタンクリック
        /// </summary>
        void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            if (this.searchInfo == null) return;

            int rows = this.getRsultDocumentCount();
            int start = this.searchInfo.startIndex - rows;
            if (start < 0) start = 0;

            this.doSearch(this.searchInfo.keyWord, start, this.getRsultDocumentCount());
        }
        /// <summary>
        /// 次ページ表示ボタンクリック
        /// </summary>
        void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (this.searchInfo == null) return;

            int rows = this.getRsultDocumentCount();
            int start = this.searchInfo.startIndex + rows;
            if (start > searchInfo.numFound) return;

            this.doSearch(this.searchInfo.keyWord, start, this.getRsultDocumentCount());
        }
    }
    // [ValueConversion(typeof(sourceType), typeof(targetType))]
    public class ListTitleDataConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is SearchDocument)
            {
                return (value as SearchDocument).title[0];
            }
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}