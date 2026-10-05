using System.Text.RegularExpressions;

namespace Nk_Colletion_New.Helpers
{
    internal static class FormValidators
    {
        private static readonly Regex NameRegex = new(@"^[\p{L}][\p{L}\s'\-\.]{1,}$", RegexOptions.Compiled);
        // Allow cedula with letters, digits, spaces or hyphens. Validation normalizes to letters+digits and checks length.
        private static readonly Regex CedulaRegex = new(@"^[\p{L}\p{N}\-\s]+$", RegexOptions.Compiled);
        private static readonly Regex PhoneRegex = new(@"^\+?[0-9\s\-]{7,20}$", RegexOptions.Compiled);
        private static readonly Regex EmailRegex = new(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", RegexOptions.Compiled);
        private static readonly Regex UsernameRegex = new(@"^[a-zA-Z0-9_\.]{4,30}$", RegexOptions.Compiled);

        public static bool IsValidName(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            value = value.Trim();
            return value.Length >= 2 && NameRegex.IsMatch(value);
        }

        public static bool IsValidCedula(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            var trimmed = value.Trim();
            // Allow digits, spaces or hyphens in input, but validate by counting digits only.
            if (!CedulaRegex.IsMatch(trimmed))
                return false;

            // Remove non alphanumeric characters and validate char count (6..15)
            var alnumOnly = System.Text.RegularExpressions.Regex.Replace(trimmed, "[^\\p{L}\\p{N}]", "");
            return alnumOnly.Length >= 6 && alnumOnly.Length <= 15;
        }

        public static bool IsValidPhone(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            return PhoneRegex.IsMatch(value.Trim());
        }

        public static bool IsValidEmail(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            return EmailRegex.IsMatch(value.Trim());
        }

        public static bool IsValidUsername(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            return UsernameRegex.IsMatch(value.Trim());
        }
    }
}
