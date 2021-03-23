using MERP.DataAccess;
using MERP.Engine;
using MERP.Engine.GlobalClass;
using MERP.Engine.GlobalGateway;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MERP.Services
{
    public class BranchService : CommonGateway, IBranch
    {
        #region Maping
        private Branch MapObject(NullHandler oReader)
        {
            Branch oBranch = new Branch();
            oBranch.BranchID = oReader.GetInt32("BranchID");
            oBranch.BranchName = oReader.GetString("BranchName");
            oBranch.LSOPrefix = oReader.GetString("LSOPrefix");
            oBranch.BranchAddress = oReader.GetString("BranchAddress");
            oBranch.BranchCode = oReader.GetString("BranchCode");
            oBranch.BranchPrefix = oReader.GetString("BranchPrefix");
            oBranch.BranchNote = oReader.GetString("BranchNote");
            return oBranch;
        }
        private Branch MakeObject(NullHandler oReader)
        {
            Branch oBranch = new Branch();
            oBranch = MapObject(oReader);
            return oBranch;
        }
        private List<Branch> MakeObjects(IDataReader oReader)
        {
            List<Branch> oBranchs = new List<Branch>();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                Branch oBranch = MapObject(oHandler);
                oBranchs.Add(oBranch);
            }
            return oBranchs;
        }
        #endregion


        #region Function Implementation
        public Branch IUD(Branch oBranch, EnumDBOperation oDBOperation, string sUserID)
        {
            Branch _oBranch = new Branch();
            try
            {
                Connection.Open();
                Command.CommandText = BranchDA.IUD(oBranch, oDBOperation, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oBranch = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oBranch = new Branch();
                _oBranch.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oBranch;
        }
        public List<Branch> Gets(string sSQL, string sUserID)
        {
            Branch _oBranch = new Branch();
            List<Branch> _oBranchs = new List<Branch>();
            try
            {
                Connection.Open();
                Command.CommandText = BranchDA.Gets(sSQL, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oBranchs = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oBranch = new Branch();
                _oBranchs = new List<Branch>();
                _oBranch.ErrorMessage = e.Message.Split('~')[1];
                _oBranchs.Add(_oBranch);
            }
            return _oBranchs;
        }
        public Branch Get(int nID, string sUserID)
        {
            Branch _oBranch = new Branch();
            try
            {
                Connection.Open();
                Command.CommandText = BranchDA.Get(nID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oBranch = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oBranch = new Branch();
                _oBranch.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oBranch;
        }
        public string Delete(int nBranchID, string sUserID)
        {
            try
            {
                Connection.Open();
                Command.CommandText = BranchDA.Delete(nBranchID, sUserID);
                Command.ExecuteNonQuery();
                Connection.Close();
            }
            catch (Exception e)
            {
                FeedbackMessage.Deleted = e.Message.Split('~')[0];
            }
            return FeedbackMessage.Deleted;
        }
        #endregion
    }
}