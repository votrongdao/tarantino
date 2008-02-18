using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Tarantino.Core.Commons.Services.Configuration;
using Tarantino.Core.Commons.Services.Configuration.Impl;

namespace Tarantino.DatabaseManager
{
	public partial class Main : Form
	{
		public Main()
		{
			InitializeComponent();

			chkIntegratedSecurity.CheckedChanged += chkIntegratedSecurity_OnCheckedChanged;
			btnBrowse.Click += btnBrowse_OnClick;

			cboAction.Items.Add("Create");
			cboAction.Items.Add("Update");
			cboAction.Items.Add("Drop");
			cboAction.Items.Add("Rebuild");
			cboAction.SelectedIndex = 0;

			IConfigurationReader reader = new ConfigurationReader(new ApplicationConfiguration());

			string scriptFolder = reader.GetOptionalSetting("ScriptFolder") ?? string.Empty;
			string server = reader.GetOptionalSetting("Server") ?? string.Empty;
			string database = reader.GetOptionalSetting("Database") ?? string.Empty;
			string username = reader.GetOptionalSetting("Username") ?? string.Empty;
			string password = reader.GetOptionalSetting("Password") ?? string.Empty;
			bool integratedSecurity = reader.GetOptionalBooleanSetting("IntegratedSecurity") ?? false;

			txtScriptFolder.Text = scriptFolder;
			txtServer.Text = server;
			txtDatabase.Text = database;
			txtUsername.Text = username;
			txtPassword.Text = password;
			chkIntegratedSecurity.Checked = integratedSecurity;

			updateAuthenticationFields();
		}

		private void btnBrowse_OnClick(object sender, EventArgs e)
		{
			FolderBrowserDialog dialog = new FolderBrowserDialog();
			dialog.RootFolder = Environment.SpecialFolder.Desktop;
			dialog.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;
			dialog.ShowNewFolderButton = false;
			dialog.Description = "Please select the database script folder that contains the 'Create' and 'Update' sub-folders";
			DialogResult result = dialog.ShowDialog(this);

			if (result == DialogResult.OK)
			{
				txtScriptFolder.Text = dialog.SelectedPath;
			}
		}

		private void chkIntegratedSecurity_OnCheckedChanged(object sender, EventArgs e)
		{
			updateAuthenticationFields();
		}

		private void updateAuthenticationFields()
		{
			txtUsername.Enabled = !chkIntegratedSecurity.Checked;
			txtPassword.Enabled = !chkIntegratedSecurity.Checked;

			if (chkIntegratedSecurity.Checked)
			{
				txtUsername.Text = string.Empty;
				txtPassword.Text = string.Empty;
			}
		}

		private StringBuilder AddArgument(StringBuilder sb, string argName, string argValue)
		{
			sb.Append(" -D:");
			sb.Append(argName);
			sb.Append("=\"");
			sb.Append(argValue);
			sb.Append("\"");
			return sb;
		}

		private void RunCommandLine(string commandLine, string args)
		{
			ProcessProgressForm processForm = new ProcessProgressForm();

			processForm.ProcessCommand = commandLine;
			if (args != null)
				processForm.ProcessArguments = args;

			processForm.ProcessWorkingDir = AppDomain.CurrentDomain.BaseDirectory;
			processForm.StandardErrorColor = Color.Maroon;
			processForm.StandardOutColor = Color.Blue;
			processForm.Text = "Database process output";
			processForm.ErrorDialogMessage = "Error running database process";
			processForm.BeginProcess();
			processForm.ShowDialog(this);
		}

		private void btnExecute_Click(object sender, EventArgs e)
		{
			StringBuilder arguments = new StringBuilder("-buildfile:databaseManagerTargets.build");
			AddArgument(arguments, "database.script.directory", txtScriptFolder.Text);
			AddArgument(arguments, "database.server", txtServer.Text);
			AddArgument(arguments, "database.name", txtDatabase.Text);
			AddArgument(arguments, "database.integrated", chkIntegratedSecurity.Checked.ToString().ToLower());
			AddArgument(arguments, "database.username", txtUsername.Text);
			AddArgument(arguments, "database.password", txtPassword.Text);
			AddArgument(arguments, "action", cboAction.SelectedItem.ToString());

			IConfigurationReader reader = new ConfigurationReader(new ApplicationConfiguration());

			RunCommandLine(string.Format(@"{0}\nant.exe", reader.GetRequiredSetting("NAntFolder")), arguments.ToString());
		}
	}
}