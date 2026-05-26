using QLDSV.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDSV.BLL
{
    public class CanhBaoHocVuBLL
    {
        CanhBaoHocVuDAL dal = new CanhBaoHocVuDAL();

        public DataTable GetCanhBao()
        {
            return dal.LoadCanhBao();
        }

        public DataTable GetCanhBaoSinhVien()
        {
            return dal.LoadCanhBaoSinhVien();
        }

        public DataTable GetNamHoc()
        {
            return dal.LoadNamHoc();
        }

        public DataTable GetHocKy()
        {
            return dal.LoadHocKy();
        }

        public DataTable GetLop()
        {
            return dal.LoadLop();
        }
    }
}
