using BUS.IService;
using BUS.Service;
using DAL.Entities;
using DAL.Enums;
using ICSharpCode.SharpZipLib.Zip;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iText.Layout.Properties;
using HorizontalAlignment = iText.Layout.Properties.HorizontalAlignment;
using iText.Kernel.Colors;
using System.Diagnostics;
using iText.Kernel.Exceptions;
using BUS.EntitiesApiPay;
using Newtonsoft.Json;
using RestSharp;
using Image = System.Drawing.Image;
using System.Drawing.Drawing2D;
using Rectangle = System.Drawing.Rectangle;
using iText.IO.Image;
namespace QuanLyPhong
{
    public partial class frmQuanLyOrder : Form
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
        private IEmployeeService _employeeService;
        private IVoucherSevice _voucherSevice;
        private Guid SelectServiceId = Guid.Empty;
        private Guid OrderId = Guid.Empty;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem toolStripEdit;
        private ToolStripMenuItem toolStripDelete;

        public frmQuanLyOrder()
        {
            InitializeComponent();
            _serviceSevice = new ServiceService();
            _roomService = new RoomService();
            _customerService = new CustomerService();
            _orderServiceService = new OrderServiceService();
            _kindOfRoomService = new KindOfRoomService();
            _temporaryServices = new List<OrderService>();
            _orderService = new OrderServicess();
            _employeeService = new EmployeeService();
            ServiceTongTien();
            LoadDataGridViewOrder();
            LoadMeNu();
            LoadCbbPayment();

        }
        void LoadMeNu()
        {
            contextMenuStrip = new ContextMenuStrip();
            toolStripEdit = new ToolStripMenuItem("Edit");
            toolStripDelete = new ToolStripMenuItem("Delete");

            toolStripEdit.Click += ToolStripEdit_Click;
            toolStripDelete.Click += ToolStripDelete_Click;

            contextMenuStrip.Items.AddRange(new ToolStripItem[] { toolStripEdit, toolStripDelete });

            dtgService.ContextMenuStrip = contextMenuStrip;
        }

        private void ToolStripEdit_Click(object? sender, EventArgs e)
        {
            if (dtgService.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dtgService.SelectedRows[0];
                if (selectedRow.Cells["Id"].Value == null || selectedRow.Cells["Quantity"].Value == null)
                {
                    MessageBox.Show("Selected row is missing required information.");
                    return;
                }
                var serviceId = (Guid)selectedRow.Cells["Id"].Value;
                var serviceToUpdate = _orderServiceService.GetAllOrderServiceFromDb()
                    .FirstOrDefault(s => s.ServiceId == serviceId && s.OrderId == OrderId);
                if (serviceToUpdate == null)
                {
                    MessageBox.Show("Service not found in the order.");
                    return;
                }
                var orderViewModel = _orderService.GetOrdersViewModels(OrderId);
                if (orderViewModel == null)
                {
                    MessageBox.Show("Order details not found or the order has been paid.");
                    return;
                }
                int newQuantity;
                if (!int.TryParse(selectedRow.Cells["Quantity"].Value.ToString(), out newQuantity) || newQuantity < 0)
                {
                    MessageBox.Show("The quantity must be greater than 0.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var service = _serviceSevice.GetAllServiceFromDb().FirstOrDefault(s => s.Id == serviceToUpdate.ServiceId);
                if (service != null)
                {
                    int quantityDifference = newQuantity - serviceToUpdate.Quantity;
                    if (service.Quantity - quantityDifference < 0)
                    {
                        MessageBox.Show($"Not enough product quantity.Quantity now: {service.Quantity}.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    service.Quantity -= quantityDifference;
                    _serviceSevice.UpdateService(service);
                }
                serviceToUpdate.Quantity = newQuantity;
                serviceToUpdate.TotalPrice = newQuantity * service.Price;
                _orderServiceService.UpdateOrderService(serviceToUpdate);
                MessageBox.Show("Order Service updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataGridViewService(OrderId);
            }
            else
            {
                MessageBox.Show("No row selected.");
            }
        }



        private void ToolStripDelete_Click(object? sender, EventArgs e)
        {
            try
            {
                if (dtgService.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dtgService.SelectedRows[0];
                    if (selectedRow.Cells["Id"].Value == null)
                    {
                        MessageBox.Show("Selected row is missing required information.");
                        return;
                    }
                    var serviceId = (Guid)selectedRow.Cells["Id"].Value;
                    var serviceToRemove = _orderServiceService.GetAllOrderServiceFromDb()
                        .FirstOrDefault(s => s.ServiceId == serviceId && s.OrderId == OrderId);
                    var orderViewModel = _orderService.GetOrdersViewModels(OrderId)
                        .FirstOrDefault(x => x.DatePayment == null);

                    if (orderViewModel == null)
                    {
                        MessageBox.Show("Order has already been paid or does not exist.");
                        return;
                    }
                    if (serviceToRemove != null)
                    {
                        var service = _serviceSevice.GetAllServiceFromDb().FirstOrDefault(s => s.Id == serviceToRemove.ServiceId);
                        if (service != null)
                        {
                            service.Quantity += serviceToRemove.Quantity;
                            _serviceSevice.UpdateService(service);
                        }
                        else
                        {
                            MessageBox.Show("Service not found in the inventory.");
                            return;
                        }
                        _orderServiceService.RemoveOrderServicee(serviceToRemove.OrderId, serviceToRemove.ServiceId);
                        MessageBox.Show("Service removed successfully.");
                        LoadDataGridViewService(OrderId);
                    }
                    else
                    {
                        MessageBox.Show("Service not found in the order.");
                    }
                }
                else
                {
                    MessageBox.Show("No row selected.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
        void LoadDataGridViewOrder()
        {
            dtgv_order.ColumnCount = 19;
            dtgv_order.Columns[0].Name = "Id";
            dtgv_order.Columns[0].Visible = false;
            dtgv_order.Columns[1].Name = "STT";
            dtgv_order.Columns[2].Name = "OrderCode";
            dtgv_order.Columns[3].Name = "Employee";
            dtgv_order.Columns[4].Name = "DateCreated";
            dtgv_order.Columns[5].Name = "DatePayment";
            dtgv_order.Columns[6].Name = "Customer";
            dtgv_order.Columns[7].Name = "CustomerPhone";
            dtgv_order.Columns[8].Name = "Prepay";
            dtgv_order.Columns[9].Name = "ToTalPriceRoom";
            dtgv_order.Columns[10].Name = "NameRoom";
            dtgv_order.Columns[11].Name = "Note";
            dtgv_order.Columns[12].Name = "PriceRoom";
            dtgv_order.Columns[13].Name = "PointAdded";
            dtgv_order.Columns[14].Name = "TotalPricePoint";
            dtgv_order.Columns[15].Name = "TotalDiscount";
            dtgv_order.Columns[16].Name = "RenType";
            dtgv_order.Columns[17].Name = "TotalTime";
            dtgv_order.Columns[18].Name = "Total";

            dtgv_order.Rows.Clear();
            int Count = 0;
            decimal PriceRoom = 0;
            foreach (var item in _orderService.GetAllOrdersViewModels())
            {
                TimeSpan? ToTalTime;

                if (item.DatePayment.HasValue)
                {
                    ToTalTime = item.DatePayment.Value - item.DateCreated;
                }
                else
                {
                    ToTalTime = DateTime.Now - item.DateCreated;
                }

                string totalTimeString = "Unavailable";

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
                var employ = _employeeService.GetAllEmployeeFromDb().Where(x => x.Id == item.EmployeeId).Select(x => x.Name).FirstOrDefault();
                var Customer = _customerService.GetAllCustomerFromDb().Where(x => x.Id == item.CustomerId).Select(x => x.Name).FirstOrDefault();
                var CustomerPhone = _customerService.GetAllCustomerFromDb().Where(x => x.Id == item.CustomerId).Select(x => x.PhoneNumber).FirstOrDefault();

                var room = _roomService.GetAllRoomsFromDb().Where(x => x.Id == item.RoomId).Select(x => x.RoomName).FirstOrDefault();
                var datePayment = item.DatePayment.HasValue ? item.DatePayment.Value.ToString("dd/MM/yyyy HH:mm") : "Chưa thanh toán";
                Count++;

                decimal PointAdd = item.ToTalPrice * 0.01m;
                dtgv_order.Rows.Add(item.Id, Count, item.OrderCode, employ, item.DateCreated.Value.ToString("dd/MM/yyyy HH:mm"), datePayment, Customer,
                    CustomerPhone, item.Prepay,
                 item.ToTalPrice, room, item.Note, PriceRoom, PointAdd, item.TotalPricePoint.ToString(), item.TotalDiscount, item.Rentaltype, totalTimeString, item.ToTal);
            }
        }
        void ServiceTongTien()
        {
            txt_priretotalser.Enabled = false;
            txt_TotalPrice.Enabled = false;
            txt_price.Enabled = false;
            txt_quantity.TextChanged += Txt_Quantity_TextChanged;

        }
        private void Txt_Quantity_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txt_quantity.Text, out int quantity) &&
             decimal.TryParse(txt_price.Text, out decimal price))
            {
                decimal total = quantity * price;
                txt_priretotalser.Text = total.ToString("N0");
            }
            else
            {
                txt_priretotalser.Text = "0";
            }
        }
        void LoadDataGridViewService(Guid orderId)
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
            var orderservice = _orderServiceService.GetAllOrderService().Where(x => x.OrderId == orderId);
            int count = 0;
            foreach (var item in orderservice)
            {
                count++;
                var serviceName = _serviceSevice.GetAllServiceFromDb().Where(x => x.Id == item.ServiceId).Select(x => x.Name).FirstOrDefault();
                dtgService.Rows.Add(item.ServiceId, count, serviceName, item.QuantityOrderService, item.PriceOrderService, item.TotalPrice.ToString("0"));
            }
        }
        private void dtgv_order_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.RowIndex < dtgv_order.Rows.Count)
                {
                    var selectedRow = dtgv_order.Rows[e.RowIndex];

                    if (selectedRow.IsNewRow)
                    {
                        MessageBox.Show("no information.");
                        return;
                    }

                    if (selectedRow.Cells["Id"].Value == null || selectedRow.Cells["Id"].Value == DBNull.Value)
                    {
                        MessageBox.Show("no information.");
                        return;
                    }

                    txt_Code.Text = selectedRow.Cells["OrderCode"].Value?.ToString();
                    txt_employe.Text = selectedRow.Cells["Employee"].Value?.ToString();
                    txt_Cus.Text = selectedRow.Cells["Customer"].Value?.ToString();
                    txt_phone.Text = selectedRow.Cells["CustomerPhone"].Value?.ToString();
                    txt_prepay.Text = selectedRow.Cells["Prepay"].Value?.ToString();
                    txt_TotalPrice.Text = selectedRow.Cells["Total"].Value?.ToString();
                    txt_Roomprice.Text = selectedRow.Cells["ToTalPriceRoom"].Value?.ToString();
                    txt_nameroom.Text = selectedRow.Cells["NameRoom"].Value?.ToString();

                    OrderId = Guid.Parse(selectedRow.Cells["Id"].Value.ToString());
                    LoadDataGridViewService(OrderId);
                }
                else
                {
                    MessageBox.Show("no information.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error ! An error occurred. Please try again later : " + ex.Message);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (dtgv_order.SelectedRows.Count > 0)
            {
                var selectedRow = dtgv_order.SelectedRows[0];

                var Value = selectedRow.Cells["DatePayment"].Value;

                if (Value != "Chưa thanh toán")
                {
                    MessageBox.Show("Paid invoices cannot be edited");
                    return;
                }
                else
                {
                    tabControl1.SelectedTab = tabPage2;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e) // tim kiem
        {
            string searchText = textBox1.Text.ToLower();
            var filteredOrders = _orderService.GetAllOrdersFromDb().Where(order =>
            {
                var customer = _customerService.GetAllCustomerFromDb().FirstOrDefault(x => x.Id == order.CustomerId);
                if (customer != null)
                {
                    var customerName = customer.Name.ToLower();
                    var customerPhone = customer.PhoneNumber.ToLower();
                    var orderCode = order.OrderCode.ToLower();
                    return customerName.Contains(searchText) || customerPhone.Contains(searchText) ||
                   orderCode.Contains(searchText);
                }

                return false;
            }).ToList();
            dtgv_order.Rows.Clear();
            int count = 0;
            foreach (var item in filteredOrders)
            {
                var employ = _employeeService.GetAllEmployeeFromDb().Where(x => x.Id == item.EmployeeId).Select(x => x.Name).FirstOrDefault();
                var customer = _customerService.GetAllCustomerFromDb().Where(x => x.Id == item.CustomerId).Select(x => x.Name).FirstOrDefault();
                var customerPhone = _customerService.GetAllCustomerFromDb().Where(x => x.Id == item.CustomerId).Select(x => x.PhoneNumber).FirstOrDefault();
                var room = _roomService.GetAllRoomsFromDb().Where(x => x.Id == item.RoomId).Select(x => x.RoomName).FirstOrDefault();
                var datePayment = item.DatePayment.HasValue ? item.DatePayment.Value.ToString("dd/MM/yyyy HH:mm") : "Chưa thanh toán";
                count++;
                dtgv_order.Rows.Add(item.Id, count, item.OrderCode, employ, item.DateCreated.Value.ToString("dd/MM/yyyy HH:mm"), datePayment, customer, customerPhone, item.Prepay, item.ToTal, item.ToTalPrice, room, item.Note);
            }
        }

        private void dtgv_order_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void btn_edit_Click(object sender, EventArgs e)
        {
            if (dtgv_order.SelectedRows.Count > 0)
            {
                var selectedRow = dtgv_order.SelectedRows[0];
                Guid orderId = (Guid)selectedRow.Cells["Id"].Value;
                string orderCode = txt_Code.Text;
                string employeeName = txt_employe.Text;
                string customerName = txt_Cus.Text;
                string customerPhone = txt_phone.Text;
                decimal prepay = decimal.Parse(txt_prepay.Text);
                decimal totalPrice = decimal.Parse(txt_TotalPrice.Text);
                decimal roomPrice = decimal.Parse(txt_Roomprice.Text);
                string roomName = txt_nameroom.Text;

                var order = _orderService.GetAllOrdersFromDb().FirstOrDefault(o => o.Id == orderId);
                if (order != null)
                {
                    order.OrderCode = orderCode;
                    order.EmployeeId = _employeeService.GetAllEmployeeFromDb()
                                                       .FirstOrDefault(e => e.Name == employeeName)?.Id ?? order.EmployeeId;
                    order.CustomerId = _customerService.GetAllCustomerFromDb()
                                                       .FirstOrDefault(c => c.Name == customerName)?.Id ?? order.CustomerId;
                    order.Prepay = prepay;
                    order.ToTal = totalPrice;
                    order.ToTalPrice = roomPrice;
                    _orderService.UpdateOrders(order);
                    MessageBox.Show("Successfully updated!");

                    tabControl1.SelectedTab = Information;
                    LoadDataGridViewOrder();
                    LoadDataGridViewService(orderId);
                }
                else
                {
                    MessageBox.Show("No orders found to update.");
                }
            }
            else
            {
                MessageBox.Show("Please select the order that needs editing.");
            }
        }


        private void dtgService_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dtgService.Rows.Count)
            {
                var selectedRow = dtgService.Rows[e.RowIndex];
                txt_nameService.Text = selectedRow.Cells["Name"].Value?.ToString();
                txt_quantity.Text = selectedRow.Cells["Quantity"].Value?.ToString();
                txt_priretotalser.Text = selectedRow.Cells["TotalPrice"].Value?.ToString();
                txt_price.Text = selectedRow.Cells["Price"].Value?.ToString();
            }
        }

        private void btn_editorderser_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtgService.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select the Service needs editing.");
                    return;
                }
                var selectedRow = dtgService.SelectedRows[0];
                Guid serviceId = (Guid)selectedRow.Cells["Id"].Value;
                int newQuantity;
                if (!int.TryParse(txt_quantity.Text, out newQuantity) || newQuantity <= 0)
                {
                    MessageBox.Show("Please enter a valid quantity (positive integer).");
                    return;
                }
                var selectedOrderRow = dtgv_order.SelectedRows[0];
                Guid orderId = (Guid)selectedOrderRow.Cells["Id"].Value;
                var orderService = _orderServiceService.GetOrderServicesByOrderId(orderId)
                    .FirstOrDefault(os => os.ServiceId == serviceId);
                if (orderService == null)
                {
                    MessageBox.Show("Service not found in the order.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var service = _serviceSevice.GetAllServiceFromDb().FirstOrDefault(s => s.Id == serviceId);
                if (service != null)
                {
                    int quantityDifference = newQuantity - orderService.Quantity;
                    if (service.Quantity - quantityDifference < 0)
                    {
                        MessageBox.Show($"Not enough product quantity.Quantity now: {service.Quantity}.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    service.Quantity -= quantityDifference;
                    _serviceSevice.UpdateService(service);
                }
                orderService.Quantity = newQuantity;
                orderService.TotalPrice = decimal.Parse(txt_priretotalser.Text);
                _orderServiceService.UpdateOrderService(orderService);
                MessageBox.Show("Updated the number of services successfully.");
                LoadDataGridViewService(orderId);
                LoadDataGridViewOrder();
                tabControl1.SelectedTab = Information;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error ! An error occurred. Please try again later : {ex.Message}");
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {

        }

        private void rdHavePaymented_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdNotyetPaid_CheckedChanged(object sender, EventArgs e)
        {

        }
        void LoadDataGridViewOrderFilter()
        {
            dtgv_order.ColumnCount = 20;
            dtgv_order.Columns[0].Name = "Id";
            dtgv_order.Columns[0].Visible = false;
            dtgv_order.Columns[1].Name = "STT";
            dtgv_order.Columns[2].Name = "OrderCode";
            dtgv_order.Columns[3].Name = "Employee";
            dtgv_order.Columns[4].Name = "DateCreated";
            dtgv_order.Columns[5].Name = "DatePayment";
            dtgv_order.Columns[6].Name = "Customer";
            dtgv_order.Columns[7].Name = "CustomerPhone";
            dtgv_order.Columns[8].Name = "Prepay";
            dtgv_order.Columns[9].Name = "OrderType";
            dtgv_order.Columns[10].Name = "ToTalPriceRoom";
            dtgv_order.Columns[11].Name = "NameRoom";
            dtgv_order.Columns[12].Name = "Note";
            dtgv_order.Columns[13].Name = "PriceRoom";
            dtgv_order.Columns[14].Name = "PointAdded";
            dtgv_order.Columns[15].Name = "TotalPricePoint";
            dtgv_order.Columns[16].Name = "TotalDiscount";
            dtgv_order.Columns[17].Name = "RenType";
            dtgv_order.Columns[18].Name = "TotalTime";
            dtgv_order.Columns[19].Name = "Total";

            dtgv_order.Rows.Clear();
            int Count = 0;
            decimal PriceRoom = 0;
            foreach (var item in _orderService.GetAllOrdersViewModels().Where(x => x.PayMents == null))
            {
                TimeSpan? ToTalTime;

                if (item.DatePayment.HasValue)
                {
                    ToTalTime = item.DatePayment.Value - item.DateCreated;
                }
                else
                {
                    ToTalTime = DateTime.Now - item.DateCreated;
                }

                string totalTimeString = "Unavailable";

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
                var employ = _employeeService.GetAllEmployeeFromDb().Where(x => x.Id == item.EmployeeId).Select(x => x.Name).FirstOrDefault();
                var Customer = _customerService.GetAllCustomerFromDb().Where(x => x.Id == item.CustomerId).Select(x => x.Name).FirstOrDefault();
                var CustomerPhone = _customerService.GetAllCustomerFromDb().Where(x => x.Id == item.CustomerId).Select(x => x.PhoneNumber).FirstOrDefault();

                var room = _roomService.GetAllRoomsFromDb().Where(x => x.Id == item.RoomId).Select(x => x.RoomName).FirstOrDefault();
                var datePayment = item.DatePayment.HasValue ? item.DatePayment.Value.ToString("dd/MM/yyyy HH:mm") : "Chưa thanh toán";
                Count++;

                decimal PointAdd = item.ToTalPrice * 0.01m;
                dtgv_order.Rows.Add(item.Id, Count, item.OrderCode, employ, item.DateCreated.Value.ToString("dd/MM/yyyy HH:mm"), datePayment, Customer,
                    CustomerPhone, item.Prepay,
                    item.OrderType, item.ToTalPrice, room, item.Note, PriceRoom, PointAdd, item.TotalPricePoint.ToString(), item.TotalDiscount, item.Rentaltype, totalTimeString, item.ToTal);
            }
        }

        void LoadDataGridViewOrderFilter2()
        {
            dtgv_order.ColumnCount = 20;
            dtgv_order.Columns[0].Name = "Id";
            dtgv_order.Columns[0].Visible = false;
            dtgv_order.Columns[1].Name = "STT";
            dtgv_order.Columns[2].Name = "OrderCode";
            dtgv_order.Columns[3].Name = "Employee";
            dtgv_order.Columns[4].Name = "DateCreated";
            dtgv_order.Columns[5].Name = "DatePayment";
            dtgv_order.Columns[6].Name = "Customer";
            dtgv_order.Columns[7].Name = "CustomerPhone";
            dtgv_order.Columns[8].Name = "Prepay";
            dtgv_order.Columns[9].Name = "OrderType";
            dtgv_order.Columns[10].Name = "ToTalPriceRoom";
            dtgv_order.Columns[11].Name = "NameRoom";
            dtgv_order.Columns[12].Name = "Note";
            dtgv_order.Columns[13].Name = "PriceRoom";
            dtgv_order.Columns[14].Name = "PointAdded";
            dtgv_order.Columns[15].Name = "TotalPricePoint";
            dtgv_order.Columns[16].Name = "TotalDiscount";
            dtgv_order.Columns[17].Name = "RenType";
            dtgv_order.Columns[18].Name = "TotalTime";
            dtgv_order.Columns[19].Name = "Total";

            dtgv_order.Rows.Clear();
            int Count = 0;
            decimal PriceRoom = 0;
            foreach (var item in _orderService.GetAllOrdersViewModels().Where(x => x.PayMents != null))
            {
                TimeSpan? ToTalTime;

                if (item.DatePayment.HasValue)
                {
                    ToTalTime = item.DatePayment.Value - item.DateCreated;
                }
                else
                {
                    ToTalTime = DateTime.Now - item.DateCreated;
                }

                string totalTimeString = "Unavailable";

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
                var employ = _employeeService.GetAllEmployeeFromDb().Where(x => x.Id == item.EmployeeId).Select(x => x.Name).FirstOrDefault();
                var Customer = _customerService.GetAllCustomerFromDb().Where(x => x.Id == item.CustomerId).Select(x => x.Name).FirstOrDefault();
                var CustomerPhone = _customerService.GetAllCustomerFromDb().Where(x => x.Id == item.CustomerId).Select(x => x.PhoneNumber).FirstOrDefault();

                var room = _roomService.GetAllRoomsFromDb().Where(x => x.Id == item.RoomId).Select(x => x.RoomName).FirstOrDefault();
                var datePayment = item.DatePayment.HasValue ? item.DatePayment.Value.ToString("dd/MM/yyyy HH:mm") : "Chưa thanh toán";
                Count++;

                decimal PointAdd = item.ToTalPrice * 0.01m;
                dtgv_order.Rows.Add(item.Id, Count, item.OrderCode, employ, item.DateCreated.Value.ToString("dd/MM/yyyy HH:mm"), datePayment, Customer,
                    CustomerPhone, item.Prepay,
                    item.OrderType, item.ToTalPrice, room, item.Note, PriceRoom, PointAdd, item.TotalPricePoint.ToString(), item.TotalDiscount, item.Rentaltype, totalTimeString, item.ToTal);
            }
        }
        void LoadCbbPayment()
        {
            string[] Payment = { "All", "Have been paid", "Not yet paid" };
            foreach (var item in Payment)
            {
                cbbPayment.Items.Add(item);
            }
        }
        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbPayment.SelectedIndex == 2)
            {
                LoadDataGridViewOrderFilter();
            }
            else if (cbbPayment.SelectedIndex == 1)
            {
                LoadDataGridViewOrderFilter2();
            }
            else
            {
                LoadDataGridViewOrder();
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtgService.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select service needs delete.");
                    return;
                }
                var selectedServiceRow = dtgService.SelectedRows[0];
                if (selectedServiceRow.Cells["Id"].Value == null || selectedServiceRow.Cells["Id"].Value == DBNull.Value)
                {
                    MessageBox.Show("The selected service is invalid.");
                    return;
                }
                Guid serviceId = (Guid)selectedServiceRow.Cells["Id"].Value;
                if (dtgv_order.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select order needs delete.");
                    return;
                }
                var selectedOrderRow = dtgv_order.SelectedRows[0];
                if (selectedOrderRow.Cells["Id"].Value == null || selectedOrderRow.Cells["Id"].Value == DBNull.Value)
                {
                    MessageBox.Show("The selected order is invalid.");
                    return;
                }
                Guid orderId = (Guid)selectedOrderRow.Cells["Id"].Value;
                var nameroom = selectedOrderRow.Cells["NameRoom"].Value?.ToString();
                var paymentStatus = selectedOrderRow.Cells["DatePayment"].Value?.ToString();
                if (paymentStatus != null && paymentStatus != "Chưa thanh toán")
                {
                    MessageBox.Show("Paid invoices cannot be deleted.");
                    return;
                }
                var confirmResult = MessageBox.Show("Are you sure you want to remove this service from your order?", "Confirm", MessageBoxButtons.YesNo);
                if (confirmResult != DialogResult.Yes) return;
                var orderServiceToDelete = _orderServiceService.GetOrderServicesByOrderId(orderId)
                    .FirstOrDefault(os => os.ServiceId == serviceId);
                if (orderServiceToDelete != null)
                {
                    var service = _serviceSevice.GetAllServiceFromDb().FirstOrDefault(s => s.Id == orderServiceToDelete.ServiceId);
                    if (service != null)
                    {
                        service.Quantity += orderServiceToDelete.Quantity;
                        _serviceSevice.UpdateService(service);
                    }
                    else
                    {
                        MessageBox.Show($"Cannot found service: {orderServiceToDelete.ServiceId}");
                    }
                    _orderServiceService.RemoveOrderServicee(orderId, serviceId);
                    MessageBox.Show("service deleted successfully");
                }
                else
                {
                    MessageBox.Show("Cannot found serivce in orders.");
                }

                LoadDataGridViewOrder();
                LoadDataGridViewService(orderId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error ! An error occurred. Please try again later :: {ex.Message}");
            }
        }
        private void btn_addNewSer_Click(object sender, EventArgs e)
        {

        }

        void Issue_An_invoice()
        {
            var filterData = _orderService.GetOrdersViewModels(OrderId).FirstOrDefault();
            if (filterData == null)
            {
                MessageBox.Show("Order data not found.");
                return;
            }

            var customer = _customerService.GetAllCustomerFromDb().FirstOrDefault(x => x.Id == filterData.CustomerId);
            if (customer == null)
            {
                MessageBox.Show("Customer not found.");
                return;
            }
            string directoryPath = @"D:\Tai_Lieu_Sinh_Vien\duan1\pdf";
            string fileName = $"invoice_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.pdf";
            string filePath = System.IO.Path.Combine(directoryPath, fileName);

            try
            {
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                using (PdfWriter writer = new PdfWriter(filePath))
                {
                    using (PdfDocument pdf = new PdfDocument(writer))
                    {
                        Document document = new Document(pdf, PageSize.A4);

                        PdfFont font = PdfFontFactory.CreateFont(@"C:\Windows\Fonts\ARIAL.TTF", PdfEncodings.IDENTITY_H);

                        // Thêm tiêu đề hóa đơn
                        document.Add(new Paragraph("HÓA ĐƠN")
                             .SetFont(font)
                             .SetBold()
                             .SetTextAlignment(TextAlignment.CENTER)
                             .SetFontSize(26)
                             .SetMarginBottom(20));

                        // Thêm thông tin khách hàng
                        document.Add(new Paragraph($"Khách hàng: {customer.Name}")
                            .SetFont(font)
                            .SetFontSize(14)
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetMarginBottom(10));
                        document.Add(new Paragraph($"Địa chỉ: {customer.Address}")
                            .SetFont(font)
                            .SetFontSize(14)
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetMarginBottom(20));
                        document.Add(new Paragraph($"Mã Hóa Đơn: {filterData.OrderCode}")
                           .SetFont(font)
                           .SetFontSize(14)
                           .SetTextAlignment(TextAlignment.CENTER)
                           .SetMarginBottom(20));
                        var qrData = GetQRCodeImageData();
                        if (filterData.PayMents == "Payment by card")
                        {
                            if (!string.IsNullOrEmpty(qrData))
                            {
                                Image qrImage = Base64ToImage(qrData);
                                if (qrImage != null)
                                {
                                    int qrImageSize = 150;
                                    Image resizedQrImage = ResizeImage(qrImage, qrImageSize);
                                    byte[] qrImageData = ImageToByteArray(resizedQrImage);

                                    iText.Layout.Element.Image pdfImage = new iText.Layout.Element.Image(ImageDataFactory.Create(qrImageData));
                                    pdfImage.SetWidth(qrImageSize);
                                    pdfImage.SetHeight(qrImageSize);

                                    Paragraph qrImageParagraph = new Paragraph().Add(pdfImage);
                                    qrImageParagraph.SetTextAlignment(TextAlignment.CENTER);
                                    qrImageParagraph.SetMarginTop(20);

                                    document.Add(qrImageParagraph);
                                }
                                else
                                {
                                    MessageBox.Show("Không thể chuyển đổi hình ảnh QR từ dữ liệu base64.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("QR image data is empty or invalid from API.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                        // Bảng sản phẩm
                        float[] productColumnWidths = { 1, 3, 2, 2, 2 };
                        Table productTable = new Table(productColumnWidths);
                        productTable.SetBorder(new SolidBorder(1));
                        productTable.SetWidth(UnitValue.CreatePercentValue(100));

                        productTable.SetHorizontalAlignment(HorizontalAlignment.CENTER);
                        document.Add(new Paragraph($"Hóa Đơn Dịch Vụ")
                         .SetFont(font)
                         .SetFontSize(14)
                         .SetTextAlignment(TextAlignment.LEFT)
                         .SetMarginBottom(20));

                        productTable.AddHeaderCell(new Cell().Add(new Paragraph("Số thứ tự"))
                            .SetFont(font)
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetBold()
                            .SetBackgroundColor(new DeviceRgb(0, 102, 204))
                            .SetFontColor(ColorConstants.WHITE));
                        productTable.AddHeaderCell(new Cell().Add(new Paragraph("Tên hàng hóa"))
                            .SetFont(font)
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetBold()
                            .SetBackgroundColor(new DeviceRgb(0, 102, 204))
                            .SetFontColor(ColorConstants.WHITE));
                        productTable.AddHeaderCell(new Cell().Add(new Paragraph("Giá"))
                            .SetFont(font)
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetBold()
                            .SetBackgroundColor(new DeviceRgb(0, 102, 204))
                            .SetFontColor(ColorConstants.WHITE));
                        productTable.AddHeaderCell(new Cell().Add(new Paragraph("Số Lượng"))
                            .SetFont(font)
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetBold()
                            .SetBackgroundColor(new DeviceRgb(0, 102, 204))
                            .SetFontColor(ColorConstants.WHITE));
                        productTable.AddHeaderCell(new Cell().Add(new Paragraph("Tổng Giá"))
                            .SetFont(font)
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetBold()
                            .SetBackgroundColor(new DeviceRgb(0, 102, 204))
                            .SetFontColor(ColorConstants.WHITE));

                        var getOrderService = _orderServiceService.GetAllOrderService().Where(x => x.OrderId == OrderId);
                        // Thêm từng sản phẩm vào bảng
                        int count = 0;
                        decimal ToTalPriceService = 0;
                        foreach (var item in getOrderService)
                        {
                            count++;
                            AddTableRow(productTable, count, item.Name, item.PriceOrderService, item.QuantityOrderService, item.TotalPrice);
                            ToTalPriceService = getOrderService.Sum(s => s.TotalPrice);
                        }
                        Cell totalLabelCell = new Cell(1, 4)
                           .Add(new Paragraph("Tổng cộng"))
                           .SetFont(font)
                           .SetTextAlignment(TextAlignment.CENTER)
                           .SetBold()
                           .SetBorder(new SolidBorder(1));
                        productTable.AddCell(totalLabelCell);
                        productTable.AddCell(new Cell().Add(new Paragraph($"{ToTalPriceService.ToString("0")} VNĐ"))
                            .SetFont(font)
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetBorder(new SolidBorder(1)));
                        document.Add(productTable);
                        productTable.SetHorizontalAlignment(HorizontalAlignment.CENTER);
                        document.Add(new Paragraph($"Hóa Đơn Thuê Phòng")
                         .SetFont(font)
                         .SetFontSize(14)
                         .SetTextAlignment(TextAlignment.LEFT)
                         .SetMarginBottom(20));
                        float[] orderColumnWidths = { 1, 2 };
                        Table orderTable = new Table(orderColumnWidths);
                        orderTable.SetBorder(new SolidBorder(1));
                        orderTable.SetWidth(UnitValue.CreatePercentValue(100));
                        orderTable.SetHorizontalAlignment(HorizontalAlignment.CENTER);

                        void AddOrderHeaderCell(string text)
                        {
                            orderTable.AddCell(new Cell().Add(new Paragraph(text))
                                .SetFont(font)
                                .SetTextAlignment(TextAlignment.CENTER)
                                .SetBold()
                                .SetBackgroundColor(new DeviceRgb(0, 102, 204))
                                .SetFontColor(ColorConstants.WHITE)
                                .SetBorder(new SolidBorder(1)));
                        }

                        void AddOrderDataCell(string text)
                        {
                            orderTable.AddCell(new Cell().Add(new Paragraph(text))
                                .SetFont(font)
                                .SetTextAlignment(TextAlignment.CENTER)
                                .SetBorder(new SolidBorder(1)));
                        }

                        // Add order information headers and data
                        AddOrderHeaderCell("Thông tin");
                        AddOrderHeaderCell("Giá trị");

                        AddOrderDataCell("Mã Hóa Đơn");
                        AddOrderDataCell(filterData.OrderCode);

                        AddOrderDataCell("Ngày Tạo Hóa Đơn");
                        AddOrderDataCell(filterData.DateCreated.HasValue ? filterData.DateCreated.Value.ToString("dd/MM/yyyy") : "N/A");

                        AddOrderDataCell("Ngày Thanh Toán");
                        AddOrderDataCell(filterData.DatePayment.HasValue ? filterData.DatePayment.Value.ToString("dd/MM/yyyy") : "Chưa thanh toán");

                        AddOrderDataCell("Hóa Đơn Được Tạo Bởi");
                        AddOrderDataCell(filterData.EmployeeName);

                        decimal TotalPriceDiscount = 0;
                        if (filterData.TotalDiscount > 0)
                        {
                            TotalPriceDiscount = filterData.TotalDiscount.Value;
                        }

                        AddOrderDataCell("Tổng Tiền Giảm Giá");
                        AddOrderDataCell(TotalPriceDiscount.ToString("0") + " VND");

                        decimal TotalPricePoint = 0;
                        if (filterData.TotalPricePoint.HasValue && filterData.TotalPricePoint.Value > 0)
                        {
                            TotalPricePoint = filterData.TotalPricePoint.Value;
                        }

                        AddOrderDataCell("Tổng Tiền Giảm Giá Bằng Điểm");
                        AddOrderDataCell(TotalPricePoint.ToString("0") + " VND");

                        TimeSpan? totalTime = filterData.DatePayment - filterData.DateCreated;
                        string totalTimeString = null;

                        if (totalTime.HasValue)
                        {

                            if (filterData.Rentaltype == RentalTypeEnum.Daily)
                            {
                                int NgayLamTron = (int)Math.Round(totalTime.Value.TotalDays);
                                NgayLamTron = NgayLamTron < 1 ? 1 : NgayLamTron;
                                totalTimeString = $"{NgayLamTron} days";
                            }
                            else if (filterData.Rentaltype == RentalTypeEnum.Hourly)
                            {
                                int GioLamTron = (int)Math.Round(totalTime.Value.TotalHours);
                                GioLamTron = GioLamTron < 1 ? 1 : GioLamTron;
                                totalTimeString = $"{GioLamTron} hours";
                            }
                        }




                        AddOrderDataCell("Số Giờ/Ngày Thuê");
                        AddOrderDataCell(totalTimeString);

                        decimal pointAdd = filterData.ToTalPrice * 0.01m;

                        AddOrderDataCell("Số Điểm Cộng");
                        AddOrderDataCell(pointAdd.ToString() + " Points");

                        AddOrderDataCell("Tiền Trả Trước");
                        AddOrderDataCell(filterData.Prepay?.ToString("0") + " VND");

                        string PriceRoom = null;
                        if (filterData.Rentaltype == RentalTypeEnum.Daily)
                        {
                            PriceRoom = filterData.PricePerDay.ToString("0") + " VNĐ";
                        }
                        else
                        {
                            PriceRoom = filterData.PriceByHour.ToString("0") + " VNĐ";
                        }
                        AddOrderDataCell("Giá Phòng");
                        AddOrderDataCell(PriceRoom + " VND");

                        AddOrderDataCell("Tổng Tiền Phòng");
                        AddOrderDataCell(filterData.ToTalPrice.ToString("0") + " VND");

                        AddOrderDataCell("Loại Hình thuê");
                        AddOrderDataCell(filterData.Rentaltype.ToString());

                        AddOrderDataCell("Loại Phòng");
                        AddOrderDataCell(filterData.KindOfRoomName.ToString());

                        decimal total = (filterData.ToTal ?? 0) - (filterData.Prepay ?? 0);
                        decimal TotalLeftOverPrice = 0;
                        if (total < 0)
                        {
                            TotalLeftOverPrice = Math.Abs(total);
                        }
                        AddOrderDataCell("Tiền Thừa");
                        AddOrderDataCell(TotalLeftOverPrice.ToString("0") + " VND");

                        AddOrderDataCell("Tổng Tiền Hóa Đơn");
                        AddOrderDataCell($"{filterData.ToTal?.ToString("0")} VND");
                        document.Add(orderTable);
                        document.Close();
                    }
                }
                Process.Start(new ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true
                });

                MessageBox.Show("The Invoice has been successfully issued and automatically opened!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("Lỗi quyền truy cập: " + ex.Message);
            }
            catch (PdfException ex)
            {
                MessageBox.Show("Lỗi PDF: " + ex.Message, "Lỗi PDF", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private Image ResizeImage(Image image, int maxDimension)
        {
            int newWidth, newHeight;
            double ratio = (double)image.Width / image.Height;

            if (image.Width > image.Height)
            {
                newWidth = maxDimension;
                newHeight = (int)(maxDimension / ratio);
            }
            else
            {
                newHeight = maxDimension;
                newWidth = (int)(maxDimension * ratio);
            }

            int highResWidth = newWidth * 2;
            int highResHeight = newHeight * 2;

            Bitmap highResImage = new Bitmap(highResWidth, highResHeight);

            using (Graphics gr = Graphics.FromImage(highResImage))
            {
                gr.SmoothingMode = SmoothingMode.AntiAlias;
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gr.PixelOffsetMode = PixelOffsetMode.HighQuality;

                gr.DrawImage(image, new Rectangle(0, 0, highResWidth, highResHeight));
            }

            Bitmap resizedImage = new Bitmap(newWidth, newHeight);

            using (Graphics gr = Graphics.FromImage(resizedImage))
            {
                gr.SmoothingMode = SmoothingMode.AntiAlias;
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gr.PixelOffsetMode = PixelOffsetMode.HighQuality;

                gr.DrawImage(highResImage, new Rectangle(0, 0, newWidth, newHeight),
                    new Rectangle(0, 0, highResWidth, highResHeight), GraphicsUnit.Pixel);
            }

            return resizedImage;
        }

        private byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }
        private string GetQRCodeImageData()
        {
            var filterData = _orderService.GetOrdersViewModels(OrderId).FirstOrDefault();

            try
            {
                var apiRequest = new ApiRequest
                {
                    acqId = 970436,
                    accountNo = "0731000935774",
                    accountName = "HOANG THAI SON",
                    amount = Convert.ToInt32(filterData.ToTal),
                    format = "text",
                    template = "compact2"
                };

                var jsonRequest = JsonConvert.SerializeObject(apiRequest);

                var client = new RestClient("https://api.vietqr.io/v2/generate");
                var request = new RestRequest
                {
                    Method = Method.Post
                };
                request.AddHeader("Accept", "application/json");
                request.AddJsonBody(apiRequest);

                var response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    var content = response.Content;
                    var dataResult = JsonConvert.DeserializeObject<ApiResponse>(content);

                    if (dataResult != null && dataResult.data != null)
                    {
                        return dataResult.data.qrDataURL.Replace("data:image/png;base64,", "");
                    }
                    else
                    {
                        MessageBox.Show("Empty or invalid response received from API.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show($"Failed to get a successful response from API. Status Code: {response.StatusCode} - {response.ErrorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating QR code: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        private Image Base64ToImage(string base64String)
        {
            try
            {
                byte[] imageBytes = Convert.FromBase64String(base64String);
                using (var ms = new MemoryStream(imageBytes))
                {
                    return Image.FromStream(ms);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error converting base64 to image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void AddTableRow(Table table, int STT, string itemName, decimal itemPrice, int? quantity, decimal totalPrice)
        {
            PdfFont font = PdfFontFactory.CreateFont(@"C:\Windows\Fonts\ARIAL.TTF", PdfEncodings.IDENTITY_H);

            table.AddCell(new Cell().Add(new Paragraph(STT.ToString()))
                .SetFont(font)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetPadding(5)
                .SetBorder(new SolidBorder(1)));
            table.AddCell(new Cell().Add(new Paragraph(itemName))
                .SetFont(font)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetPadding(5)
                .SetBorder(new SolidBorder(1)));
            table.AddCell(new Cell().Add(new Paragraph(itemPrice.ToString("0") + " VNĐ"))
                .SetFont(font)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetPadding(5)
                .SetBorder(new SolidBorder(1)));
            table.AddCell(new Cell().Add(new Paragraph(quantity.HasValue ? quantity.Value.ToString() : ""))
                .SetFont(font)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetPadding(5)
                .SetBorder(new SolidBorder(1)));
            table.AddCell(new Cell().Add(new Paragraph(totalPrice.ToString("0") + " VNĐ"))
                .SetFont(font)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetPadding(5)
                .SetBorder(new SolidBorder(1)));
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            var filterData = _orderService.GetOrdersViewModels(OrderId).FirstOrDefault();
            if (filterData.DatePayment == null)
            {
                MessageBox.Show("\r\nInvoices cannot be issued without payment");
                return;
            }
            Issue_An_invoice();
        }
    }
}

