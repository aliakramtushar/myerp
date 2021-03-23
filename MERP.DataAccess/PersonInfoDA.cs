using MERP.Engine.GlobalClass;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.DataAccess
{
    public class PersonInfoDA
    {
        public static string IUD(PersonInfo oPersonInfo, EnumDBOperation oDBOperation, string sUserID)
        {
            return GlobalHelpers.ExcecuteQurey("EXEC COM.SP_IUD_PersonInfo",
                oPersonInfo.PersonID, oPersonInfo.PersonName, oPersonInfo.Mobile, oPersonInfo.Address,(int)oPersonInfo.PersonType, 
                (int)oDBOperation, sUserID);
        }
        public static string Gets(string sSQL, string sUserID)
        {
            if (sSQL == "") return "SELECT * FROM COM.PersonInfo ORDER BY PersonName";
            else return sSQL;
        }
        public static string GetsPersonByType(EnumPersonType oPersonType, string sUserID)
        {
            return "SELECT * FROM COM.PersonInfo WHERE PersonType = " + (int)oPersonType + " ORDER BY PersonName";
        }
        public static string Get(int nID, string sUserID)
        {
            return "SELECT * FROM COM.PersonInfo WHERE [PersonID] =" + nID;
        }
        public static string Delete(int nID, string sUserID)
        {
            return "DELETE FROM COM.PersonInfo WHERE PersonID = " + nID;
        }
    }
}