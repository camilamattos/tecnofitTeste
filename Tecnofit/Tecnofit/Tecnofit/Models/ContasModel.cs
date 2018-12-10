namespace Tecnofit.Models
{
    public class ContasModel
    {
        public string origin { get; set; }
        public int id { get; set; }
        public int companyId { get; set; }
        public string dateCredit { get; set; }
        public string expirationDate { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int grossValue { get; set; }
        public int penaltyValue { get; set; }
        public int discountValue { get; set; }
        public int installment { get; set; }
        public int installments { get; set; }
        public int status { get; set; }
        public string accountType { get; set; }
        public int bankAccountId { get; set; }
        public string bankAccount { get; set; }
        public int paymentMethodId { get; set; }
        public string paymentMethod { get; set; }
        public int billingMethodId { get; set; }
        public object billingMethod { get; set; }
        public int accountCategoryId { get; set; }
        public int centerResponsibilityId { get; set; }
        public string centerResponsibility { get; set; }
        public bool transference { get; set; }

        public string ImageList { get; set; }

        private string _netValue;
        public string netValue
        {
            get { return _netValue; }
            set
            {
                _netValue = $"R$ {value}";
            }
        }


        private string _accountCategory;
        public string accountCategory
        {
            get { return _accountCategory; }
            set
            {
                if (value.Contains("ENTRADA"))
                {
                    _accountCategory =  "ENTRADA";
                    ImageList = "rightArrow.png";
                }
                else
                {
                    _accountCategory = "SAIDA";
                    ImageList = "leftArrow.png";
                }
            }
        }
    }
}
