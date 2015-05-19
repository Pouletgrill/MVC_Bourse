using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaasOne;
using MaasOne.Base;
using MaasOne.Finance.YahooFinance;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] symbole = new string[] { "MSFT", "GOOG", "YHOO" };
            List<double> prix = new List<double>();
            YahooFinance YF = new YahooFinance();

            prix = YF.GetStockPriceFromSymbolList(symbole);
            for (int i = 0; i < symbole.Count(); i++)
            {
                listBox1.Items.Add(symbole[i]);
            }

            if (prix != null)
            {
                for (int i = 0; i < prix.Count; i++)
                {
                    listBox2.Items.Add(prix[i].ToString());
                }
            }
            else
            {
                MessageBox.Show("une erreur de connexion est survennue");
            }
            //QuotesDownload dl = new QuotesDownload();
            //DownloadClient<QuotesResult> baseDl = dl;

            //QuotesDownloadSettings settings = dl.Settings;
            //settings.IDs = new string[] { "MSFT", "GOOG", "YHOO" };
            //settings.Properties = new QuoteProperty[] { QuoteProperty.Symbol,
            //                                   QuoteProperty.Name, 
            //                                   QuoteProperty.LastTradePriceOnly
            //                                 };
            //SettingsBase baseSettings = baseDl.Settings;

            //Response<QuotesResult> resp = baseDl.Download();

            //ConnectionInfo connInfo = resp.Connection;
            //if (connInfo.State == MaasOne.Base.ConnectionState.Success)
            //{
            //   QuotesResult result = resp.Result;
            //   label1.Text = result.Items[0].LastTradePriceOnly.ToString();
            //   LB_Prix_Goo.Text = result.Items[1].LastTradePriceOnly.ToString();
            //   LB_Prix_Yahoo.Text = result.Items[2].LastTradePriceOnly.ToString();
            //   //...
            //}
            //else
            //{
            //   Exception ex = connInfo.Exception;
            //   MessageBox.Show(ex.Message);
            //}
        }
    }
}
