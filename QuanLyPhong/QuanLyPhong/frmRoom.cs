using BUS.IService;
using BUS.Service;
using BUS.ViewModels;
using DAL.Data;
using DAL.Entities;
using DAL.Enums;
using NPOI.SS.Formula.Functions;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QuanLyPhong
{
	public partial class frmRoom : Form
	{
		Guid IdCell = Guid.Empty;
		private IRoomService _roomService;
		private IFloorService _floorService;
		private IKindOfRoomService _kindOfRoomService;

		public frmRoom()
		{
			InitializeComponent();
			_roomService = new RoomService();
			_floorService = new FloorService();
			_kindOfRoomService = new KindOfRoomService();
			LoadCbbStatus();
			LoadCbbFloor();
			LoadCbbKindOfRoom();
			loadDtg();
			LoadStatusFiler();
			LoadCbbFloorFilter();
			Clear();

			if (Session.RoleCode == "emp")
			{
				btn_addFloor.Enabled = false;
				btn_addTypeRoom.Enabled = false;
			}
		}

		void LoadCbbStatus()
		{
			cbbStatus.DataSource = Enum.GetValues(typeof(RoomStatus));
			cbbStatus.SelectedItem = RoomStatus.Unknown;
		}

		void LoadCbbFloor()
		{
			var floors = _floorService.GetAllFloorFromDb();
			cbb_floor.DataSource = floors;
			cbb_floor.DisplayMember = "FloorName";
			cbb_floor.SelectedIndex = -1;
		}

		void LoadCbbKindOfRoom()
		{
			var kindsOfRoom = _kindOfRoomService.GetAllKindOfRoomFromDb();
			cbb_typeroom.DataSource = kindsOfRoom;
			cbb_typeroom.DisplayMember = "KindOfRoomName";
			cbb_typeroom.SelectedIndex = -1;
		}

		void loadDtg()
		{
			dtgPhong.ColumnCount = 8;
			dtgPhong.Columns[0].Name = "Id";
			dtgPhong.Columns[0].Visible = false;
			dtgPhong.Columns[1].Name = "STT";
			dtgPhong.Columns[2].Name = "RoomName";
			dtgPhong.Columns[3].Name = "Status";
			dtgPhong.Columns[4].Name = "FloorName";
			dtgPhong.Columns[5].Name = "KindOFRoomName";
			dtgPhong.Columns[6].Name = "PricePerDay";
			dtgPhong.Columns[7].Name = "PriceByHour";
			dtgPhong.Rows.Clear();
			int Count = 0;
			foreach (var item in _roomService.GetAllRooms())
			{
				Count++;
				dtgPhong.Rows.Add(item.Id, Count, item.RoomName, item.Status, item.FloorName, item.KindOfRoomName, item.PricePerDay, item.PriceByHour);
			}
		}

		void Clear()
		{
			tb_nameroom.Clear();
			cbbStatus.SelectedIndex = -1;
			cbb_floor.SelectedIndex = -1;
			cbb_typeroom.SelectedIndex = -1;
		}
		void loadDtgSearch(string KeyWord)
		{
			dtgPhong.ColumnCount = 8;
			dtgPhong.Columns[0].Name = "Id";
			dtgPhong.Columns[0].Visible = false;
			dtgPhong.Columns[1].Name = "STT";
			dtgPhong.Columns[2].Name = "RoomName";
			dtgPhong.Columns[3].Name = "Status";
			dtgPhong.Columns[4].Name = "FloorName";
			dtgPhong.Columns[5].Name = "KindOFRoomName";
			dtgPhong.Columns[6].Name = "PricePerDay";
			dtgPhong.Columns[7].Name = "PriceByHour";
			dtgPhong.Rows.Clear();
			int Count = 0;
			foreach (var item in _roomService.GetAllRooms().Where(x => x.RoomName.ToLower().StartsWith(KeyWord.ToLower())|| 
			x.Status.ToString().ToLower().StartsWith(KeyWord.ToLower()) || x.FloorName.ToLower().StartsWith(KeyWord.ToLower()) 
			|| x.KindOfRoomName.ToLower().StartsWith(KeyWord.ToLower())))
			{
				Count++;
				dtgPhong.Rows.Add(item.Id, Count, item.RoomName, item.Status, item.FloorName, item.KindOfRoomName, item.PricePerDay, item.PriceByHour);
			}
		}


		private void guna2TextBox1_TextChanged(object sender, EventArgs e)
		{
			loadDtgSearch(tb_search.Text);
		}


		private void panel2_Paint(object sender, PaintEventArgs e)
		{

		}

		bool Validate()
		{
			if (string.IsNullOrWhiteSpace(tb_nameroom.Text) ||
				cbbStatus.SelectedIndex == -1 ||
				cbb_floor.SelectedIndex == -1 ||
				cbb_typeroom.SelectedIndex == -1)
			{
				return false;
			}
			return true;
		}

		bool ValidateName()
		{
			var Exists = _roomService.GetAllRoomsFromDb().Any(x => x.RoomName == tb_nameroom.Text);
			if (Exists)
			{
				return false;
			}
			return true;
		}

		bool ValidateNameRoom()
		{
			string roomName = tb_nameroom.Text.Trim();
			string floorNumberText = cbb_floor.Text.Trim();

			System.Text.RegularExpressions.Match match = Regex.Match(floorNumberText, @"\d+");

			if (match.Success)
			{
				int number = int.Parse(match.Value);

				string expectedNameFormat = $"^Room\\s+{number}\\d+$";

				if (!Regex.IsMatch(roomName, expectedNameFormat, RegexOptions.IgnoreCase))
				{
					MessageBox.Show($"Room name '{roomName}' invalid. Exp: Room {number}07", "Notificatiom", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return false;
				}

				return true;
			}
			else
			{
				MessageBox.Show("Not fint floor number.", "Notificatiom", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
		}

		private void btn_addRoom_Click(object sender, EventArgs e)
		{
			if (cbbStatus.Text == null)
			{
				MessageBox.Show("Status is not empty", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (cbb_floor.Text == null)
			{
				MessageBox.Show("Floor is not empty", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (cbb_typeroom.Text == null)
			{
				MessageBox.Show("Kind Of Room is not empty", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (!Validate())
			{
				MessageBox.Show("Room is not empty", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (!ValidateName())
			{
				MessageBox.Show("Name room Exist", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (!ValidateNameRoom())
			{
				return;
			}



			var Room = new RoomViewModels()
			{
				RoomName = tb_nameroom.Text,
				Status = Enum.TryParse(cbbStatus.Text, out RoomStatus status) ? status : RoomStatus.Unknown,
				FloorId = _floorService.GetAllFloorFromDb().Where(x => x.FloorName == cbb_floor.Text).Select(x => x.Id).FirstOrDefault(),
				KindOfRoomId = _kindOfRoomService.GetAllKindOfRoomFromDb().Where(x => x.KindOfRoomName == cbb_typeroom.Text).Select(x => x.Id).FirstOrDefault(),
			};

			if (MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				string result = _roomService.AddRoom(Room);
				MessageBox.Show(result, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}

			loadDtg();
			Clear();
		}

		private void btnRemove_Click(object sender, EventArgs e)
		{
			//var exists = _roomService.GetAllRoomsFromDb().Any(x => x.Id == IdCell);
			//if (!exists)
			//{
			//	MessageBox.Show("Phòng không tồn tại");
			//	return;
			//}

			//if (MessageBox.Show("Bạn có muốn xóa phòng này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			//{
			//	string result = _roomService.RemoveRoom(IdCell);
			//	MessageBox.Show(result, "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
			//}

			//loadDtg();
			//Clear();
		}

		private void dtgPhong_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}

		private void dtgPhong_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				DataGridViewRow selected = dtgPhong.SelectedRows[0];
				tb_nameroom.Text = selected.Cells[2].Value?.ToString() ?? "";
				cbbStatus.Text = selected.Cells[3].Value?.ToString() ?? "";
				cbb_floor.Text = selected.Cells[4].Value?.ToString() ?? "";
				cbb_typeroom.Text = selected.Cells[5].Value?.ToString() ?? "";
				IdCell = Guid.Parse(selected.Cells[0].Value?.ToString() ?? "");
			}
			catch (Exception)
			{
				return;
			}
		}

		private void btnUpdate_Click(object sender, EventArgs e)
		{
			var exists = _roomService.GetAllRoomsFromDb().Any(x => x.Id == IdCell);
			if (!exists)
			{
				MessageBox.Show("Room not is exist", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (cbbStatus.Text == null)
			{
				MessageBox.Show("Status is not empty", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (cbb_floor.Text == null)
			{
				MessageBox.Show("Floor is not empty", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (cbb_typeroom.Text == null)
			{
				MessageBox.Show("Kind Of Room is not empty", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (!Validate())
			{
				MessageBox.Show("Room is not empty", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (!ValidateName() && tb_nameroom.Text.Trim() != _roomService.GetAllRoomsFromDb().Where(x => x.Id == IdCell).Select(x => x.RoomName).FirstOrDefault())
			{
				MessageBox.Show("Name room Exist", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (!ValidateNameRoom())
			{
				return;
			}

			var Room = new RoomViewModels()
			{
				Id = IdCell,
				RoomName = tb_nameroom.Text,
				Status = Enum.TryParse(cbbStatus.Text, out RoomStatus status) ? status : RoomStatus.Unknown,
				FloorId = _floorService.GetAllFloorFromDb().Where(x => x.FloorName == cbb_floor.Text).Select(x => x.Id).FirstOrDefault(),
				KindOfRoomId = _kindOfRoomService.GetAllKindOfRoomFromDb().Where(x => x.KindOfRoomName == cbb_typeroom.Text).Select(x => x.Id).FirstOrDefault(),
			};

			if (MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				string result = _roomService.UpdateRoom(Room);
				MessageBox.Show(result, "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}

			loadDtg();
			Clear();
		}

		private void tb_price_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
			{
				e.Handled = true;
			}
		}

		private void btn_addFloor_Click(object sender, EventArgs e)
		{
			frmFloor addFloor = new frmFloor();
			addFloor.FloorUpdated += new frmFloor.FloorUpdatedEventHandler(OnFloorUpdated);
			addFloor.Show();
		}

		private void btn_addTypeRoom_Click(object sender, EventArgs e)
		{
			frmKindOfRoom addTypeRoom = new frmKindOfRoom();
			LoadCbbKindOfRoom();
			addTypeRoom.Show();
		}

		private void frmRoom_Load(object sender, EventArgs e)
		{

		}

		private void OnFloorUpdated(object sender, EventArgs e)
		{
			LoadCbbFloor();
		}

		void LoadStatusFiler()
		{

			cbbStatusfilter.Items.Add("All");
			foreach (RoomStatus status in Enum.GetValues(typeof(RoomStatus)))
			{
				cbbStatusfilter.Items.Add(status);
			}
			cbbStatusfilter.SelectedIndex = 0;
		}

		void loadDtg(string Status)
		{
			dtgPhong.ColumnCount = 8;
			dtgPhong.Columns[0].Name = "Id";
			dtgPhong.Columns[0].Visible = false;
			dtgPhong.Columns[1].Name = "STT";
			dtgPhong.Columns[2].Name = "RoomName";
			dtgPhong.Columns[3].Name = "Status";
			dtgPhong.Columns[4].Name = "FloorName";
			dtgPhong.Columns[5].Name = "KindOFRoomName";
			dtgPhong.Columns[6].Name = "PricePerDay";
			dtgPhong.Columns[7].Name = "PriceByHour";
			dtgPhong.Rows.Clear();
			int Count = 0;
			var rooms = _roomService.GetAllRooms();

			if (Status == "All")
			{
				foreach (var item in rooms)
				{
					Count++;
					dtgPhong.Rows.Add(item.Id, Count, item.RoomName, item.Status, item.FloorName, item.KindOfRoomName, item.PricePerDay, item.PriceByHour);
				}
			}
			else
			{
				if (Enum.TryParse(Status, out RoomStatus statusEnum))
				{
					foreach (var item in rooms.Where(x => x.Status == statusEnum))
					{
						Count++;
						dtgPhong.Rows.Add(item.Id, Count, item.RoomName, item.Status, item.FloorName, item.KindOfRoomName, item.PricePerDay, item.PriceByHour);
					}
				}
			}
		}
		private void cbbStatusfilter_SelectedIndexChanged(object sender, EventArgs e)
		{
			string selectedStatus = cbbStatusfilter.SelectedItem.ToString();
			loadDtg(selectedStatus);
		}

		void LoadCbbFloorFilter()
		{

			cbbFloorFilter.Items.Add("All");
			foreach (var item in _floorService.GetAllFloorFromDb())
			{
				cbbFloorFilter.Items.Add(item.FloorName);
			}
			cbbFloorFilter.SelectedIndex = 0;
		}


		void loadDtgFloor(Guid IdFloor)
		{
			dtgPhong.ColumnCount = 8;
			dtgPhong.Columns[0].Name = "Id";
			dtgPhong.Columns[0].Visible = false;
			dtgPhong.Columns[1].Name = "STT";
			dtgPhong.Columns[2].Name = "RoomName";
			dtgPhong.Columns[3].Name = "Status";
			dtgPhong.Columns[4].Name = "FloorName";
			dtgPhong.Columns[5].Name = "KindOFRoomName";
			dtgPhong.Columns[6].Name = "PricePerDay";
			dtgPhong.Columns[7].Name = "PriceByHour";
			dtgPhong.Rows.Clear();
			int Count = 0;
			foreach (var item in _roomService.GetAllRooms().Where(x => x.FloorId == IdFloor))
			{
				Count++;
				dtgPhong.Rows.Add(item.Id, Count, item.RoomName, item.Status, item.FloorName, item.KindOfRoomName, item.PricePerDay, item.PriceByHour);
			}
		}

		private void cbbFloorFilter_SelectedIndexChanged(object sender, EventArgs e)
		{
			string selectedStatus = cbbFloorFilter.SelectedItem.ToString();
			if (selectedStatus == "All")
			{
				loadDtg();
				return;
			}
			Guid Id = _floorService.GetAllFloorFromDb().Where(x => x.FloorName == selectedStatus).Select(x => x.Id).FirstOrDefault();
			loadDtgFloor(Id);
		}

		private void cbb_typeroom_Click(object sender, EventArgs e)
		{
			LoadCbbKindOfRoom();
		}
	}
}
