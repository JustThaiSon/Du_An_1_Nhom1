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

namespace QuanLyPhong
{
	public partial class frmKindOfRoom : Form
	{
		private readonly IKindOfRoomService _service;
		Guid IdCell;
		public frmKindOfRoom()
		{
			InitializeComponent();
			_service = new KindOfRoomService();
			Load();
		}
		public void Load()
		{
			DTgrvkindofroom.ColumnCount = 5;
			DTgrvkindofroom.Columns[0].Name = "STT";
			DTgrvkindofroom.Columns[1].Name = "KindOfRoomName";
			DTgrvkindofroom.Columns[2].Name = "PricePerDay";
			DTgrvkindofroom.Columns[3].Name = "PriceByHour";
			DTgrvkindofroom.Columns[4].Name = "Id";
			DTgrvkindofroom.Columns[4].Visible = false;
			DTgrvkindofroom.Rows.Clear();
			int stt = 0;
			foreach (var item in _service.GetAllKindOfRoomFromDb())
			{
				stt++;
				DTgrvkindofroom.Rows.Add(stt, item.KindOfRoomName, item.PricePerDay, item.PriceByHour, item.Id);
			}
		}
		public void Clear()
		{
			txtName.Clear();
			txtDay.Clear();
			txtHour.Clear();
		}
		public void Create()
		{

			if (decimal.TryParse(txtDay.Text, out decimal pricePerDay) &&
			decimal.TryParse(txtHour.Text, out decimal priceByHour))
			{
				var kindOfRoom = new KindOfRoom
				{
					KindOfRoomName = txtName.Text,
					PricePerDay = pricePerDay,
					PriceByHour = priceByHour
				};

				if (MessageBox.Show("Do you want to add this KindOfRoom?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					string result = _service.AddKindOfRoom(kindOfRoom);
					MessageBox.Show(result, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				Load();
				Clear();
			}
			else
			{
				MessageBox.Show("kiểm tra lại giá .");
			}
		}
		public void Update()
		{
			if (decimal.TryParse(txtDay.Text, out decimal pricePerDay) &&
		decimal.TryParse(txtHour.Text, out decimal priceByHour))
			{
				var kindOfRoom = new KindOfRoom
				{
					Id = IdCell,
					KindOfRoomName = txtName.Text,
					PricePerDay = pricePerDay,
					PriceByHour = priceByHour
				};

				if (MessageBox.Show("Do you want to Update this KindOfRoom?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					string result = _service.UpdateKindOfRoom(kindOfRoom);
					MessageBox.Show(result, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				Load();
				Clear();
			}
			else
			{
				MessageBox.Show("Vui lòng kiểm tra lại thông tin");
			}
		}
		public void Delete()
		{

			if (MessageBox.Show("Do you want to delete this KindOfRoom?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				string result = _service.RemoveKindOfRoom(IdCell);
				MessageBox.Show(result, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			Load();
			Clear();

		}


		private void KindOfRoom_Load(object sender, EventArgs e)
		{

		}

		private void DTgrvkindofroom_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0 && e.RowIndex < _service.GetAllKindOfRoomFromDb().Count())
			{
				DataGridViewRow row = DTgrvkindofroom.Rows[e.RowIndex];
				IdCell = Guid.Parse(row.Cells["Id"].Value.ToString());
				txtName.Text = row.Cells["KindOfRoomName"].Value.ToString();
				txtDay.Text = row.Cells["PricePerDay"].Value.ToString();
				txtHour.Text = row.Cells["PriceByHour"].Value.ToString();
			}
			else
			{
				Clear();
				return;
			}

		}

		private void button1_Click(object sender, EventArgs e)
		{
			Create();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Update();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			Delete();
		}
	}
}
