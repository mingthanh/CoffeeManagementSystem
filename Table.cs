using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienSuToCoffee
{
    public class Table
    {
        [Key]
        [Column("id")] // Ánh xạ với cột "id" trong bảng
        public int Id { get; set; }

        // Thuộc tính 'Name' ánh xạ với cột "name" trong cơ sở dữ liệu
        [Column("name")]
        public string Name { get; set; }

        // Thuộc tính 'Status' ánh xạ với cột "status" trong cơ sở dữ liệu
        [Column("status")]
        public string Status { get; set; }

        // Constructor không tham số cần thiết cho Entity Framework
        public Table() { }

        // Constructor với tham số, nếu cần khởi tạo thủ công
        public Table(int id, string name, string status)
        {
            this.Id = id;
            this.Name = name;
            this.Status = status;
        }
    }
}
