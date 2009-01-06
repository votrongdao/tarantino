using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Tarantino.Core.Commons.Services.Repositories;
using Tarantino.Core.Deployer.Model;
using Tarantino.Core.Deployer.Services;
using Tarantino.Core.Deployer.Services.Configuration;
using Tarantino.Core.Deployer.Services.UI;
using Tarantino.Deployer;
using Tarantino.Core.Commons.Services.Configuration.Impl;
using StructureMap;
using Tarantino.Infrastructure;
using Application=Tarantino.Core.Deployer.Services.Configuration.Impl.Application;
using Environment=Tarantino.Core.Deployer.Services.Configuration.Impl.Environment;

namespace Tarantino.Deployer
{
	public partial class DeployPackage : Form
	{
		public DeployPackage()
		{
			InfrastructureDependencyRegistrar.RegisterInfrastructure();

			InitializeComponent();
			wireEvents();
			populateApplicationDropdown();
		}

		private void populateRevisions()
		{
			var repository = ObjectFactory.GetInstance<IDeploymentRepository>();

			IEnumerable<Deployment> certified = repository.FindCertified(SelectedApplication.Name, SelectedEnvironment.Predecessor);
			IEnumerable<Deployment> uncertified = repository.FindSuccessfulUncertified(SelectedApplication.Name, SelectedEnvironment.Name);

			populateRevisionDropdown(certified, cboRevision);
			populateRevisionDropdown(uncertified, cboCertifyRevision);

			populateDeploymentGrid(repository);
		}

		private void populateDeploymentGrid(IDeploymentRepository repository)
		{
			var rowFactory = ObjectFactory.GetInstance<IDeploymentRowFactory>();

			grdDeployments.Rows.Clear();

			IEnumerable<Deployment> deployments = repository.Find(SelectedApplication.Name, SelectedEnvironment.Name);

			int rowNumber = 0;
			foreach (Deployment deployment in deployments)
			{
				string[] deploymentRow = rowFactory.ConstructRow(deployment);
				grdDeployments.Rows.Add(deploymentRow);
				
				if (deployment.Result == DeploymentResult.Failure)
				{
					grdDeployments.Rows[rowNumber].DefaultCellStyle.BackColor = Color.Pink;
				}

				rowNumber++;
			}
		}

		private void btnCertify_Click(object sender, EventArgs e)
		{
			var selectedRevision = cboCertifyRevision.SelectedItem as Deployment;

			var certifier = ObjectFactory.GetInstance<IRevisionCertifier>();
			certifier.Certify(selectedRevision);

			if (selectedRevision != null)
			{
				populateRevisions();
			}
			else
			{
				MessageBox.Show("Please select a revision!");
			}
		}

		private void btnDeploy_OnClick(object sender, EventArgs e)
		{
			var arguments = new StringBuilder("-buildfile:Deployer.build");
			addArgument(arguments, "application", SelectedApplication.Name);
			addArgument(arguments, "revision", cboRevision.Text);
			addArgument(arguments, "environment", SelectedEnvironment.Name);
			addArgument(arguments, "url", SelectedApplication.Url);
			addArgument(arguments, "zip.file", SelectedApplication.ZipFile);
			addArgument(arguments, "username", txtUsername.Text);
			addArgument(arguments, "password", txtPassword.Text);

			RunCommandLine(@"NAnt\nant.exe", arguments.ToString());
		}

		private void grdDeployments_OnDoubleClick(object sender, EventArgs e)
		{
			DataGridViewSelectedRowCollection selectedRows = grdDeployments.SelectedRows;

			if (selectedRows.Count == 1)
			{
				var deploymentId = new Guid(grdDeployments.Rows[0].Cells[6].FormattedValue.ToString());
				var repository = ObjectFactory.GetInstance<IPersistentObjectRepository>();
				repository.ConfigurationFile = "deployer.hibernate.cfg.xml";

				var deployment = repository.GetByIdWithoutClosingSession<Deployment>(deploymentId);
				var outputText = deployment.Output.Output;
				var outputWindow = new DeploymentOutput { Output = outputText};
				outputWindow.ShowDialog();
			}
		}

		private void RunCommandLine(string executable, string arguments)
		{
			var processForm = new ProcessProgressForm {ProcessCompleted = processCompleted};

			string workingDir = AppDomain.CurrentDomain.BaseDirectory;
			string executableWithPath = string.Format(@"{0}{1}", workingDir, executable);

			processForm.ProcessCommand = executableWithPath;

			if (arguments != null)
			{
				processForm.ProcessArguments = arguments;
			}

			processForm.ProcessWorkingDir = workingDir;
			processForm.BeginProcess();
			processForm.ShowDialog(this);
		}

		private void processCompleted(string output)
		{
			var recorder = ObjectFactory.GetInstance<IDeploymentRecorder>();
			recorder.RecordDeployment(SelectedApplication.Name, SelectedEnvironment.Name, output);

			populateRevisions();
		}

		private static void populateRevisionDropdown(IEnumerable<Deployment> deployments, ComboBox combo)
		{
			combo.Text = string.Empty;
			combo.DataSource = deployments;
		}

		private void populateEnvironments()
		{
			cboEnvironment.Items.Clear();

			ElementCollection<Environment> environments = SelectedApplication.Environments;

			if (environments.Count > 0)
			{
				foreach (Environment environment in environments)
				{
					cboEnvironment.Items.Add(environment);
				}

				cboEnvironment.SelectedIndex = 0;
			}
		}

		private Application SelectedApplication
		{
			get { return (Application)cboApplication.SelectedItem; }
		}

		private Environment SelectedEnvironment
		{
			get { return (Environment)cboEnvironment.SelectedItem; }
		}

		private void populateApplicationDropdown()
		{
			var repository = ObjectFactory.GetInstance<IApplicationRepository>();

			foreach (Application application in repository.GetAll())
			{
				cboApplication.Items.Add(application);
			}

			cboApplication.SelectedIndex = 0;
		}

		private void cboRevision_OnTextChanged(object sender, EventArgs e)
		{
			var deployment = cboRevision.SelectedItem as Deployment;
			string revision = cboRevision.Text;

			var generator = ObjectFactory.GetInstance<ILabelTextGenerator>();
			lblDeployed.Text = generator.GetDeploymentText(SelectedEnvironment, revision, deployment);
			lblCertified.Text = generator.GetCertificationText(revision, deployment);
		}

		private void cboApplication_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			txtUsername.Text = SelectedApplication.Username;
			txtPassword.Text = SelectedApplication.Password;

			populateEnvironments();
		}

		private void cboEnvironment_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			populateRevisions();
		}

		private void wireEvents()
		{
			cboApplication.SelectedIndexChanged += cboApplication_OnSelectedIndexChanged;
			cboEnvironment.SelectedIndexChanged += cboEnvironment_OnSelectedIndexChanged;
			cboRevision.TextChanged += cboRevision_OnTextChanged;
			btnDeploy.Click += btnDeploy_OnClick;
			grdDeployments.DoubleClick += grdDeployments_OnDoubleClick;
		}

		private static void addArgument(StringBuilder sb, string argName, string argValue)
		{
			sb.Append(" -D:");
			sb.Append(argName);
			sb.Append("=\"");
			sb.Append(argValue);
			sb.Append("\"");
			return;
		}
	}
}