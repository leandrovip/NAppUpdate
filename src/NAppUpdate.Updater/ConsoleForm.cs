using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace NAppUpdate.Updater
{
	public partial class ConsoleForm : Form
	{
		public ConsoleForm()
		{
			InitializeComponent();
			Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
		}

		private void ConsoleForm_Load(object sender, EventArgs e)
		{
			rtbConsole.Clear();
		}

		public void WriteLine()
		{
			rtbConsole.AppendText(Environment.NewLine);
		}

		public void WriteLine(string message)
		{
			rtbConsole.AppendText(message);
			rtbConsole.AppendText(Environment.NewLine);
		}

		public void WriteLine(string message, params object[] args)
		{
			WriteLine(string.Format(message, args));
		}

		public void ReadKey()
		{
			// attach the keypress event and then wait for it to receive something
			KeyPress += ConsoleForm_KeyPress;
			rtbConsole.ReadOnly = false;
			while (_keyPresses == 0)
			{
				Application.DoEvents();
				Thread.Sleep(100);
			}
		}

		private int _keyPresses;

		private void ConsoleForm_KeyPress(object sender, KeyPressEventArgs e)
		{
			HandleKeyPress();
		}

		private void ConsoleForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			HandleKeyPress(); // allow readkey to finish
		}

		private void HandleKeyPress()
		{
			KeyPress -= ConsoleForm_KeyPress;
			_keyPresses++;
		}
	}
}
