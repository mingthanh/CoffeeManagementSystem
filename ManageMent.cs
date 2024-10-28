using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using System.Globalization;
using System.Threading;
using System.Runtime.InteropServices;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text.pdf.qrcode;
using System.Drawing.Imaging;
using QRCoder;
using QRCodeGen = QRCoder.QRCode;
using System.IdentityModel.Tokens;

// Để sử dụng Bitmap


namespace TienSuToCoffee


{
    public partial class frmManageMent : Form
    {
        public enum TableStatus
        {
            Empty = 0,
            HavingPeople = 1
        }
        MYCOFFEEEntitiesS me = DataContextSingleton.Instance;
        
        private BILL currentBill;
        private decimal totalAmount = 0; // Tổng tiền
        private Income incomeForm;
        public frmManageMent()
        {
            InitializeComponent();
            List<Customer> customerList = me.Customers.ToList();

           
            if (UserSession.UserType != 1)
            {
                menuAdmin.Visible = false; // Ẩn menu Admin nếu không phải admin
            }
         

        }




        private void frmManageMent_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                e.Cancel = true; // Hủy sự kiện đóng form
            }
            UserSession.UserType = 0;
        }

     
        public void menuAdmin_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có khách hàng nào không
         
                    frmAdministrator frmAdministrator = new frmAdministrator();
                    this.Hide();
                    frmAdministrator.ShowDialog();
                    this.Show();
          
        }




        private void menuLogOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void menuPersonal_Click(object sender, EventArgs e)
        {
            frmPersonal frmPersonal = new frmPersonal();
            this.Hide();
            frmPersonal.ShowDialog();
            this.Show();
        }

        void LoadCategories()
        {// Truy vấn để lấy danh sách danh mục thực phẩm

            var categories = me.FoodCategories
                               .Select(c => new { c.id, c.name })
                               .ToList();

            if (categories.Count > 0)
            {
                cmbCategory.DataSource = categories;
                cmbCategory.DisplayMember = "name"; // Hiển thị tên trong ComboBox
                cmbCategory.ValueMember = "id";     // Lưu id khi chọn
            }
            else
            {
                MessageBox.Show("Không có danh mục nào để hiển thị.");
            }
        }
        private void LoadFoodsByCategory(int selectedCategoryId)
        {

            var foods = me.Foods
                          .Where(f => f.price > 0 && f.idCategory == selectedCategoryId)
                          .Select(f => new { f.id, f.name })
                          .ToList();

            // Gán dữ liệu vào ComboBox thức ăn
            cmbCategoryFood.DataSource = foods;
            cmbCategoryFood.DisplayMember = "name"; // Hiển thị tên món ăn trong ComboBox
            cmbCategoryFood.ValueMember = "id";     // Lưu id khi chọn

            // Nếu không có món ăn nào, xóa dữ liệu trong ComboBox thức ăn
            if (foods.Count == 0)
            {
                cmbCategoryFood.DataSource = null;
            }
        }

        private void frmManageMent_Load(object sender, EventArgs e)
        {
            LoadCategories();

            LoadTable();
            lblDisplayname.Text = UserSession.UserDisplayname;
            lblCustomername.Text = UserSession.UserFullname;
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmbCategory.SelectedValue != null)
            {
                // Thử ép kiểu SelectedValue về int một cách an toàn
                int selectedCategoryId;
                if (int.TryParse(cmbCategory.SelectedValue.ToString(), out selectedCategoryId))
                {
                    // Gọi hàm LoadFoodsByCategory với id của danh mục
                    LoadFoodsByCategory(selectedCategoryId);
                }
                else
                {
                    foreach (var food in me.Foods)
                    {
                        if (food.price <= 0)
                        {
                            // Bỏ qua nếu điều kiện không thỏa mãn
                            continue;
                        }
                    }


                }
            }

        }

        void LoadTable()
        {
            var tableList = me.TableFoods.ToList(); // Lấy danh sách bàn

            foreach (var table in tableList)
            {
                Button btn = new Button
                {
                    Width = 100,
                    Height = 100,
                    Text = $"{table.name}\n{table.status}", // Hiển thị tên và trạng thái
                    BackColor = (table.status == "Empty") ? Color.HotPink : Color.DeepPink,
                    Tag = table // Lưu đối tượng TableFood vào Tag
                };

                btn.Click += btn_Click; // Gọi hàm xử lý sự kiện khi nhấn nút
                flpTable.Controls.Add(btn); // Thêm nút vào FlowLayoutPanel
            }

        }



        private void btn_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null && clickedButton.Tag is TableFood)
            {
                TableFood selectedTable = (TableFood)clickedButton.Tag;
                // Thực hiện các thao tác với selectedTable, ví dụ:
                MessageBox.Show($"Bạn đã chọn bàn: {selectedTable.name}, Trạng thái: {selectedTable.status}");

           
                OpenBillForm(selectedTable);
            }
        }

        private void OpenBillForm(TableFood selectedTable)
        {

            try
            {
                // Kiểm tra xem hóa đơn hiện tại có tồn tại cho bàn này không
                BILL bill = me.BILLs.FirstOrDefault(b => b.idTable == selectedTable.id && b.status == 0);
                if (bill == null)
                {
                    // Nếu chưa có hóa đơn, tạo mới một hóa đơn
                    bill = new BILL
                    {
                        idTable = selectedTable.id,
                        DateCheckIn = DateTime.Now,
                        status = 0
                    };
                    me.BILLs.Add(bill);
                    me.SaveChanges();
                }

                currentBill = bill; // Thiết lập currentBill

                // Cập nhật ListView và tổng tiền dựa trên hóa đơn hiện tại
                RefreshOrderList();
                UpdateTotalAmount();

                // Cập nhật trạng thái bàn trên giao diện
                RefreshTableStatus(selectedTable.id);
            }
            catch (DbUpdateException dbEx)
            {
                string errorMessage = "Đã xảy ra lỗi khi cập nhật cơ sở dữ liệu: " + (dbEx.InnerException?.Message ?? dbEx.Message);
                MessageBox.Show(errorMessage, "Lỗi Cơ Sở Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                
               // MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void RefreshTableStatus(int tableId)
        {


            try
            {
                // Tìm bàn trong cơ sở dữ liệu
                TableFood table = me.TableFoods.Find(tableId);
                if (table != null)
                {
                    // Kiểm tra xem còn hóa đơn nào chưa thanh toán cho bàn này không
                    bool hasUnpaidBill = me.BILLs.Any(b => b.idTable == tableId && b.status == 0);

                    // Nếu status là 0, thiết lập trạng thái là "Empty"
                    if (!hasUnpaidBill)
                    {
                        table.status = "Empty"; // Cập nhật trạng thái bàn
                    }
                    else
                    {
                        table.status = "Having People"; // Hoặc "Có Người"
                    }

                    // Cập nhật trạng thái trong cơ sở dữ liệu
                    me.SaveChanges();

                    // Cập nhật màu sắc và trạng thái trên button
                    foreach (Control control in flpTable.Controls)
                    {
                        if (control is Button btn && btn.Tag is TableFood tf && tf.id == tableId)
                        {
                            btn.Text = $"{tf.name}\n{tf.status}";
                            btn.BackColor = (tf.status == "Empty") ? Color.HotPink : Color.DeepPink;
                            break;
                        }
                    }
                }
            }
            catch (DbUpdateException dbEx)
            {
                string errorMessage = "Đã xảy ra lỗi khi cập nhật trạng thái bàn: " + (dbEx.InnerException?.Message ?? dbEx.Message);
                MessageBox.Show(errorMessage, "Lỗi Cơ Sở Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshOrderList()
        {
            if (currentBill == null)
            {
                lvOrder.Items.Clear();
                return;
            }

            // Lấy danh sách món ăn trong hóa đơn hiện tại
            var billDetails = me.BillInfoes
                                .Where(bd => bd.idBill == currentBill.id)
                                .Select(bd => new
                                {
                                    bd.Food.name,
                                    bd.count,
                                    Đơn_Giá = bd.Food.price,
                                    Thành_Tiền = bd.count * bd.Food.price
                                })
                                .ToList();

            // Làm sạch ListView trước khi thêm
            lvOrder.Items.Clear();
            CultureInfo culture = new CultureInfo("vi-VN");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            foreach (var detail in billDetails)
            {
                ListViewItem item = new ListViewItem(detail.name);
                item.SubItems.Add(detail.count.ToString());
                item.SubItems.Add(detail.Đơn_Giá.ToString());
                item.SubItems.Add(detail.Thành_Tiền.ToString());
                lvOrder.Items.Add(item);
            }
        }

        private void menuLoadTable_Click(object sender, EventArgs e)
        {
            flpTable.Controls.Clear();
            LoadTable();
        }





        private void btnPay_Click(object sender, EventArgs e)
        {
           

            if (currentBill == null)
            {
                MessageBox.Show("Không có hóa đơn để thanh toán.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (totalAmount == 0)
            {

                MessageBox.Show("Tổng tiền phải lớn hơn 0 để thực hiện thanh toán.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lvOrder.Items.Clear();
                return;
            }
            bool hasValidQuantity = lvOrder.Items.Cast<ListViewItem>().Any(item =>
            {
                int quantity;
                return int.TryParse(item.SubItems[1].Text, out quantity) && quantity > 0;
            });

            if (!hasValidQuantity)
            {
                MessageBox.Show("Không có món ăn nào với số lượng lớn hơn 0 để thanh toán.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            // Xác nhận thanh toán
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thanh toán hóa đơn này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    currentBill.status = 1; // Đánh dấu hóa đơn đã thanh toán
                    currentBill.DateCheckOut = DateTime.Now;
                    // Xóa các BillInfo liên quan đến hóa đơn này
                   // incomeForm.UpdateRevenueAfterBillPayment(currentBill.DateCheckOut.Value, (double)totalAmount); // incomeForm là tham chiếu đến form Income

                    var billDetails = me.BillInfoes.Where(bd => bd.idBill == currentBill.id).ToList();
                    
                    foreach (var detail in billDetails)
                    {
                        me.BillInfoes.Remove(detail);
                    }

                    me.SaveChanges();
                   

                    MessageBox.Show("Thanh toán thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    

                    // Cập nhật trạng thái bàn

                    RefreshTableStatus(currentBill.idTable.Value);
                    currentBill = null;// Reset currentBill
                    // Làm sạch ListView và tổng tiền
                    lvOrder.Items.Clear();
                    txtTotal.Text = "0";
                }
                catch (DbUpdateException dbEx)
                {
                    string errorMessage = "Đã xảy ra lỗi khi cập nhật cơ sở dữ liệu: " + (dbEx.InnerException?.Message ?? dbEx.Message);
                    MessageBox.Show(errorMessage, "Lỗi Cơ Sở Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }





        private void AddOrUpdateListViewItem(string foodName, int quantity, decimal price, decimal total)
        {
            // Tìm kiếm xem món ăn đã tồn tại trong ListView chưa
            ListViewItem existingItem = lvOrder.Items.Cast<ListViewItem>().FirstOrDefault(item => item.Text == foodName);
            if (existingItem != null)
            {
                // Cập nhật số lượng và thành tiền
                int existingQuantity = int.Parse(existingItem.SubItems[1].Text);
                existingQuantity += quantity;
                existingItem.SubItems[1].Text = existingQuantity.ToString();

                decimal newTotal = existingQuantity * price;
                existingItem.SubItems[3].Text = newTotal.ToString("C");
            }
            else
            {
                // Thêm một ListViewItem mới
                ListViewItem item = new ListViewItem(foodName); // Cột "Món ăn"
                item.SubItems.Add(quantity.ToString()); // Cột "Số lượng"
                item.SubItems.Add(price.ToString("C")); // Cột "Đơn giá"
                item.SubItems.Add(total.ToString("C")); // Cột "Thành tiền"
                lvOrder.Items.Add(item);
            }
        }
        private void UpdateTotalAmount()
        {
            if (currentBill != null)
            {
                totalAmount = (decimal)me.BillInfoes
                                .Where(bd => bd.idBill == currentBill.id)
                                .Sum(bd => bd.count * bd.Food.price);

                txtTotal.Text = totalAmount.ToString("C", CultureInfo.CurrentCulture);
            }
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có bàn nào được chọn và hóa đơn hiện tại đã được thiết lập chưa

                if (currentBill == null && currentBill.status == 0) // Kiểm tra trạng thái hóa đơn
                {
                    currentBill.status = 1; // Cập nhật trạng thái hóa đơn                  
                    //RefreshTableStatus(currentBill.idTable.Value);
                }
                // Kiểm tra xem có món ăn nào được chọn không
                if (cmbCategoryFood.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn một món ăn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy ID của món ăn được chọn
                int selectedFoodId = (int)cmbCategoryFood.SelectedValue;
                string foodName = cmbCategoryFood.Text;
                int quantity = (int)nmCount.Value; // Đảm bảo nmCount là NumericUpDown hoặc một điều khiển tương tự

                // Tìm món ăn trong cơ sở dữ liệu

             
                // Tìm món ăn trong cơ sở dữ liệu
                Food food = me.Foods.FirstOrDefault(s => s.id == selectedFoodId);
                if (food == null)
                {
                    MessageBox.Show("Món ăn không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                // Kiểm tra món ăn đã tồn tại trong hóa đơn chưa
                BillInfo existingDetail = me.BillInfoes.FirstOrDefault(bd => bd.idBill == currentBill.id && bd.idFood == selectedFoodId);

                // Tìm món ăn trong ListView
                ListViewItem existingItem = lvOrder.Items.Cast<ListViewItem>().FirstOrDefault(item => item.Text == foodName);

                // Nếu món ăn đã có trong ListView, kiểm tra nếu tổng số lượng mới không được < 0
                if (existingItem != null)
                {
                    int currentQuantity = int.Parse(existingItem.SubItems[1].Text);
                    int newTotalQuantity = currentQuantity + quantity;

                    // Cập nhật số lượng món ăn trong BillInfo và ListView nếu hợp lệ
                    existingDetail.count = newTotalQuantity;
                    existingItem.SubItems[1].Text = newTotalQuantity.ToString();
                    existingItem.SubItems[3].Text = (newTotalQuantity * food.price).ToString();
                }
                else
                {
                    // Nếu món ăn chưa có trong ListView, chỉ cho phép thêm nếu số lượng > 0
                    if (quantity > 0)
                    {
                        BillInfo newBillDetail = new BillInfo
                        {
                            idBill = currentBill.id,
                            idFood = selectedFoodId,
                            count = quantity
                        };
                        me.BillInfoes.Add(newBillDetail);
                        me.SaveChanges();
                        //// Cập nhật tổng tiền
                        decimal price = (decimal)food.price;
                        decimal total = price * quantity;                        
                        UpdateTotalAmount();
                        AddOrUpdateListViewItem(foodName, quantity, price, total);
                    }
                    else
                    {
                        MessageBox.Show("Số lượng không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lvOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra xem có mục nào được chọn trong ListView không
            if (lvOrder.SelectedItems.Count > 0)
            {
                // Lấy hàng được chọn (SelectedItem)
                ListViewItem selectedItem = lvOrder.SelectedItems[0];

                // Giả sử cột đầu tiên là tên món ăn và cột thứ 2 là số lượng
                string selectedFoodName = selectedItem.SubItems[0].Text; // Tên món ăn
                string selectedQuantity = selectedItem.SubItems[1].Text; // Số lượng

                // Tìm món ăn trong ComboBox theo tên (hoặc bạn có thể tìm theo ID nếu có)
                for (int i = 0; i < cmbCategoryFood.Items.Count; i++)
                {
                    // Ép kiểu Item của ComboBox về dạng object bạn đã dùng để đổ dữ liệu
                    var item = (dynamic)cmbCategoryFood.Items[i];

                    // So sánh tên món ăn với dữ liệu từ ComboBox
                    if (item.name == selectedFoodName)
                    {
                        // Nếu tìm thấy, đặt món ăn này được chọn trong ComboBox
                        cmbCategoryFood.SelectedIndex = i;
                        break;
                    }               
                }

                // Cập nhật số lượng trong NumericUpDown (giả sử tên là nmCount)
                nmCount.Value = int.Parse(selectedQuantity);
            }
        }

        private void btnDiscount_Click(object sender, EventArgs e)
        {

            // Kiểm tra nếu hóa đơn hiện tại có tồn tại
            if (currentBill == null)
            {
                MessageBox.Show("Không có hóa đơn nào để áp dụng giảm giá.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy giá trị tổng tiền của hóa đơn (totalAmount là tổng tiền trước khi giảm giá)
            decimal originalTotalAmount = totalAmount;

            // Lấy giá trị phần trăm giảm giá từ NumericUpDown (giả sử tên là nmDiscount)
            decimal discountPercentage = nmDiscount.Value;

            // Tính toán số tiền được giảm
            decimal discountAmount = (discountPercentage / 100) * originalTotalAmount;

            // Tính tổng tiền cuối cùng sau khi giảm giá
            decimal finalTotalAmount = originalTotalAmount - discountAmount;

            // Cập nhật tổng tiền sau giảm giá vào TextBox tổng tiền (txtTotal)
            txtTotal.Text = finalTotalAmount.ToString("C", CultureInfo.CurrentCulture);

            // Hiển thị thông báo hoặc cập nhật thông tin
            MessageBox.Show($"Đã áp dụng giảm giá {discountPercentage}%. Số tiền phải trả: {finalTotalAmount:C}", "Giảm giá thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void flpTable_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblDisplayname_Click(object sender, EventArgs e)
        {
           
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có hóa đơn hiện tại không
                if (currentBill == null)
                {
                    MessageBox.Show("Không có hóa đơn nào để in.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Mở hộp thoại lưu file PDF
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF files (*.pdf)|*.pdf",
                    Title = "Lưu Hóa Đơn"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;
                    CreatePdfInvoice(filePath);
                    MessageBox.Show("Hóa đơn đã được lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Process.Start(filePath); // Mở file PDF vừa tạo
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreatePdfInvoice(string filePath)
        {
            Document document = new Document();
            PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
            document.Open();

            // Thêm tiêu đề
           
            document.Add(new Paragraph($"Customer: {UserSession.UserFullname}"));
            document.Add(new Paragraph($"Phone: {UserSession.UserPhone}"));
            document.Add(new Paragraph($"Email: {UserSession.UserEmail}"));
            document.Add(new Paragraph($"Date: {DateTime.Now.ToString("dd/MM/yyyy")}"));
            document.Add(new Paragraph("")); // Thêm một khoảng trống
            document.Add(new Paragraph("\n"));
            // Thêm danh sách mặt hàng
            PdfPTable table = new PdfPTable(3); // 3 cột: Tên món, Số lượng, Giá
            table.AddCell("Dish");
            table.AddCell("Quantities");
            table.AddCell("Price");



            string qrContent = $"Customer: {UserSession.UserFullname}\n" +
                      $"Phone: {UserSession.UserPhone}\n" +
                      $"Email: {UserSession.UserEmail}\n" +
                      $"Date: {DateTime.Now.ToString("dd/MM/yyyy")}\n" +
                      $"Total: {txtTotal.Text}";
            Bitmap qrCodeImage = GenerateQRCode(qrContent);
            using (MemoryStream ms = new MemoryStream())
            {
                qrCodeImage.Save(ms, ImageFormat.Png);
                iTextSharp.text.Image qrImage = iTextSharp.text.Image.GetInstance(ms.ToArray());
                qrImage.ScaleToFit(100f, 100f); // Thay đổi kích thước mã QR nếu cần
                document.Add(qrImage);
            }


            foreach (ListViewItem item in lvOrder.Items)
            {
                table.AddCell(item.SubItems[0].Text); // Tên món
                table.AddCell(item.SubItems[1].Text); // Số lượng
                table.AddCell(item.SubItems[2].Text); // Giá
            }

            document.Add(table);

            // Thêm tổng tiền
            document.Add(new Paragraph($"TOTAL: {txtTotal.Text} VND"));

            document.Close();
        }

       
        private Bitmap GenerateQRCode(string content)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
                QRCodeGen qrCode = new QRCodeGen(qrCodeData); // Sử dụng alias
                return qrCode.GetGraphic(20); // Kích thước mã QR
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void UpdateCustomerPoints(string customerId, decimal totalAmount)
        {
            // Tính toán số điểm tích lũy
            int pointsToAdd = (int)(totalAmount / 100000); // Ví dụ: 1 điểm cho mỗi 100.000 VNĐ

            // Lấy khách hàng từ cơ sở dữ liệu
            var customer = me.Customers.FirstOrDefault(c => c.CustomerGmail == customerId);
            if (customer != null)
            {
                customer.Points += pointsToAdd; // Cập nhật điểm tích lũy
                me.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
            }
        }

        private void contactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmContact contact = new frmContact();
            this.Hide();
            contact.ShowDialog();
            this.Show();
        }
    }
}



 

