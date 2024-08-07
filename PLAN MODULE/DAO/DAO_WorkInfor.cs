using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace PLAN_MODULE.DAO
{
    class DAO_WorkInfor:BaseDAO
    {

        ROSE_Dll.DAO.BomDao BomDao = new ROSE_Dll.DAO.BomDao();
        public bool checkenoughwork(DTO.WorkOrder workOrder,int pcbOnpanel, int type)
        {
            int current_qty = 0;
            DataTable dt = mySql.GetDataMySQL($"SELECT CURRENT_QTY FROM TRACKING_SYSTEM.WORK_ID_TRACER where WORK_ID='{workOrder.WorkID}' and TEM='{type}';");
            if (!istableNull(dt))
                current_qty = int.Parse(dt.Rows[0][0].ToString());
            if(type==0)//pcb
            {
                if (current_qty >= workOrder.totalPcs) return false;
            }    else//panel
            {
                if (current_qty * pcbOnpanel >= workOrder.totalPcs)
                    return false;
            }    
            
            return true;
        }
        public int getPartExported(string work_id, string part_number)
        {
            string cmd = $"SELECT sum(qty) FROM STORE_MATERIAL_DB.BILL_EXPORT_WH where WORK_ID='{work_id}' and part_number='{part_number}';";
            DataTable dt = mySql.GetDataMySQL(cmd);
            if (istableNull(dt)) return 0;
            return int.Parse(dt.Rows[0][0].ToString());
        }
        public List<DTO.Part_export> getPartExported(string work_id)
        {
            List<DTO.Part_export> part_Exports = new List<DTO.Part_export>();
            string cmd = $"SELECT part_number,sum(qty) qty FROM STORE_MATERIAL_DB.BILL_EXPORT_WH where WORK_ID='{work_id}' group by part_number;";
            DataTable dt = mySql.GetDataMySQL(cmd);
            if (istableNull(dt)) return part_Exports;
            part_Exports = (from a in dt.AsEnumerable()
                            select new DTO.Part_export
                            {
                                part = a.Field<string>("part_number"),
                                qty = a.Field<int>("qty"),
                                qty_remain = a.Field<int>("qty")
                            }).ToList();
            return part_Exports;
        }
        public bool bookMaterialForWork(List<DTO.work_remain_Part> workbook,List<ROSE_Dll.DTO.BomContent> bomContent, List<DTO.work_remain_Part> referenceList, string userID)
        {
            string cmd = "";
            List<DTO.work_remain_Part> listMissed = workbook.Where(a => a.qtyMainExport < a.qtyMainrequest).ToList();
            foreach (var item in referenceList)
            {
                int lastIndex = -1;
                int soluongma =int.Parse( item.qtyPartRemain.ToString());
                for (int i = 0; i < listMissed.Count; i++)
                {                   
                    if (listMissed[i].qtyMainrequest <= listMissed[i].qtyMainExport)
                        continue;
                    List<string> subPart = bomContent.Where(a => a.MainPart == listMissed[i].main_part).Select(a => a.InterPart).ToList();
                    if (!subPart.Contains(item.PART_NUMBER)) continue;
                    lastIndex = i;
                    int addNum = listMissed[i].qtyMainrequest - listMissed[i].qtyMainExport;
                    if (soluongma <= addNum)
                    {
                        cmd += $"update STORE_MATERIAL_DB.WORK_REMAIN_MAINPART  set QTY_EXPORT=qty_export+'{soluongma}' where work_id='{listMissed[i].WORK_ID}' and main_part='{listMissed[i].main_part}';";
                        cmd += $"INSERT INTO `STORE_MATERIAL_DB`.`WORK_REMAIN_PART` (`WORK_ID`, `MAIN_PART`, `PART_NUMBER`, `QTY_EXPORT`) VALUES ('{listMissed[i].WORK_ID}'," +
                            $" '{listMissed[i].main_part}', '{item.PART_NUMBER}', '{soluongma}') on duplicate key update QTY_EXPORT=qty_export+'{soluongma}' ;";
                        cmd += $"INSERT INTO `STORE_MATERIAL_DB`.`WORK_BOOK_PART` (`WORK_ID`, `WORK_BOOK`, `MAIN_PART`, `PART_NUMBER`, `QTY_BOOK`,  `OP`) " +
                            $"VALUES ('{listMissed[i].WORK_ID}', '{item.WORK_ID}', '{listMissed[i].main_part}', '{item.PART_NUMBER}', '{soluongma}',  '{userID}')" +
                            $" on duplicate key update QTY_BOOK=QTY_BOOK+{soluongma};";
                        cmd += $"update `STORE_MATERIAL_DB`.`WORK_REMAIN_PART` set is_book=1 where WORK_ID='{item.WORK_ID}' AND MAIN_PART='{item.main_part}' AND PART_NUMBER='{item.PART_NUMBER}';";
                        soluongma = 0;
                        break;
                    } else
                    {
                        listMissed[i].qtyMainExport = listMissed[i].qtyMainExport + addNum;
                        cmd += $"update STORE_MATERIAL_DB.WORK_REMAIN_MAINPART  set QTY_EXPORT=qty_export+'{addNum}' where work_id='{listMissed[i].WORK_ID}' and main_part='{listMissed[i].main_part}';";
                        cmd += $"INSERT INTO `STORE_MATERIAL_DB`.`WORK_REMAIN_PART` (`WORK_ID`, `MAIN_PART`, `PART_NUMBER`, `QTY_EXPORT`) VALUES ('{listMissed[i].WORK_ID}', '{listMissed[i].main_part}', " +
                            $"'{item.PART_NUMBER}', '{addNum}')   on duplicate key update QTY_EXPORT=qty_export+'{addNum}' ;";
                        cmd += $"INSERT INTO `STORE_MATERIAL_DB`.`WORK_BOOK_PART` (`WORK_ID`, `WORK_BOOK`, `MAIN_PART`, `PART_NUMBER`, `QTY_BOOK`, `OP`) " +
                            $"VALUES ('{listMissed[i].WORK_ID}', '{item.WORK_ID}', '{listMissed[i].main_part}', '{item.PART_NUMBER}', '{addNum}',  '{userID}')" +
                            $" on duplicate key update QTY_BOOK=QTY_BOOK+{addNum};";
                        cmd += $"update `STORE_MATERIAL_DB`.`WORK_REMAIN_PART` set is_book=1 where WORK_ID='{item.WORK_ID}' AND MAIN_PART='{item.main_part}' AND PART_NUMBER='{item.PART_NUMBER}';";
                        soluongma -= addNum;
                    }    

                }
                if(soluongma>0&& lastIndex>0)
                {
                    listMissed[lastIndex].qtyMainExport = listMissed[lastIndex].qtyMainExport + soluongma;
                    cmd += $"update STORE_MATERIAL_DB.WORK_REMAIN_MAINPART  set QTY_EXPORT=qty_export+'{soluongma}',QTY_REMAIN=QTY_REMAIN+{soluongma} where work_id='{listMissed[lastIndex].WORK_ID}' " +
                        $"and main_part='{listMissed[lastIndex].main_part}';";
                    cmd += $"INSERT INTO `STORE_MATERIAL_DB`.`WORK_REMAIN_PART` (`WORK_ID`, `MAIN_PART`, `PART_NUMBER`, `QTY_EXPORT`, `QTY_REMAIN`) VALUES ('{listMissed[lastIndex].WORK_ID}', " +
                        $"'{listMissed[lastIndex].main_part}', '{item.PART_NUMBER}', '{soluongma}', '{soluongma}') " +
                                    $"on duplicate key update QTY_EXPORT=qty_export+'{soluongma}',QTY_REMAIN=QTY_REMAIN+{soluongma} ;";
                    cmd += $"INSERT INTO `STORE_MATERIAL_DB`.`WORK_BOOK_PART` (`WORK_ID`, `WORK_BOOK`, `MAIN_PART`, `PART_NUMBER`, `QTY_BOOK`,  `OP`) " +
                            $"VALUES ('{listMissed[lastIndex].WORK_ID}', '{item.WORK_ID}', '{listMissed[lastIndex].main_part}', '{item.PART_NUMBER}', '{soluongma}',  '{userID}')" +
                            $" on duplicate key update QTY_BOOK=QTY_BOOK+{soluongma};";
                    cmd += $"update `STORE_MATERIAL_DB`.`WORK_REMAIN_PART` set is_book=1 where WORK_ID='{item.WORK_ID}' AND MAIN_PART='{item.main_part}' AND PART_NUMBER='{item.PART_NUMBER}';";
                }    
                
            }
            return mySql.InsertDataMySQL(cmd);
            
        }
        public List<DTO.MainPart_infor> getMainPartExportOnWork(DTO.WorkOrder work_id)
        {
            List<DTO.MainPart_infor> part_Exports = new List<DTO.MainPart_infor>();
            List<ROSE_Dll.DTO.BomContent> bomContents = new List<ROSE_Dll.DTO.BomContent>();
            bomContents = BomDao.GetBomContents(new ROSE_Dll.DTO.BomGeneral { Model = work_id.ModelID, BomVersion = work_id.bomVersion });
            foreach (var item in bomContents)
            {
                int qty = 0;
                int.TryParse(item.Quantity,out qty);
                part_Exports.Add(new DTO.MainPart_infor
                {
                    workID=work_id.WorkID,
                    mainPart = item.MainPart,
                    partNumber = item.InterPart,
                    csPart = item.CSPart,
                    mfgPart = item.MfgPart,
                    qtyBom = qty,
                    qtyRequest = qty * work_id.totalPcs
                });
               
            }
            return part_Exports;
        }

        //public List<DTO.work_remain_Part> getRemainPartExportOnWork(DTO.WorkOrder work_id)
        //{
        //    List<DTO.work_remain_Part> part_Exports = new List<DTO.work_remain_Part>();
        //    DataTable dt = mySql.GetDataMySQL($"SELECT WORK_ID,PART_NUMBER,QTY_REMAIN, IS_BOOK FROM STORE_MATERIAL_DB.WORK_REMAIN_PART WHERE WORK_ID='{work_id.WorkID}' and is_book=0 and qty_remain>0;");
        //    if (istableNull(dt)) return part_Exports;
        //    return (from a in dt.AsEnumerable()
        //            select new DTO.work_remain_Part
        //            {
        //                workID = a.Field<string>("work_id"),
        //                //main_part = a.Field<string>("main_part"),
        //                //workBook = a.Field<string>("work_book"),
        //                isBook = a.Field<int>("is_book"),
        //                //qtyRequest = a.Field<int>("qty_request"),
        //                qtyExport = a.Field<int>("qty_export"),
        //                qtyRemain = a.Field<int>("qty_remain"),
        //                partNumber = a.Field<string>("part_number"),
        //                //miss = a.Field<int>("qty_export") >= a.Field<int>("qty_request") ? 0 : 1
        //            }).ToList();
        //}
        public List<DTO.Part_infor> getCsPartExportOnWork(DTO.WorkOrder work_id)
        {
            List<DTO.Part_infor> part_Exports = new List<DTO.Part_infor>();
            List<ROSE_Dll.DTO.BomContent> bomContents = new List<ROSE_Dll.DTO.BomContent>();
            bomContents = BomDao.GetBomContents(new ROSE_Dll.DTO.BomGeneral { Model = work_id.ModelID, BomVersion = work_id.bomVersion });
            foreach (var item in bomContents)
            {
                int qty = 0;
                int.TryParse(item.Quantity, out qty);
                if (qty == 0) continue;
                part_Exports.Add(new DTO.Part_infor
                {
                    part=item.CSPart,
                    qtyBom = qty,
                    qtyRequest = qty * work_id.totalPcs,
                    workID=work_id.WorkID
                });

            }
            return part_Exports;
        }
        public List<DTO.work_remain_Part> getRemainMainPart(string workID)
        {
            DataTable dt= mySql.GetDataMySQL($"SELECT A.WORK_ID,A.MAIN_PART,A.QTY_REQUEST,A.QTY_EXPORT 'MAIN_EXPORT',A.QTY_REMAIN 'MAIN_REMAIN',B.PART_NUMBER,B.QTY_EXPORT,B.QTY_REMAIN FROM STORE_MATERIAL_DB.WORK_REMAIN_MAINPART " +
                $"A LEFT join STORE_MATERIAL_DB.WORK_REMAIN_PART B  on " +
                $"A.main_part = B.MAIN_PART and A.WORK_ID = B.WORK_ID WHERE A.WORK_ID = '{workID}' ; ");
            return (from a in dt.AsEnumerable()
                    select new DTO.work_remain_Part
                    {
                        WORK_ID=a.Field<string>("WORK_ID"),
                        main_part = a.Field<string>("MAIN_PART"),
                        PART_NUMBER = a.Field<string>("PART_NUMBER"),
                        qtyMainExport = a.Field<int>("MAIN_EXPORT"),
                        qtyMainRemain = a.Field<int>("MAIN_REMAIN"),
                        qtyMainrequest = a.Field<int>("QTY_REQUEST"),
                        qtyPartExport = a.Field<int?>("QTY_EXPORT")==null?0: a.Field<int>("QTY_EXPORT") ,
                        qtyPartRemain = a.Field<int?>("QTY_REMAIN") == null ? 0 : a.Field<int>("QTY_REMAIN"),

                    }).ToList();
        }
        public DataTable getLinkPart(string work_id)
        {
            return mySql.GetDataMySQL($"SELECT WORK_ID,MAIN_PART,PART_NUMBER,QTY_BOOK, WORK_BOOK 'FROM_WORK' FROM STORE_MATERIAL_DB.WORK_BOOK_PART WHERE WORK_ID='{work_id}';");
        }
        public List<DTO.work_remain_SPart> getPartRemainNotBook(string partNumber, string workID)
        {

            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(partNumber)) 
                mySql.GetDataMySQL($"SELECT WORK_ID,PART_NUMBER,QTY_REMAIN,MAIN_PART FROM STORE_MATERIAL_DB.WORK_REMAIN_PART WHERE PART_NUMBER='{partNumber}' and is_book=0 and qty_remain>0;");
            if(!string.IsNullOrEmpty(workID))
                dt = mySql.GetDataMySQL($"SELECT WORK_ID,MAIN_PART,PART_NUMBER,QTY_REMAIN FROM STORE_MATERIAL_DB.WORK_REMAIN_PART WHERE WORK_ID='{workID}' and is_book=0 and qty_remain>0;");
            return (from a in dt.AsEnumerable()
                    select new DTO.work_remain_SPart
                    {
                        WORK_ID = a.Field<string>("WORK_ID"),
                        qtyPartRemain = a.Field<int>("QTY_REMAIN"),
                        PART_NUMBER = a.Field<string>("PART_NUMBER"),
                        main_part = a.Field<string>("MAIN_PART"),
                    }).ToList();
        }       
        
        public DTO.WorkOrder getWorkInfor(string workID)
        {
            DTO.WorkOrder workOrder = new DTO.WorkOrder();
            DataTable dt = mySql.GetDataMySQL($"SELECT * FROM TRACKING_SYSTEM.WORK_ORDER where work_id='{workID}';");
            if (istableNull(dt))
                return workOrder;
            workOrder = (from a in dt.AsEnumerable()
                         select new DTO.WorkOrder
                         {
                             ModelID = dt.Rows[0]["model"].ToString(),
                             WorkID = dt.Rows[0]["work_id"].ToString(),
                             totalPcs = int.Parse(dt.Rows[0]["total_pcbs"].ToString()),
                             isRMA = int.Parse(dt.Rows[0]["is_rma"].ToString()),
                             bomVersion = dt.Rows[0]["bom_version"].ToString(),
                             state = dt.Rows[0]["status"].ToString(),
                             NumberTemp= a.Field<int>("STAMP_NUMBER"),
                         }).ToList().FirstOrDefault();

            return workOrder;
        }
         
        public string getPartTem(string model)
        {
            DataTable dt = mySql.GetDataMySQL($"SELECT PART_NAME FROM TRACKING_SYSTEM.TEM_SEF_DEFINED WHERE MODEL_ID='{model}';");
            if (istableNull(dt)) return "";
            return dt.Rows[0]["PART_NAME"].ToString();
        }
        public bool isTemonWork(string tem, string work)
        {
            DataTable dt = mySql.GetDataMySQL($"SELECT * FROM TRACKING_SYSTEM.TEM_DATA where BARCODE='{tem}' and order_no='{work}';");
            if (istableNull(dt))
                return false;
            return true;
        }
    }
}
