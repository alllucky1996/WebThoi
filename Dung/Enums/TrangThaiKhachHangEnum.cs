using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dung.Enums
{
    public enum TrangThaiKhachHangEnum
    {
        /// <summary>
        /// khách mới gửi thông tin chưa liên hệ
        /// </summary>
        ChuaLienHe = 0,
        /// <summary>
        /// khách hàng đã liên hệ mồi trài
        /// </summary>
        DaLienHe = 1,
        /// <summary>
        /// khách có tiềm năng 
        /// có thể chưa mua nhưng sẽ mua trong tương lai gần
        /// hoặc chưa gặp được
        /// </summary>
        KhachHangTiemNang = 2,
        /// <summary>
        /// khách hàng đã sắp lịch hẹn gặp 
        /// </summary>
        HenGap = 3,
        /// <summary>
        /// khách đã gặp và  tư vấn 
        /// </summary>
        DaGap = 5,
        /// <summary>
        /// khách hàng đã tư vấn thành công
        /// </summary>
        ThanhCong = 4
    }
}
