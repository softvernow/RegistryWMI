using System;
using System.Management;
using System.Windows.Forms;

namespace RegistryWMI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private String Get_Registry_Values()
        {
            String result = String.Empty;
            ManagementScope scope = null;
            try
            {
                ConnectionOptions oConn = new ConnectionOptions();
                oConn.Impersonation = System.Management.ImpersonationLevel.Impersonate;
                //oConn.Username = "userName";
                //oConn.Password = "password";
                //oConn.Authority = "NTLMDOMAIN:MY_DOMAIN";

                String REGISTRY_KEY_NAME = @"SOFTWARE\WOW6432Node\Apple Inc.\Apple Application Support";
                String REGISTRY_VAlUE_NAME = "InstallDir";

                scope = new ManagementScope(@"\\" + "PCNAME" + @"\root\default", oConn);
                scope.Connect();
                ManagementClass registry = new ManagementClass(scope, new ManagementPath("StdRegProv"), null);

                //used to String values 
                ManagementBaseObject inParams = registry.GetMethodParameters("GetStringValue");
                inParams["sSubKeyName"] = REGISTRY_KEY_NAME;
                inParams["sValueName"] = REGISTRY_VAlUE_NAME;
                ManagementBaseObject outParams = registry.InvokeMethod("GetStringValue", inParams, null);
                result = (String)outParams["sValue"];

                //used to Int values = DWORD
                //ManagementBaseObject inParams = registry.GetMethodParameters("GetDWORDValue");
                //inParams["sSubKeyName"] = REGISTRY_KEY_NAME;
                //inParams["sValueName"] = REGISTRY_VAlUE_NAME;
                //ManagementBaseObject outParams = registry.InvokeMethod("GetDWORDValue", inParams, null);
                //UInt32 uValue = (UInt32)outParams["uValue"];


            }
            catch
            {
                result = "Unable to Retrieve Registry Value";
            }

            return result;

        }

        private void btnGetValues_Click(object sender, EventArgs e)
        {
            txtValues.Text = Get_Registry_Values();
        }
    }
}

//ManagementBaseObject inParams = registry.GetMethodParameters("GetDWORDValue");
//inParams["sSubKeyName"] = REGISTRY_KEY_NAME;
//inParams["sValueName"] = REGISTRY_VAlUE_NAME;
//ManagementBaseObject outParams = registry.InvokeMethod("GetDWORDValue", inParams, null);
//UInt32 uValue = (UInt32)outParams["uValue"];

