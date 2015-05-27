using MaasOne.Base;
using MaasOne.Finance.YahooFinance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bourse
{
    public class YahooFinance
    {
        public List<double> GetStockPriceFromSymbolList(string[] Symbole)
        {
            List<double> Resultat = new List<double>();
            QuotesDownload dl = new QuotesDownload();
            DownloadClient<QuotesResult> baseDl = dl;

            QuotesDownloadSettings settings = dl.Settings;
            settings.IDs = Symbole;
            settings.Properties = new QuoteProperty[] { QuoteProperty.Symbol,
                                            QuoteProperty.Name, 
                                            QuoteProperty.LastTradePriceOnly
                                          };
            SettingsBase baseSettings = baseDl.Settings;

            Response<QuotesResult> resp = baseDl.Download();

            ConnectionInfo connInfo = resp.Connection;
            if (connInfo.State == MaasOne.Base.ConnectionState.Success)
            {
                QuotesResult result = resp.Result;
                for (int i = 0; i < result.Items.Count(); i++)
                {
                    Resultat.Add(Convert.ToDouble(result.Items[i].LastTradePriceOnly.ToString()));
                }
                return Resultat;
            }
            else
            {
                return null;
            }
        }

        public double GetStockPriceFromSymbol(string Symbole)
        {
            double Resultat = new double();
            QuotesDownload dl = new QuotesDownload();
            DownloadClient<QuotesResult> baseDl = dl;

            QuotesDownloadSettings settings = dl.Settings;
            settings.IDs = new string[] { Symbole };
            settings.Properties = new QuoteProperty[] { QuoteProperty.Symbol,
                                            QuoteProperty.Name, 
                                            QuoteProperty.LastTradePriceOnly
                                          };
            SettingsBase baseSettings = baseDl.Settings;

            Response<QuotesResult> resp = baseDl.Download();

            ConnectionInfo connInfo = resp.Connection;
            if (connInfo.State == MaasOne.Base.ConnectionState.Success)
            {
                QuotesResult result = resp.Result;
                Resultat = result.Items[0].LastTradePriceOnly;
                return Resultat;
            }
            else
            {
                return 0;
            }
        }
    }
}