using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WinFormsApp11
{
    public partial class AccountForm : Form
    {
        private User currentUser;
        private decimal balance;
        private void AccountForm_Load(object sender, EventArgs e)
        {

        }

        public AccountForm(User user)
        {
            InitializeComponent();
            currentUser = user;
            balance = 0;

            labelWelcome.Text = $"Добро пожаловать, {currentUser.Username}!";
            labelBalance.Text = $"Ваш баланс: {balance:C}";

            this.Load += new EventHandler(AccountForm_Load);

        }




        private void buttonLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonTransfer_Click(object sender, EventArgs e)
        {
            TransferForm transferForm = new TransferForm();
            transferForm.Show();
        }

    }
}
