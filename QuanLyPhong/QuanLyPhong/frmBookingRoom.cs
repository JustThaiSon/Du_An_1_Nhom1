using BUS.IService;
using BUS.Service;
using BUS.ViewModels;
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

namespace QuanLyPhong
{
	public partial class frmBookingRoom : Form
	{
		private IRoomService _roomService;
		private IFloorService _floorService;
		private IKindOfRoomService _kindOfRoomService;
		public frmBookingRoom()
		{
			_roomService = new RoomService();
			_floorService = new FloorService();
			_kindOfRoomService = new KindOfRoomService();
			InitializeComponent();
			LoadBookingRoom();
		}

		private void LoadBookingRoom()
		{
			if (tableLayoutPanelRoom != null)
			{
				this.Controls.Remove(tableLayoutPanelRoom);
			}

			var floors = _floorService.GetAllFloorFromDb();
			var rooms = _roomService.GetAllRooms();

			// Extract numeric part from FloorName and sort by that numeric part
			var sortedFloors = floors
				.Select(floor => new
				{
					Floor = floor,
					SortingNumber = GetSortingNumber(floor.FloorName)
				})
				.OrderBy(f => f.SortingNumber) // Sort by the numeric part
				.Select(f => f.Floor)
				.ToList();

			tableLayoutPanelRoom = new TableLayoutPanel
			{
				Dock = DockStyle.Fill,
				AutoScroll = true,
				ColumnCount = 1,
				RowCount = sortedFloors.Count, // Set RowCount based on sortedFloors
				RowStyles = { new RowStyle(SizeType.AutoSize) }
			};

			foreach (var floor in sortedFloors)
			{
				var roomsOnFloor = rooms.Where(r => r.FloorId == floor.Id).ToList();

				if (roomsOnFloor.Any())
				{
					var floorName = floor.FloorName.Length > 5 ? floor.FloorName.Substring(5) : floor.FloorName;
					int sortingNumber;
					bool isNumber = int.TryParse(floorName, out sortingNumber);

					GroupBox floorGroupBox = new GroupBox
					{
						Text = "Floor " + floorName, 
						Font = new Font("Arial", 12, FontStyle.Bold),
						Dock = DockStyle.Top,
						Height = 300,
						Margin = new Padding(10)
					};

					FlowLayoutPanel floorFlowLayoutPanel = new FlowLayoutPanel
					{
						Dock = DockStyle.Fill,
						AutoScroll = true,
						WrapContents = true,
						FlowDirection = FlowDirection.LeftToRight
					};

					foreach (var room in roomsOnFloor)
					{
						var roomControl = new RoomControl();
						roomControl.SetRoom(room);
						floorFlowLayoutPanel.Controls.Add(roomControl);
					}

					floorGroupBox.Controls.Add(floorFlowLayoutPanel);
					// Use the index of sortedFloors to place the GroupBox correctly
					tableLayoutPanelRoom.Controls.Add(floorGroupBox, 0, sortedFloors.IndexOf(floor));
				}
			}

			this.Controls.Add(tableLayoutPanelRoom);
		}

		private int GetSortingNumber(string floorName)
		{
			if (floorName.Length > 5)
			{
				var numberPart = floorName.Substring(5);
				int sortingNumber;
				if (int.TryParse(numberPart, out sortingNumber))
				{
					return sortingNumber;
				}
			}
			return int.MaxValue;
		}
	}
}

