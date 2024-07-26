using BUS.IService;
using BUS.Service;
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
    public partial class KindOfRoom : Form
    {
        private readonly KindOfRoomService _service;
        Guid Idofedit;
        public KindOfRoom(KindOfRoomService _service)
        {
            InitializeComponent();
            this._service = _service;
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
            txtId.Clear();
            txtName.Clear();
            txtDay.Clear();
            txtHour.Clear();
        }
        public void Create()
        {

            if (decimal.TryParse(txtDay.Text, out decimal pricePerDay) &&
            decimal.TryParse(txtHour.Text, out decimal priceByHour))
            {
                KindOfRoom kindOfRoom = new KindOfRoom
                {
                    KindOfRoomName = txtName.Text,
                    PricePerDay = pricePerDay,
                    PriceByHour = priceByHour
                };

                string result = _service.AddKindOfRoom("", kindOfRoom);
                MessageBox.Show(result);
                if (result == "thêm thành công ")
                {
                    Load();
                    Clear();
                }
            }
            else
            {
                MessageBox.Show("kiểm tra lại giá .");
            }
        }
        public void Update()
        {
            if (Guid.TryParse(txtId.Text, out Guid id) &&
            decimal.TryParse(txtDay.Text, out decimal pricePerDay) &&
            decimal.TryParse(txtHour.Text, out decimal priceByHour))
            {
                KindOfRoom kin = new KindOfRoom
                {
                    Id = id,
                    KindOfRoomName = txtName.Text,
                    PricePerDay = pricePerDay,
                    PriceByHour = priceByHour
                };

                string result = _service.UpdateKindOfRoom(kin);
                MessageBox.Show(result);
                if (result == "Update thành công ")
                {
                    Load();
                    Clear();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng kiểm tra lại thông tin");
            }
        }
        public void Delete()
        {
            if (Guid.TryParse(txtId.Text, out Guid id))
            {
                string result = _service.RemoveKindOfRoom(id);
                MessageBox.Show(result);
                if (result == "Xóa thành công")
                {
                    Load();
                    Clear();
                }
            }
            else
            {
                MessageBox.Show("kiểm tra lại thông tin");
            }
        }


        private void KindOfRoom_Load(object sender, EventArgs e)
        {

        }

        private void DTgrvkindofroom_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = DTgrvkindofroom.Rows[e.RowIndex];
                Idofedit = Guid.Parse(row.Cells["Id"].Value.ToString());
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
    }
}
