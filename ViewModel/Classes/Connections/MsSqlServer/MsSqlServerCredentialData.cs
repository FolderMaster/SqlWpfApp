using Model;
using Model.ObservableObjects;

namespace ViewModel.Classes.Connections.MsSqlServer
{
    public class MsSqlServerCredentialData : ObservableObject
    {
        public static readonly ObservableProperty UserProperty = RegisterProperty
            (typeof(MsSqlServerCredentialData), nameof(User), null,
            [(o) => ValueValidator.AssertStringIsNotNullOrEmpty((string)o.NewValue, o.Property.Name)]);

        public string User
        {
            get => (string)GetValue(UserProperty);
            set => SetValue(value, UserProperty);
        }

        public string Password { get; set; }

        static MsSqlServerCredentialData() { }
    }
}
