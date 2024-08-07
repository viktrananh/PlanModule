using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO
{
    public class StatusDefine
    {
        public int StatusID { get; set; }
        public string StatusName { get; set; }
    }


    public static class LoadStatusDefineCustomer
    {
        public const int DELETE = -1;
        public const int CREATE = 0;
        public const int UPDATE = 1;
        public const int LOCK = 2;
        public const int LOAD_LIST_MODEL = 3;
        public const int CREATE_NEW_MODEL = 4;


        public static List<StatusDefine> statusDefineCustomers = new List<StatusDefine>();
        static LoadStatusDefineCustomer()
        {
            //statusDefineCustomers = new List<StatusDefineCustomer>();
            statusDefineCustomers.Add(new StatusDefine()
            {
                StatusID = DELETE,
                StatusName = "Xóa khách hàng"
            });
            statusDefineCustomers.Add(new StatusDefine()
            {
                StatusID = CREATE,
                StatusName = "Tạo mới khách hàng"
            });
            statusDefineCustomers.Add(new StatusDefine()
            {
                StatusID = UPDATE,
                StatusName = "Cập nhật thông tin khách hàng"
            });
            statusDefineCustomers.Add(new StatusDefine()
            {
                StatusID = LOCK,
                StatusName = "Khóa"
            });
            statusDefineCustomers.Add(new StatusDefine()
            {
                StatusID = LOAD_LIST_MODEL,
                StatusName = "Danh sách sản phẩm"
            });
            statusDefineCustomers.Add(new StatusDefine()
            {
                StatusID = CREATE_NEW_MODEL,
                StatusName = "Tạo sản phẩm mới"
            });
        }

    }
    /// <summary>
    /// Phiếu nhập linh kiện
    /// </summary>
    public static class LoadFunctionBillImportMaterial
    {
        public const int CANCEL = -1;
        public const int CREATE = 0;
        public const int UPDATE = 1;

        public static List<StatusDefine> statusBillImportMaterial = new List<StatusDefine>();
        static LoadFunctionBillImportMaterial()
        {
            //statusDefineCustomers = new List<StatusDefineCustomer>();
            statusBillImportMaterial.Add(new StatusDefine()
            {
                StatusID = CANCEL,
                StatusName = "Xóa Sản phẩm"
            });
            statusBillImportMaterial.Add(new StatusDefine()
            {
                StatusID = CREATE,
                StatusName = "Tạo mới phiếu"
            });
            statusBillImportMaterial.Add(new StatusDefine()
            {
                StatusID = UPDATE,
                StatusName = "Cập nhật thông tin Sản phẩm"
            });
           
        }

    }
    public static class LoadStatusBillImportMaterial
    {
        public const int KHOI_TAO = 0;
        public const int XAC_NHAN_CHO_NHAP = 1;
        public const int KHAI_BAO_DU_LIEU = 2;
        public const int NHAP_KHO = 3;
        public const int XAC_NHAN_NHAP_LOI = 4;
        public const int XAC_NHAN_THU_KHO = 5;
        public const int XAC_NHAN_HOAN_THANH = 6;

        public static List<StatusDefine> statusBillImportMaterial = new List<StatusDefine>();
        static LoadStatusBillImportMaterial()
        {
            //statusDefineCustomers = new List<StatusDefineCustomer>();
            statusBillImportMaterial.Add(new StatusDefine()
            {
                StatusID = KHOI_TAO,
                StatusName = "Khởi tạo phiếu"
            });
            statusBillImportMaterial.Add(new StatusDefine()
            {
                StatusID = XAC_NHAN_CHO_NHAP,
                StatusName = "Xác nhận phiếu"
            });
            statusBillImportMaterial.Add(new StatusDefine()
            {
                StatusID = KHAI_BAO_DU_LIEU,
                StatusName = "Khai báo dữ liệu khách hàng"
            });
            statusBillImportMaterial.Add(new StatusDefine()
            {
                StatusID = NHAP_KHO,
                StatusName = "Nhập kho"
            });
            statusBillImportMaterial.Add(new StatusDefine()
            {
                StatusID = XAC_NHAN_NHAP_LOI,
                StatusName = "Nhập kho chênh lệch"
            });
            statusBillImportMaterial.Add(new StatusDefine()
            {
                StatusID = XAC_NHAN_THU_KHO,
                StatusName = "Nhập kho lỗi thủ kho xác nhận"
            });
            statusBillImportMaterial.Add(new StatusDefine()
            {
                StatusID = XAC_NHAN_HOAN_THANH,
                StatusName = "Hoàn thành nhập kho"
            });
        }

    }
    /// <summary>
    /// Phiếu xuất thành phẩm
    /// </summary>
    public static class LoadFunctionBillExportGoodsToCus
    {
        public const int CANCEL = -1;
        public const int CREATE = 0;
        public const int UPDATE = 1;

        public static List<StatusDefine> StatusBillExportGoodToCus = new List<StatusDefine>();
        static LoadFunctionBillExportGoodsToCus()
        {
            //statusDefineCustomers = new List<StatusDefineCustomer>();
            StatusBillExportGoodToCus.Add(new StatusDefine()
            {
                StatusID = CANCEL,
                StatusName = "Hủy phiếu"
            });
            StatusBillExportGoodToCus.Add(new StatusDefine()
            {
                StatusID = CREATE,
                StatusName = "Tạo mới phiếu"
            });
            StatusBillExportGoodToCus.Add(new StatusDefine()
            {
                StatusID = UPDATE,
                StatusName = "Cập nhật phiếu"
            });

        }

    }

    public static class LoadStateBillExportGoodsToCus
    {
        public const int CANCEL = -1;
        public const int CREATE = 0;
        public const int APP = 1;
        public const int EXPORT = 2; 
        public const int COMPLETE = 3; 


        public static List<StatusDefine> StatusBillExportGoodToCus = new List<StatusDefine>();
        static LoadStateBillExportGoodsToCus()
        {
            //statusDefineCustomers = new List<StatusDefineCustomer>();
            StatusBillExportGoodToCus.Add(new StatusDefine()
            {
                StatusID = CANCEL,
                StatusName = "Hủy phiếu"
            });
            StatusBillExportGoodToCus.Add(new StatusDefine()
            {
                StatusID = CREATE,
                StatusName = "Khởi tạo"
            });
            StatusBillExportGoodToCus.Add(new StatusDefine()
            {
                StatusID = APP,
                StatusName = "Đã phê duyệt"
            });
            StatusBillExportGoodToCus.Add(new StatusDefine()
            {
                StatusID = EXPORT,
                StatusName = "Đang xuất hàng"
            });
            StatusBillExportGoodToCus.Add(new StatusDefine()
            {
                StatusID = COMPLETE,
                StatusName = "Hoàn thành phiếu"
            });
        }

    }

    public static class LoadStatusBillExportGoodToCus
    {
        public const int HUY_PHIEU = -1;
        public const int KHOI_TAO = 0;
        public const int XAC_NHAN_CHO_XUAT = 1;
        public const int XUAT_HANG = 2;
        public const int XAC_NHAN_HOAN_THANH = 3;

        public static List<StatusDefine> StatusBillExportGoodToCus = new List<StatusDefine>();
        static LoadStatusBillExportGoodToCus()
        {
            //statusDefineCustomers = new List<StatusDefineCustomer>();
            StatusBillExportGoodToCus.Add(new StatusDefine()
            {
                StatusID = KHOI_TAO,
                StatusName = "Khởi tạo phiếu"
            });
            StatusBillExportGoodToCus.Add(new StatusDefine()
            {
                StatusID = XAC_NHAN_CHO_XUAT,
                StatusName = "Xác nhận phiếu"
            });
            StatusBillExportGoodToCus.Add(new StatusDefine()
            {
                StatusID = XUAT_HANG,
                StatusName = "Xuất hàng"
            });
            StatusBillExportGoodToCus.Add(new StatusDefine()
            {
                StatusID = XAC_NHAN_HOAN_THANH,
                StatusName = "Hoàn thành phiếu"
            });
           
        }

    }
    /// <summary>
    /// Phiếu xuất linh kiện
    /// </summary>
    public static class LoadFunctionBillExportMaterial
    {
        public const int CANCEL = -1;
        public const int CREATE = 0;
        public const int VIEW = 1;
        public const int UPDATE = 2;
        public static List<StatusDefine> StatusBillExportMaterial = new List<StatusDefine>();
        static LoadFunctionBillExportMaterial()
        {
            //statusDefineCustomers = new List<StatusDefineCustomer>();
            StatusBillExportMaterial.Add(new StatusDefine()
            {
                StatusID = CANCEL,
                StatusName = "Hủy phiếu"
            });
            StatusBillExportMaterial.Add(new StatusDefine()
            {
                StatusID = CREATE,
                StatusName = "Tạo mới phiếu"
            });
            StatusBillExportMaterial.Add(new StatusDefine()
            {
                StatusID = VIEW,
                StatusName = "Xem thông tin phiếu"
            });
            StatusBillExportMaterial.Add(new StatusDefine()
            {
                StatusID = UPDATE,
                StatusName = "Cập nhật phiếu"
            });

        }
    }


    public static class LoadSubTypeBillExportMaterial
    {
        public const int BOM = 0;
        public const int ARISE = 1;
        public const int FORMING = 2;
        public const int FILE = 3;
        public const int AI = 4;

        public static List<StatusDefine> SubTypeBillExportMaterial = new List<StatusDefine>();
        static LoadSubTypeBillExportMaterial()
        {
            //statusDefineCustomers = new List<StatusDefineCustomer>();
            SubTypeBillExportMaterial.Add(new StatusDefine()
            {
                StatusID = BOM,
                StatusName = "Phiếu theo bom"
            });
            SubTypeBillExportMaterial.Add(new StatusDefine()
            {
                StatusID = ARISE,
                StatusName = "Phiếu phát sinh"
            });
            SubTypeBillExportMaterial.Add(new StatusDefine()
            {
                StatusID = FORMING,
                StatusName = "Phiếu gia công"
            });
            SubTypeBillExportMaterial.Add(new StatusDefine()
            {
                StatusID = FILE,
                StatusName = "Phiếu theo File"
            });
            SubTypeBillExportMaterial.Add(new StatusDefine()
            {
                StatusID = AI,
                StatusName = "Phiếu AI"
            });
        }
    }
    /// <summary>
    /// Model
    /// </summary>
    public static class LoadStatusDefineModel
    {
        public const int DELETE = -1;
        public const int CREATE = 0;
        public const int UPDATE = 1;
        public const int LOCK = 2;

        public static List<StatusDefine> statusDefineModel = new List<StatusDefine>();
        static LoadStatusDefineModel()
        {
            //statusDefineCustomers = new List<StatusDefineCustomer>();
            statusDefineModel.Add(new StatusDefine()
            {
                StatusID = DELETE,
                StatusName = "Xóa Sản phẩm"
            });
            statusDefineModel.Add(new StatusDefine()
            {
                StatusID = CREATE,
                StatusName = "Tạo mới sản phẩm"
            });
            statusDefineModel.Add(new StatusDefine()
            {
                StatusID = UPDATE,
                StatusName = "Cập nhật thông tin Sản phẩm"
            });
            statusDefineModel.Add(new StatusDefine()
            {
                StatusID = LOCK,
                StatusName = "Khóa sản phẩm"
            });
        }

    }


    public static class LoadActionWorkOrder
    {
        public const int CREAT_BLL_EXPORT_MATERIAL = 5;
        public const int BOOK_MATERIAL = 6;
        public const int KE_HOACH_GIAO_HANG = 7;
        public const int DU_LIEU_KHACH_HANG = 8;

        public static List<StatusDefine> ActionWorkOrders = new List<StatusDefine>();
        static LoadActionWorkOrder()
        {
            //statusDefineCustomers = new List<StatusDefineCustomer>();
            ActionWorkOrders.Add(new StatusDefine()
            {
                StatusID = CREAT_BLL_EXPORT_MATERIAL,
                StatusName = "Tạo phiếu xuất lienh kiện"
            });
            ActionWorkOrders.Add(new StatusDefine()
            {
                StatusID = BOOK_MATERIAL,
                StatusName = "Book linh kiện"
            });
            ActionWorkOrders.Add(new StatusDefine()
            {
                StatusID = KE_HOACH_GIAO_HANG,
                StatusName = "Kế hoạch giao hàng"
            });
            ActionWorkOrders.Add(new StatusDefine()
            {
                StatusID = DU_LIEU_KHACH_HANG,
                StatusName = "Dữ liệu khách hàng"
            });
        }

    }
}
