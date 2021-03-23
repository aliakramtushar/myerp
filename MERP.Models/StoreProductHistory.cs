using MERP.Engine;
using MERP.Engine.GlobalClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Models
{
    public class StoreProductHistory
    {
        #region StoreProductHistory Defult
        public StoreProductHistory()
        {
            StoreProductHistoryID = 0;
            DBOperationID = EnumDBOperation.None;
            StoreID = 0;
            ProductID = 0;
            SupplierID = 0;
            TransitStoreID = 0;
            Remarks = "";
            ProductOldQty = 0;
            ProductNewQty = 0;
            AddedBy = "";
            DateAdded = DateTime.Now;
            StoreName = "";
            ProductName = "";
            SupplierName = "";
            TransitStoreName = "";
            ErrorMessage = "";
        }
        #endregion

        #region Properties
        public int StoreProductHistoryID { get; set; }
        public EnumDBOperation DBOperationID { get; set; }
        public int StoreID { get; set; }
        public int ProductID { get; set; }
        public int SupplierID { get; set; }
        public int TransitStoreID { get; set; }
        public string Remarks { get; set; }
        public int ProductOldQty { get; set; }
        public int ProductNewQty { get; set; }
        public string AddedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public string StoreName { get; set; }
        public string ProductName { get; set; }
        public string SupplierName { get; set; }
        public string TransitStoreName { get; set; }
        public string ErrorMessage { get; set; }
        #endregion

        #region Derived Properties
        public int MModelPK
        {
            get { return this.StoreProductHistoryID; }
        }
        public string MModelString
        {
            get { return this.Remarks; }
        }
        public string DBOperationIDSt
        {
            get { return this.DBOperationID.ToString(); }
        }
        public string DateAddedSt
        {
            get { return (this.DateAdded == DateTime.MinValue) ? "" : this.DateAdded.ToString("dd-MM-yyyy"); }
        }
        #endregion

        #region Functions
        public StoreProductHistory Get(int nId, string sUserID)
        {
            return StoreProductHistory.Service.Get(nId, sUserID);
        }
        public static List<StoreProductHistory> Gets(string sSQL, string sUserID)
        {
            return StoreProductHistory.Service.Gets(sSQL, sUserID);
        }
        #endregion

        #region ServiceFactory
        internal static IStoreProductHistory Service
        {
            get { return (IStoreProductHistory)Services.Factory.CreateService(typeof(IStoreProductHistory)); }
        }
        #endregion
    }
    #region IStoreProductHistory interface
    public interface IStoreProductHistory
    {
        StoreProductHistory Get(int id, string sUserID);
        List<StoreProductHistory> Gets(string sSQL, string sUserID);
    }
    #endregion
}