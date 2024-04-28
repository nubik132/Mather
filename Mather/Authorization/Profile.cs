using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Mather.Authorization
{
    [Serializable]
    public class Profile
    {
        public enum Type { Student, Teacher }
        Type type;
        public string Login { get; set; }
        public string Password { get; set; }

        public Profile(string login, string password, Type type = Type.Student)
        {
            Login = login;
            Password = password;
            this.type = type;
        }
        public Type GetProfileType() { return type; }
        public static Profile? CheckProfile(string login, string password)
        {
            string SavePath = Environment.ExpandEnvironmentVariables(@"%appdata%\Mather");
            if (!Directory.Exists(SavePath))
            {
                Directory.CreateDirectory(SavePath);
            }
            foreach (var file in Directory.EnumerateFiles(SavePath))
            {
                bool? isExist = Check(file, login, password);
                if (isExist != null)
                    return new Profile(login, password, (bool)isExist ? Type.Teacher : Type.Student);
            }
            return null;
        }
        public void SaveProfile()
        {
            string SavePath = Environment.ExpandEnvironmentVariables(@"%appdata%\Mather");
            if (!Directory.Exists(SavePath))
            {
                Directory.CreateDirectory(SavePath);
            }
            Write(Path.Combine(SavePath, Login +  ".mp"));
        }
        private static string GetHash(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        private void Write(string filePath)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
            {
                // Получение хэшей логина и пароля
                string loginHash = GetHash(Login);
                string passwordHash = GetHash(Password);

                // Получение номера типа из перечисления Type
                int typeNumber = (int)type;

                // Запись данных в файл
                writer.Write(loginHash);
                writer.Write(passwordHash);
                writer.Write(typeNumber);
            }
        }
        private static bool? Check(string filePath, string login, string password)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {
                // Чтение данных из файла
                string storedLoginHash = reader.ReadString();
                string storedPasswordHash = reader.ReadString();
                int storedTypeNumber = reader.ReadInt32();

                // Получение хэшей введенного логина и пароля
                string enteredLoginHash = GetHash(login);
                string enteredPasswordHash = GetHash(password);

                // Сравнение хэшей и номера типа
                bool isExist = storedLoginHash == enteredLoginHash && storedPasswordHash == enteredPasswordHash;
                if (isExist)
                    return storedTypeNumber == 1;
                return null;
            }
        }
    }
}
