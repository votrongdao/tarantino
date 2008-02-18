using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Tarantino.Core.Deployer.Model;
using Tarantino.Core.Deployer.Services;
using Tarantino.Core.Deployer.Services.Configuration;
using Tarantino.Deployer;
using Tarantino.Deployer.Services;
using Tarantino.Core.Commons.Services.Configuration.Impl;
using StructureMap;
using Tarantino.Deployer.Services.UI;
using Tarantino.Deployer.Services.UI.Impl;
using Application=Tarantino.Core.Deployer.Services.Configuration.Impl.Application;
using Environment=Tarantino.Core.Deployer.Services.Configuration.Impl.Environment;

namespace Tarantino.Deployer
{
	public partial class DeployPackage : Form
	{
		public DeployPackage()
		{
			InitializeComponent();
			wireEvents();
			populateApplicationDropdown();
		}

		private void populateRevisions()
		{
			IDeploymentRepository repository = ObjectFactory.GetInstance<IDeploymentRepository>();

			IEnumerable<Deployment> certified = repository.FindCertified(SelectedApplication.Name, SelectedEnvironment.Predecessor);
			IEnumerable<Deployment> uncertified = repository.FindSuccessfulUncertified(SelectedApplication.Name, SelectedEnvironment.Name);

			populateRevisionDropdown(certified, cboRevision);
			populateRevisionDropdown(uncertified, cboCertifyRevision);

			populateDeploymentGrid(repository);
		}

		private void populateDeploymentGrid(IDeploymentRepository repository)
		{
			IDeploymentRowFactory rowFactory = ObjectFactory.GetInstance<IDeploymentRowFactory>();
		
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
			Deployment selectedRevision = cboCertifyRevision.SelectedItem as Deployment;

			IRevisionCertifier certifier = ObjectFactory.GetInstance<IRevisionCertifier>();
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
			IDeploymentFormValidator validator = new DeploymentFormValidator();
		
			if (validator.IsValid(SelectedEnvironment, cboRevision.Text))
			{
				RunCommandLine("Deploy.bat", getArguments());
			}
			else
			{
				MessageBox.Show("Please complete the form above before continuing");
			}
		}

		private void grdDeployments_OnDoubleClick(object sender, EventArgs e)
		{
			DataGridViewSelectedRowCollection selectedRows = grdDeployments.SelectedRows;

			if (selectedRows.Count == 1)
			{
				string output = selectedRows[0].Cells["Output"].Value.ToString();

				DeploymentOutput outputWindow = new DeploymentOutput();
				outputWindow.Output = output;
				outputWindow.ShowDialog();
			}
		}

		private string getArguments()
		{
			return string.Format("{0} {1} {2} {3} {4} {5} {6}",
													 SelectedApplication.Name, SelectedApplication.Url,
													 SelectedApplication.ZipFile, SelectedApplication.Username, 
													 SelectedApplication.Password, SelectedEnvironment.Name,
													 cboRevision.Text);
		}

		private void RunCommandLine(string executable, string arguments)
		{
			ProcessProgressForm processForm = new ProcessProgressForm();
			processForm.ProcessCompleted = processCompleted;

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
			IDeploymentRecorder recorder = ObjectFactory.GetInstance<IDeploymentRecorder>();
			recorder.RecordDeployment(SelectedApplication.Name, SelectedEnvironment.Name, output);

			populateRevisions();
		}

		private void populateRevisionDropdown(IEnumerable<Deployment> deployments, ComboBox combo)
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
			IApplicationRepository repository = ObjectFactory.GetInstance<IApplicationRepository>();

			foreach (Application application in repository.GetAll())
			{
				cboApplication.Items.Add(application);
			}

			cboApplication.SelectedIndex = 0;
		}

		private void cboRevision_OnTextChanged(object sender, EventArgs e)
		{
			Deployment deployment = cboRevision.SelectedItem as Deployment;
			string revision = cboRevision.Text;

			ILabelTextGenerator generator = ObjectFactory.GetInstance<ILabelTextGenerator>();
			lblDeployed.Text = generator.GetDeploymentText(SelectedEnvironment, revision, deployment);
			lblCertified.Text = generator.GetCertificationText(revision, deployment);
		}

		private void cboApplication_OnSelectedIndexChanged(object sender, EventArgs e)
		{
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
	}
}