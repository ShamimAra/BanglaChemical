using System;
using System.Collections.Generic;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data;
using System.Data.OleDb;
using System.Reflection;
using System.Collections;

namespace Connection
{
    public class Database_ora
    {

        #region " Global variables"

        OracleConnection conn = default(OracleConnection);
        OracleDataReader reader = default(OracleDataReader);
        DataSet dataset = default(DataSet);
        DataTable datatable = default(DataTable);
        OracleCommand command = default(OracleCommand);


        #endregion


        #region "Constructor"

        public Database_ora(bool IsConnOpen, string ConnectionString_Config, string Provider_Name)
        {

            if (Provider_Name == "ODP")
            {
                if (IsConnOpen == true)
                {
                    try
                    {
                        string connectionString = null;
                        connectionString = ConnectionStringByServer(ConnectionString_Config);
                        conn = new OracleConnection(connectionString);
                        conn.Open();
                    }
                    catch (Exception ex)
                    {

                    }
                }


            }
            else if (Provider_Name == "OLEDB")
            {
                if (IsConnOpen == true)
                {
                    OleDbConnection oracleConn = new OleDbConnection();
                    string connectionString = null;
                    connectionString = ConnectionStringByServer(ConnectionString_Config);
                    oracleConn.ConnectionString = connectionString;
                    oracleConn.Open();
                }

            }

        }

        public Database_ora(string ConnectionString_Config)
        {
            string ConnectionString = null;
            ConnectionString = ConnectionStringByServer(ConnectionString_Config);
            conn = new OracleConnection(ConnectionString);
            conn.Open();
        }

        #endregion


        #region "User Defined Connection Strings Function By different server"

        public string ConnectionStringByServer(string server_identity)
        {

            string ConnString = null;

            if (server_identity == "IMS")
            {
                ConnString = "Data Source = IMS_BILLING_TEST_25; User ID = IMS; Password = IMS098; Connection Timeout = 300";
            }

            return ConnString;

        }

        #endregion

        #region "CreateSqlCommand"

        public void CreateSqlCommand(string SpName, OracleParameter[] parms)
        {
            command = new OracleCommand(SpName, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 1200;

            if ((parms != null))
            {
                foreach (OracleParameter parmaeter in parms)
                {
                    command.Parameters.Add(parmaeter);
                }
            }

        }

        public void CreateSqlCommand(string SqlStatement)
        {
            command = new OracleCommand(SqlStatement, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 1200;
        }

        #endregion


        #region "RunProcedure"

        public int RunProcedureWithReturnVal(string SpName, OracleParameter[] parms)
        {

            CreateSqlCommand(SpName, parms);

            Int32 retval = 0;

            try
            {
                command.ExecuteNonQuery();
                retval = ((OracleDecimal)command.Parameters[0].Value).ToInt32();

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                command.Dispose();
                conn.Close();

            }

            return retval;

        }



        public void RunProcedure(string SpName)
        {
            CreateSqlCommand(SpName);

            try
            {
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                command.Dispose();
                conn.Close();

            }

        }

        public void RunProcedurewithparameter(string SpName, ref OracleParameter[] parms)
        {
            CreateSqlCommand(SpName, parms);
            try
            {
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                command.Dispose();
                conn.Close();

            }
        }

        #endregion

        #region "RunFunction"

        public int RunFunction(string SpName, ref OracleParameter[] parms)
        {


            CreateSqlCommand(SpName, parms);
            int retVal = 0;
            try
            {
                command.ExecuteNonQuery();
                retVal = System.Convert.ToInt32(command.Parameters["ReturnValue"].Value.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command.Dispose();
            }

            return retVal;
        }


        public string RunFunctionString(string SpName, OracleParameter[] parms)
        {
            //success return 0 fail return 1 

            CreateSqlCommand(SpName, parms);
            string retVal = null;
            try
            {
                command.ExecuteNonQuery();
                retVal = command.Parameters["ReturnValue"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command.Dispose();
            }

            return retVal;
        }

        #endregion


        #region "GetList"


        public OracleDataReader GetList(string SpName)
        {
            return GetList(SpName, null);
        }

        public OracleDataReader GetList(string SpName, OracleParameter[] parms)
        {
            CreateSqlCommand(SpName, parms);
            try
            {
                reader = command.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command.Dispose();
            }
            return reader;
        }

        public List<T> GetList<T>(string SpName, OracleParameter[] parms)
        {
            CreateSqlCommand(SpName, parms);

            List<T> list = new List<T>();

            try
            {
                using (OracleDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        T local = (T)Activator.CreateInstance(typeof(T));
                        PropertyInfo[] properties = local.GetType().GetProperties();
                        foreach (PropertyInfo info in properties)
                        {
                            info.SetValue(local, this.GetValue(info.Name, reader), null);
                        }
                        list.Add(local);
                    }
                    return list;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command.Dispose();
                //     CleanDatabase();
            }

            return list;
        }

        private object GetValue(string proname, OracleDataReader dreader)
        {
            try
            {
                object p = dreader[proname];
                if ((object.ReferenceEquals(p.GetType(), typeof(DBNull))))
                {
                    return null;
                }
                return p;
            }
            catch (Exception ex)
            {
                return null;
            }

        }


        public OracleDataReader GetListWithReturnValue(string SpName, OracleParameter[] parms, ref int ReturnValue)
        {

            CreateSqlCommand(SpName, parms);

            try
            {
                reader = command.ExecuteReader();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ReturnValue = ((OracleDecimal)command.Parameters[0].Value).ToInt32();
                command.Dispose();
                //CleanDatabase()
            }

            return reader;
        }
        #endregion

        #region " Clean Database"



        public void CleanDatabase()
        {

            try
            {
                if ((conn != null))
                {
                    conn.Close();
                }

                if ((reader != null))
                {
                    reader.Close();
                }

                if ((command != null))
                {
                    command.Dispose();
                }

                if ((datatable != null))
                {
                    datatable.Dispose();
                }


            }
            catch (Exception ex)
            {
            }
        }

        #endregion


        #region "getDataSet"

        public DataSet GetDataSet(string SpName)
        {
            return GetDataSet(SpName, null);
        }

        public DataSet GetDataSet(string SpName, OracleParameter[] parms)
        {
            return GetDataSet(SpName, parms, null);
        }


        public DataSet GetDataSet(string SpName, OracleParameter[] parms, Queue MapList)
        {
            return GetDataSet(SpName, parms, MapList, "IBudget");
        }

        public DataSet GetDataSet(string SpName, OracleParameter[] parms, Queue MapList, string DataSetName)
        {

            CreateSqlCommand(SpName, parms);

            OracleDataAdapter da = new OracleDataAdapter(command);
            try
            {
                dataset = new DataSet(DataSetName);

                int i = 0;
                string MapName = "Table";
                while ((MapList != null) && MapList.Count > 0)
                {
                    da.TableMappings.Add(MapName, MapList.Dequeue().ToString());
                    i += 1;
                    MapName = "Table" + i;
                }

                da.Fill(dataset);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command.Dispose();
                CleanDatabase();
            }

            return dataset;

        }
        #endregion

        #region "Execute scaler"

        public object ExecuteScaler(string SpName, OracleParameter[] parms)
        {

            if (conn != null && conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            CreateSqlCommand(SpName, parms);
            try
            {
                return command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command.Dispose();
                //CleanDatabase()
            }
        }
        #endregion

        #region " Make Parameter"

        // Makes input type param
        public OracleParameter MakeInParameter(string ParmName, object Value, OracleDbType ParamType)
        {
            return MakeParam(ParmName, ParameterDirection.Input, Value, ParamType, 0, OracleCollectionType.None);
        }

        public OracleParameter MakeReturnValue(string ParmName, OracleDbType ParamType, int ParamSize)
        {
            return MakeParam(ParmName, ParameterDirection.ReturnValue, null, ParamType, ParamSize, OracleCollectionType.None);
        }


        public OracleParameter MakeOutParameter(string ParmName, OracleDbType ParamType, ParameterDirection ParmDirection)
        {

            return MakeParam(ParmName, ParmDirection, null, ParamType, 0, OracleCollectionType.None);
        }

        public OracleParameter MakeCollectionParameter(string ParmName, object Value, OracleDbType ParamType, int ParamSize)
        {
            return MakeParam(ParmName, ParameterDirection.Input, Value, ParamType, ParamSize, OracleCollectionType.PLSQLAssociativeArray);
        }



        public OracleParameter MakeParam(string ParmName, ParameterDirection Direction, object Value, OracleDbType ParamType, int ParamSize, OracleCollectionType ParamCollectionType)
        {
            OracleParameter Param = default(OracleParameter);
            try
            {
                if (Direction == ParameterDirection.Input)
                {
                    Param = new OracleParameter(ParmName, ParamType, ParamSize, Direction);
                    Param.Value = Value;
                }
                else if (Direction == ParameterDirection.Output)
                {
                    Param = new OracleParameter(ParmName, ParamType, ParamSize, Direction);
                    Param.Value = Value;
                }
                else if (Direction == ParameterDirection.ReturnValue)
                {
                    Param = new OracleParameter(ParmName, ParamType, ParamSize, null, Direction);
                    Param.Value = Value;
                }

                if (ParamCollectionType == OracleCollectionType.PLSQLAssociativeArray)
                {
                    Param.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
                }

            }
            catch (Exception ex)
            {
                throw ex;

            }
            return Param;

        }

        #endregion
    }
}
