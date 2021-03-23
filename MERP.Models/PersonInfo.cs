using MERP.Engine;
using MERP.Engine.GlobalClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Models
{
    public class PersonInfo
    {
        #region PersonInfo Defult
        public PersonInfo()
        {
            PersonID = 0;
            PersonName = "";
            Mobile = "";
            Address = "";
            PersonType = EnumPersonType.None;
            ErrorMessage = "";
        }
        #endregion

        #region Properties
        public int PersonID { get; set; }
        public string PersonName { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public EnumPersonType PersonType { get; set; }
        public string ErrorMessage { get; set; }

        #endregion

        #region Derived Properties
        public int MModelPK
        {
            get { return this.PersonID; }
        }
        public string MModelString
        {
            get { return this.PersonName; }
        }
        public string PersonTypeSt
        {
            get { return this.PersonType.ToString(); }
        }
        #endregion

        #region Functions
        public PersonInfo Get(int nId, string sUserID)
        {
            return PersonInfo.Service.Get(nId, sUserID);
        }
        public PersonInfo IUD(PersonInfo oPersonInfo, EnumDBOperation oDBOperation, string sUserID)
        {
            return PersonInfo.Service.IUD(oPersonInfo, oDBOperation, sUserID);
        }
        public static List<PersonInfo> Gets(string sSQL, string sUserID)
        {
            return PersonInfo.Service.Gets(sSQL, sUserID);
        }
        public static List<PersonInfo> GetsPersonByType(EnumPersonType oPersonType, string sUserID)
        {
            return PersonInfo.Service.GetsPersonByType(oPersonType, sUserID);
        }
        
        public string Delete(int id, string sUserID)
        {
            return PersonInfo.Service.Delete(id, sUserID);
        }
        #endregion

        #region ServiceFactory
        internal static IPersonInfo Service
        {
            get { return (IPersonInfo)Services.Factory.CreateService(typeof(IPersonInfo)); }
        }
        #endregion
    }
    #region IPersonInfo interface
    public interface IPersonInfo
    {
        PersonInfo Get(int id, string sUserID);
        List<PersonInfo> Gets(string sSQL, string sUserID);
        List<PersonInfo> GetsPersonByType(EnumPersonType oPersonType, string sUserID);
        PersonInfo IUD(PersonInfo oPersonInfo, EnumDBOperation oDBOperation, string sUserID);
        string Delete(int id, string sUserID);
    }
    #endregion
}