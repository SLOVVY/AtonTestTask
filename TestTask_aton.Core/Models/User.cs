using System.Text.RegularExpressions;

namespace TestTask_aton.Core.Models
{
    public class User
    {
        public Guid Id { get; }
        public string Login { get; } = string.Empty;
        public string Password { get; } = string.Empty;
        public string Name { get; } = string.Empty;
        public int Gender { get; } = 2;
        public DateTime? BirthDay { get; } = null;
        public bool IsAdmin { get; } = false;
        public DateTime CreatedAt { get; }
        public string CreatedBy { get; } = string.Empty;
        public DateTime ModifiedAt { get; }
        public string ModifiedBy { get; } = string.Empty;
        public DateTime RevokedAt { get; }
        public string RevokeddBy { get; } = string.Empty;

        private User (
            Guid id, 
            string login, 
            string password, 
            string name, 
            int gender, 
            DateTime? birthDay, 
            bool isAdmin, 
            DateTime createdAt, 
            string createdBy, 
            DateTime modifiedAt, 
            string modifiedBy, 
            DateTime revokedAt, 
            string revokeddBy)
        {
            Id = id;
            Login = login;
            Password = password;
            Name = name;
            Gender = gender;
            BirthDay = birthDay;
            IsAdmin = isAdmin;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
            ModifiedAt = modifiedAt;
            ModifiedBy = modifiedBy;
            RevokedAt = revokedAt;
            RevokeddBy = revokeddBy;
        }

        public static bool IsAlphaNumeric(string value)
        {
            Regex regex = new Regex("^[a-zA-Z0-9]+$");

            return regex.IsMatch(value);
        }

        public static bool IsRusOrEngLetters(string value)
        {
            Regex regex = new Regex("^[a-zA-Zа-яА-ЯёЁ]+$");

            return regex.IsMatch(value);
        }

        public static (User user, string Error) Create(
            Guid id,
            string login,
            string password,
            string name,
            int gender,
            DateTime? birthDay,
            bool isAdmin,
            DateTime createdAt,
            string createdBy,
            DateTime modifiedAt,
            string modifiedBy,
            DateTime revokedAt,
            string revokedBy)
        {
            var error = string.Empty;

            if (!IsAlphaNumeric(login)) error += "Логин не соответствует правилам создания логина! " +
                    "Логин должен содержать только латинские буквы и/или цифры";

            if (!IsAlphaNumeric(password)) error += "\n Пароль не соответствует правилам создания пароля! " +
                    "Пароль должен содержать только латинские буквы и/или цифры";

            if (!IsRusOrEngLetters(name)) error += "\n Имя пользователя не соответствует правилам создания имени! " +
                    "Имя должно содержать только латинские и/или русские буквы";

            var user = new User(
                id,
                login, 
                password, 
                name, 
                gender, 
                birthDay, 
                isAdmin, 
                createdAt, 
                createdBy, 
                modifiedAt, 
                modifiedBy, 
                revokedAt, 
                revokedBy);

            return (user, error);
        }
    }
}
