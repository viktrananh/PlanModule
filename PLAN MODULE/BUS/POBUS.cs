using PLAN_MODULE.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.BUS
{
    public class POBUS : BaseBUS
    {
        public bool POUpdate(string cusId, string modelId, string PO, string mfgDate, int count, string comment, string op, int invoiceQty)
        {
            string cmd = $"INSERT INTO `TRACKING_SYSTEM`.`POOrder` (`CusId`, `PO`, `DateCreat`, `OP`) " +
                $" VALUES ('{cusId}', '{PO}', now(), '{op}') " +
                $" ON DUPLICATE KEY UPDATE `DateCreat` = now(), `OP` = '{op}' ;";
            cmd += $"INSERT INTO `TRACKING_SYSTEM`.`PODetail` ( `ModelId`, `PO`, `Count`,  `MFGDate`, `State`, `Comment` , `InvoiceQty`) " +
                $" VALUES ( '{modelId}', '{PO}', '{count}', '{mfgDate}', '{0}', '{comment}' , '0') " +
                $" ON DUPLICATE KEY UPDATE `Count` = '{count}' , `MFGDate` ='{mfgDate}' , `Comment` = '{comment}' , `InvoiceQty` = '{invoiceQty}' ; ";
            cmd += $"update TRACKING_SYSTEM.PODetail set State = 1 and CLOSE_TIME = NOW() where PO = '{PO}' and ModelId = '{modelId}' AND Count = InvoiceQty;";
            cmd += $"INSERT INTO `TRACKING_SYSTEM`.`POHistory` (`PO`, `DateAction`, `Action`, `Op`, `Count`, `ModelId`)" +
                $"  VALUES ('{PO}', now(), 'Update', '{op}', '{count}', '{modelId}');";

            if (mySQL.InsertDataMySQL(cmd)) return true;
            return false;

        }

        public bool POClose(string PO, string ModelId, string _UserId)
        {
            string cmd = $"update TRACKING_SYSTEM.PODetail set `State` = '1' where PO ='{PO}' and ModelId = '{ModelId}';";

            cmd += $"INSERT INTO `TRACKING_SYSTEM`.`POHistory` (`PO`, `DateAction`, `Action`, `Op`, `Count`, `ModelId`)" +
              $"  VALUES ('{PO}', now(), 'Close', '{_UserId}', '{1}', '{ModelId}');";
            if (mySQL.InsertDataMySQL(cmd)) return true;
            return false;
        }

        public bool PODelete(string PO, string ModelId, string _UserId)
        {
            string cmd = $"DELETE FROM TRACKING_SYSTEM.PODetail where PO ='{PO}' and ModelId = '{ModelId}';";
            cmd += $"INSERT INTO `TRACKING_SYSTEM`.`POHistory` (`PO`, `DateAction`, `Action`, `Op`, `Count`, `ModelId`) " +
                $" VALUES ('{PO}', now(), 'Delete', '{_UserId}', '1', '{ModelId}');";
            if (mySQL.InsertDataMySQL(cmd)) return true;
            return false;
        }
    }
}
