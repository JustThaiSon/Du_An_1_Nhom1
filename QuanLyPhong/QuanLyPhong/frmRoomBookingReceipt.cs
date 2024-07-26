using BUS.IService;
using BUS.Service;
using DAL.Entities;
using DAL.Enums;
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
		private decimal RoomPrice { get; set; }
		private decimal TotalPriceOrderService { get; set; }
		public Guid RoomId { get; set; }
		public frmRoomBookingReceipt(Guid RoomId)
		{
			InitializeComponent();
			_serviceSevice = new ServiceService();
			_roomService = new RoomService();
			_customerService = new CustomerService();
			_orderServiceService = new OrderServiceService();
			_temporaryServices = new List<OrderService>();
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
			TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

			DateTime utcNow = DateTime.UtcNow;

			DateTime vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, vietnamTimeZone);


			DtGioCheckIn.Format = DateTimePickerFormat.Time;
			DtGioCheckIn.ShowUpDown = true;
			DtGioCheckIn.Value = vietnamTime;
			var room = _roomService.GetAllRooms().FirstOrDefault(x => x.Id == RoomId);
			if (room != null)
			{
				 RoomPrice = room.Price;
				lbGiaRoom.Text = room.Price.ToString("F2") + " VNĐ";
			}
			else
			{
				RoomPrice = 0;
				lbGiaRoom.Text = "0 VNĐ";
			}
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
				lbGia.Text = selectedService.Price.ToString("F2");
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
			var selectedService = _serviceSevice.GetAllServiceFromDb().FirstOrDefault(s => s.Name == selectedServiceName);
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
						Service = selectedService
					};
					TotalPriceOrderService = orderService.TotalPrice;
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
				dtgService.Rows.Add(item.ServiceId, Count, item.Service.Name, item.Quantity, item.Price, item.TotalPrice);
			}
			TotalPrice();
		}
		void TotalPrice()
		{
			var ToTal = RoomPrice + TotalPriceOrderService;
			lbTotalPrice.Text = ToTal.ToString("N0") + "VNĐ";
		}
	}
} 
