using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public partial class MenuGroup
    {
        public static MenuGroup QuanTriTinTuc
         {
             get{
                 return new MenuGroup
                 {
                     Title = "Quản trị tin tức",
                     GroupId = 1,
                     Ordinal = 1,
                     Icon = "fa  fa-list-ul",
                     Color = ""
                 };
             }
         }
        public static MenuGroup QuanTriHeThong
         {
             get{
                 return new MenuGroup
                 {
                     Title = "Quản trị hệ thống",
                     GroupId = 5,
                     Ordinal = 2,
                     Icon = "fa  fa-list-ul",
                     Color = ""
                 };
             }
         }
        public static MenuGroup QuanTriDanhMuc
         {
             get{
                 return new MenuGroup
                 {
                     Title="Quản trị hệ thống",
                     GroupId=6,
                     Ordinal=2,
                     Icon="fa  fa-list-ul",
                     Color=""       
                 };
             }
         }
        
        public static MenuGroup QuanLyToChucKHCN
         {
             get{
                 return new MenuGroup
                 {
                     Title="Quản lý tổ chức KHCN",
                     GroupId=8,
                     Ordinal=2,
                     Icon="fa  fa-list-ul",
                     Color=""       
                 };
             }
         }
         public static MenuGroup DuAnDauTu
         {
             get{
                 return new MenuGroup
                 {
                     Title="Quản lý tổ chức KHCN",
                     GroupId=9,
                     Ordinal=2,
                     Icon="fa  fa-list-ul",
                     Color=""       
                 };
             }
         }
        public static MenuGroup SanPhamKHCN
         {
             get{
                 return new MenuGroup
                 {
                     Title="Sản phẩm KHCN",
                     GroupId=10,
                     Ordinal=2,
                     Icon="fa  fa-list-ul",
                     Color=""       
                 };
             }
         }
        public static MenuGroup DeXuatDeTai
         {
             get{
                 return new MenuGroup
                 {
                     Title="Đề xuất đề tài",
                     GroupId=11,
                     Ordinal=2,
                     Icon="fa  fa-list-ul",
                     Color=""       
                 };
             }
         }
        public static MenuGroup DeTaiKHCN
         {
             get{
                 return new MenuGroup
                 {
                     Title="Đề tài KHCN",
                     GroupId=12,
                     Ordinal=2,
                     Icon="fa  fa-list-ul",
                     Color=""       
                 };
             }
         }
        public static MenuGroup QuanLyCongViec
         {
             get{
                 return new MenuGroup
                 {
                     Title="Quản lý công việc",
                     GroupId=13,
                     Ordinal=2,
                     Icon="fa  fa-list-ul",
                     Color=""       
                 };
             }
         }
        public static MenuGroup GiaiThuong
         {
             get{
                 return new MenuGroup
                 {
                     Title="Giải thưởng",
                     GroupId=14,
                     Ordinal=2,
                     Icon="fa  fa-list-ul",
                     Color=""       
                 };
             }
         }
        public static MenuGroup HoiNghiHoiThao
         {
             get{
                 return new MenuGroup
                 {
                     Title="Hội nghị hội thảo",
                     GroupId=15,
                     Ordinal=2,
                     Icon="fa  fa-list-ul",
                     Color=""       
                 };
             }
         }
        public static MenuGroup VanBan
         {
             get{
                 return new MenuGroup
                 {
                     Title="Văn bản biểu mẫu",
                     GroupId=99,
                     Ordinal=2,
                     Icon="fa  fa-list-ul",
                     Color=""       
                 };
             }
         }
        public static MenuGroup InAnThongKe
         {
             get{
                 return new MenuGroup
                 {
                     Title="In ấn thống kê",
                     GroupId=7,
                     Ordinal=2,
                     Icon="fa  fa-list-ul",
                     Color=""       
                 };
             }
         }
        public static List<MenuGroup> ListGroup()
        {
            var result = new List<MenuGroup>();
            result.Add(QuanTriTinTuc);
            result.Add(QuanTriHeThong);
            result.Add(QuanTriDanhMuc);
            result.Add(QuanLyToChucKHCN);
            result.Add(DuAnDauTu);
            result.Add(SanPhamKHCN);
            result.Add(DeXuatDeTai);
            result.Add(DeTaiKHCN);
            result.Add(QuanLyCongViec);
            result.Add(GiaiThuong);
            result.Add(HoiNghiHoiThao);
            result.Add(InAnThongKe);
            return result;
        }
    }
}
