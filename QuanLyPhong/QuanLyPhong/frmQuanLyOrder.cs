﻿using BUS.IService;
using BUS.Service;
using DAL.Entities;
using DAL.Enums;
using iText.Layout.Borders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        }
        void LoadDataGridViewOrder()
        {
            dtgv_order.ColumnCount = 13;
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
            dtgv_order.Columns[9].Name = "Total";
            dtgv_order.Columns[10].Name = "PriceRoom";
            dtgv_order.Columns[11].Name = "NameRoom";
            dtgv_order.Columns[12].Name = "Note";

            dtgv_order.Rows.Clear();
            int Count = 0;
            foreach (var item in _orderService.GetAllOrdersFromDb())
            {
                var employ = _employeeService.GetAllEmployeeFromDb().Where(x => x.Id == item.EmployeeId).Select(x => x.Name).FirstOrDefault();
                var Customer = _customerService.GetAllCustomerFromDb().Where(x => x.Id == item.CustomerId).Select(x => x.Name).FirstOrDefault();
                var CustomerPhone = _customerService.GetAllCustomerFromDb().Where(x => x.Id == item.CustomerId).Select(x => x.PhoneNumber).FirstOrDefault();

                var room = _roomService.GetAllRoomsFromDb().Where(x => x.Id == item.RoomId).Select(x => x.RoomName).FirstOrDefault();
                var datePayment = item.DatePayment.HasValue ? item.DatePayment.Value.ToString("dd/MM/yyyy HH:mm") : "Chưa thanh toán";
                Count++;
                dtgv_order.Rows.Add(item.Id, Count, item.OrderCode, employ, item.DateCreated.Value.ToString("dd/MM/yyyy HH:mm"), datePayment, Customer, CustomerPhone, item.Prepay, item.ToTal, item.ToTalPrice, room, item.Note);

            }
        }
        void ServiceTongTien() { 
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
                txt_priretotalser.Text = total.ToString("N2");
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
            if (e.RowIndex >= 0 && e.RowIndex < dtgv_order.Rows.Count)
            {
                var cellValue = dtgv_order.Rows[e.RowIndex].Cells["Id"].Value;
                var selectedRow = dtgv_order.Rows[e.RowIndex];

                txt_Code.Text = selectedRow.Cells["OrderCode"].Value?.ToString();
                txt_employe.Text = selectedRow.Cells["Employee"].Value?.ToString();
                txt_Cus.Text = selectedRow.Cells["Customer"].Value?.ToString();
                txt_phone.Text = selectedRow.Cells["CustomerPhone"].Value?.ToString();
                txt_prepay.Text = selectedRow.Cells["Prepay"].Value?.ToString();
                txt_TotalPrice.Text = selectedRow.Cells["Total"].Value?.ToString();
                txt_Roomprice.Text = selectedRow.Cells["PriceRoom"].Value?.ToString();
                txt_nameroom.Text = selectedRow.Cells["NameRoom"].Value?.ToString();

                if (cellValue != null && cellValue is Guid)
                {
                    Guid orderId = (Guid)cellValue;
                    LoadDataGridViewService(orderId);
                }
                else
                {
                    MessageBox.Show("Không có thông tin.");
                }
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
                    MessageBox.Show("Không thể sửa hóa đơn đã thanh toán");
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
                    return customerName.Contains(searchText) || customerPhone.Contains(searchText);
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
                    MessageBox.Show("Thông tin đã được cập nhật thành công!");

                    tabControl1.SelectedTab = Information;
                    LoadDataGridViewOrder();
                    LoadDataGridViewService(orderId);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy đơn hàng để cập nhật.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn đơn hàng cần sửa.");
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
                    MessageBox.Show("Vui lòng chọn dịch vụ cần chỉnh sửa.");
                    return;
                }

                var selectedRow = dtgService.SelectedRows[0];
                Guid serviceId = (Guid)selectedRow.Cells["Id"].Value;
                int newQuantity;

                if (!int.TryParse(txt_quantity.Text, out newQuantity) || newQuantity <= 0)
                {
                    MessageBox.Show("Vui lòng nhập số lượng hợp lệ (số nguyên dương).");
                    return;
                }

                var selectedOrderRow = dtgv_order.SelectedRows[0];
                Guid orderId = (Guid)selectedOrderRow.Cells["Id"].Value;

                var orderService = _orderServiceService.GetOrderServicesByOrderId(orderId)
                    .FirstOrDefault(os => os.ServiceId == serviceId);

                if (orderService == null)
                {
                    MessageBox.Show("Không tìm thấy dịch vụ trong đơn hàng.");
                    return;
                }

                int quantityDifference = newQuantity - orderService.Quantity;
                orderService.Quantity = newQuantity;
                orderService.TotalPrice = orderService.TotalPrice * newQuantity;

                _orderServiceService.UpdateOrderService(orderService);

                var service = _serviceSevice.GetAllServiceFromDb().FirstOrDefault(s => s.Id == serviceId);
                if (service != null)
                {
                    service.Quantity -= quantityDifference;
                    _serviceSevice.UpdateService(service);
                }

                MessageBox.Show("Cập nhật số lượng dịch vụ thành công.");
                LoadDataGridViewService(orderId);
                LoadDataGridViewOrder();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}");
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtgv_order.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn đơn hàng cần xóa.");
                    return;
                }

                var selectedRow = dtgv_order.SelectedRows[0];
                Guid orderId = (Guid)selectedRow.Cells["Id"].Value;
                var nameroom = selectedRow.Cells["NameRoom"].Value.ToString();
                var Value = selectedRow.Cells["DatePayment"].Value;

                if (Value.ToString() != "Chưa thanh toán")
                {
                    MessageBox.Show("Không thể xóa hóa đơn đã thanh toán");
                    return;
                }

                var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa đơn hàng này?",
                                                    "Xác nhận xóa",
                                                    MessageBoxButtons.YesNo);
                if (confirmResult != DialogResult.Yes) return;

                var orderServices = _orderServiceService.GetOrderServicesByOrderId(orderId);
                

                foreach (var orderService in orderServices)
                {
                    var service = _serviceSevice.GetAllServiceFromDb().FirstOrDefault(s => s.Id == orderService.ServiceId);
                    if (service != null)
                    {
                        service.Quantity += orderService.Quantity;
                        _serviceSevice.UpdateService(service);
                    }
                    else
                    {
                        MessageBox.Show($"Không tìm thấy dịch vụ : {service.Name}");
                    }
                }

                _orderServiceService.RemoveOrderServiceeAll(orderId);
                _orderService.RemoveOrders(orderId);

                var room = _roomService.GetAllRoomsFromDb().FirstOrDefault(x => x.RoomName == nameroom);
                if (room != null)
                {
                    room.Status = RoomStatus.Available;
                    _roomService.UpdadateStatusRoom(room);
                }
                else
                {
                    MessageBox.Show($"Không tìm thấy phòng có tên: {nameroom}");
                }

                MessageBox.Show("Xóa thành công");
                LoadDataGridViewOrder();
                LoadDataGridViewService(orderId);
            }
            catch 
            {
                MessageBox.Show("Loi");
            }
        }
    } 
}

