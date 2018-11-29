using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public partial class MenuItem
    {
       public static MenuItem Acount
         {
             get{
                 return new MenuItem
                 {
                     Title = "Quản lý tài khoản",
                     MenuId = 500,
                     Group = MenuGroup.QuanTriHeThong,
                     Icon = "fa fa-book",
                     OrdinalNumber = 1,
                     Actions = new List<ActionEnum>
                     {
                        ActionEnum.Create, ActionEnum.Delete, ActionEnum.Read, ActionEnum.Update 
                     }

                 };
             }
         }

        public static MenuItem Role
         {
             get{
                 return new MenuItem
                 {
                     Title="Quản lý vai trò người dùng (Nhóm người dùng)",
                     MenuId=501,
                     Group=MenuGroup.QuanTriHeThong,
                     Icon="fa fa-book",
                     OrdinalNumber=1,
                     Actions=new List<ActionEnum>
                     {
                        ActionEnum.Create, ActionEnum.Delete, ActionEnum.Read, ActionEnum.Update 
                     }
      
                 };
             }
         }

        public static MenuItem  SystemInformation 
         {
             get{
                 return new MenuItem
                 {
                     Title="Tham số hệ thống",
                     MenuId=502,
                     Group=MenuGroup.QuanTriHeThong,
                     Icon="fa fa-book",
                     OrdinalNumber=1,
                     Actions=new List<ActionEnum>
                     {
                        ActionEnum.Create, ActionEnum.Delete, ActionEnum.Read, ActionEnum.Update 
                     }
                 };
             }
         }

        public static MenuItem  UngDung 
         {
             get{
                 return new MenuItem
                 {
                     Title="Quản trị ứng dụng",
                     MenuId=503,
                     Group=MenuGroup.QuanTriHeThong,
                     Icon="fa fa-book",
                     OrdinalNumber=1,
                     Actions=new List<ActionEnum>
                     {
                        ActionEnum.Create, ActionEnum.Delete, ActionEnum.Read, ActionEnum.Update 
                     }
                 };
             }
         }
        public static MenuItem  MucTin 
         {
             get{
                 return new MenuItem
                 {
                     Title="Quản trị mục tin",
                     MenuId=100,
                     Group=MenuGroup.QuanTriTinTuc,
                     Icon="fa fa-book",
                     OrdinalNumber=1,
                     Actions=new List<ActionEnum>
                     {
                        ActionEnum.Create, ActionEnum.Delete, ActionEnum.Read, ActionEnum.Update 
                     }
                 };
             }
         }
        public static MenuItem  BaiViet 
         {
             get{
                 return new MenuItem
                 {
                     Title="Quản trị bài viết",
                     MenuId=101,
                     Group=MenuGroup.QuanTriTinTuc,
                     Icon="fa fa-book",
                     OrdinalNumber=1,
                     Actions=new List<ActionEnum>
                     {
                        ActionEnum.Create, ActionEnum.Delete, ActionEnum.Read, ActionEnum.Update 
                     }
                 };
             }
         }
        public static MenuItem  LoaiMucTin 
         {
             get{
                 return new MenuItem
                 {
                     Title="Loại mục tin",
                     MenuId=600,
                     Group=MenuGroup.QuanTriDanhMuc,
                     Icon="fa fa-book",
                     OrdinalNumber=1,
                     Actions=new List<ActionEnum>
                     {
                        ActionEnum.Create, ActionEnum.Delete, ActionEnum.Read, ActionEnum.Update 
                     }
                 };
             }
         }

        public static MenuItem  TrangThaiBaiViet 
         {
             get{
                 return new MenuItem
                 {
                     Title="Trạng thái bài viết",
                     MenuId=601,
                     Group=MenuGroup.QuanTriDanhMuc,
                     Icon="fa fa-book",
                     OrdinalNumber=1,
                     Actions=new List<ActionEnum>
                     {
                        ActionEnum.Create, ActionEnum.Delete, ActionEnum.Read, ActionEnum.Update 
                     }
                 };
             }
         }

        public static MenuItem  ThaoTacChuyenTrangThaiBaiViet 
         {
             get{
                 return new MenuItem
                 {
                     Title="Chuyển trạng thái bài viết",
                     MenuId=602,
                     Group=MenuGroup.QuanTriDanhMuc,
                     Icon="fa fa-book",
                     OrdinalNumber=1,
                     Actions=new List<ActionEnum>
                     {
                        ActionEnum.Create, ActionEnum.Delete, ActionEnum.Read, ActionEnum.Update 
                     }
                 };
             }
         }
        public static MenuItem  HocHam 
         {
             get{
                 return new MenuItem
                 {
                     Title="Học hàm",
                     MenuId=603,
                     Group=MenuGroup.QuanTriDanhMuc,
                     Icon="fa fa-book",
                     OrdinalNumber=1,
                     Actions=new List<ActionEnum>
                     {
                        ActionEnum.Create, ActionEnum.Delete, ActionEnum.Read, ActionEnum.Update 
                     }
                 };
             }
         }
        public static MenuItem  HocVi 
         {
             get{
                 return new MenuItem
                 {
                     Title="Học vị",
                     MenuId=604,
                     Group=MenuGroup.QuanTriDanhMuc,
                     Icon="fa fa-book",
                     OrdinalNumber=1,
                     Actions=new List<ActionEnum>
                     {
                        ActionEnum.Create, ActionEnum.Delete, ActionEnum.Read, ActionEnum.Update 
                     }
                 };
             }
         }

        public static MenuItem  CapPhongThiNghiem 
         {
             get{
                 return new MenuItem
                 {
                     Title="Cấp phòng thí nghiệm",
                     MenuId=605,
                     Group=MenuGroup.QuanTriDanhMuc,
                     Icon="fa fa-book",
                     OrdinalNumber=1,
                     Actions=new List<ActionEnum>
                     {
                        ActionEnum.Create, ActionEnum.Delete, ActionEnum.Read, ActionEnum.Update 
                     }
                 };
             }
         }

        public static MenuItem  ChuyenNganh 
         {
             get{
                 return new MenuItem
                 {
                     Title="Chuyên ngành",
                     MenuId=606,
                     Group=MenuGroup.QuanTriDanhMuc,
                     Icon="fa fa-book",
                     OrdinalNumber=1,
                     Actions=new List<ActionEnum>
                     {
                        ActionEnum.Create, ActionEnum.Delete, ActionEnum.Read, ActionEnum.Update 
                     }
                 };
             }
         }

        public static MenuItem  Nganh 
         {
             get{
                 return new MenuItem
                 {
                     Title="Ngành",
                     MenuId=607,
                     Group=MenuGroup.QuanTriDanhMuc,
                     Icon="fa fa-book",
                     OrdinalNumber=1,
                     Actions=new List<ActionEnum>
                     {
                        ActionEnum.Create, ActionEnum.Delete, ActionEnum.Read, ActionEnum.Update 
                     }
                 };
             }
         }

        public static MenuItem  TrangThaiDeXuat 
         {
             get{
                 return new MenuItem
                 {
                     Title="Trạng thái đề xuất",
                     MenuId=608,
                     Group=MenuGroup.QuanTriDanhMuc,
                     Icon="fa fa-book",
                     OrdinalNumber=1,
                     Actions=new List<ActionEnum>
                     {
                        ActionEnum.Create, ActionEnum.Delete, ActionEnum.Read, ActionEnum.Update 
                     }
                 };
             }
         }

        public static MenuItem  TrangThaiDeTai 
         {
             get{
                 return new MenuItem
                 {
                     Title="Trạng thái đề tài",
                     MenuId=609,
                     Group=MenuGroup.QuanTriDanhMuc,
                     Icon="fa fa-book",
                     OrdinalNumber=1,
                     Actions=new List<ActionEnum>
                     {
                        ActionEnum.Create, ActionEnum.Delete, ActionEnum.Read, ActionEnum.Update 
                     }
                 };
             }
         }

        public static MenuItem  TrangThaiDADT
         {
             get{
                 return new MenuItem
                 {
                     Title="Trạng thái dự án đầu tư",
                     MenuId=610,
                     Group=MenuGroup.QuanTriDanhMuc,
                     Icon="fa fa-book",
                     OrdinalNumber=1,
                     Actions=new List<ActionEnum>
                     {
                        ActionEnum.Create, ActionEnum.Delete, ActionEnum.Read, ActionEnum.Update 
                     }
                 };
             }
         }
        public static MenuItem  TrangThaiHangMucDADT
         {
             get{
                 return new MenuItem
                 {
                     Title="Trạng thái hạng mục dự án đầu tư",
                     MenuId=611,
                     Group=MenuGroup.QuanTriDanhMuc,
                     Icon="fa fa-book",
                     OrdinalNumber=1,
                     Actions=new List<ActionEnum>
                     {
                        ActionEnum.Create, ActionEnum.Delete, ActionEnum.Read, ActionEnum.Update 
                     }
                 };
             }
         }

        public static MenuItem  TrangThaiGiaiThuong
         {
             get{
                 return new MenuItem
                 {
                     Title="Trạng thái hạng mục dự án đầu tư",
                     MenuId=612,
                     Group=MenuGroup.QuanTriDanhMuc,
                     Icon="fa fa-book",
                     OrdinalNumber=1,
                     Actions=new List<ActionEnum>
                     {
                        ActionEnum.Create, ActionEnum.Delete, ActionEnum.Read, ActionEnum.Update 
                     }
                 };
             }
         }
        public static MenuItem  TrangThaiCongViec
         {
             get{
                 return new MenuItem
                 {
                     Title="Trạng thái công việc",
                     MenuId=613,
                     Group=MenuGroup.QuanTriDanhMuc,
                     Icon="fa fa-book",
                     OrdinalNumber=1,
                     Actions=new List<ActionEnum>
                     {
                        ActionEnum.Create, ActionEnum.Delete, ActionEnum.Read, ActionEnum.Update 
                     }
                 };
             }
         }

        public static MenuItem  ThaoTacChuyenTrangThaiDeXuat
         {
             get{
                 return new MenuItem
                 {
                     Title="Chuyển trạng thái đề xuất",
                     MenuId=614,
                     Group=MenuGroup.QuanTriDanhMuc,
                     Icon="fa fa-book",
                     OrdinalNumber=1,
                     Actions=new List<ActionEnum>
                     {
                        ActionEnum.Create, ActionEnum.Delete, ActionEnum.Read, ActionEnum.Update 
                     }
                 };
             }
         }
        public static MenuItem  ThaoTacChuyenTrangThaiDeTai
         {
             get{
                 return new MenuItem
                 {
                     Title="Chuyển trạng thái đề tài",
                     MenuId=615,
                     Group=MenuGroup.QuanTriDanhMuc,
                     Icon="fa fa-book",
                     OrdinalNumber=1,
                     Actions=new List<ActionEnum>
                     {
                        ActionEnum.Create, ActionEnum.Delete, ActionEnum.Read, ActionEnum.Update 
                     }
                 };
             }
         }
        public static MenuItem  ThaoTacChuyenTrangThaiCongViec
         {
             get{
                 return new MenuItem
                 {
                     Title="Chuyển trạng thái công việc",
                     MenuId=616,
                     Group=MenuGroup.QuanTriDanhMuc,
                     Icon="fa fa-book",
                     OrdinalNumber=1,
                     Actions=new List<ActionEnum>
                     {
                        ActionEnum.Create, ActionEnum.Delete, ActionEnum.Read, ActionEnum.Update 
                     }
                 };
             }
         }
        public static MenuItem  ThaoTacChuyenTrangHoatDong
         {
             get{
                 return new MenuItem
                 {
                     Title="Chuyển trạng thái hoạt động",
                     MenuId=617,
                     Group=MenuGroup.QuanTriDanhMuc,
                     Icon="fa fa-book",
                     OrdinalNumber=1,
                     Actions=new List<ActionEnum>
                     {
                        ActionEnum.Create, ActionEnum.Delete, ActionEnum.Read, ActionEnum.Update 
                     }
                 };
             }
         }

        public static MenuItem  ThaoTacChuyenTrangDADT
         {
             get{
                 return new MenuItem
                 {
                     Title="Chuyển trạng thái dự án đầu tư",
                     MenuId=618,
                     Group=MenuGroup.QuanTriDanhMuc,
                     Icon="fa fa-book",
                     OrdinalNumber=1,
                     Actions=new List<ActionEnum>
                     {
                        ActionEnum.Create, ActionEnum.Delete, ActionEnum.Read, ActionEnum.Update 
                     }
                 };
             }
         }

        public static MenuItem  ThaoTacChuyenTrangHangMucDADT
         {
             get{
                 return new MenuItem
                 {
                     Title="Chuyển trạng thái hạng mục DADT",
                     MenuId=619,
                     Group=MenuGroup.QuanTriDanhMuc,
                     Icon="fa fa-book",
                     OrdinalNumber=1,
                     Actions=new List<ActionEnum>
                     {
                        ActionEnum.Create, ActionEnum.Delete, ActionEnum.Read, ActionEnum.Update 
                     }
                 };
             }
         }
        public static MenuItem  DanhMucLoaiVanBan
         {
             get{
                 return new MenuItem
                 {
                     Title="Danh mục loại văn bản",
                     MenuId=620,
                     Group=MenuGroup.QuanTriDanhMuc,
                     Icon="fa fa-book",
                     OrdinalNumber=1,
                     Actions=new List<ActionEnum>
                     {
                        ActionEnum.Create, ActionEnum.Delete, ActionEnum.Read, ActionEnum.Update 
                     }
                 };
             }
         }

        public static MenuItem  ThaoTacChuyenTrangGiaiThuong
         {
             get{
                 return new MenuItem
                 {
                     Title="Chuyển trạng thái giải thưởng",
                     MenuId=621,
                     Group=MenuGroup.QuanTriDanhMuc,
                     Icon="fa fa-book",
                     OrdinalNumber=1,
                     Actions=new List<ActionEnum>
                     {
                        ActionEnum.Create, ActionEnum.Delete, ActionEnum.Read, ActionEnum.Update 
                     }
                 };
             }
         }

        public static MenuItem DonViKHCN
        {
            get
            {
                return new MenuItem
                {
                    Title = "Đơn vị KHCN",
                    MenuId = 800,
                    Group = MenuGroup.QuanTriDanhMuc,
                    Icon = "fa fa-book",
                    OrdinalNumber = 1,
                    Actions = new List<ActionEnum>
                     {
                        ActionEnum.Create, ActionEnum.Delete, ActionEnum.Read, ActionEnum.Update 
                     }
                };
            }
        }
    }
}
