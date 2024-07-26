using BUS.IService;
using BUS.Service;
using DAL.Entities;
using DAL.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using TheArtOfDevHtmlRenderer.Adapters;
using static TheArtOfDevHtmlRenderer.Adapters.RGraphicsPath;
using OrderService = DAL.Entities.OrderService;

namespace QuanLyPhong
{
	public partial class frmRoomBookingReceipt : Form
	{
		public delegate void BookRoomHandler();
		public event BookRoomHandler OnBookRoom;
		private IServiceSevice _serviceSevice;
		private IRoomService _roomService;
		private ICustomerService _customerService;
		private IOrderServiceService _orderServiceService;
		private List<OrderService> _temporaryServices;
		private IKindOfRoomService _kindOfRoomService;
		private IOrderService _orderService;
		private decimal RoomPrice { get; set; }
		private decimal TotalPriceOrderService { get; set; }
		private decimal TotalAmount { get; set; }
		public Guid RoomId { get; set; }
		public frmRoomBookingReceipt(Guid RoomId)
		{
			InitializeComponent();
			_serviceSevice = new ServiceService();
			_roomService = new RoomService();
			_customerService = new CustomerService();
			_orderServiceService = new OrderServiceService();
			_kindOfRoomService = new KindOfRoomService();
			_temporaryServices = new List<OrderService>();
			_orderService = new OrderServicess();
			this.RoomId = RoomId;
			Load();
		}
		void Load()
		{
			loadNameroom();
			LoadCustomer();
			LoadCbbOrderService();
			LoadStatusOrder();
			LoadDataGridViewService();
			LoadTime();
			LoadBookingDetails();
			LoadPrice();
		}


		void LoadPrice()
		{
			var room = _roomService.GetAllRooms().Where(x => x.Id == RoomId).Select(x => new { x.PriceByHour, x.PricePerDay }).FirstOrDefault();

			if (room != null)
			{
				if (rdHourly.Checked)
				{
					RoomPrice = room.PriceByHour;
				}
				else
				{
					RoomPrice = room.PricePerDay;
				}
				lbGiaRoom.Text = RoomPrice.ToString("0") + " VNĐ";
			}
			else
			{
				RoomPrice = 0;
				lbGiaRoom.Text = "0 VNĐ";
			}
		}
		void LoadTime()
		{
			TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

			DateTime utcNow = DateTime.UtcNow;

			DateTime vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, vietnamTimeZone);


			DtGioCheckIn.Format = DateTimePickerFormat.Time;
			DtGioCheckIn.ShowUpDown = true;
			DtGioCheckIn.CustomFormat = "HH:mm";
			DtGioCheckIn.Value = vietnamTime;

			dtGioCheckout.Format = DateTimePickerFormat.Time;
			dtGioCheckout.ShowUpDown = true;
			dtGioCheckout.CustomFormat = "HH:mm";
			dtGioCheckout.Value = vietnamTime;

			dt_checkin.ShowUpDown = true;
			dt_checkin.Value = vietnamTime;
			//dt_checkout.ShowUpDown = true;
			//dt_checkout.Value = vietnamTime;
		}
		void loadNameroom()
		{
			var NameRoom = _roomService.GetAllRooms().Where(x => x.Id == RoomId).Select(x => x.RoomName).FirstOrDefault();
			lbRoomName.Text = NameRoom;
		}
		void LoadCbbOrderService()
		{
			foreach (var item in _serviceSevice.GetAllServiceFromDb())
			{
				cbb_NameService.Items.Add(item.Name);
			}
		}
		void LoadCustomer()
		{
			cbb_Customer.Items.Clear();
			foreach (var item in _customerService.GetAllCustomerFromDb())
			{
				cbb_Customer.Items.Add(item.Name);
			}
		}
		void LoadStatusOrder()
		{
			cbbOrderType.DataSource = Enum.GetValues(typeof(OrderType));
			cbbOrderType.SelectedItem = OrderType.RegularCustomer;
		}
		private void btn_cancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btn_checkin_Click(object sender, EventArgs e)
		{
			//TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

			//DateTime utcNow = DateTime.UtcNow;

			//DateTime vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, vietnamTimeZone);

			//DtGioCheckIn.Value = vietnamTime;
			//dt_checkout.Value = vietnamTime;
			var selectedCustomerName = cbb_Customer.SelectedItem?.ToString();
			if (string.IsNullOrEmpty(selectedCustomerName))
			{
				MessageBox.Show("Please select a customer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			var selectedCustomer = _customerService.GetAllCustomerFromDb().FirstOrDefault(c => c.Name == selectedCustomerName);
			if (selectedCustomer == null)
			{
				MessageBox.Show("Selected customer does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			var newOrder = new Orders
			{
				RoomId = RoomId,
				CustomerId = selectedCustomer.Id,
				DateCreated = DtGioCheckIn.Value,
				DatePayment = null,
				OrderType = (OrderType)cbbOrderType.SelectedItem,
				ToTalPrice = TotalAmount,

			};

			if (MessageBox.Show("Do you want to add this Order?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				string result = _orderService.AddOrders(newOrder);
				MessageBox.Show(result, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			foreach (var service in _temporaryServices)
			{
				service.OrderId = newOrder.Id;
				string serviceResult = _orderServiceService.AddOrderService(service);
			}
			OnBookRoom?.Invoke();
			this.Close();
		}

		private void cbbnum_quantitySer_ValueChanged(object sender, EventArgs e)
		{

		}

		private void cbb_NameService_SelectedIndexChanged(object sender, EventArgs e)
		{
			var selectedServiceName = cbb_NameService.SelectedItem.ToString();
			var selectedService = _serviceSevice.GetAllServiceFromDb().FirstOrDefault(s => s.Name == selectedServiceName);
			if (selectedService != null)
			{
				txtStatus.Text = selectedService.Status.ToString();
				lbGia.Text = selectedService.Price.ToString("0");
			}
		}

		private void lbRoomName_Click(object sender, EventArgs e)
		{

		}

		private void btn_Create_Click(object sender, EventArgs e)
		{
			var addCustomer = new Customer()
			{
				Name = tbCustomerName.Text,
				Email = tb_emailCus.Text,
				CCCD = tb_cccd.Text,
				Address = tb_Address.Text,
				Gender = rdFemale.Checked ? MenuGender.Female : (rdMale.Checked ? MenuGender.Male : MenuGender.Other),
				PhoneNumber = tb_phoneCus.Text
			};
			if (MessageBox.Show("Do you want to add this Customer?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				string result = _customerService.AddCustomer(addCustomer);
				MessageBox.Show(result, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			LoadCustomer();
		}
		private void cbb_Customer_SelectedIndexChanged(object sender, EventArgs e)
		{
			var selectedCustomerName = cbb_Customer.SelectedItem.ToString();
			var selectedCustomer = _customerService.GetAllCustomerFromDb().FirstOrDefault(c => c.Name == selectedCustomerName);
			if (selectedCustomer != null)
			{
				tbCustomerName.Text = selectedCustomer.Name;
				tb_Address.Text = selectedCustomer.Address;
				tb_emailCus.Text = selectedCustomer.Email;
				tb_cccd.Text = selectedCustomer.CCCD;
				tb_phoneCus.Text = selectedCustomer.PhoneNumber;
				switch (selectedCustomer.Gender)
				{
					case MenuGender.Male:
						rdMale.Checked = true;
						break;
					case MenuGender.Female:
						rdFemale.Checked = true;
						break;
					case MenuGender.Other:
						rdMale.Checked = false;
						rdFemale.Checked = false;
						break;
					default:
						break;
				}
			}
		}

		private void btn_AddService_Click(object sender, EventArgs e)
		{
			var selectedServiceName = cbb_NameService.SelectedItem.ToString();
			var selectedService = _serviceSevice.GetAllServiceFromDb().FirstOrDefault(x => x.Name == cbb_NameService.Text);

			if (selectedService != null)
			{
				var existingOrderService = _temporaryServices.FirstOrDefault(s => s.ServiceId == selectedService.Id);

				if (existingOrderService != null)
				{
					existingOrderService.Quantity += (int)cbbnum_quantitySer.Value;
					existingOrderService.TotalPrice = existingOrderService.Price * existingOrderService.Quantity;
				}
				else
				{
					var orderService = new OrderService()
					{
						ServiceId = selectedService.Id,
						Quantity = (int)cbbnum_quantitySer.Value,
						Price = selectedService.Price,
						TotalPrice = selectedService.Price * (int)cbbnum_quantitySer.Value,
					};
					//_orderServiceService.AddOrderService(orderService);
					_temporaryServices.Add(orderService);
				}

				MessageBox.Show("Service added successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
				LoadDataGridViewService();
			}
		}
		void LoadDataGridViewService()
		{
			dtgService.ColumnCount = 6;
			dtgService.Columns[0].Name = "Id";
			dtgService.Columns[0].Visible = false;
			dtgService.Columns[1].Name = "STT";
			dtgService.Columns[2].Name = "Name";
			dtgService.Columns[3].Name = "Quantity";
			dtgService.Columns[4].Name = "Price";
			dtgService.Columns[5].Name = "TotalPrice";
			dtgService.Rows.Clear();
			int Count = 0;
			foreach (var item in _temporaryServices)
			{
				Count++;
				dtgService.Rows.Add(item.ServiceId, Count, _serviceSevice.GetAllServiceFromDb().Where(x => x.Id == item.ServiceId).Select(x => x.Name).FirstOrDefault(), item.Quantity, item.Price, item.TotalPrice);
				TotalPriceOrderService += item.TotalPrice;

			}
			TotalPrice();
		}
		void TotalPrice()
		{

			TimeSpan timeSpanHours = dtGioCheckout.Value - DtGioCheckIn.Value;
			TimeSpan timeSpanDays = dt_checkout.Value.Date - dt_checkin.Value.Date;
			var room = _roomService.GetAllRooms().Where(x => x.Id == RoomId).Select(x => new { x.PriceByHour, x.PricePerDay }).FirstOrDefault();

			decimal totalPrice = 0;

			if (rddaily.Checked)
			{
				totalPrice = room.PricePerDay * (decimal)timeSpanDays.TotalDays;
			}
			else if (rdHourly.Checked)
			{
				decimal totalHours = (decimal)timeSpanHours.TotalHours;
				decimal roundedHours = Math.Ceiling(totalHours);
				totalPrice = room.PriceByHour * roundedHours;
			}

			TotalAmount = totalPrice + TotalPriceOrderService;
			lbTotalPrice.Text = TotalAmount.ToString("0") + " VNĐ";
		}

		private void rdHourly_CheckedChanged(object sender, EventArgs e)
		{
			LoadPrice();
			TotalPrice();
		}

		private void rđaily_CheckedChanged(object sender, EventArgs e)
		{
			LoadPrice();
			TotalPrice();
		}

		private void dtGioCheckout_ValueChanged(object sender, EventArgs e)
		{
			TotalPrice();
		}

		private void dt_checkout_ValueChanged(object sender, EventArgs e)
		{
			TotalPrice();
		}

		private void btn_addToOrder_Click(object sender, EventArgs e)
		{


		}
		void LoadBookingDetails()
		{
			var room = _roomService.GetAllRooms().FirstOrDefault(x => x.Id == RoomId);

			if (room != null && room.Status == RoomStatus.UnAvailable)
			{
				var currentOrder = _orderService.GetByRoomId(RoomId).FirstOrDefault(x => x.PayMents == null);

				if (currentOrder != null)
				{
					var customer = _customerService.GetAllCustomerFromDb().FirstOrDefault(c => c.Id == currentOrder.CustomerId);

					if (customer != null)
					{
						cbb_Customer.SelectedItem = customer.Name;
						tbCustomerName.Text = customer.Name;
						tb_Address.Text = customer.Address;
						tb_emailCus.Text = customer.Email;
						tb_cccd.Text = customer.CCCD;
						tb_phoneCus.Text = customer.PhoneNumber;

						switch (customer.Gender)
						{
							case MenuGender.Male:
								rdMale.Checked = true;
								break;
							case MenuGender.Female:
								rdFemale.Checked = true;
								break;
							case MenuGender.Other:
								rdMale.Checked = false;
								rdFemale.Checked = false;
								break;
						}
					}

					if (currentOrder.DateCreated.HasValue)
					{
						DtGioCheckIn.Value = currentOrder.DateCreated.Value;
						dt_checkin.Value = currentOrder.DateCreated.Value;
					}

					_temporaryServices = _orderServiceService.GetOrderServicesByOrderId(currentOrder.Id);
					LoadDataGridViewService();
					TotalPrice();
				}
			}
			else
			{
				ClearBookingDetails();
			}
		}

		void ClearBookingDetails()
		{
			cbb_Customer.SelectedIndex = -1;
			tbCustomerName.Clear();
			tb_Address.Clear();
			tb_emailCus.Clear();
			tb_cccd.Clear();
			tb_phoneCus.Clear();
			rdMale.Checked = false;
			rdFemale.Checked = false;
		}


		private void dtgListOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}
	}
}
