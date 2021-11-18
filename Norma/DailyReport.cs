using System;
using System.Collections.Generic;

namespace Norma
{
    class DailyReport
	{
		public string UserName { get; set; }
		public string Position { get; set; }
		public string AssisName { get; set; }
		public DateTime Date { get; set; }
		public List<CountDownItem> CountDownList { get; set; }
		// todo: weather todo-list
	}
}
