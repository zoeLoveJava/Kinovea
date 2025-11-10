/*
Copyright © Joan Charmant 2024.
jcharmant@gmail.com 

This file is part of Kinovea.

Kinovea is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License version 2 
as published by the Free Software Foundation.

Kinovea is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with Kinovea. If not, see http://www.gnu.org/licenses/.

*/

using System;
using System.Windows.Forms;
using Kinovea.Services;

namespace Kinovea.Authentication.UserInterface
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
            this.Text = "Login";
            lblUsername.Text = "Username:";
            lblPassword.Text = "Password:";
            btnLogin.Text = "Login";
            btnCancel.Text = "Cancel";
            
            // Load saved username if available
            if (!string.IsNullOrEmpty(PreferencesManager.AuthenticationPreferences.Username))
            {
                txtUsername.Text = PreferencesManager.AuthenticationPreferences.Username;
                chkRememberUsername.Checked = PreferencesManager.AuthenticationPreferences.RememberUsername;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Please enter a username.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter a password.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            // TODO: Implement actual authentication logic here
            // For now, this is a simple demo that accepts any username/password
            // In a real implementation, you would:
            // 1. Call an authentication service/API
            // 2. Validate credentials
            // 3. Store tokens/session information
            
            // Simulate authentication (replace with real authentication)
            bool authenticated = AuthenticateUser(username, password);
            
            if (authenticated)
            {
                // Save authentication info
                PreferencesManager.AuthenticationPreferences.Username = username;
                PreferencesManager.AuthenticationPreferences.IsLoggedIn = true;
                PreferencesManager.AuthenticationPreferences.RememberUsername = chkRememberUsername.Checked;
                
                PreferencesManager.Save();
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool AuthenticateUser(string username, string password)
        {
            // TODO: Replace this with actual authentication logic
            // This is a placeholder that accepts any non-empty credentials
            // In production, you would:
            // - Call an authentication API
            // - Validate against a database
            // - Use OAuth/OpenID Connect
            // - etc.
            if (username == "admin" && password == "admin")
            {
                return true;
            }
            return false;
        }
    }
}

