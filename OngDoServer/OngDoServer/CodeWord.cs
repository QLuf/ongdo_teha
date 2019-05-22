using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OngDoServer
{
	public class CodeWord
	{
		public string Code { get; set; }
		public string Word { get; set; }

		public CodeWord(string code, string word)
		{
			Code = code;
			Word = word;
		}	
	}
}
