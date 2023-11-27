namespace LshGlobalSetting
{
    public class ProgressReport
    {
        private string _message;
        private string _test;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
            }
        }

        public string Test { get => _test; set => _test = value; }
    }
}
