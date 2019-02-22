namespace BirthdayApp
{
    public class User
    {
        private string _birthday;
        private string _age;
        private string _zodiac;
        private string _animal;

        public string Birthday {
            get { return _birthday; }
            set { _birthday = value; }
        }

        public string Age
        {
            get { return _age; }
            set { _age = value; }
        }

        public string Zodiac
        {
            get { return _zodiac; }
            set { _zodiac = value; }
        }

        public string Animal
        {
            get { return _animal; }
            set { _animal = value; }
        }
    }
}