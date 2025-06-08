using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

namespace WinFormsApp11
{
    public class UserManager
    {
        private const string FilePath = "users.json";
        public List<User> Users { get; private set; } = new List<User>();

        public UserManager()
        {
            LoadUsers();
        }

        public void LoadUsers()
        {
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
           //     Users = JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
            }
        }

        public void SaveUsers()
        {
            string json = JsonSerializer.Serialize(Users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }

        public bool AddUser(User user)
        {
            if (Users.Exists(u => u.Username == user.Username))
                return false;

            Users.Add(user);
            SaveUsers();
            return true;
        }
    }
}