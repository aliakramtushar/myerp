using MERP.Engine.GlobalClass;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.DataAccess
{
    public class StudentTrainingMappingDA
    {
        public static string IUD(StudentTrainingMapping oSTM, EnumDBOperation oDBOperation, string sUserID)
        {
            return GlobalHelpers.ExcecuteQurey("EXEC PSDP.SP_IUD_StudentTrainingMapping",
                oSTM.StudentTrainingMappingID, oSTM.StudentID, oSTM.TrainingID, oSTM.BranchID, oSTM.Amount, oSTM.DiscountAmount, oSTM.AdjustmentAmount, (int)oSTM.ReferenceType,
                oSTM.ReferenceName, oSTM.ReferenceContactNo, oSTM.StartDate, oSTM.Remarks, oSTM.BlankField_1, oSTM.BlankField_2, oSTM.BlankField_3, oSTM.BlankField_4,
                oSTM.BlankField_5, (int)oDBOperation, sUserID);
        }
        public static string Gets(string sSQL, string sUserID)
        {
            if (sSQL == "") return "SELECT * FROM PSDP.View_StudentTrainingMapping";
            else return sSQL;
        }
        public static string Get(int nID, string sUserID)
        {
            return "SELECT * FROM PSDP.View_StudentTrainingMapping WHERE [TrainingID] =" + nID;
        }
    }
}