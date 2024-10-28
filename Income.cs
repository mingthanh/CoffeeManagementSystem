using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using System.Windows.Forms.DataVisualization.Charting;




namespace TienSuToCoffee
{
    public partial class Income : UserControl
    {
        private readonly MYCOFFEEEntitiesS me;
        public Income()
        {
            InitializeComponent();
            me = new MYCOFFEEEntitiesS();
            this.Load += Income_Load;
        }

        private void btnGmail_Click(object sender, EventArgs e)
        {
            frmGmail frmGmail = new frmGmail();
            this.Hide();
            frmGmail.ShowDialog();
            this.Show();
        }
        
        private void Income_Load(object sender, EventArgs e)
        {

            dateTimePicker1.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); // Tháng hiện tại, ngày 1
            dateTimePicker2.Value = dateTimePicker1.Value.AddMonths(1).AddDays(-1); // Tháng hiện tại, ngày cuối cùng

            // Hiển thị biểu đồ với khoảng thời gian mặc định
            DisplayRevenueChart(dateTimePicker1.Value, dateTimePicker2.Value);
            //dateTimePicker1.Value = DateTime.Now;
            //dateTimePicker2.Value = DateTime.Now;
            LoadBillData();


        }

        private void dgvBill_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime DateCheckIn = DateTime.Now; // Giả sử giá trị DateCheckIn đã có
            dateTimePicker1.Value = DateCheckIn; // Gán thời gian vào dateTimePicker1
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            DateTime DateCheckOut = DateTime.Now.AddMonths(1); // Giả sử giá trị DateCheckOut đã có
            dateTimePicker2.Value = DateCheckOut; // Gán thời gian vào dateTimePicker2
        }

        private void LoadBillData()
        {
            try
            {
                // Truy vấn lấy dữ liệu từ bảng BILL và TableFood
                var billData = (from bill in me.BILLs
                                join table in me.TableFoods
                                on bill.idTable equals table.id
                                select new
                                {
                                    BillID = bill.id,
                                    DateCheckIn = bill.DateCheckIn,
                                    DateCheckOut = bill.DateCheckOut,
                                    TableName = table.name, // Giả sử bảng TableFood có cột "name"
                                    Status = bill.status == 1 ? "Đã thanh toán" : "Chưa thanh toán"
                                }).ToList();

                dgvBill.DataSource = billData;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi tải dữ liệu hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
        public class MonthlyRevenue
        {
            public int Month { get; set; }          // Tháng (1-12)
            public string MonthName { get; set; }  // Tên tháng (Ví dụ: Tháng 1, Tháng 2,...)
            public double? TotalRevenue { get; set; } // Tổng doanh thu trong tháng đó
        }
        public List<MonthlyRevenue> GetMonthlyRevenue(DateTime startDate, DateTime endDate)
        {
            try
            {
                var query = me.BillInfoes
                    .Include(bi => bi.BILL)
                    .Include(bi => bi.Food)
                    .Where(bi => bi.BILL != null
                                 && bi.Food != null
                                 && bi.BILL.DateCheckIn.HasValue
                                 && bi.BILL.DateCheckIn.Value.Month >= startDate.Month
                                 && bi.BILL.DateCheckIn.Value.Month <= endDate.Month)
                    .GroupBy(bi => new { bi.BILL.DateCheckIn.Value.Year, bi.BILL.DateCheckIn.Value.Month })
                    .Select(g => new MonthlyRevenue
                    {
                        Month = g.Key.Month,
                        MonthName = "Tháng " + g.Key.Month,
                        TotalRevenue = g.Sum(bi => bi.Food.price * (bi.count ?? 0))
                    })
                    .OrderBy(mr => mr.Month)
                    .ToList();

                return query;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi tính toán doanh thu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<MonthlyRevenue>();
            }
        }

        public void SetupChart()
        {
            // Cấu hình trục X
            ChartArea chartArea = chartRevenue.ChartAreas[0];
            chartArea.AxisX.Minimum = 1;
            chartArea.AxisX.Maximum = 12;
            chartArea.AxisX.Interval = 1;
            chartArea.AxisX.Title = "Tháng";

            // Cấu hình trục Y
            chartArea.AxisY.Title = "Doanh Thu (VNĐ)";
            chartArea.AxisY.LabelStyle.Format = "N0"; // Hiển thị số không có chữ số thập phân

            // Đặt tiêu đề cho biểu đồ
            chartRevenue.Titles.Clear();
            chartRevenue.Titles.Add("Thống Kê Doanh Thu Theo Tháng");
        }

        public void DisplayRevenueChart(DateTime startDate, DateTime endDate)
        {
            try
            {
                // Lấy doanh thu theo tháng trong khoảng thời gian đã cho
                List<MonthlyRevenue> monthlyRevenues = GetMonthlyRevenue(startDate, endDate);

                // Cấu hình biểu đồ
                SetupChart();

                // Nếu không có dữ liệu thì không cần xử lý
                if (monthlyRevenues.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Nếu đã có series trong biểu đồ, chỉ cần cập nhật dữ liệu
                if (chartRevenue.Series.Count > 0)
                {
                    Series existingSeries = chartRevenue.Series[0];
                    foreach (var revenue in monthlyRevenues)
                    {
                        // Kiểm tra xem điểm đã có trong series chưa
                        bool pointExists = existingSeries.Points.Any(p => p.XValue == revenue.Month);
                        if (pointExists)
                        {
                            // Nếu có rồi, chỉ cần cập nhật giá trị doanh thu
                            existingSeries.Points.First(p => p.XValue == revenue.Month).YValues[0] += revenue.TotalRevenue ?? 0;
                        }
                        else
                        {
                            // Nếu chưa có, thêm điểm mới vào series
                            existingSeries.Points.AddXY(revenue.Month, revenue.TotalRevenue);
                        }
                    }
                }
                else
                {
                    // Nếu chưa có series nào, tạo series mới
                    Series series = new Series("Doanh Thu")
                    {
                        ChartType = SeriesChartType.Column,
                        Color = System.Drawing.Color.Green
                    };

                    // Thêm dữ liệu vào Series
                    foreach (var revenue in monthlyRevenues)
                    {
                        series.Points.AddXY(revenue.Month, revenue.TotalRevenue);
                    }

                    // Thêm Series vào Chart
                    chartRevenue.Series.Add(series);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi hiển thị biểu đồ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //try
            //{
            //    List<MonthlyRevenue> monthlyRevenues = GetMonthlyRevenue(startDate, endDate);

            //    // Cấu hình biểu đồ
            //    SetupChart();

            //    // Xóa các Series cũ nếu có
            //    chartRevenue.Series.Clear();

            //    // Tạo Series mới
            //    Series series = new Series("Doanh Thu")
            //    {
            //      ChartType = SeriesChartType.Column, 
            //      Color = System.Drawing.Color.Green
            //    };

            //    // Thêm dữ liệu vào Series
            //    foreach (var revenue in monthlyRevenues)
            //    {
            //        series.Points.AddXY(revenue.Month, revenue.TotalRevenue);
            //    }

            //    // Thêm Series vào Chart
            //    chartRevenue.Series.Add(series);

            //    // Tùy chỉnh thêm nếu cần
            //    chartRevenue.ChartAreas[0].AxisX.Title = "Tháng";
            //    chartRevenue.ChartAreas[0].AxisY.Title = "Doanh Thu (VNĐ)";
            //    chartRevenue.Titles.Clear();
            //    chartRevenue.Titles.Add("Thống Kê Doanh Thu Theo Tháng");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Đã xảy ra lỗi khi hiển thị biểu đồ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

      

        private void btnShowRevenue_Click_1(object sender, EventArgs e)
        {
            DateTime startDate = dateTimePicker1.Value.Date;
            DateTime endDate = dateTimePicker2.Value.Date;

            if (startDate > endDate)
            {
                MessageBox.Show("Ngày bắt đầu không thể sau ngày kết thúc.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DisplayRevenueChart(startDate, endDate);
        }

        private void chartRevenue_Click(object sender, EventArgs e)
        {

        }

        /*private List<MonthlyRevenue> totalMonthlyRevenues = new List<MonthlyRevenue>();

        public void DisplayRevenueChart1(DateTime startDate, DateTime endDate)
        {
            try
            {
                // Lấy doanh thu theo tháng trong khoảng thời gian đã cho
                List<MonthlyRevenue> monthlyRevenues = GetMonthlyRevenue(startDate, endDate);

                // Cập nhật danh sách doanh thu tổng
                foreach (var revenue in monthlyRevenues)
                {
                    var existingRevenue = totalMonthlyRevenues.FirstOrDefault(r => r.Month == revenue.Month);
                    if (existingRevenue != null)
                    {
                        existingRevenue.TotalRevenue += revenue.TotalRevenue ?? 0;
                    }
                    else
                    {
                        totalMonthlyRevenues.Add(revenue);
                    }
                }

                // Cấu hình biểu đồ
                SetupChart();

                // Xóa các Series cũ nếu có
                chartRevenue.Series.Clear();

                // Tạo Series mới
                Series series = new Series("Doanh Thu")
                {
                    ChartType = SeriesChartType.Column,
                    Color = System.Drawing.Color.Green
                };

                // Thêm dữ liệu vào Series từ danh sách tổng doanh thu
                foreach (var revenue in totalMonthlyRevenues)
                {
                    series.Points.AddXY(revenue.Month, revenue.TotalRevenue);
                }

                // Thêm Series vào Chart
                chartRevenue.Series.Add(series);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi hiển thị biểu đồ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void UpdateRevenueAfterBillPayment(DateTime billDate, double amount)
        {
            int month = billDate.Month;
            var existingRevenue = totalMonthlyRevenues.FirstOrDefault(r => r.Month == month);
            if (existingRevenue != null)
            {
                existingRevenue.TotalRevenue += amount;
            }
            else
            {
                totalMonthlyRevenues.Add(new MonthlyRevenue
                {
                    Month = month,
                    MonthName = "Tháng " + month,
                    TotalRevenue = amount
                });
            }

            // Cập nhật biểu đồ
            DisplayRevenueChart1(dateTimePicker1.Value, dateTimePicker2.Value);
        }
        */
      

    }
}
