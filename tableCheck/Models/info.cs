using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tableCheck.Models
{
	public class info
	{
		public bool check { get; set; }
		public string tableName { get; set; }
		public int fieldNumber { get; set; }
		public int Progress { get; set; }
		public string status { get; set; }
	}
}
