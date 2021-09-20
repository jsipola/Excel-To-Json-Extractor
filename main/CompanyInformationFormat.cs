using System;
using System.Collections.Generic;

namespace main
{
    class CompanyInformationFormat
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<TradeAction> Buys { get; set; }
        
        public IList<TradeAction> Sells { get; set; }

        /* TODO Add dividend */
        public IList<TradeAction> Dividend { get; set; }

        public CompanyInformationFormat()
        {
            Buys = new List<TradeAction>();
            Sells = new List<TradeAction>();
            Dividend = new List<TradeAction>();
        }

        internal void AddBuy(TradeAction trade)
        {
            Buys.Add(trade);
        }

        internal void AddSell(TradeAction trade)
        {
            Sells.Add(trade);
        }
    }
}
