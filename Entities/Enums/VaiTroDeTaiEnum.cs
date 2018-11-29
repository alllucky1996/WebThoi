using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Enums
{
    /// <summary>
    /// Định nghĩa các giá trị của mã vai trò tương ứng
    /// </summary>
    public static class VaiTroDeTaiEnum
    {
        /// <summary>
        /// Vai trò chủ trì đề tài
        /// </summary>
        public static string ChuTri
        {
            get { return "VT_CT"; }
        }

        /// <summary>
        /// Thành viên đề tài
        /// </summary>
        public static string ThanhVien
        {
            get { return "VT_TV"; }
        }

        /// <summary>
        /// Thư ký đề tài
        /// </summary>
        public static string ThuKy
        {
            get { return "VT_TK"; }
        }
        /// <summary>
        /// Đồng chủ trì đề tài
        /// </summary>
        public static string DongChuTri
        {
            get { return "VT_DCT"; }
        }


    }
}
