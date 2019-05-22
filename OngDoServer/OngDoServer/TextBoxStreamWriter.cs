using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OngDoServer
{
	public class TextBoxStreamWriter : StringWriter
	{
		#region Properties
		private TextBox textBoxOutput;
		protected StreamWriter writer;
		protected MemoryStream mem;
		#endregion

		#region Constructor
		public TextBoxStreamWriter(TextBox _output)
		{
			textBoxOutput = _output;
			mem = new MemoryStream(1000000);
			writer = new StreamWriter(mem);
			writer.AutoFlush = true;
		}
		#endregion

		#region Override Mathods
		// override methods
		public override void Write(char value)
		{
			base.Write(value);
			textBoxOutput.Dispatcher.BeginInvoke(new Action(() =>
			{
				textBoxOutput.AppendText(value.ToString());
			}));
			writer.Write(value);
		}

		// override methods
		public override void Write(string value)
		{
			base.Write(value);
			textBoxOutput.Dispatcher.BeginInvoke(new Action(() =>
			{
				textBoxOutput.AppendText(value.ToString());
			}));
			writer.Write(value);
		}

		public override void WriteLine(string value)
		{
			base.WriteLine(DateTime.Now.ToString(value));
			textBoxOutput.Dispatcher.BeginInvoke(new Action(() =>
			{
				textBoxOutput.AppendText(value.ToString() + Environment.NewLine);
			}));
			
			writer.Write(value);
		}
		#endregion
	}
}
