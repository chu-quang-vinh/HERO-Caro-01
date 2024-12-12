using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace WindowsFormsApp10
{
    public class UserManager
    {
        
            private Dictionary<string, string> users = new Dictionary<string, string>();
            private string filePath;

            public UserManager(string filePath)
            {
                this.filePath = filePath;
                LoadUsersFromFile(); // Nạp dữ liệu khi khởi tạo
            }

            private void LoadUsersFromFile()
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    users = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                }
            }

            private void SaveUsersToFile()
            {
                string json = JsonConvert.SerializeObject(users);
                File.WriteAllText(filePath, json);
            }


            public bool Register(string username, string password)
            {
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                    return false;

                if (users.ContainsKey(username))
                    return false;

                users[username] = password;
                SaveUsersToFile(); // Lưu dữ liệu sau khi đăng ký
                return true;
            }
        public bool Login(string username, string password) // Phương thức Login
        {
            return users.ContainsKey(username) && users[username] == password;
        }
    }
    
}