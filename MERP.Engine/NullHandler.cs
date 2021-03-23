using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace MERP.Engine
{
    #region DataAccess: Null Handler
    public class NullHandler
    {
        #region Constructor & Declaration
        private IDataReader _reader;
        public NullHandler() { }
        public NullHandler(IDataReader reader)
        {
            _reader = reader;
        }
        #endregion

        #region Get Null value
        public static object GetNullValue(int Value)
        {
            if (Value == 0)
            { return null; }
            else
            { return Value; }
        }

        public static object GetNullValue(DateTime Value)
        {
            if (DateTime.MinValue == Value)
            { return null; }
            else
            { return Value; }
        }

        public static object GetNullValue(string Value)
        {
            if (Value.Length <= 0)
            { return null; }
            else
            { return Value; }
        }
        #endregion

        #region Default Null Values
        public IDataReader Reader
        {
            get { return _reader; }
        }
        #region DB Schema Part
        public DataTable GetSchemaTable()
        {
            return _reader.GetSchemaTable();
        }
        #endregion
        public bool IsNull(int index)
        {
            return _reader.IsDBNull(index);
        }

        public int GetInt32(int i)
        {
            return _reader.IsDBNull(i) ? 0 : _reader.GetInt32(i);
        }

        public byte GetByte(int i)
        {
            return _reader.IsDBNull(i) ? (byte)0 : _reader.GetByte(i);
        }

        public decimal GetDecimal(int i)
        {
            return _reader.IsDBNull(i) ? 0 : _reader.GetDecimal(i);
        }

        public long GetInt64(int i)
        {
            return _reader.IsDBNull(i) ? (long)0 : _reader.GetInt64(i);
        }

        public double GetDouble(int i)
        {
            return _reader.IsDBNull(i) ? 0 : _reader.GetDouble(i);
        }

        public bool GetBoolean(int i)
        {
            return _reader.IsDBNull(i) ? false : _reader.GetBoolean(i);
        }

        public Guid GetGuid(int i)
        {
            return _reader.IsDBNull(i) ? Guid.Empty : _reader.GetGuid(i);
        }

        public DateTime GetDateTime(int i)
        {
            return _reader.IsDBNull(i) ? DateTime.MinValue : _reader.GetDateTime(i);
        }

        public float GetFloat(int i)
        {
            return _reader.IsDBNull(i) ? 0 : _reader.GetFloat(i);
        }

        public string GetString(int i)
        {
            return _reader.IsDBNull(i) ? null : _reader.GetString(i);
        }

        public char GetChar(int i)
        {
            return _reader.IsDBNull(i) ? '\0' : _reader.GetChar(i);
        }

        public short GetInt16(String sFieldName)
        {
            if (!HasColumn(_reader, sFieldName))
            {
                return (Int16)0;
            }
            else
            {
                return (_reader[sFieldName] == DBNull.Value) ? (Int16)0 : Convert.ToInt16(_reader[sFieldName]);
            }
        }
        public int GetInt32(String sFieldName)
        {
            if (!HasColumn(_reader, sFieldName))
            {
                return (Int32)0;
            }
            else
            {
                return (_reader[sFieldName] == DBNull.Value) ? (Int32)0 : Convert.ToInt32(_reader[sFieldName]);
            }
        }
        public byte GetByte(String sFieldName)
        {
            if (!HasColumn(_reader, sFieldName))
            {
                return (byte)0;
            }
            else
            {
                return _reader[sFieldName] == DBNull.Value ? (byte)0 : Convert.ToByte(_reader[sFieldName]);
            }
        }
        public byte[] GetBytes(String sFieldName)
        {
            byte[] ba = null;
            if (!HasColumn(_reader, sFieldName))
            {
                return null;
            }
            else
            {

                object obj = _reader[sFieldName];
                if (obj == DBNull.Value)
                {
                    return null;
                }
                ba = (byte[])obj;
            }
            return ba;
        }
        public long GetInt64(String sFieldName)
        {
            if (!HasColumn(_reader, sFieldName))
            {
                return (Int64)0;
            }
            else
            {
                return (_reader[sFieldName] == DBNull.Value) ? (Int64)0 : Convert.ToInt64(_reader[sFieldName]);
            }
        }
        public Double GetDouble(String sFieldName)
        {
            if (!HasColumn(_reader, sFieldName))
            {
                return (Double)0;
            }
            else
            {
                return (_reader[sFieldName] == DBNull.Value) ? (Double)0 : Convert.ToDouble(_reader[sFieldName]);
            }
        }
        public Double GetDoubleRound(String sFieldName)
        {
            if (!HasColumn(_reader, sFieldName))
            {
                return (Double)0;
            }
            else
            {
                return (_reader[sFieldName] == DBNull.Value) ? (Double)0 : (Math.Truncate(Convert.ToDouble(_reader[sFieldName]) * 10000) / 10000);
            }
        }
        public bool GetBoolean(String sFieldName)
        {
            if (!HasColumn(_reader, sFieldName))
            {
                return false;
            }
            else
            {
                return (_reader[sFieldName] == DBNull.Value) ? false : Convert.ToBoolean(_reader[sFieldName]);
            }
        }
        public DateTime GetDateTime(String sFieldName)
        {
            if (!HasColumn(_reader, sFieldName))
            {
                return DateTime.MinValue;
            }
            else
            {
                return (_reader[sFieldName] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(_reader[sFieldName]);
            }
        }
        public char GetChar(String sFieldName)
        {
            if (!HasColumn(_reader, sFieldName))
            {
                return '\0';
            }
            else
            {
                return (_reader[sFieldName] == DBNull.Value) ? '\0' : Convert.ToChar(_reader[sFieldName]);
            }
        }
        public string GetString(String sFieldName)
        {
            if (!HasColumn(_reader, sFieldName))
            {
                return (string)"";
            }
            else
            {
                return (_reader[sFieldName] == DBNull.Value) ? (string)"" : Convert.ToString(_reader[sFieldName]);
            }
        }
        #endregion

        #region Parameterized Null Value
        public int GetInt32(int i, int valueIfNull)
        {
            return _reader.IsDBNull(i) ? valueIfNull : _reader.GetInt32(i);
        }

        public byte GetByte(int i, byte valueIfNull)
        {
            return _reader.IsDBNull(i) ? valueIfNull : _reader.GetByte(i);
        }

        public decimal GetDecimal(int i, decimal valueIfNull)
        {
            return _reader.IsDBNull(i) ? valueIfNull : _reader.GetDecimal(i);
        }

        public long GetInt64(int i, long valueIfNull)
        {
            return _reader.IsDBNull(i) ? valueIfNull : _reader.GetInt64(i);
        }

        public double GetDouble(int i, double valueIfNull)
        {
            return _reader.IsDBNull(i) ? valueIfNull : _reader.GetDouble(i);
        }

        public bool GetBoolean(int i, bool valueIfNull)
        {
            return _reader.IsDBNull(i) ? valueIfNull : _reader.GetBoolean(i);
        }

        public Guid GetGuid(int i, Guid valueIfNull)
        {
            return _reader.IsDBNull(i) ? valueIfNull : _reader.GetGuid(i);
        }

        public DateTime GetDateTime(int i, DateTime valueIfNull)
        {
            return _reader.IsDBNull(i) ? valueIfNull : _reader.GetDateTime(i);
        }

        public float GetFloat(int i, float valueIfNull)
        {
            return _reader.IsDBNull(i) ? valueIfNull : _reader.GetFloat(i);
        }

        public string GetString(int i, string valueIfNull)
        {
            return _reader.IsDBNull(i) ? valueIfNull : _reader.GetString(i);
        }

        public char GetChar(int i, char valueIfNull)
        {
            return _reader.IsDBNull(i) ? valueIfNull : _reader.GetChar(i);
        }

        public short GetInt16(int i, short valueIfNull)
        {
            return _reader.IsDBNull(i) ? valueIfNull : _reader.GetInt16(i);
        }
        #endregion

        #region 
        private static bool HasColumn(IDataRecord reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }
        #endregion
    }
    #endregion
}