using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.IO; // Add this line for File access
using System;
using System.Windows.Forms;
public class UserManager
{
    private Dictionary<string, Dictionary<string, string>> users = new Dictionary<string, Dictionary<string, string>>();
    private string filePath;

    public UserManager(string filePath)
    {
        users = new Dictionary<string, Dictionary<string, string>>(); // Khởi tạo TRƯỚC
        this.filePath = filePath;
        LoadUsersFromFile();
    }

    private void LoadUsersFromFile()
    {
        if (File.Exists(filePath))
        {
            try
            {
                string json = File.ReadAllText(filePath);

                // ONLY create a new dictionary if deserialization fails.
                // Otherwise, populate the existing dictionary.
                var loadedUsers = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(json);
                if (loadedUsers != null)
                {
                    users = loadedUsers; // Use the loaded data
                }
                else
                {
                    // Handle deserialization failure (e.g., invalid JSON).
                    MessageBox.Show("Invalid JSON in users file. Creating a new user database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (JsonReaderException ex)
            {
                // Explicitly handle JSON parsing errors.
                MessageBox.Show("Error reading JSON file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } // NO ELSE BLOCK HERE. If the file doesn't exist, use the empty dictionary created in the constructor.
    }

    private void SaveUsersToFile()
    {
        string json = JsonConvert.SerializeObject(users, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }

    public bool Register(string username, string password, string email, string fullName)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            return false;

        if (users.ContainsKey(username))
            return false;

        // Lưu thông tin người dùng dưới dạng Dictionary
        users[username] = new Dictionary<string, string>
    {
        { "password", HashPassword(password) },
        { "email", email },
        { "fullName", fullName }
    };

        SaveUsersToFile(); // Lưu dữ liệu sau khi đăng ký
        return true;
    }


    public Dictionary<string, string> GetUserInfo(string username)
    {
        if (users.ContainsKey(username))
        {
            return users[username]; // Trả về thông tin người dùng
        }
        return null;
    }

    public bool Login(string username, string password)
    {
        return users.ContainsKey(username) && users[username]["password"] == HashPassword(password);
    }

    private string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
