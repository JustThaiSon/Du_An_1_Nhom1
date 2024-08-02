using BUS.IService;
using BUS.Service;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Windows.Forms;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

using System.Windows.Forms;


namespace QuanLyPhong
{
    public partial class frmThongKe : Form
    {
        //Thời gian mặc định của dtP_TuNgay,dtP_DenNgay là 17/07/2024 12:00:00 AM
        //Lưu ý đổi đường dẫn path_Excel này:
        private string path_Excel = "C:\\Users\\UBC\\Desktop\\Các Môn Học\\4-Summer 2024\\Block - 2\\Dự Án 1\\Check 1\\Du_An_1_Nhom1\\ThongKe.xlsx";
        private IOrderService _orderService;
        private IOrderServiceService _orderServiceService;
        private IRoomService _roomService;
        private IFloorService _floorService;
        private ICustomerService _customerService;
        private IEmployeeService _employeeService;
        public frmThongKe()
        {
            InitializeComponent();
            _orderService = new OrderServicess();
            _orderServiceService = new OrderServiceService();
            _roomService = new RoomService();
            _floorService = new FloorService();
            _customerService = new CustomerService();
            _employeeService = new EmployeeService();
            this.Load_cbb_Tang();
            cbb_Tang.SelectedIndex = 0;
            this.Load_cbb_Phong();
            cbb_Phong.SelectedIndex = 0;
        }
        public void Load_dtGV_ThongKe()
        {
            if (cbb_Phong.SelectedIndex != 0)
            {
                var idroom = _roomService.GetAllRooms().Where(x => x.RoomName == cbb_Phong.Text).Select(x => x.Id);
                if (idroom.Any())
                {
                    var phong = _orderService.GetAllOrdersFromDb()
                             .Where(x => (x.DateCreated >= dtP_TuNgay.Value) && (x.DatePayment <= dtP_DenNgay.Value)
                             && x.RoomId == idroom.FirstOrDefault()).ToList();
                    this.check_dtGV_ThongKe(phong);
                }
            }
            else if (cbb_Phong.SelectedIndex == 0)
            {
                if (cbb_Tang.SelectedIndex == 0)
                {
                    var phong = _orderService.GetAllOrdersFromDb()
                                .Where(x => (x.DateCreated >= dtP_TuNgay.Value) && (x.DatePayment <= dtP_DenNgay.Value)).ToList();
                    this.check_dtGV_ThongKe(phong);
                }
                else if (cbb_Tang.SelectedIndex != 0)
                {
                    var idtang = _floorService.GetAllFloorFromDb().Where(x => x.FloorName == cbb_Tang.Text).Select(x => x.Id);

                    if (idtang.Any())
                    {
                        var idroom = _roomService.GetAllRooms().Where(x => x.FloorId == idtang.FirstOrDefault()).Select(x => x.Id);
                        var phong = _orderService.GetAllOrdersFromDb()
                                   .Where(x => (x.DateCreated >= dtP_TuNgay.Value) && (x.DatePayment <= dtP_DenNgay.Value)).ToList();
                        List<Orders> orders = new List<Orders>();
                        foreach (var id in phong)
                        {
                            foreach (var item in idroom)
                            {
                                if (item == id.RoomId)
                                {
                                    orders.Add(id);
                                }
                            }
                        }
                        this.check_dtGV_ThongKe(orders);
                    }
                }
            }
        }
        private void check_dtGV_ThongKe(List<Orders> oders)
        {
            dtGV_ThongKe.Rows.Clear();
            if (dtP_TuNgay.Value < dtP_DenNgay.Value && oders != null)
            {
                dtGV_ThongKe.Rows.Clear();
                dtGV_ThongKe.ColumnCount = 11;
                dtGV_ThongKe.Columns[0].Name = "STT";
                dtGV_ThongKe.Columns[1].Name = "Tên Khách Hàng";
                dtGV_ThongKe.Columns[2].Name = "Hình Thức Thanh Toán";
                dtGV_ThongKe.Columns[3].Name = "Nhân Viên Tiếp Nhận";
                dtGV_ThongKe.Columns[4].Name = "Phòng";
                dtGV_ThongKe.Columns[5].Name = "Ngày Đặt Phòng";
                dtGV_ThongKe.Columns[6].Name = "Tiền Phòng";
                dtGV_ThongKe.Columns[7].Name = "Tổng,Mã Giảm Giá";
                dtGV_ThongKe.Columns[8].Name = "Tổng Điểm Sử Dụng";
                dtGV_ThongKe.Columns[9].Name = "Dịch Vụ Thêm";
                dtGV_ThongKe.Columns[10].Name = "Tổng Tiền";

                int Count = 0;

                //Hàng 1 --- Tổng Tiền
                decimal? tonggiamgia = oders.Sum(x => x.TotalDiscount);
                decimal tongtiendichvu = 0;
                decimal tonggiaphong = oders.Sum(x => x.ToTalPrice);
                decimal? tongdiem = oders.Sum(x => x.TotalPricePoint);
                var tongtienphong = oders.Sum(x => x.ToTal)+tongtiendichvu;

                foreach (var item in oders.Select(x => x.Id))
                {
                    try
                    {
                        var order = _orderServiceService.GetAllOrderService().First(x => x.OrderId == item);
                        if (order != null)
                        {
                            tongtiendichvu += order.TotalPrice;
                        }
                    }
                    catch (Exception ex) { }
                }
                dtGV_ThongKe.Rows.Add(Count, "All", "All", "All", "All", "All", tonggiaphong, tonggiamgia, tongdiem, tongtiendichvu, tongtienphong + tongtiendichvu);
                //

                foreach (var item in oders)
                {
                    Count++;
                    string tenkh = "";
                    string tennv = "";
                    string tenphong = "";
                    foreach (var kh in _customerService.GetAllCustomerFromDb())
                    {
                        if (item.CustomerId == kh.Id)
                        {
                            tenkh = kh.Name;
                        }
                    }
                    foreach (Employee employee in _employeeService.GetAllEmployeeFromDb())
                    {
                        if (item.EmployeeId == employee.Id)
                        {
                            tennv = employee.Name;
                        }
                    }
                    foreach (var room in _roomService.GetAllRooms())
                    {
                        if (item.RoomId == room.Id)
                        {
                            tenphong = room.RoomName;
                        }
                    }
                    decimal tienDichVu = _orderServiceService.GetAllOrderService().Where(x => x.OrderId == item.Id).Sum(x => x.TotalPrice);
                    dtGV_ThongKe.Rows.Add(Count, tenkh, item.PayMents, tennv, tenphong, item.DateCreated.Value.ToString("dd/MM/yyy"), item.ToTalPrice, item.ToTalPrice - item.ToTal - item.TotalPricePoint, item.TotalPricePoint, tienDichVu, item.ToTal + tienDichVu);
                }
            }
            else
            {
                MessageBox.Show($"Không Có Phòng Nào Được Đặt Trong Thời Gian Từ {dtP_TuNgay.Value.ToString("dd/MM/yyy")} Đến Ngày {dtP_DenNgay.Value.ToString("dd/MM/yyy")}");
            }
        }
        private void btn_XemThongKe_Click(object sender, EventArgs e)
        {
            this.Load_dtGV_ThongKe();
        }
        private void btn_XuatFileExcel_Click(object sender, EventArgs e)
        {
            if (dtGV_ThongKe.Rows.Count <= 2)
            {
                MessageBox.Show("Vui Lòng Lựa Chọn Ngày Tháng Cần Thống Kê!!!");
                return;
            }
            if (MessageBox.Show("Bạn Có Muốn Xuất File Excel Không?", "Xác Nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                this.ExportToExcel(dtGV_ThongKe, path_Excel);
            }
        }
        public void ExportToExcel(DataGridView gridView, string filePath)
        {
            // Verify file path ends with .xlsx
            if (Path.GetExtension(filePath).ToLower() != ".xlsx")
            {
                throw new ArgumentException("The file path must end with .xlsx extension");
            }

            IWorkbook workbook;
            try
            {

                // Load existing workbook if file exists, otherwise create a new one
                if (File.Exists(filePath))
                {
                    using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite))
                    {
                        workbook = new XSSFWorkbook(fileStream);
                    }

                }

                else
                {
                    workbook = new XSSFWorkbook();
                }

                // Find a unique sheet name
                string sheetName = "Sheet";
                int sheetIndex = 1;

                while (workbook.GetSheet(sheetName) != null)
                {
                    sheetName = "Sheet" + sheetIndex;
                    sheetIndex++;
                }

                // Create new sheet with a unique name
                ISheet sheet = workbook.CreateSheet($"{sheetName}({DateTime.Now:dd-MM-yyyy HH-mm-ss})");

                // Add column headers
                IRow headerRow = sheet.CreateRow(0);
                for (int i = 0; i < gridView.Columns.Count; i++)
                {
                    headerRow.CreateCell(i).SetCellValue(gridView.Columns[i].HeaderText);
                }

                // Add rows
                for (int i = 0; i < gridView.Rows.Count; i++)
                {
                    IRow row = sheet.CreateRow(i + 1);
                    for (int j = 0; j < gridView.Columns.Count; j++)
                    {
                        row.CreateCell(j).SetCellValue(gridView.Rows[i].Cells[j].Value?.ToString() ?? string.Empty);
                    }
                }

                // Save Excel file to the specified file path
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(fileStream);
                }

                // Dispose workbook
                workbook.Close();
                MessageBox.Show("Xuất File Excel thành công!!!");
            }
            catch (Exception)
            {
                MessageBox.Show("Vui lòng tắt file Excel!!!");
                return;
            }
        }
        private void Load_cbb_Tang()
        {
            cbb_Tang.Items.Clear();
            cbb_Tang.Items.Add("Tất Cả");
            foreach (var item in _floorService.GetAllFloorFromDb())
            {
                cbb_Tang.Items.Add(item.FloorName);
            }
        }
        private void Load_cbb_Phong()
        {
            cbb_Phong.Items.Clear();
            cbb_Phong.Items.Add("Tất Cả");
            var ListPhong = _roomService.GetAllRoomsFromDb();        
            if (cbb_Tang != null && cbb_Tang.SelectedIndex != 0)
            {
                var tangid = _floorService.GetAllFloorFromDb().FirstOrDefault(x => x.FloorName == cbb_Tang.Text).Id;
                ListPhong = ListPhong.Where(x=>x.FloorId == tangid).ToList();
            }
            foreach (var item in ListPhong)
            {
                cbb_Phong.Items.Add(item.RoomName);
            }
            if (cbb_Phong.Items.Count <= 1)
            {
                cbb_Phong.Items.Clear();
                cbb_Phong.Items.Add("Không có phòng");
                MessageBox.Show($"Không có phòng nào");
            }
            cbb_Phong.SelectedIndex = 0;
        }
        private void cbb_Tang_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Load_cbb_Phong();
        }
        private void btn_LuotThue_Click(object sender, EventArgs e)
        {
            var mostFrequentRoomId = _orderService.GetAllOrdersFromDb()
                                     .Where(x => (x.DateCreated >= dtP_TuNgay.Value) && (x.DatePayment <= dtP_DenNgay.Value))
                                     .GroupBy(o => o.RoomId)
                                     .OrderByDescending(g => g.Count())
                                     .Select(g => g.Key)
                                     .FirstOrDefault();
            var listMostOrderRoom = _orderService.GetAllOrdersFromDb().Where(x => (x.DateCreated >= dtP_TuNgay.Value) && (x.DatePayment <= dtP_DenNgay.Value) && x.RoomId == mostFrequentRoomId).ToList();
            this.check_dtGV_ThongKe(listMostOrderRoom);
        }
        private void btn_DoanhThu_Click(object sender, EventArgs e)
        {
            var mostFrequentRoomId = _orderService.GetAllOrdersFromDb()
                                    .Where(x => (x.DateCreated >= dtP_TuNgay.Value) && (x.DatePayment <= dtP_DenNgay.Value));
            decimal? max = 0;
            List<Orders> order = new List<Orders>();
            foreach (var item in mostFrequentRoomId)
            {
                var i = _orderServiceService.GetAllOrderService().Where(x => x.OrderId == item.Id);
                decimal? tong = item.ToTal;
                foreach (var item1 in i)
                {
                    tong += item1.TotalPrice;
                }
                if (tong >= max)
                {
                    max = tong;
                }
            }
            foreach (var item in mostFrequentRoomId)
            {
                var i = _orderServiceService.GetAllOrderService().Where(x => x.OrderId == item.Id);
                decimal? tong = item.ToTal;
                foreach (var item1 in i)
                {
                    tong += item1.TotalPrice;
                }
                if (tong == max)
                {
                    order.Add(item);
                }
            }
            this.check_dtGV_ThongKe(order);
        }
        private void btn_KhachThue_Click(object sender, EventArgs e)
        {
            var mostFrequentRoomId = _orderService.GetAllOrdersFromDb()
                                     .Where(x => (x.DateCreated >= dtP_TuNgay.Value) && (x.DatePayment <= dtP_DenNgay.Value))
                                     .GroupBy(o => o.CustomerId)
                                     .OrderByDescending(g => g.Count())
                                     .Select(g => g.Key)
                                     .FirstOrDefault();
            var listMostOrderRoom = _orderService.GetAllOrdersFromDb().Where(x => (x.DateCreated >= dtP_TuNgay.Value) && (x.DatePayment <= dtP_DenNgay.Value) && x.CustomerId == mostFrequentRoomId).ToList();
            this.check_dtGV_ThongKe(listMostOrderRoom);
        }
        private void frmThongKe_Load(object sender, EventArgs e)
        {
        }

    }
}

