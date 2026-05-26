namespace QLDSV.GUI
{
    /// <summary>
    /// Interface dành cho các Form con được nhúng vào Shell (frmMain).
    /// Giúp ẩn đi các thanh Sidebar và Header trùng lặp nội bộ một cách an toàn thông qua OOP.
    /// </summary>
    public interface IShellChildForm
    {
        /// <summary>
        /// Được gọi bởi Shell khi nhúng Form con vào panel.
        /// Thực hiện ẩn Sidebar/Header tĩnh trùng lặp và kích hoạt dịch chuyển responsive.
        /// </summary>
        void OnEmbeddedInShell();
    }
}
