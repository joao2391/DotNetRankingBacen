namespace DotNet.Ranking.Bacen
{
    public static class Constants
    {
        public const string URL_BACEN = "https://www3.bcb.gov.br/ranking/";
        public const string BREAKLINE_SPACE = "\n&nbsp;";
        public const string SPACE_BREAKLINE = "&nbsp;\n";
        public const string SPACE = "&nbsp;";
        public const string BREAKLINE = "\n";
        public const string XPATH_TABLE_BF = "//*[@id='container']/div[2]/div/div[1]/div[1]/span/span/table";
        public const string XPATH_TABLE_DBF = "//*[@id='container']/div[2]/div/div[1]/div[2]/span/span/table";
        public const string XPATH_TABLE_REC = "//*[@id='container']/div[2]/div/div[2]/div[1]/span/span/table";
        public const string XPATH_TABLE_ADM = "//*[@id='container']/div[2]/div/div[2]/div[2]/span/span/table";
        public const int AMOUNT_OBJECTS = 3;
    }
}