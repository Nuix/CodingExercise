namespace InvestmentAPINuix_JBF.Classes
{
    public class APIError
    {
        public int Status {  get; set; }
        public string Error { get; set; } = string.Empty;
        public string ErrorMessage {  get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } 
    }
}
