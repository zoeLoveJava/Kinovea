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

using Kinovea.Services;
using System;
using System.Windows.Forms;
using Kinovea.Authentication.UserInterface;

namespace Kinovea.Authentication
{
    public class AuthenticationKernel : IKernel 
    {
        #region Members
        private ToolStripMenuItem mnuAccount = new ToolStripMenuItem();
        private ToolStripMenuItem mnuLogin = new ToolStripMenuItem();
        private ToolStripMenuItem mnuLogout = new ToolStripMenuItem();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        public AuthenticationKernel()
        {
            log.Debug("Module Construction: Authentication.");
        }

        #region IKernel Implementation
        public void BuildSubTree()
        {
            // No sub modules.
        }
        
        public void ExtendMenu(ToolStrip menu)
        {
            // Create a new top-level menu item for Authentication
            mnuAccount.Text = "Account";
            
            mnuLogin.Click += new EventHandler(mnuLogin_OnClick);
            mnuLogout.Click += new EventHandler(mnuLogout_OnClick);

            // Add login/logout items to the Account menu
            mnuAccount.DropDownItems.AddRange(new ToolStripItem[] { 
                mnuLogin, 
                mnuLogout
            });

            // Set merge index to 8 (after Help menu which is at index 7)
            // This will create a new top-level menu item
            mnuAccount.MergeIndex = 8;
            mnuAccount.MergeAction = MergeAction.Insert;

            MenuStrip ThisMenu = new MenuStrip();
            ThisMenu.Items.AddRange(new ToolStripItem[] { mnuAccount });
            ThisMenu.AllowMerge = true;

            ToolStripManager.Merge(ThisMenu, menu);

            RefreshUICulture();
        }
        
        public void ExtendToolBar(ToolStrip _toolbar) {}
        public void ExtendStatusBar(ToolStrip _statusbar) {}
        public void ExtendUI() {}

        public void RefreshUICulture()
        {
            mnuLogin.Text = "Login...";
            mnuLogout.Text = "Logout";
            
            UpdateMenuVisibility();
        }
        
        public bool CloseSubModules()
        {
            return false;
        }
        
        public void PreferencesUpdated()
        {
            RefreshUICulture();
        }
        #endregion

        #region Menu Event Handlers
        private void mnuLogin_OnClick(object sender, EventArgs e)
        {
            FormLogin loginForm = new FormLogin();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                // Login successful, update menu
                UpdateMenuVisibility();
                log.InfoFormat("User logged in: {0}", PreferencesManager.AuthenticationPreferences.Username);
            }
        }
        
        private void mnuLogout_OnClick(object sender, EventArgs e)
        {
            PreferencesManager.AuthenticationPreferences.Clear();
            PreferencesManager.Save();
            UpdateMenuVisibility();
            log.Info("User logged out");
        }
        #endregion

        #region Private Methods
        private void UpdateMenuVisibility()
        {
            bool isLoggedIn = PreferencesManager.AuthenticationPreferences.IsLoggedIn;
            mnuLogin.Visible = !isLoggedIn;
            mnuLogout.Visible = isLoggedIn;
        }
        #endregion
    }
}

