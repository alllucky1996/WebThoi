namespace Entities.Enums
{
    /// <summary>
    /// Các hành động của chức năng
    /// </summary>
    public enum ActionEnum
    {
        Read = 1,
        Create = 2,
        Update = 3,
        Delete = 4,
        Verify = 5,
        Publish = 6,
    }
    public class ActionType
    {
        public const int Read = 1;
        public const int Create = 2;
        public const int Update = 3;
        public const int Delete = 4;
        public const int Verify = 5;
        public const int Publish = 6;
    }
    public enum PhamViDuLieuEnum
    {
        /// <summary>
        /// Quyền Quản lý văn bản tài liệu đề tài
        /// </summary>
        UpoadVanBanTaiLieu = 1,
        /// <summary>
        /// Quyền quản lý Lý lịch khoa học của thành viên
        /// </summary>
        LLKHThanhVien = 2,
        /// <summary>
        /// Quyền quản lý Tình hình giải ngân
        /// </summary>
        TinhHinhGiaiNgan = 3,
        /// <summary>
        /// Quyền quản lý Tiến độ thực hiện đề tài
        /// </summary>
        TienDoThucHien = 4,
        /// <summary>
        /// Quyền quản lý Danh sách thành viên tham gia đề tài
        /// </summary>
        DSThanhVien = 5,
        /// <summary>
        /// Quyền quản lý Kinh phí thực hiện
        /// </summary>
        KinhPhiThuHien = 6
    }
}
