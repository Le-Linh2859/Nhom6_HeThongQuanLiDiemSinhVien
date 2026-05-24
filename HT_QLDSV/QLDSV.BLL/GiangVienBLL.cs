using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLDSV.DAL;

namespace QLDSV.BLL
{
    public class GiangVienBLL
    {
        GiangVienDAL dal = new GiangVienDAL();
        public DataTable GetGiangVien()
        {
            return dal.LoadGiangVien();
        }
    }
}
