using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using PLAN_MODULE.DTO;
using System.Data;
namespace PLAN_MODULE.DAO
{
    class DaoUPH:BaseDAO
    {

        public List<DTO.UPH> GetUPHs()
        {
            List<DTO.UPH> uPHs = new List<UPH>();
            DataTable dt = mySQL.GetDataMySQL($"SELECT MODEL_NAME,Cluster,UPH,USER,EVENT_TIME FROM MasterSys.UPH;");
            if (istableNull(dt)) return uPHs;
            return (from a in dt.AsEnumerable()
             select new DTO.UPH()
             {
                 cluster = a.Field<string>("Cluster"),
                 model = a.Field<string>("MODEL_NAME"),
                 uph = a.Field<int>("uph"),
                 user_create= a.Field<string>("user"),
                 creat_time= a.Field<DateTime>("EVENT_TIME")
             }).ToList();
            
        }
        public bool updateUPH(string user,string model, string cluster, int uph)
        {
            string cmd = $"INSERT INTO `MasterSys`.`UPH` (`MODEL_NAME`, `CLUSTER`, `UPH`,`user`) VALUES ('{model}', '{cluster}', '{uph}','{user}')" +
                $" on duplicate key update uph={uph}, user='{user}', event_time=now();";
            return mySql.InsertDataMySQL(cmd);
        }
        public bool deleteUPH( string model, string cluster)
        {
            string cmd = $"delete from `MasterSys`.`UPH` where `MODEL_NAME`='{model}' and  `CLUSTER`='{cluster}';";
            return mySql.InsertDataMySQL(cmd);
        }
    }
}
