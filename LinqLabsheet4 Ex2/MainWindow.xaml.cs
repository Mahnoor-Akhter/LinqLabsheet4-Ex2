using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LinqLabsheet4_Ex2
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		AdventureLiteEntities db;
		public MainWindow()
		{
			InitializeComponent();
		}
		public void Window_Loaded(object sender, RenderingEventArgs e)
		{
			db = new AdventureLiteEntities();

			var query = from o in db.SalesOrderDetails
						select o.customer
			//select o.Customer.CompanyName;

			var result = query.ToList();
			lbxCustomers.ItemsSource = query.ToList().Distinct();
		}

		private void  lbxCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			string customer = lbxCustomers.SelectedItems as string;

			
			
			if (customer != null)
			{
				var query = from o in db.SalesOrderHeaders
							orderby o.Customer.CompanyName.Equals(customer)
							select new OrderSummary
							{
								SalesOrderID = o.SalesOrderID,
								OrderDate = o.OrderDate,
								TotalDue = o.TotalDue

							};

				//var query = from o in db.SalesOrderHeaders
				//			where o.Customer.CompanyName.Equals(customer)
				//			select o;

				lbxOrders.ItemsSource = query.ToList();
			}
		}

		private void lbxOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			int orderID = Convert.ToInt32(lbxOrders.SelectedValue);
              
			if(orderID > 0)
			{
				var query = from od in db.SalesOrderDetails
							where od.SalesOrderID == orderID
							select new
							{
								ProductName = od.Product.Name,
								od.UnitPrice,
								od.UnitPriceDiscount,
								od.OrderQty,
								od.LineTotal
							};
				dgOrderDeatils.ItemsSource = query.ToList();
			}
		}
	}


	// end of class
	//public parial class SalesOrderHeader
	//{

	//public partial class ToString()
	//{
	// return string.Formate("{0}.{1} {2:C}",
	// OrderDate.ToShortDateString(), SalesOrderID, TotalDue);
	//}
	//}


	public class OrderSummary : SalesOrderHeaders
	{
		public object SalesOrderID { get; internal set; }
		public object OrderDate { get; internal set; }
		public object TotalDue { get; internal set; }

		public override string ToString()
		{
			return string.Format("{0}:{1} {2:C}",
			OrderDate.ToShortDateString(), SalesOrderID, TotalDue);
		}
	}
}