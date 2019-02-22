namespace BirthdayApp
{
    public class User
    {
        private string _age;
        private string _sign;

        public string Age
        {
            get { return _age; }
            set { _age = value; }
        }

        public string Sign
        {
            get { return _sign; }
            set { _sign = value; }
        }
    }
}