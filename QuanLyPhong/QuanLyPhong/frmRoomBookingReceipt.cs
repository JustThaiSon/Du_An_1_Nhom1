using AForge.Video;
using AForge.Video.DirectShow;
using BUS.IService;
using BUS.Service;
using DAL.Data;
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using TheArtOfDevHtmlRenderer.Adapters;
using ZXing.Common;
using ZXing;
using ZXing.Windows.Compatibility;
using static Guna.UI2.Native.WinApi;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static TheArtOfDevHtmlRenderer.Adapters.RGraphicsPath;
using OrderService = DAL.Entities.OrderService;
using QuanLyPhong;

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
        private FilterInfoCollection _videoDevices;
        private VideoCaptureDevice _videoCaptureDevice;
        private BarcodeReader _barcodeReader;

        private decimal RoomPrice { get; set; }
        private decimal TotalPriceOrderService { get; set; }
        private decimal TotalAmount { get; set; }
        private decimal TotalPriceRoom { get; set; }
        public Guid RoomId { get; set; }
        public Guid OrderId { get; set; }
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem toolStripEdit;
        private ToolStripMenuItem toolStripDelete;

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
            contextMenuStrip = new ContextMenuStrip();
        }
        private void MenuStrip()
        {
            contextMenuStrip = new ContextMenuStrip();
            toolStripEdit = new ToolStripMenuItem("Edit");
            toolStripDelete = new ToolStripMenuItem("Delete");

            toolStripEdit.Click += ToolStripEdit_Click;
            toolStripDelete.Click += ToolStripDelete_Click;

            contextMenuStrip.Items.AddRange(new ToolStripItem[] { toolStripEdit, toolStripDelete });

            dtgService.ContextMenuStrip = contextMenuStrip;
        }

        private void ToolStripDelete_Click(object? sender, EventArgs e)
        {
            if (dtgService.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dtgService.SelectedRows[0];
                var serviceId = (Guid)selectedRow.Cells["Id"].Value;
                var serviceToRemove = _temporaryServices.FirstOrDefault(s => s.ServiceId == serviceId);

                if (serviceToRemove != null)
                {
                    TotalPriceOrderService = TotalPriceOrderService - serviceToRemove.TotalPrice;
                    _temporaryServices.Remove(serviceToRemove);
                    LoadDataGridViewService();
                    TotalPrice();
                }
            }
        }

        private void ToolStripEdit_Click(object? sender, EventArgs e)
        {
            if (dtgService.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dtgService.SelectedRows[0];
                var serviceId = (Guid)selectedRow.Cells["Id"].Value;
                var serviceToUpdate = _temporaryServices.FirstOrDefault(x => x.ServiceId == serviceId);
                if (serviceToUpdate != null)
                {
                    int newQuantity = Convert.ToInt32(selectedRow.Cells["Quantity"].Value);
                    decimal price = Convert.ToDecimal(selectedRow.Cells["Price"].Value);

                    decimal newTotalPrice = newQuantity * price;

                    serviceToUpdate.Quantity = newQuantity;
                    serviceToUpdate.TotalPrice = newTotalPrice;

                    selectedRow.Cells["Quantity"].Value = newQuantity;
                    selectedRow.Cells["TotalPrice"].Value = newTotalPrice.ToString("0");

                    MessageBox.Show("Update Thành Công");
                    LoadDataGridViewService();
                }
            }
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
            MenuStrip();
            LoadDtgOrders();
            LoadCamera();
            var btnCheckin = _roomService.GetAllRooms().Where(x => x.Id == RoomId && x.Status == RoomStatus.UnAvailable);
            if (btnCheckin != null)
            {
                btn_checkin.Enabled = true;
            }
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
            DtGioCheckIn.Enabled = false;

            dtGioCheckout.Format = DateTimePickerFormat.Time;
            dtGioCheckout.ShowUpDown = true;
            dtGioCheckout.CustomFormat = "HH:mm";
            dtGioCheckout.Value = vietnamTime;
            dtGioCheckout.Enabled = false;

            dt_checkin.ShowUpDown = true;
            dt_checkin.Value = vietnamTime;
            dt_checkin.Enabled = false;
            dt_checkout.ShowUpDown = true;
            dt_checkout.Value = vietnamTime;
            dt_checkout.Enabled = false;

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

            decimal prepay = 0;
            if (!string.IsNullOrEmpty(txtPrepay.Text) && decimal.TryParse(txtPrepay.Text, out decimal parsedPrepay))
            {
                prepay = parsedPrepay;
            }


            var newOrder = new Orders
            {
                RoomId = RoomId,
                CustomerId = selectedCustomer.Id,
                DateCreated = DtGioCheckIn.Value,
                DatePayment = null,
                Note = tb_note.Text,
                Prepay = prepay,
                EmployeeId = Session.UserId,
                Rentaltype = rdHourly.Checked ? RentalTypeEnum.Hourly : RentalTypeEnum.Daily,
                OrderType = (OrderType)cbbOrderType.SelectedItem,
                ToTalPrice = TotalPriceRoom,
                ToTal = TotalAmount,
            };

            if (MessageBox.Show("Do you want to add this Order?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string result = _orderService.AddOrders(newOrder);
                MessageBox.Show(result, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            foreach (var service in _temporaryServices)
            {
                service.OrderId = newOrder.Id;
                Guid OrderId = service.OrderId;
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

        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[_a-z0-9A-Z-]+(\.[_a-z0-9A-Z-]+)*@[a-z0-9A-Z-]+(\.[a-z0-9A-Z-]+)*(\.[a-zA-Z]{2,3})$");
        }
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            return Regex.IsMatch(phoneNumber, @"^(03|05|07|08|09|01[2|6|8|9])+([0-9]{8})\b$");
        }
        private bool IsValidCCCD(string cccd)
        {
            string cccdPattern = @"^\d{12}$";
            return Regex.IsMatch(cccd, cccdPattern);
        }
        private void btn_Create_Click(object sender, EventArgs e)
        {


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
            var selectedService = _serviceSevice.GetAllServiceFromDb().FirstOrDefault(x => x.Name == selectedServiceName);

            if (selectedService != null)
            {
                if (selectedService.Status == ServiceStatus.OutOfStock)
                {
                    MessageBox.Show("The selected service is out of stock and cannot be added to the order.", "Out of Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int quantityToAdd = (int)cbbnum_quantitySer.Value;

                if (selectedService.Quantity < quantityToAdd)
                {
                    MessageBox.Show("Not enough product quantity.", "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if ((int)cbbnum_quantitySer.Value <= 0)
                {
                    MessageBox.Show("The quantity must be greater than 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                var existingOrderService = _temporaryServices.FirstOrDefault(s => s.ServiceId == selectedService.Id);

                if (existingOrderService != null)
                {
                    existingOrderService.Quantity += quantityToAdd;
                    existingOrderService.TotalPrice = existingOrderService.Price * existingOrderService.Quantity;
                }
                else
                {
                    var orderService = new OrderService()
                    {
                        ServiceId = selectedService.Id,
                        Quantity = quantityToAdd,
                        Price = selectedService.Price,
                        TotalPrice = selectedService.Price * quantityToAdd,
                    };
                    _temporaryServices.Add(orderService);
                }

                selectedService.Quantity -= quantityToAdd;
                _serviceSevice.UpdateService(selectedService);
                _serviceSevice.UpdateServiceStatusAuto();

                MessageBox.Show("Service added successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataGridViewService();
                ClearService();
            }
            else
            {
                MessageBox.Show("Selected service not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                dtgService.Rows.Add(item.ServiceId, Count, _serviceSevice.GetAllServiceFromDb().Where(x => x.Id == item.ServiceId).Select(x => x.Name).FirstOrDefault(), item.Quantity, item.Price, item.TotalPrice.ToString("0"));
                TotalPriceOrderService = _temporaryServices.Sum(s => s.TotalPrice);

            }
            TotalPrice();
        }
        void TotalPrice()
        {
            TimeSpan timeSpanHours = dtGioCheckout.Value - DtGioCheckIn.Value;
            TimeSpan timeSpanDays = dt_checkout.Value.Date - dt_checkin.Value.Date;

            var room = _roomService.GetAllRooms().FirstOrDefault(x => x.Id == RoomId);
            if (room == null)
            {
                lbTotalPrice.Text = "Error: Room not found.";
                return;
            }

            decimal totalDays = 0;
            decimal totalHours = 0;

            if (rddaily.Checked)
            {
                totalDays = (decimal)timeSpanDays.TotalDays;
                totalDays = totalDays <= 0 ? 1 : totalDays; 
                decimal roundedDays = Math.Round(totalDays, MidpointRounding.AwayFromZero);
                TotalPriceRoom = room.PricePerDay * roundedDays;
            }
            else if (rdHourly.Checked)
            {
                totalHours = (decimal)timeSpanHours.TotalHours;
                totalHours = totalHours <= 0 ? 1 : totalHours; 
                decimal roundedHours = Math.Round(totalHours, MidpointRounding.AwayFromZero);
                TotalPriceRoom = room.PriceByHour * roundedHours;
            }
            else
            {
                lbTotalPrice.Text = "Error: Please select a pricing method.";
                return;
            }

            decimal Prepay = 0;
            string prepayText = txtPrepay.Text.Trim();
            if (!string.IsNullOrEmpty(prepayText) && decimal.TryParse(prepayText, out decimal parsedPrepay))
            {
                Prepay = parsedPrepay;
            }

            TotalAmount = TotalPriceRoom + TotalPriceOrderService;
            decimal remainingBalance = TotalAmount - Prepay;

            if (remainingBalance > 0)
            {
                lbTotalPrice.Text = $"{remainingBalance:0} VNĐ";
            }
            else
            {
                lbTotalPrice.Text = $"{Math.Abs(remainingBalance):0} VNĐ Excess";
            }
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
                    if (currentOrder.Rentaltype == RentalTypeEnum.Daily)
                    {
                        rddaily.Checked = true;
                    }
                    else
                    {
                        rdHourly.Checked = true;
                    }
                    OrderId = currentOrder.Id;
                    txtPrepay.Text = currentOrder.Prepay.ToString();
                    tb_note.Text = currentOrder.Note;
                    cbbOrderType.Text = currentOrder.OrderType.ToString();
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

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
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

            decimal prepay = 0;
            if (!string.IsNullOrEmpty(txtPrepay.Text) && decimal.TryParse(txtPrepay.Text, out decimal parsedPrepay))
            {
                prepay = parsedPrepay;
            }
            if (Session.UserId == Guid.Empty)
            {
                MessageBox.Show("Employee does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!rddaily.Checked && !rdHourly.Checked)
            {
                MessageBox.Show("Rental type is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var newOrder = new Orders
            {
                Id = OrderId,
                RoomId = RoomId,
                CustomerId = selectedCustomer.Id,
                DateCreated = DtGioCheckIn.Value,
                DatePayment = null,
                Prepay = prepay,
                Note = tb_note.Text,
                EmployeeId = Session.UserId,
                Rentaltype = rdHourly.Checked ? RentalTypeEnum.Hourly : RentalTypeEnum.Daily,
                OrderType = (OrderType)cbbOrderType.SelectedItem,
                ToTalPrice = TotalPriceRoom,
                ToTal = TotalAmount,
            };

            if (MessageBox.Show("Do you want to Update this Order?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string result = _orderService.UpdateOrders(newOrder);
                MessageBox.Show(result, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            var currentServices = _orderServiceService.GetOrderServicesByOrderId(newOrder.Id);

            var servicesToDelete = currentServices.Where(s => !_temporaryServices.Any(ts => ts.ServiceId == s.ServiceId)).ToList();
            foreach (var service in servicesToDelete)
            {
                string deleteResult = _orderServiceService.RemoveOrderServicee(service.OrderId, service.ServiceId);
            }

            foreach (var service in _temporaryServices)
            {
                service.OrderId = newOrder.Id;
                string updateServiceResult = _orderServiceService.UpdateOrderService(service);
                if (updateServiceResult == "Update failcure")
                {
                    string addServiceResult = _orderServiceService.AddOrderService(service);
                }
            }
            this.Close();
        }
        private void cbb_NameService_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            var selectedServiceName = cbb_NameService.SelectedItem.ToString();
            var selectedService = _serviceSevice.GetAllServiceFromDb().FirstOrDefault(x => x.Name == selectedServiceName);
            lbGia.Text = selectedService.Price.ToString();
            txtStatus.Text = selectedService.Status.ToString();
        }
        void LoadDtgOrders()
        {
            dtgListOrders.ColumnCount = 13;
            dtgListOrders.Columns[0].Name = "Id";
            dtgListOrders.Columns[0].Visible = false;
            dtgListOrders.Columns[1].Name = "OrderCode";
            dtgListOrders.Columns[2].Name = "DateCreated";
            dtgListOrders.Columns[3].Name = "Note";
            dtgListOrders.Columns[4].Name = "Rentaltype";
            dtgListOrders.Columns[5].Name = "Employee";
            dtgListOrders.Columns[6].Name = "Customer";
            dtgListOrders.Columns[7].Name = "Prepay";
            dtgListOrders.Columns[8].Name = "OrderType";
            dtgListOrders.Columns[9].Name = "Floor";
            dtgListOrders.Columns[10].Name = "KindOfRoom";
            dtgListOrders.Columns[11].Name = "Room";
            dtgListOrders.Columns[12].Name = "ToTalTime";
            dtgListOrders.Rows.Clear();
            decimal PriceRoom = 0;
            foreach (var item in _orderService.GetOrdersViewModels(OrderId))
            {
                TimeSpan? ToTalTime = DateTime.Now - item.DateCreated;
                string totalTimeString = "Unavailable";
                decimal priceRoom = 0;

                if (ToTalTime.HasValue)
                {
                    if (item.Rentaltype == RentalTypeEnum.Daily)
                    {
                        int NgayLamTron = (int)Math.Round(ToTalTime.Value.TotalDays);
                        NgayLamTron = NgayLamTron < 1 ? 1 : NgayLamTron;
                        totalTimeString = $"{NgayLamTron} days";
                    }
                    else if (item.Rentaltype == RentalTypeEnum.Hourly)
                    {
                        int GioLamTron = (int)Math.Round(ToTalTime.Value.TotalHours);
                        GioLamTron = GioLamTron < 1 ? 1 : GioLamTron;
                        totalTimeString = $"{GioLamTron} hours";
                    }
                }

                if (item.Rentaltype == RentalTypeEnum.Daily)
                {
                    PriceRoom = item.PricePerDay;
                }
                else
                {
                    PriceRoom = item.PriceByHour;
                }

                dtgListOrders.Rows.Add(item.Id, item.OrderCode, item.DateCreated,
                                       item.Note, item.Rentaltype, item.EmployeeName, item.CustomerName,
                                       item.Prepay, item.OrderType, item.FloorName, item.KindOfRoomName,
                                       item.RoomName, totalTimeString, PriceRoom);
            }
        }
        private void btn_ClearCus_Click(object sender, EventArgs e)
        {
            ClearBookingDetails();
        }

        private void btn_clearSer_Click(object sender, EventArgs e)
        {
            ClearService();
        }
        void ClearService()
        {
            cbbnum_quantitySer.Value = 0;
            lbGia.Text = "";
            cbb_NameService.SelectedItem = -1;
            txtStatus.Text = "";
        }
        void LoadCamera()
        {
            _videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (_videoDevices.Count == 0)
            {
                MessageBox.Show("No video devices found.");
                return;
            }

            cbbcam.Items.Clear();
            foreach (FilterInfo device in _videoDevices)
            {
                cbbcam.Items.Add(device.Name);
            }

            if (cbbcam.Items.Count > 0)
            {
                cbbcam.SelectedIndex = 0;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (cbbcam.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a camera.");
                return;
            }

            try
            {
                _videoCaptureDevice = new VideoCaptureDevice(_videoDevices[cbbcam.SelectedIndex].MonikerString);
                _videoCaptureDevice.NewFrame += new NewFrameEventHandler(videoCaptureDevice_NewFrame);
                _videoCaptureDevice.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error starting video capture: " + ex.Message);
            }
        }

        private bool qrCodeFound = false;
        private void videoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                if (!qrCodeFound)
                {

                    Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();

                    BarcodeReader reader = new BarcodeReader
                    {
                        Options = new DecodingOptions
                        {
                            TryHarder = true,
                            PossibleFormats = new[] { BarcodeFormat.QR_CODE }
                        }
                    };

                    Result result = reader.Decode(bitmap);

                    txtReadCode.Invoke(new MethodInvoker(delegate ()
                    {
                        if (result != null)
                        {
                            txtReadCode.Text = result.Text;
                            DisplayDetails(txtReadCode.Text);
                            qrCodeFound = true;
                        }
                        else
                        {
                            txtReadCode.Text = "No QR code found.";
                        }
                    }));

                    if (ptCam.Image != null)
                    {
                        ptCam.Image.Dispose();
                    }
                    ptCam.Image = bitmap;
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private async void btnStop_Click(object sender, EventArgs e)
        {
            if (_videoCaptureDevice != null && _videoCaptureDevice.IsRunning)
            {
                try
                {
                    _videoCaptureDevice.SignalToStop();

                    await Task.Run(() =>
                    {
                        while (_videoCaptureDevice.IsRunning)
                        {
                            Thread.Sleep(100);
                        }
                    });

                    _videoCaptureDevice = null;

                    if (ptCam.Image != null)
                    {
                        qrCodeFound = false;
                        ptCam.Image.Dispose();
                        ptCam.Image = null;
                    }

                    txtReadCode.Text = string.Empty;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error stopping video capture: " + ex.Message);
                }
            }
        }
        private void DisplayDetails(string input)
        {
            string[] parts = input.Split('|');

            if (parts.Length >= 6)
            {
                string id = parts[0];
                string name = parts[2];
                string address = parts[5];
                string gender = parts[4];

                tb_cccd.Text = id;
                tbCustomerName.Text = name;
                tb_Address.Text = address;
                if (gender == "Nam")
                {
                    rdMale.Checked = true;
                }
                rdFemale.Checked = true;
            }
            else
            {
                MessageBox.Show("The input string does not have the expected format.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            frmPay frm = new frmPay(OrderId);
            frm.Show();
        }

        private void frmRoomBookingReceipt_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbCustomerName.Text))
            {
                MessageBox.Show("Customer name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(tb_emailCus.Text) || !IsValidEmail(tb_emailCus.Text))
            {
                MessageBox.Show("A valid email address is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(tb_cccd.Text))
            {
                MessageBox.Show("CCCD is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(tb_Address.Text))
            {
                MessageBox.Show("Address is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (rdFemale.Checked == false && rdMale.Checked == false)
            {
                MessageBox.Show("Please select a gender.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(tb_phoneCus.Text) || !IsValidPhoneNumber(tb_phoneCus.Text))
            {
                MessageBox.Show("A valid phone number is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!IsValidEmail(tb_emailCus.Text))
            {
                MessageBox.Show("Email is invalid.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!IsValidPhoneNumber(tb_phoneCus.Text))
            {
                MessageBox.Show("Email is invalid.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!IsValidCCCD(tb_cccd.Text))
            {
                MessageBox.Show("CCCD is invalid.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
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
            ClearBookingDetails();
        }
    }
}
