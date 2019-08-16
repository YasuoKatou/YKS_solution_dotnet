using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            // 検索結果一覧のダブルクリックイベントを設定
            this.lstSearchResultView.MouseDoubleClick += this.resultListDoubleClicked;
        }

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

        private void openDefaultWebBrowsew(string uri)
        {
            try {
                System.Diagnostics.Process.Start("explorer.exe", uri);
            } catch (System.ComponentModel.Win32Exception ex) {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.Message, "エラー"
                            , System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
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

            Binding bind = new Binding();
            bind.Source = dto.responseData.docs.ToList();
            this.lstSearchResultView.SetBinding(ListBox.ItemsSourceProperty, bind);
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