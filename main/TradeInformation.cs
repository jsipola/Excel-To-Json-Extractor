using System;

namespace main
{
    class TradeInformation
    {
        public TradeInformation(string recordNumber,
                                string typeOfAction,
                                string name,
                                string isinCode,
                                string tradeIdentifier,
                                string stockExchange,
                                string dateOfAction,
                                string paymentDate,
                                object quantity,
                                object price,
                                double currency,
                                string currencyRate,
                                object marketValue,
                                object commision,
                                object totalTransactionCost)
        {
            RecordNumber = recordNumber;
            TypeOfAction = typeOfAction;
            Name = name ?? "NULL";
            IsinCode = isinCode;
            TradeIdentifier = tradeIdentifier ?? "NULL";
            StockExchange = stockExchange;
            DateOfAction = dateOfAction;
            PaymentDate = paymentDate;
            Quantity = (double)(quantity ?? 0.0);
            Price = price;
            Currency = currency;
            CurrencyRate = currencyRate;
            MarketValue = marketValue;
            TotalTransactionCost = totalTransactionCost ?? "NULL";
        }

        public string RecordNumber { get; }
        public string TypeOfAction { get; }
        public string Name { get; }
        public string IsinCode { get; }
        public string TradeIdentifier { get; }
        public string StockExchange { get; }
        public string DateOfAction { get; }
        public string PaymentDate { get; }
        public double Quantity { get; }
        public object Price { get; }
        public double Currency { get; }
        public string CurrencyRate { get; }
        public object MarketValue { get; }
        public object Commision { get; }
        public object TotalTransactionCost { get; }

        public override string ToString()
        {
            return String.Format("| {0,-40} | {1,-10} | {2,10} | {3,10} |", Name.ToString(), TypeOfAction.ToString(), Quantity.ToString(), Price.ToString());
        }
    }
}
