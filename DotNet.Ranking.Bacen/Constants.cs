namespace DotNet.Ranking.Bacen
{
    public static class Constants
    {
        public const string URL_BACEN = "https://www3.bcb.gov.br/ranking/";
        public const string BREAKLINE_SPACE = "\n&nbsp;";
        public const string SPACE_BREAKLINE = "&nbsp;\n";
        public const string SPACE = "&nbsp;";
        public const string BTN_BACK = "btn-back";
        public const string BREAKLINE = "\n";
        public const string XPATH_TABLE_BF = "//*[@id='container']/div[2]/div/div[1]/div[1]/span/span/table";
        public const string XPATH_TABLE_DBF = "//*[@id='container']/div[2]/div/div[1]/div[2]/span/span/table";
        public const string XPATH_TABLE_REC = "//*[@id='container']/div[2]/div/div[2]/div[1]/span/span/table";
        public const string XPATH_TABLE_ADM = "//*[@id='container']/div[2]/div/div[2]/div[2]/span/span/table";
        public const string XPATH_ALL_DBF = "/html/body/div/div[2]/div/div[1]/div[2]/div/button";
        public const string XPATH_ALL_CLAIMS = "/html/body/div/div[2]/div/div[2]/div[1]/div/button";
        public const string XPATH_ALL_ADM = "/html/body/div/div[2]/div/div[2]/div[2]/div/button";
        public const string XPATH_NODES_REC = "/html[1]/body[1]/div[1]/form[1]/div[4]/div[2]";
        public const string XPATH_NODES_DBF = "/html[1]/body[1]/div[1]/form[1]/div[4]/span[1]/span[1]/div[2]";
        public const string XPATH_NODES_BF = "/html[1]/body[1]/div[1]/form[1]/div[4]/span[1]/span[1]/div[2]";
        public const string XPATH_NODES_ADM = "/html[1]/body[1]/div[1]/form[1]/div[4]/span[1]/span[1]/div[2]";
        public const int QUANTIDADE_OBJETOS_TRES = 3;
        public const int QUANTIDADE_OBJETOS_DEZ = 10;
        public const int QUANTIDADE_OBJETOS_VINTE_E_QUATRO = 24;
        public const int QUANTIDADE_OBJETOS_OITENTA_E_QUATRO = 84;
        public const int QUANTIDADE_OBJETOS_TRINTA_E_CINCO = 35;
    }
}