using BUS.IService;
using BUS.Service;
using BUS.ViewModels;
using DAL.Data;
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

namespace QuanLyPhong
{
    public partial class frmPay : Form
    {
        public Guid OrderId { get; set; }
        private IOrderServiceService _orderServiceService;
        private IServiceSevice _serviceSevice;
        private IOrderService _orderService;
        private IVoucherSevice _voucherSevice;
        private ICustomerService _customerService;
        private int PointAdd { get; set; }
        private decimal ToTalPriceRoom { get; set; }
        private List<OrderViewModel> _lstOrderViewModes;
        private decimal? ToTal { get; set; }
        private decimal? ToTalDiscount { get; set; }
        private decimal? ToTalPricePoints { get; set; }
        private IHistorypointService _historypointService;
        private IRoomService _roomService;
        public frmPay(Guid OrderId)
        {
            InitializeComponent();
            this.OrderId = OrderId;
            _orderServiceService = new OrderServiceService();
            _serviceSevice = new ServiceService();
            _orderService = new OrderServicess();
            _voucherSevice = new VoucherSevice();
            _lstOrderViewModes = new List<OrderViewModel>();
            _customerService = new CustomerService();
            _historypointService = new HistoryPointsService();
            _roomService = new RoomService();
            LoadDtgService();
            LoadDtgOrders();
            LoadDataOnLabel();
            LoadPayMent();
            //Loaddb();
            LoadCbbPoint();
        }

        void LoadDtgService()
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
            foreach (var item in _orderServiceService.GetAllOrderService().Where(x => x.OrderId == OrderId))
            {
                Count++;
                dtgService.Rows.Add(item.ServiceId, Count, _serviceSevice.GetAllServiceFromDb().Where(x => x.Id == item.ServiceId).Select(x => x.Name).FirstOrDefault(), item.QuantityOrderService, item.Price, item.TotalPrice.ToString("0"));
            }
        }
        void LoadDtgOrders()
        {

            dtgListOrders.ColumnCount = 14;
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
            dtgListOrders.Columns[9].Name = "KindOfRoom";
            dtgListOrders.Columns[10].Name = "Room";
            dtgListOrders.Columns[11].Name = "Price";
            dtgListOrders.Columns[12].Name = "ToTalTime";
            dtgListOrders.Columns[13].Name = "PointsAdd";
            dtgListOrders.Rows.Clear();
            decimal PriceRoom = 0;
            PointAdd = 0;
            foreach (var item in _orderService.GetOrdersViewModels(OrderId))
            {
                PointAdd = Convert.ToInt32((double)item.ToTalPrice * 0.01);
                TimeSpan? ToTalTime = DateTime.Now - item.DateCreated;
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
                dtgListOrders.Rows.Add(item.Id, item.OrderCode, item.DateCreated, item.Note, item.Rentaltype,
                                       item.EmployeeName, item.CustomerName, item.Prepay, item.OrderType,
                                       item.KindOfRoomName, item.RoomName, PriceRoom, totalTimeString, PointAdd);
            }
        }
        void Loaddb()
        {
            var GetOrderById = _orderService.GetOrdersViewModels(OrderId);
            _lstOrderViewModes = GetOrderById;
            foreach (var order in _lstOrderViewModes)
            {
                lbAddress.Text = order.Address;
                lbCCCD.Text = order.CCCD;
                lbCustomer.Text = order?.CustomerName;
                lbDateCheckIn.Text = order.DateCreated.ToString();
                lbDateCheckOut.Text = DateTime.Now.ToString();
                lbEmail.Text = order.Email;
                lbGender.Text = order.Gender.ToString();
                lbNote.Text = order.Note.ToString();
                lbNumberPhone.Text = order.PhoneNumber.ToString();
                lbPrePay.Text = order.Prepay.ToString();
                lbRentaltype.Text = order.Rentaltype.ToString();
                lbRoomName.Text = order.RoomName.ToString();
                lbToTalPriceRoom.Text = order.ToTalPrice.ToString("0") + " VNĐ";
                lbKindOfRoom.Text = order.KindOfRoomName.ToString();
                lbPointsAreAdded.Text = PointAdd.ToString() + " Points";
                ToTalPriceRoom = order.ToTalPrice;
                if (order.TotalDiscount != null)
                {
                    lbTotalDiscount.Text = order.TotalDiscount.ToString() + " VNĐ";
                }
                else
                {
                    lbTotalDiscount.Text = "0 VNĐ";
                }

                if (order.TotalPricePoint != null)
                {
                    lbPricePoint.Text = order.TotalPricePoint.ToString() + " VNĐ";
                }
                else
                {
                    lbPricePoint.Text = "0 VNĐ";
                }
                lbTotalPrice.Text = order.ToTal.ToString() + " VNĐ";

                TimeSpan? totalTime = DateTime.Now - order.DateCreated;

                if (totalTime.HasValue)
                {
                    if (order.Rentaltype == RentalTypeEnum.Daily)
                    {
                        int totalDays = (int)Math.Round(totalTime.Value.TotalDays);
                        lbTotalTime.Text = $"{totalDays} days";
                    }
                    else
                    {
                        int totalHours = (int)Math.Round(totalTime.Value.TotalHours);
                        lbTotalTime.Text = $"{totalHours} hours";
                    }
                }
                else
                {
                    lbTotalTime.Text = "Unavailable";
                }

                if (order.Rentaltype == RentalTypeEnum.Daily)
                {
                    lbRoomPrice.Text = order.PricePerDay.ToString("0") + " VNĐ";
                }
                else
                {
                    lbRoomPrice.Text = order.PriceByHour.ToString("0") + " VNĐ";
                }
            }
        }
        void LoadDataOnLabel()
        {

            var filterData = _orderService.GetOrdersViewModels(OrderId);
            _lstOrderViewModes = filterData?.ToList() ?? new List<OrderViewModel>();

            if (_lstOrderViewModes.Count > 0)
            {
                var firstOrderViewModel = _lstOrderViewModes[0];

                lbAddress.Text = firstOrderViewModel.Address.ToString();
                lbCCCD.Text = firstOrderViewModel.CCCD.ToString();
                lbCustomer.Text = firstOrderViewModel.CustomerName.ToString();
                lbDateCheckIn.Text = firstOrderViewModel.DateCreated.ToString();
                lbDateCheckOut.Text = DateTime.Now.ToString();
                lbEmail.Text = firstOrderViewModel.Email.ToString();
                lbGender.Text = firstOrderViewModel.Gender.ToString();
                lbNote.Text = firstOrderViewModel.Note.ToString();
                lbNumberPhone.Text = firstOrderViewModel.PhoneNumber.ToString();
                lbPrePay.Text = firstOrderViewModel.Prepay.ToString();
                lbRentaltype.Text = firstOrderViewModel.Rentaltype.ToString();
                lbRoomName.Text = firstOrderViewModel.RoomName.ToString();
                lbToTalPriceRoom.Text = firstOrderViewModel.ToTalPrice.ToString("0") + " VNĐ";
                lbKindOfRoom.Text = firstOrderViewModel.KindOfRoomName.ToString();
                lbPointsAreAdded.Text = PointAdd.ToString() + " Points";
                ToTalPriceRoom = firstOrderViewModel.ToTalPrice;

                if (firstOrderViewModel.TotalDiscount != null)
                {
                    lbTotalDiscount.Text = firstOrderViewModel.TotalDiscount.ToString() + " VNĐ";
                }
                else
                {
                    lbTotalDiscount.Text = "0 VNĐ";
                }

                if (firstOrderViewModel.TotalPricePoint != null)
                {
                    lbPricePoint.Text = firstOrderViewModel.TotalPricePoint.ToString() + " VNĐ";
                }
                else
                {
                    lbPricePoint.Text = "0 VNĐ";
                }
                ToTal = firstOrderViewModel.ToTal;
                lbTotalPrice.Text = ToTal + " VNĐ";

                TimeSpan? totalTime = DateTime.Now - firstOrderViewModel.DateCreated;
                lbTotalTime.Text = "Unavailable";
                if (totalTime.HasValue)
                {
                    if (firstOrderViewModel.Rentaltype == RentalTypeEnum.Daily)
                    {
                        int NgayLamTron = (int)Math.Round(totalTime.Value.TotalDays);
                        NgayLamTron = NgayLamTron < 1 ? 1 : NgayLamTron;
                        lbTotalTime.Text = $"{NgayLamTron} days";
                    }
                    else if (firstOrderViewModel.Rentaltype == RentalTypeEnum.Hourly)
                    {
                        int GioLamTron = (int)Math.Round(totalTime.Value.TotalHours);
                        GioLamTron = GioLamTron < 1 ? 1 : GioLamTron;
                        lbTotalTime.Text = $"{GioLamTron} hours";
                    }
                }
                if (firstOrderViewModel.Rentaltype == RentalTypeEnum.Daily)
                {
                    lbRoomPrice.Text = firstOrderViewModel.PricePerDay.ToString("0") + " VNĐ";
                }
                else
                {
                    lbRoomPrice.Text = firstOrderViewModel.PriceByHour.ToString("0") + " VNĐ";
                }
            }
        }

        private void dtgService_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmPay_Load(object sender, EventArgs e)
        {

        }

        private void TbVoucher_TextChanged(object sender, EventArgs e)
        {
            string voucherCode = TbVoucher.Text.Trim();

            string validationResult = _voucherSevice.ValidateVoucher(voucherCode, OrderId);

            if (validationResult == "Voucher validation successful")
            {
                TbVoucher.BackColor = Color.LightGreen;

                string applyResult = ApplyVoucher(voucherCode);

                MessageBox.Show(applyResult);
            }
            else
            {
                TbVoucher.BackColor = Color.LightPink;

                MessageBox.Show(validationResult);
            }
        }

        private decimal ToTalPriceDiscount = 0;
        private List<Guid> AppliedVouchers = new List<Guid>();

        private string ApplyVoucher(string voucherCode)
        {
            var voucher = _voucherSevice.GetAllVoucherFromDb().FirstOrDefault(v => v.VoucherCode == voucherCode);

            if (voucher == null)
            {
                return "Invalid voucher code. Voucher not applied.";
            }

            if (AppliedVouchers.Contains(voucher.Id))
            {
                return "Voucher has already been applied to this order.";
            }

            decimal discountRate = voucher.DiscountRate ?? 0;
            ToTalPriceDiscount = ToTalPriceRoom * discountRate;
            ToTalDiscount = ToTalPriceDiscount;
            ToTalPriceRoom -= ToTalPriceDiscount;
            AppliedVouchers.Add(voucher.Id);
            ToTal = _orderService.GetOrdersViewModels(OrderId)[0].ToTal - ToTalPriceDiscount;
            lbTotalDiscount.Text = ToTalPriceDiscount.ToString("0") + " VND";
            lbTotalPrice.Text = (ToTal).ToString() + " VND";
            return $"Voucher applied successfully. Discount: {ToTalPriceDiscount.ToString("0")} VNĐ";
        }
        private void btnApplyVoucher_Click(object sender, EventArgs e)
        {

        }

        private void btn_confirm_Click(object sender, EventArgs e)
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
            var pointsUsageHistory = new HistoryPoints
            {
                CustomerId = filterData.CustomerId,
                Point = (int)ToTalPricePoints.GetValueOrDefault(),
                CreatedDate = DateTime.Now,
            };

            _historypointService.AddhtrPoint(pointsUsageHistory);
            var voucherCode = TbVoucher.Text.Trim();
            var voucherId = _voucherSevice.GetAllVoucherFromDb()
                                           .Where(x => x.VoucherCode == voucherCode)
                                           .Select(x => x.Id)
                                           .FirstOrDefault();

            var validVoucher = _voucherSevice.GetAllVoucherFromDb().Any(v => v.Id == voucherId);
            Guid? validVoucherId = validVoucher ? voucherId : (Guid?)null;
            var UpdateOrders = new Orders
            {
                Id = OrderId,
                RoomId = filterData.RoomId,
                CustomerId = filterData.CustomerId,
                DateCreated = filterData.DateCreated,
                DatePayment = DateTime.Now,
                Prepay = filterData.Prepay,
                Note = filterData.Note,
                EmployeeId = Session.UserId,
                Rentaltype = filterData.Rentaltype,
                OrderType = filterData.OrderType,
                ToTalPrice = filterData.ToTalPrice,
                ToTal = ToTal,
                OrderCode = filterData.OrderCode,
                PayMents = cbbPayment.Text,
                TotalDiscount = ToTalDiscount,
                TotalPricePoint = ToTalPricePoints,
                VoucherId = validVoucherId,
                HistoryPointId = pointsUsageHistory.Id,
            };
            _orderService.UpdateOrders(UpdateOrders);

            decimal pointsUsed = ToTalPricePoints.GetValueOrDefault();
            decimal newPoints = customer.Point - pointsUsed + PointAdd;

            var updateCustomer = new Customer
            {
                Id = customer.Id,
                Point = newPoints
            };
            _customerService.UpdatePointByCustomer(updateCustomer);
            var updateStatusRoom = new Room
            {
                Id = filterData.RoomId,
                Status = RoomStatus.UnderMaintenance
            };
            _roomService.UpdadateStatusRoom(updateStatusRoom);
        }
        void LoadPayMent()
        {
            string[] Payment = { "Payment by cash", "Payment by card" };
            foreach (string s in Payment)
            {
                cbbPayment.Items.Add(s);
            }
            cbbPayment.SelectedItem = -1;
        }

        private void cbbOrderType_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void btn_addFloor_Click(object sender, EventArgs e)
        {
            var filterData = _orderService.GetOrdersViewModels(OrderId).FirstOrDefault();

            if (filterData == null)
            {
                MessageBox.Show("Order data not found.");
                return;
            }

            var customer = _customerService.GetAllCustomerFromDb()
                                            .FirstOrDefault(x => x.Id == filterData.CustomerId);

            if (customer == null)
            {
                MessageBox.Show("Customer not found.");
                return;
            }

            MessageBox.Show($"The total number of customer points available is {customer.Point} points", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        void LoadCbbPoint()
        {
            string[] points = { "All", "10000", "100000", "200000", "300000", "400000", "500000" };
            foreach (string s in points)
            {
                cbbpoint.Items.Add(s);
            }
            cbbpoint.SelectedItem = -1;
        }
        private void txtPoints_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbbpoint_SelectedIndexChanged(object sender, EventArgs e)
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
            string selectedValue = cbbpoint.SelectedItem.ToString();
            decimal pointsToUse = 0;

            if (selectedValue == "All")
            {
                pointsToUse = customer.Point;
            }
            else
            {
                pointsToUse = int.Parse(selectedValue);
            }
            if (pointsToUse > customer.Point)
            {
                MessageBox.Show("Insufficient points available.");
                return;
            }
            var totalPrice = filterData.ToTalPrice;
            var maxPointsThatCanBeUsed = Math.Min(pointsToUse, totalPrice);

            if (maxPointsThatCanBeUsed > customer.Point)
            {
                MessageBox.Show("Insufficient points available.");
                return;
            }
            int pointsUsed = (int)Math.Min(maxPointsThatCanBeUsed, customer.Point);
            ToTalPricePoints = pointsUsed;
            ToTal = ToTal - pointsToUse;
            lbPricePoint.Text = pointsUsed.ToString() + " VND";
            lbTotalPrice.Text = (ToTal).ToString() + " VND";
        }
    }
}
