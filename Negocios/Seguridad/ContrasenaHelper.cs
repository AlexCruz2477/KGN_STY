using System.Security.Cryptography;

namespace Nk_Colletion.Negocios.Seguridad
{
    public static class ContrasenaHelper
    {
        private const int Iteraciones = 100000;
        private const int TamanoSalt = 16;
        private const int TamanoHash = 32;

        public static string CrearHash(string contrasena)
        {
            if (string.IsNullOrWhiteSpace(contrasena))
                throw new ArgumentException("La contraseña no puede estar vacía.");

            byte[] salt = RandomNumberGenerator.GetBytes(TamanoSalt);

            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                contrasena,
                salt,
                Iteraciones,
                HashAlgorithmName.SHA256,
                TamanoHash
            );

            return $"PBKDF2${Iteraciones}${Convert.ToBase64String(salt)}${Convert.ToBase64String(hash)}";
        }

        public static bool EsHashPbkdf2(string? valor)
        {
            return !string.IsNullOrWhiteSpace(valor) &&
                   valor.StartsWith("PBKDF2$", StringComparison.Ordinal);
        }

        public static bool Verificar(string contrasena, string hashGuardado)
        {
            if (string.IsNullOrWhiteSpace(contrasena) ||
                string.IsNullOrWhiteSpace(hashGuardado))
                return false;

            string[] partes = hashGuardado.Split('$');

            if (partes.Length != 4 || partes[0] != "PBKDF2")
                return false;

            if (!int.TryParse(partes[1], out int iteraciones))
                return false;

            try
            {
                byte[] salt = Convert.FromBase64String(partes[2]);
                byte[] hashEsperado = Convert.FromBase64String(partes[3]);

                byte[] hashCalculado = Rfc2898DeriveBytes.Pbkdf2(
                    contrasena,
                    salt,
                    iteraciones,
                    HashAlgorithmName.SHA256,
                    hashEsperado.Length
                );

                return CryptographicOperations.FixedTimeEquals(
                    hashEsperado,
                    hashCalculado
                );
            }
            catch
            {
                return false;
            }
        }
    }
}