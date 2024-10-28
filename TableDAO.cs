using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TienSuToCoffee
{
    public class TableDAO : DbContext
    {
        // Sử dụng MYCOFFEEEntities làm DbContext
        MYCOFFEEEntitiesS context= DataContextSingleton.Instance;

        private static TableDAO instance;

        // Singleton pattern cho TableDAO
        public static TableDAO Instance
        {
            get { if (instance == null) instance = new TableDAO(); return instance; }
            private set { instance = value; }
        }

        // Kích thước mặc định của Table
        public static int TableWidth = 100;
        public static int TableHeight = 100;

      

        // Phương thức để tải danh sách bàn bằng Entity Framework
        public List<TableFood> LoadTableList()
        {
            // Truy vấn lấy tất cả các bàn từ bảng TableFoods trong cơ sở dữ liệu
            List<TableFood> tableList = context.TableFoods.ToList();
            return tableList;
        }
    }
}
