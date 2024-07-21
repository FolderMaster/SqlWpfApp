using Model;

namespace ViewModel.Classes.Connections.MsSqlServer
{
    public class MsSqlServerCredentialData : PropertyUpdaterService
    {
        public string _user;

        public string _password;

        public string User
        {
            get => _user;
            set => UpdateProperty(ref _user, value,
                ValueValidator.AssertStringIsNotNullOrEmpty);
        }

        public string Password
        {
            get => _password;
            set => UpdateProperty(ref _password, value,
                ValueValidator.AssertStringIsNotNullOrEmpty);
        }
    }
}
