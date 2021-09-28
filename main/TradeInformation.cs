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
                                double rate,
                                string currency,
                                double exchangeCurrency,
                                object marketValue,
                                object commision,
                                object totalTransactionCost)
        {
            RecordNumber = recordNumber.Trim();
            TypeOfAction = typeOfAction.Trim();
            Name = name.Trim() ?? "NULL";
            IsinCode = isinCode;
            TradeIdentifier = tradeIdentifier.Trim() ?? "NULL";
            StockExchange = stockExchange;
            DateOfAction = dateOfAction.Trim();
            PaymentDate = paymentDate.Trim();
            Quantity = (double)(quantity ?? 0.0);
            Rate = rate;
            ExchangeCurrency = exchangeCurrency;
            Currency = currency.Trim();
            MarketValue = marketValue;
            TotalTransactionCost = totalTransactionCost ?? 0;
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
        public double Rate { get; }
        public double ExchangeCurrency { get; }
        public string Currency { get; }
        public object MarketValue { get; }
        public object Commision { get; }
        public object TotalTransactionCost { get; }
    }
}
