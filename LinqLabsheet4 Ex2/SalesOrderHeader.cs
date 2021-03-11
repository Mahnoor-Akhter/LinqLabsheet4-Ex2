using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqLabsheet4_Ex2
{
	public partial class SalesOrderHeaders

	{
		public override string ToString()
		{ 
			return string.Format("{0}:{1} {2:C}",
				OrderDate.ToShortDateStrung(),
				SalesOrderID,
				TotalDue);
			
		}

	}
}
