using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using Newtonsoft.Json;

namespace WinFormsApp11
{
    public partial class TransferForm : Form
    {
        private const string UsersFilePath = "user.json";
        private List<User> users;

        public TransferForm()
        {
            InitializeComponent();
            LoadUsers();

        }

        private void LoadUsers()
        {
            try
            {
                if (File.Exists(UsersFilePath))
                {
                    string json = File.ReadAllText(UsersFilePath);
                    users = JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();
                }
                else
                {
                    users = new List<User>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки пользователей: " + ex.Message);
                users = new List<User>();
            }
        }

        private void SaveUsers()
        {
            try
            {
                string json = JsonConvert.SerializeObject(users, Formatting.Indented);
                File.WriteAllText(UsersFilePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения пользователей: " + ex.Message);
            }
        }

        private void buttonTransfer_Click(object sender, EventArgs e)
        {
            string senderUsername = textBoxSenderUsername.Text.Trim();
            string recipientUsername = textBoxRecipientUsername.Text.Trim();
            string amountText = textBoxTransferAmount.Text.Trim();

            if (string.IsNullOrEmpty(senderUsername))
            {
                labelTransferStatus.Text = "Введите логин отправителя.";
                return;
            }

            if (string.IsNullOrEmpty(recipientUsername))
            {
                labelTransferStatus.Text = "Введите логин получателя.";
                return;
            }

            if (senderUsername.Equals(recipientUsername, StringComparison.OrdinalIgnoreCase))
            {
                labelTransferStatus.Text = "Нельзя переводить самому себе.";
                return;
            }

            if (!decimal.TryParse(amountText, out decimal amount) || amount <= 0)
            {
                labelTransferStatus.Text = "Введите корректную сумму перевода.";
                return;
            }

            var senderUser = users.FirstOrDefault(u => u.Username.Equals(senderUsername, StringComparison.OrdinalIgnoreCase));
            var recipientUser = users.FirstOrDefault(u => u.Username.Equals(recipientUsername, StringComparison.OrdinalIgnoreCase));

            if (senderUser == null)
            {
                labelTransferStatus.Text = "Пользователь-отправитель не найден.";
                return;
            }

            if (recipientUser == null)
            {
                labelTransferStatus.Text = "Пользователь-получатель не найден.";
                return;
            }

            if (senderUser.Balance < amount)
            {
                labelTransferStatus.Text = "Недостаточно средств для перевода.";
                return;
            }

            senderUser.Balance -= amount;
            recipientUser.Balance += amount;

            SaveUsers();

            labelTransferStatus.Text = $"Перевод {amount.ToString("C")} от {senderUser.Username} к {recipientUser.Username} выполнен.";
        }
    }
}
