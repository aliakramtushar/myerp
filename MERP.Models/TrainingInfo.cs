using MERP.Engine;
using MERP.Engine.GlobalClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Models
{
    public class TrainingInfo
    {
        #region TrainingInfo Defult
        public TrainingInfo()
        {
            TrainingID = 0;
            TrainingCode = "";
            TrainingName = "";
            TrainingType = EnumTrainingType.None;
            Description = "";
            DurationInMonth = 0;
            ActivityStatus = EnumActivityStatus.None;
            Amount = 0;
            Description = "";
            ErrorMessage = "";
        }
        #endregion

        #region Properties
        public int TrainingID { get; set; }
        public string TrainingCode { get; set; }
        public string TrainingName { get; set; }
        public EnumTrainingType TrainingType { get; set; }
        public string Description { get; set; }
        public int DurationInMonth { get; set; }
        public EnumActivityStatus ActivityStatus { get; set; }
        public int Amount { get; set; }
        public string ErrorMessage { get; set; }

        #endregion

        #region Derived Properties
        public int MModelPK
        {
            get { return this.TrainingID; }
        }
        public string MModelString
        {
            get { return this.TrainingName; }
        }
        public string ActivityStatusSt
        {
            get { return this.ActivityStatus.ToString(); }
        }
        public string TrainingTypeSt
        {
            get { return this.TrainingType.ToString(); }
        }
        public string AmountSt
        {
            get { return this.Amount.ToString(); }
        }
        #endregion

        #region Functions
        public TrainingInfo Get(int nId, string sUserID)
        {
            return TrainingInfo.Service.Get(nId, sUserID);
        }
        public TrainingInfo IUD(TrainingInfo oTrainingInfo, EnumDBOperation oDBOperation, string sUserID)
        {
            return TrainingInfo.Service.IUD(oTrainingInfo, oDBOperation, sUserID);
        }
        public static List<TrainingInfo> Gets(string sSQL, string sUserID)
        {
            return TrainingInfo.Service.Gets(sSQL, sUserID);
        }
        #endregion

        #region ServiceFactory
        internal static ITrainingInfo Service
        {
            get { return (ITrainingInfo)Services.Factory.CreateService(typeof(ITrainingInfo)); }
        }
        #endregion
    }
    #region ITrainingInfo interface
    public interface ITrainingInfo
    {
        TrainingInfo Get(int id, string sUserID);
        List<TrainingInfo> Gets(string sSQL, string sUserID);
        TrainingInfo IUD(TrainingInfo oTrainingInfo, EnumDBOperation oDBOperation, string sUserID);
    }
    #endregion
}