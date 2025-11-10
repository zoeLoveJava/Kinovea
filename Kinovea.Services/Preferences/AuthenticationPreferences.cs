#region License
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
#endregion

using System;
using System.Xml;

namespace Kinovea.Services
{
    public class AuthenticationPreferences : IPreferenceSerializer
    {
        #region Properties
        public string Name
        {
            get { return "Authentication"; }
        }

        public string Username
        {
            get { return username; }
            set { username = value ?? string.Empty; }
        }

        public bool IsLoggedIn
        {
            get { return isLoggedIn; }
            set { isLoggedIn = value; }
        }

        public bool RememberUsername
        {
            get { return rememberUsername; }
            set { rememberUsername = value; }
        }
        #endregion

        #region Members
        private string username = string.Empty;
        private bool isLoggedIn = false;
        private bool rememberUsername = false;
        #endregion

        public AuthenticationPreferences()
        {
        }

        public void Clear()
        {
            username = string.Empty;
            isLoggedIn = false;
            if (!rememberUsername)
            {
                username = string.Empty;
            }
        }

        public void WriteXML(XmlWriter writer)
        {
            writer.WriteElementString("Username", username ?? string.Empty);
            writer.WriteElementString("IsLoggedIn", isLoggedIn ? "true" : "false");
            writer.WriteElementString("RememberUsername", rememberUsername ? "true" : "false");
        }

        public void ReadXML(XmlReader reader)
        {
            reader.ReadStartElement();

            while (reader.NodeType == XmlNodeType.Element)
            {
                switch (reader.Name)
                {
                    case "Username":
                        username = reader.ReadElementContentAsString();
                        break;
                    case "IsLoggedIn":
                        isLoggedIn = XmlHelper.ParseBoolean(reader.ReadElementContentAsString());
                        break;
                    case "RememberUsername":
                        rememberUsername = XmlHelper.ParseBoolean(reader.ReadElementContentAsString());
                        break;
                    default:
                        reader.ReadOuterXml();
                        break;
                }
            }

            reader.ReadEndElement();
        }
    }
}

