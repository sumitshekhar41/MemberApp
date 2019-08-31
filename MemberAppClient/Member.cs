using System.ComponentModel;

namespace MemberAppClient
{
    public class Member : INotifyPropertyChanged
    {
        private string _id;
        private string _firstName;
        private string _lastName;
        private string _mailId;
        private bool _status;

        public Member()
        {
        }

        public string ID
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyOfPropertyChange("ID");
            }
        }
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                NotifyOfPropertyChange("FirstName");
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                NotifyOfPropertyChange("LastName");
            }
        }

        public string MailId
        {
            get { return _mailId; }
            set
            {
                _mailId = value;
                NotifyOfPropertyChange("MailId");
            }
        }

        public bool Status
        {
            get { return _status; }
            set
            {
                _status = value;
                NotifyOfPropertyChange("Status");
            }
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyOfPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}