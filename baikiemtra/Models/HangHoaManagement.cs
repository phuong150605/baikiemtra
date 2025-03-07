using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HangHoaManagement.Models
{
    [Table("Goods")]
    public class hang_hoa
    {
        [Key]
        [Required]
        [StringLength(9)]
        [Column("ma_hanghoa")]
        public string ma_hanghoa { get; set; }

        [Required]
        [Column("ten_hanghoa")]
        public string ten_hanghoa { get; set; }

        [Column("so_luong")]
        public int so_luong { get; set; }

        [Column("ghi_chu")]
        public string ghi_chu { get; set; }
    }
}
