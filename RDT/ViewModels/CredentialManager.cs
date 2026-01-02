using CredentialManagement;
using System;
using System.Windows;

namespace RDT.ViewModels
{
    public class CredentialManager
    {
        public static bool IsValidCredential(Credential cM)
        {
            try
            {
                if (!cM.Exists())
                {
                    MessageBox.Show($"Not any Credential Exist For: {cM.Target}");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false;
            }
        }
    }
}
