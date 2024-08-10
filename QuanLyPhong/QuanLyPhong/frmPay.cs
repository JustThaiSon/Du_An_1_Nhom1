using BUS.IService;
using BUS.Service;
using BUS.ViewModels;
using DAL.Data;
using DAL.Entities;
using DAL.Enums;
using iText.IO.Font;
using iText.Kernel.Colors;
using iText.Kernel.Exceptions;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Color = System.Drawing.Color;
using HorizontalAlignment = iText.Layout.Properties.HorizontalAlignment;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using BUS.EntitiesApiPay;
using iText.Kernel.Pdf.Canvas.Wmf;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Crmf;
using RestSharp;
using Image = System.Drawing.Image;
using System.Xml.Linq;
using iText.IO.Image;
using System.Drawing.Drawing2D;
using Rectangle = System.Drawing.Rectangle;
using QuanLyPhong;
using static QuanLyPhong.frmRoomBookingReceipt;

namespace QuanLyPhong
{
	public partial class frmPay : Form
	{
		public delegate void CheckOutHandler();
		public event CheckOutHandler OnCheckOut;

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
		private decimal? PriceRoom { get; set; }
		private string totalTimeString { get; set; }

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
			LoadCbbVoucher();
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
			dtgListOrders.Columns[8].Name = "KindOfRoom";
			dtgListOrders.Columns[9].Name = "Room";
			dtgListOrders.Columns[10].Name = "Price";
			dtgListOrders.Columns[11].Name = "ToTalTime";
			dtgListOrders.Columns[12].Name = "PointsAdd";
			dtgListOrders.Rows.Clear();

			PointAdd = 0;
			foreach (var item in _orderService.GetOrdersViewModels(OrderId))
			{
				PointAdd = Convert.ToInt32((double)item.ToTalPrice * 0.01);
				TimeSpan? ToTalTime = DateTime.Now - item.DateCreated;
				totalTimeString = "Unavailable";
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
									   item.EmployeeName, item.CustomerName, item.Prepay,
									   item.KindOfRoomName, item.RoomName, PriceRoom, totalTimeString, PointAdd);
			}
		}
		//void Loaddb()
		//{
		//	var GetOrderById = _orderService.GetOrdersViewModels(OrderId);
		//	_lstOrderViewModes = GetOrderById;
		//	foreach (var order in _lstOrderViewModes)
		//	{
		//		lbAddress.Text = order.Address;
		//		lbCCCD.Text = order.CCCD;
		//		lbCustomer.Text = order?.CustomerName;
		//		lbDateCheckIn.Text = order.DateCreated.ToString();
		//		lbDateCheckOut.Text = DateTime.Now.ToString();
		//		lbEmail.Text = order.Email;
		//		lbGender.Text = order.Gender.ToString();
		//		lbNote.Text = order.Note.ToString();
		//		lbNumberPhone.Text = order.PhoneNumber.ToString();
		//		lbPrePay.Text = order.Prepay.ToString();
		//		lbRentaltype.Text = order.Rentaltype.ToString();
		//		lbRoomName.Text = order.RoomName.ToString();
		//		lbToTalPriceRoom.Text = order.ToTalPrice.ToString("0") + " VNĐ";
		//		lbKindOfRoom.Text = order.KindOfRoomName.ToString();
		//		lbPointsAreAdded.Text = PointAdd.ToString() + " Points";
		//		ToTalPriceRoom = order.ToTalPrice;
		//		if (order.TotalDiscount != null)
		//		{
		//			lbTotalDiscount.Text = order.TotalDiscount.ToString() + " VNĐ";
		//		}
		//		else
		//		{
		//			lbTotalDiscount.Text = "0 VNĐ";
		//		}

		//		if (order.TotalPricePoint != null)
		//		{
		//			lbPricePoint.Text = order.TotalPricePoint.ToString() + " VNĐ";
		//		}
		//		else
		//		{
		//			lbPricePoint.Text = "0 VNĐ";
		//		}
		//		lbTotalPrice.Text = order.ToTal.ToString() + " VNĐ";

		//		TimeSpan? totalTime = DateTime.Now - order.DateCreated;

		//		if (totalTime.HasValue)
		//		{
		//			if (order.Rentaltype == RentalTypeEnum.Daily)
		//			{
		//				int totalDays = (int)Math.Round(totalTime.Value.TotalDays);
		//				lbTotalTime.Text = $"{totalDays} days";
		//			}
		//			else
		//			{
		//				int totalHours = (int)Math.Round(totalTime.Value.TotalHours);
		//				lbTotalTime.Text = $"{totalHours} hours";
		//			}
		//		}
		//		else
		//		{
		//			lbTotalTime.Text = "Unavailable";
		//		}

		//		if (order.Rentaltype == RentalTypeEnum.Daily)
		//		{
		//			lbRoomPrice.Text = order.PricePerDay.ToString("0") + " VNĐ";
		//		}
		//		else
		//		{
		//			lbRoomPrice.Text = order.PriceByHour.ToString("0") + " VNĐ";
		//		}
		//	}
		//}
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

				decimal total = (firstOrderViewModel.ToTal ?? 0) - (firstOrderViewModel.Prepay ?? 0);


                if (total > 0)
				{
					lbTotalPrice.Text = $"{total:0} VNĐ";
				}
				else
				{
					lbTotalPrice.Text = $"{Math.Abs(total):0} VNĐ Excess";
				}

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
			//lbTotalPrice.Text = (ToTal).ToString() + " VND";
			ToTal = ToTal;

			decimal? totalPricePrepay = _orderService.GetOrdersViewModels(OrderId)[0].Prepay;

			decimal calculate = (ToTal ?? 0) - (totalPricePrepay ?? 0);
			if (calculate > 0)
			{
				lbTotalPrice.Text = $"{calculate:0} VNĐ";
			}
			else
			{
				lbTotalPrice.Text = $"{Math.Abs(calculate):0} VNĐ Excess";
			}
			return $"Voucher applied successfully. Discount: {ToTalPriceDiscount.ToString("0")} VNĐ";
		}

		private void btnApplyVoucher_Click(object sender, EventArgs e)
		{

		}

		private void btn_confirm_Click(object sender, EventArgs e)
		{
			try
			{
				if (MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					if (cbbPayment.SelectedItem == null)
					{
						MessageBox.Show("Choose Payment Method", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
						return;
					}
					MessageBox.Show("Success", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
						Status = RoomStatus.CleanUp
					};
					_roomService.UpdadateStatusRoom(updateStatusRoom);
					SentMailAddPoint(customer.Email, PointAdd, newPoints);
					if (MessageBox.Show("You want to issue an invoice?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						Issue_An_invoice();
					}
					TriggerCheckOutEvent();
				}
			}
			catch (Exception)
			{

				return;
			}
		}

		private void TriggerCheckOutEvent()
		{
			OnCheckOut?.Invoke();
			this.Close(); 
		}

		void SentMailAddPoint(string Email, decimal point, decimal ToTalPoint)
		{
			if (string.IsNullOrEmpty(Email))
			{
				MessageBox.Show("Please enter your email.");
				return;
			}

			MailMessage mail = new MailMessage();
			mail.To.Add(Email.Trim());
			mail.From = new MailAddress("thaothaobatbai123@gmail.com");
			mail.Subject = "Notification of adding points";
			mail.IsBodyHtml = true; ;
			mail.Body = $"You have received <b>{point}</b> points. Your total score is <b>{ToTalPoint}</b> points.<br><br>Thank you for using our service.";

			SmtpClient smtp = new SmtpClient("smtp.gmail.com");
			smtp.EnableSsl = true;
			smtp.Port = 587;
			smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
			smtp.Credentials = new NetworkCredential("thaothaobatbai123@gmail.com", "kaefdapftqcriiwj");

			try
			{
				smtp.Send(mail);
				MessageBox.Show("Email đã được gửi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi khi gửi email: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			try
			{

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
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
							 .SetFontSize(26)
							 .SetTextAlignment(TextAlignment.CENTER)
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
						if (cbbPayment.SelectedIndex == 1)
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


						AddOrderDataCell("Số Giờ/Ngày Thuê");
						AddOrderDataCell(totalTimeString);

						AddOrderDataCell("Số Điểm Cộng");
						AddOrderDataCell(PointAdd.ToString() + " Points");

						AddOrderDataCell("Tiền Trả Trước");
						AddOrderDataCell(filterData.Prepay?.ToString("0") + " VND");

						AddOrderDataCell("Giá Phòng");
						AddOrderDataCell(PriceRoom?.ToString("0") + " VND");

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

		void LoadPayMent()
		{
			string[] Payment = { "Payment by cash", "Payment by card" };
			foreach (string s in Payment)
			{
				cbbPayment.Items.Add(s);
			}
			cbbPayment.SelectedItem = -1;
		}
		private string GetQRCodeImageData()
		{
			try
			{
				var apiRequest = new ApiRequest
				{
					acqId = 970436,
					accountNo = "0731000935774",
					accountName = "HOANG THAI SON",
					amount = Convert.ToInt32(ToTal),
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
		private byte[] ImageToByteArray(Image image)
		{
			using (MemoryStream ms = new MemoryStream())
			{
				image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
				return ms.ToArray();
			}
		}

		private void cbbOrderType_SelectedIndexChanged(object sender, EventArgs e)
		{
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
			decimal calculate = (ToTal ?? 0) - (filterData.Prepay ?? 0);
			if (calculate > 0)
			{
				lbTotalPrice.Text = $"{calculate:0} VNĐ";
			}
			else
			{
				lbTotalPrice.Text = $"{Math.Abs(calculate):0} VNĐ Excess";
			}

			lbPricePoint.Text = pointsUsed.ToString() + " VND";
			//lbTotalPrice.Text = (ToTal).ToString() + " VND";
		}

		private void btnApply_Click(object sender, EventArgs e)
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
		void LoadCbbVoucher()
		{
			cbbVoucher.Items.Clear();
			foreach (var item in _voucherSevice.GetAllVoucherFromDb().Where(x => x.Status == VoucherStatus.Active))
			{
				cbbVoucher.Items.Add(item.VoucherCode);
			}
			cbbVoucher.DropDownStyle = ComboBoxStyle.DropDown;
			cbbVoucher.Enabled = true;
		}

		private void cbbVoucher_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cbbVoucher.SelectedItem != null)
			{
				string selectedVoucherCode = cbbVoucher.SelectedItem.ToString();

				string validationResult = _voucherSevice.ValidateVoucher(selectedVoucherCode, OrderId);

				if (validationResult == "Voucher validation successful")
				{
					TbVoucher.BackColor = Color.LightGreen;

					string applyResult = ApplyVoucher(selectedVoucherCode);

					MessageBox.Show(applyResult);
				}
				else
				{
					TbVoucher.BackColor = Color.LightPink;

					MessageBox.Show(validationResult);
				}
			}
		}

	}
}
