using MERP.Engine.GlobalClass;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.DataAccess
{
    public class TrainingInfoDA
    {
        public static string IUD(TrainingInfo oTrainingInfo, EnumDBOperation oDBOperation, string sUserID)
        {
            return GlobalHelpers.ExcecuteQurey("EXEC PSDP.SP_IUD_TrainingInfo",
                oTrainingInfo.TrainingID, oTrainingInfo.TrainingCode, oTrainingInfo.TrainingName, (int)oTrainingInfo.TrainingType, oTrainingInfo.Description,
                oTrainingInfo.DurationInMonth, (int)oTrainingInfo.ActivityStatus, oTrainingInfo.Amount, (int)oDBOperation, sUserID);
        }
        public static string Gets(string sSQL, string sUserID)
        {
            if (sSQL == "") return "SELECT * FROM PSDP.TrainingInfo";
            else return sSQL;
        }
        public static string Get(int nID, string sUserID)
        {
            return "SELECT * FROM PSDP.TrainingInfo WHERE [TrainingID] =" + nID;
        }
    }
}