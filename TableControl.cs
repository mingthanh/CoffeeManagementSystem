using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TienSuToCoffee
{
    public partial class TableControl : UserControl
    {
        MYCOFFEEEntitiesS me = DataContextSingleton.Instance;
        public TableControl()
        {
            InitializeComponent();
        }

        private void TableControl_Load(object sender, EventArgs e)
        {
            LoadTable();
            LoadCMB();
        }
        void LoadTable()
        {
            var N = me.TableFoods.ToList();
            dgvTableFood.DataSource = N;
        }

        private void dgvTableFood_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTableFood.SelectedRows.Count > 0)
            {
                // Lấy hàng đầu tiên được chọn
                DataGridViewRow selectedRow = dgvTableFood.SelectedRows[0];

                // Kiểm tra để tránh lỗi khi DataGridView chưa có dữ liệu
                if (selectedRow.Cells["id"].Value != null)
                {
                    // Lấy giá trị từ các cột và đổ vào các điều khiển
                    int idTable = int.Parse(selectedRow.Cells["id"].Value.ToString());
                    txtIDTable.Text = idTable.ToString();
                    txtTableName.Text = selectedRow.Cells["name"].Value.ToString();
                    cmbStatus.Text = selectedRow.Cells["status"].Value.ToString();

                    // Xử lý giá trị điểm trung bình

                }
            }
        }

        private void btnEditTable_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTableFood.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn một bàn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DataGridViewRow selectedRow = dgvTableFood.SelectedRows[0];
                //int id = int.Parse(selectedRow.Cells["id"].Value.ToString());
                string name = txtTableName.Text;
                string status = cmbStatus.Text;
                int id;
                if (!int.TryParse(txtIDTable.Text, out id))
                {
                    MessageBox.Show("ID bàn không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var N = me.TableFoods.FirstOrDefault(s => s.id == id);
                if (N == null)
                {
                    MessageBox.Show("Không tìm thấy bàn để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                N.id = id;
                N.name = name;
                N.status = status;
                me.SaveChanges();
                // LoadDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {

            int id = int.Parse(txtIDTable.Text);
            string name = txtTableName.Text;
            string status = cmbStatus.SelectedItem.ToString();



            // Tạo đối tượng Food mới
            TableFood sv = new TableFood()
            {
                id = id,
                name = name,
                status = status
            };
            try
            {

                me.TableFoods.Add(sv);
                me.SaveChanges();
                MessageBox.Show("Them thanh cong!");
            }
            catch (Exception ex)
            {
                LoadTable();
                MessageBox.Show(ex.Message);

            }
            LoadTable();
        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {

            if (dgvTableFood.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một bàn để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataGridViewRow selectedRow = dgvTableFood.SelectedRows[0];

            int id = int.Parse(selectedRow.Cells["id"].Value.ToString());

            var N = me.TableFoods.First(s => s.id == id);

            try
            {
                me.TableFoods.Remove(N);
                me.SaveChanges();
                MessageBox.Show("Xoa thanh cong!");

            }
            catch (Exception ex)
            {
                LoadTable();
                MessageBox.Show(ex.Message);

            }
            LoadTable();
            txtIDTable.Clear();
            txtTableName.Clear();
        }
        void LoadCMB()
        {
          
            cmbStatus.Items.Add("Empty");
            cmbStatus.Items.Add("Having people");
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
