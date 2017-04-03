namespace BossrApi
{
    public static class ValidationRules
    {
        public const int WorldNameMaxLength = 30;

        public const int UserUsernameMinLength = 3;
        public const int UserUsernameMaxLength = 30;
        public const string UserUsernameRegex = "^[a-zA-Z0-9_]*$";
        public const string UserUsernameRegexErrorMessage = "Only alphanumeric characters (A-Z, a-z, 0-9) and underscores are allowed.";

        public const int UserPasswordMinLength = 8;
        public const int UserPasswordMaxLength = 100;
    }
}
