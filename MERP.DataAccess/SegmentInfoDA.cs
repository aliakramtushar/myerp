using MERP.Engine.GlobalClass;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.DataAccess
{
    public class SegmentInfoDA
    {
        public static string IUD(SegmentInfo oSegmentInfo, EnumDBOperation oDBOperation, string sUserID)
        {
            return GlobalHelpers.ExcecuteQurey("EXEC AST.SP_IUD_SegmentInfo",
                oSegmentInfo.SegmentID, oSegmentInfo.SegmentCode, oSegmentInfo.SegmentName, oSegmentInfo.IsInActive,
                (int)oDBOperation, sUserID);
        }
        public static string Gets(string sSQL, string sUserID)
        {
            if (sSQL == "") return "SELECT * FROM AST.SegmentInfo ORDER BY SegmentName";
            else return sSQL;
        }
        public static string Get(int nID, string sUserID)
        {
            return "SELECT * FROM AST.SegmentInfo WHERE [SegmentID] =" + nID;
        }
    }
}