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
    public partial class AddFloor : Form
    
        {
            private readonly FloorService _floorService;
            Guid IdOfedit;
            public AddFloor(FloorService _floorService)
            {
                InitializeComponent();
                this._floorService = _floorService;
                Load();
            }

            public AddFloor()
            {
            }

            private void tb_nameroom_TextChanged(object sender, EventArgs e)
            {

            }
            public void Load()
            {
                DTgrvFloor.ColumnCount = 3;
                DTgrvFloor.Columns[0].Name = "STT";
                DTgrvFloor.Columns[1].Name = "Tầng";
                DTgrvFloor.Columns[2].Name = "Id";
                DTgrvFloor.Columns[2].Visible = false;
                DTgrvFloor.Rows.Clear();
                int stt = 0;
                foreach (var item in _floorService.GetAllFloorFromDb())
                {
                    stt++;
                    DTgrvFloor.Rows.Add(stt, item.FloorName, item.Id);
                }


            }
            public void Add()
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(txtFloorId.Text))
                    {
                        MessageBox.Show("Floor ID cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                if (string.IsNullOrWhiteSpace(txtFloorName.Text))
                {
                    MessageBox.Show("Floor Name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!int.TryParse(txtFloorName.Text, out _))
                {
                    MessageBox.Show("Floor Name must be a number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Floor floor = new Floor
                    {
                        Id = Guid.Parse(txtFloorId.Text),
                        FloorName = txtFloorName.Text
                    };

                    string result = _floorService.AddFloor(floor);
                    MessageBox.Show(result, "Result", MessageBoxButtons.OK, result.Contains("success") ? MessageBoxIcon.Information : MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {

                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            public void Clear()
            {
                txtFloorId.Clear();
                txtFloorName.Clear();
            }

            public void Edit()

            {
                try

                {
                    if (string.IsNullOrWhiteSpace(txtFloorId.Text) || string.IsNullOrWhiteSpace(txtFloorName.Text))
                    {
                        MessageBox.Show("Floor ID and Floor Name must not be empty.");
                        return;
                    }
                    Floor floor = new Floor
                    {
                        Id = Guid.Parse(txtFloorId.Text),


                        FloorName = txtFloorName.Text
                    };

                string result = _floorService.UpdateFloor(floor);

                MessageBox.Show(result);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");

            }
        }

        public void Remove()
        {
            try
            {

                if (string.IsNullOrWhiteSpace(txtFloorId.Text))
                {
                    MessageBox.Show("Floor ID must not be empty.");
                    return;
                }

                DialogResult dialogResult = MessageBox.Show("Are you sure?", "Confirm Deletion", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {

                    Guid floorId;
                    if (!Guid.TryParse(txtFloorId.Text, out floorId))
                    {
                        MessageBox.Show("Invalid Floor ID.");
                        return;
                    }
                    string result = _floorService.RemoveFloor(floorId);
                    MessageBox.Show(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }



        private void DTgrvFloor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == 0 && e.RowIndex < _floorService.GetAllFloorFromDb().Count)
            {
                DataGridViewRow r = DTgrvFloor.Rows[e.RowIndex];
                txtFloorName.Text = r.Cells[1].Value.ToString();
                IdOfedit = Guid.Parse(r.Cells[2].Value.ToString());

            }
            else
            {
                Clear();
                return;
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }
    }
}
    
