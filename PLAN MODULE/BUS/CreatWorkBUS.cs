using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.BUS
{
    public class CreatWorkBUS : BaseBUS
    {
        public bool CreateLink(string workMother, string modelModther, string workChild, string modelChild, string op, int rate, int location)
        {
            string sql = $"INSERT INTO `TRACKING_SYSTEM`.`WORK_ID_LINK` (`WORK_ID`, `MODEL_ID`, `WORK_CHILD`, `MODEL_CHILD`, `RATE`, `LOCATION`, `OP`) VALUES " +
                $"('{workMother}', '{modelModther}', '{workChild}', '{modelChild}', '{rate}', '{location}', '{op}')  ON DUPLICATE KEY UPDATE `WORK_CHILD` = '{workChild}', `MODEL_CHILD` = '{modelChild}', `RATE` = '{rate}', `LOCATION` = '{location}';";
            if (mySQL.InsertDataMySQL(sql)) return true;
            return false;
        }
        public bool createWork(string customer, string model, string work, int numberPCBs, string op, bool isWorkRMA, bool isWorkXout, bool isSample, int pcbXout, string woMotherRMAInput,
    string woMother, string processMother, string processChild, string PO, bool isLinkMother)
        {
            int isRMA = 0;
            int isXOUT = 0;
            int isSamp = 0;
            if (isWorkRMA) isRMA = 1;
            if (isWorkXout) isXOUT = 1;
            if (isSample) isSamp = 1;
            string cmd = string.Empty;
            cmd += $"INSERT INTO `TRACKING_SYSTEM`.`WORK_ORDER` (`WORK_ID`, `PO`, `CUSTOMER`, `MODEL`, `CREATER`, `DATE_CREATE`,`PCBS`,`TOTAL_PCBS`, IS_RMA, IS_XOUT, PCB_XOUT, `WORK_MOTHER`, `IS_SAMPLE`) " +
               $"VALUES ('{work}', '{PO}', '{customer}', '{model}', '{op}',  now(), {numberPCBs - pcbXout }, {numberPCBs}, {isRMA}, {isXOUT},  {pcbXout}, '{woMother}', {isSamp} ) " +
               $" ON DUPLICATE KEY UPDATE `TOTAL_PCBS` =  {numberPCBs }, `PCBS` =  {numberPCBs - pcbXout }  ;";
            cmd += $"INSERT INTO `TRACKING_SYSTEM`.`WORK_ORDER_HISTORY` (`WORK_ID`, `CUSTOMER`, `MODEL`, `PCB_QTY`, `WORK_ID_MOTHER`, `CREATER`,`DATE_CREATE`,`NOTE`)" +
                $"VALUES ('{work}', '{customer}', '{model}', '{numberPCBs}', '{woMotherRMAInput}', '{op}', now(),'CREATE');";
            if (isLinkMother)
                cmd += $"INSERT INTO `TRACKING_SYSTEM`.`WORK_PROCESS_LINK` (`WORK_ID`, `PROCESS`, `WORK_CHILD`, `PROCESS_CHILD`, `DATETIME`, `OP`, `NOTE`) " +
                    $"VALUES ('{woMother}', '{processMother}', '{work}', '{processChild}', NOW(),'{op}', '{woMother}-{work}');";
            if (!mySQL.InsertDataMySQL(cmd)) return false;
            return true;
        }



        public bool CreateWork(string customer, string model, string work, int numberPCBs, string op, int isRMA, int isXOUT, int isSamp, int pcbXout,
                string woMother, string processMother, string processChild, string PO, bool isLinkMother, string bomversion, string comment , bool existWorkID , int tempNumber = 0 )
        {
            string cmd = string.Empty;
            string action = existWorkID ? "UPDATE" : "CREATE" ;


            cmd += $"INSERT INTO `TRACKING_SYSTEM`.`WORK_ORDER` (`WORK_ID`, `PO`, `CUSTOMER`, `MODEL`, `CREATER`, `DATE_CREATE`,`PCBS`,`TOTAL_PCBS`, `BOM_VERSION`, IS_RMA, IS_XOUT, PCB_XOUT, `WORK_MOTHER`, `IS_SAMPLE` , `COMMENT` , `STAMP_NUMBER`) " +
               $"VALUES ('{work}', '{PO}', '{customer}', '{model}', '{op}',  now(), {numberPCBs - pcbXout }, {numberPCBs}, '{bomversion}', {isRMA}, {isXOUT},  {pcbXout}, '{woMother}', {isSamp} , '{comment}' , '{tempNumber}' ) " +
               $" ON DUPLICATE KEY UPDATE `TOTAL_PCBS` =  {numberPCBs }, `PCBS` =  {numberPCBs - pcbXout },`BOM_VERSION`= '{bomversion}',  IS_RMA = '{isRMA}',IS_XOUT = '{isXOUT}', PCB_XOUT = '{pcbXout}' ,  `IS_SAMPLE` = '{isSamp}' , `COMMENT` = '{comment}' , `STAMP_NUMBER` = '{tempNumber}';";



            cmd += $"INSERT INTO `TRACKING_SYSTEM`.`WORK_ORDER_HISTORY` (`WORK_ID`, `CUSTOMER`, `MODEL`, `PCB_QTY`, `WORK_ID_MOTHER`, `CREATER`,`DATE_CREATE`,`NOTE`)" +
                $"VALUES ('{work}', '{customer}', '{model}', '{numberPCBs}', '{woMother}', '{op}', now(),'{action}');";
            if (isLinkMother)
                cmd += $"INSERT  INTO `TRACKING_SYSTEM`.`WORK_PROCESS_LINK` (`WORK_ID`, `PROCESS`, `WORK_CHILD`, `PROCESS_CHILD`, `DATETIME`, `OP`, `NOTE`) " +
                    $"VALUES ('{woMother}', '{processMother}', '{work}', '{processChild}', NOW(),'{op}', '{woMother}-{work}') ON DUPLICATE KEY UPDATE  `DATETIME` = NOW();";
            if (!mySQL.InsertDataMySQL(cmd)) return false;
            return true;
        }
        public bool CloseWork(string work, string model, string op)
        {
            string mycmd = "";
            mycmd += $"UPDATE TRACKING_SYSTEM.WORK_ORDER SET `STATUS`='CLOSE' WHERE WORK_ID='{work}';";
            mycmd += $"INSERT INTO `TRACKING_SYSTEM`.`WORK_ORDER_HISTORY` (`WORK_ID`, `MODEL`, `CREATER`, `DATE_CREATE`, `NOTE` ) " +
                $" VALUES ('{work}', '{model}', '{op}', now(), 'CLOSE');";
            if (!mySQL.InsertDataMySQL(mycmd)) return false;
            return true;
        }
        public bool DeleteWork(string work, string user, bool IsWorkMother = false)
        {
            string cmd = string.Empty;
            cmd += $"delete from TRACKING_SYSTEM.MOUTER_PROGRAM where WORK_ID = '{work}';";
            cmd += $"delete FROM TRACKING_SYSTEM.WORK_ORDER where WORK_ID='{work}'; ";
            cmd += $"INSERT INTO `TRACKING_SYSTEM`.`WORK_ORDER_HISTORY` (`WORK_ID`,`CREATER`,`DATE_CREATE`,`NOTE`)" +
                $"VALUES ('{work}',  '{user}', now(),'DELETE');";
            cmd += $"delete FROM TRACKING_SYSTEM.WORK_PROCESS_LINK WHERE WORK_CHILD='{work}';";
            if (!mySQL.InsertDataMySQL(cmd)) return false;

            return true;
        }
    }
}

