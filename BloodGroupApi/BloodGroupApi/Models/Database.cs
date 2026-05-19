using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace BloodGroupApi.Models
{
    public class Database
    {

        #region Declaration --srinivas
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["BloodGroup"].ConnectionString);
        #endregion

        #region
        public DataSet SaveDatawithXMLParam(string SPName, string xmlParam, string strLoginStateId)
        {
            if (strLoginStateId != "NA")
            {
                SqlCommand cmd = new SqlCommand(SPName.ToString(), con);
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param1 = new SqlParameter();
                param1.Direction = ParameterDirection.Input;
                param1.DbType = DbType.String;
                param1.Value = xmlParam;
                param1.ParameterName = "@Parameters";
                cmd.Parameters.Add(param1);

                //param1.Value = xmlParam;
                //param1.ParameterName = "@Parameters";
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(ds);



            }
            return ds;

        }
        #endregion
        #region string SPName, string xmlParam
        public DataSet SaveDataReturnSingleXML(string SPName, string xmlParam)
        {
            DataSet ds = new DataSet();
            try
            {

                SqlCommand cmd = new SqlCommand(SPName.ToString(), con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param1 = new SqlParameter();
                param1.Direction = ParameterDirection.Input;
                param1.DbType = DbType.String;
                param1.Value = xmlParam;
                param1.ParameterName = "@xmlParam";
                cmd.Parameters.Add(param1);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(ds);


                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                con.Close();
            }
        }
        #endregion

        #region
        public DataSet GetDatawithXMLParam(string SPName, string xmlParam)
        {

            SqlCommand cmd = new SqlCommand(SPName.ToString(), con);
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param1 = new SqlParameter();
            param1.Direction = ParameterDirection.Input;
            param1.DbType = DbType.String;
            param1.Value = xmlParam;
            param1.ParameterName = "@Parameters";
            cmd.Parameters.Add(param1);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(ds);

            return ds;

        }
        #endregion

        #region Method : GetData(Only SPName) -- string SPName
        public DataSet GetData(string SPName)
        {
            DataSet ds = new DataSet();
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand(SPName.ToString(), con);
                command.CommandTimeout = int.MaxValue;
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                con.Close();
            }
        }
        #endregion
        #region  SaveDataReturnDoubleSingleXML string SPName, string xmlParam
        public DataSet SaveDataReturnDoubleSingleXML(string SPName, string xmlParam, string xmlParam2)
        {
            DataSet ds = new DataSet();
            try
            {

                SqlCommand cmd = new SqlCommand(SPName.ToString(), con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param1 = new SqlParameter();
                param1.Direction = ParameterDirection.Input;
                param1.DbType = DbType.String;
                param1.Value = xmlParam;
                param1.ParameterName = "@xmlParam";
                cmd.Parameters.Add(param1);

                SqlParameter param2 = new SqlParameter();
                param2.Direction = ParameterDirection.Input;
                param2.DbType = DbType.String;
                param2.Value = xmlParam2;
                param2.ParameterName = "@xmlParam2";
                cmd.Parameters.Add(param2);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                con.Close();
            }
        }
        #endregion

        #region string SPName, string xmlParam, string xmlParam1, string xmlParam2
        public DataSet SaveDataReturnThreeSingleXML(string SPName, string xmlParam, string xmlParam1, string xmlParam2)
        {
            DataSet ds = new DataSet();
            try
            {

                SqlCommand cmd = new SqlCommand(SPName.ToString(), con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param1 = new SqlParameter();
                param1.Direction = ParameterDirection.Input;
                param1.DbType = DbType.String;
                param1.Value = xmlParam;
                param1.ParameterName = "@xmlParam";
                cmd.Parameters.Add(param1);

                SqlParameter param2 = new SqlParameter();
                param2.Direction = ParameterDirection.Input;
                param2.DbType = DbType.String;
                param2.Value = xmlParam1;
                param2.ParameterName = "@xmlParam1";
                cmd.Parameters.Add(param2);

                SqlParameter param3 = new SqlParameter();
                param3.Direction = ParameterDirection.Input;
                param3.DbType = DbType.String;
                param3.Value = xmlParam2;
                param3.ParameterName = "@xmlParam2";
                cmd.Parameters.Add(param3);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                con.Close();
            }
        }
        #endregion

        #region string SPName, string xmlParam, string xmlParam1
        public DataSet SaveDataReturnTwoSingleXML(string SPName, string xmlParam, string xmlParam1)
        {
            DataSet ds = new DataSet();
            try
            {

                SqlCommand cmd = new SqlCommand(SPName.ToString(), con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param1 = new SqlParameter();
                param1.Direction = ParameterDirection.Input;
                param1.DbType = DbType.String;
                param1.Value = xmlParam;
                param1.ParameterName = "@xmlParam";
                cmd.Parameters.Add(param1);

                SqlParameter param2 = new SqlParameter();
                param2.Direction = ParameterDirection.Input;
                param2.DbType = DbType.String;
                param2.Value = xmlParam1;
                param2.ParameterName = "@xmlParam1";
                cmd.Parameters.Add(param2);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                con.Close();
            }
        }
        #endregion



        #region Check Data in DataSet @VIJAy
        /// <summary>
        /// Checking The Data in DataSet @DATA available or not
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="TableNo"></param>
        /// <returns>True or False</returns>
        public bool CheckDatainDS(DataSet ds, int TableNo)
        {
            bool Status = false;
            if (ds.Tables.Contains("Table") == true)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[TableNo].Rows.Count > 0)
                    {
                        Status = true;
                    }
                    else
                    {
                        Status = false;
                    }
                }
                else
                {
                    Status = false;
                }
            }
            else
            {
                Status = false;
            }
            return Status;
        }
        #endregion
        #region Method : GetDataWithThreeParam (3 Paraneters) -- string SPName, string Param1, string Param2, string Param3
        public DataSet GetDataWithThreeParam(string SPName, string Param1, string Param2, string Param3)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(SPName.ToString(), con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = int.MaxValue;
                SqlParameter Parm1 = new SqlParameter();
                Parm1.Direction = ParameterDirection.Input;
                Parm1.DbType = DbType.String;
                Parm1.Value = Param1;
                Parm1.ParameterName = "@Param1";
                cmd.Parameters.Add(Parm1);

                SqlParameter Parm2 = new SqlParameter();
                Parm2.Direction = ParameterDirection.Input;
                Parm2.DbType = DbType.String;
                Parm2.Value = Param2;
                Parm2.ParameterName = "@Param2";
                cmd.Parameters.Add(Parm2);

                SqlParameter Parm3 = new SqlParameter();
                Parm3.Direction = ParameterDirection.Input;
                Parm3.DbType = DbType.String;
                Parm3.Value = Param3;
                Parm3.ParameterName = "@Param3";
                cmd.Parameters.Add(Parm3);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                return ds;
            }
            catch (Exception Ex)
            {
                return ds;
            }
            finally
            {
                GC.Collect();
                con.Close();
            }
        }
        #endregion
        #region Method : GetDataWithFourParam (4 Paraneters) -- string SPName, string Param1, string Param2, string Param3,string Param4
        public DataSet GetDataWithFourParam(string SPName, string Param1, string Param2, string Param3, string Param4)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(SPName.ToString(), con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = int.MaxValue;
                SqlParameter Parm1 = new SqlParameter();
                Parm1.Direction = ParameterDirection.Input;
                Parm1.DbType = DbType.String;
                Parm1.Value = Param1;
                Parm1.ParameterName = "@Param1";
                cmd.Parameters.Add(Parm1);

                SqlParameter Parm2 = new SqlParameter();
                Parm2.Direction = ParameterDirection.Input;
                Parm2.DbType = DbType.String;
                Parm2.Value = Param2;
                Parm2.ParameterName = "@Param2";
                cmd.Parameters.Add(Parm2);

                SqlParameter Parm3 = new SqlParameter();
                Parm3.Direction = ParameterDirection.Input;
                Parm3.DbType = DbType.String;
                Parm3.Value = Param3;
                Parm3.ParameterName = "@Param3";
                cmd.Parameters.Add(Parm3);

                SqlParameter Parm4 = new SqlParameter();
                Parm4.Direction = ParameterDirection.Input;
                Parm4.DbType = DbType.String;
                Parm4.Value = Param4;
                Parm4.ParameterName = "@Param4";
                cmd.Parameters.Add(Parm4);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                return ds;
            }
            catch (Exception Ex)
            {
                return ds;
            }
            finally
            {
                GC.Collect();
                con.Close();
            }
        }
        #endregion

        #region Method : GetDataWithfiveParam (6 Paraneters) -- string SPName, string Param1, string Param2, string Param3,string Param4
        public DataSet GetDataWithFiveParam(string SPName, string Param1, string Param2, string Param3, string Param4, string Param5, string Param6)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(SPName.ToString(), con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = int.MaxValue;
                SqlParameter Parm1 = new SqlParameter();
                Parm1.Direction = ParameterDirection.Input;
                Parm1.DbType = DbType.String;
                Parm1.Value = Param1;
                Parm1.ParameterName = "@Param1";
                cmd.Parameters.Add(Parm1);

                SqlParameter Parm2 = new SqlParameter();
                Parm2.Direction = ParameterDirection.Input;
                Parm2.DbType = DbType.String;
                Parm2.Value = Param2;
                Parm2.ParameterName = "@Param2";
                cmd.Parameters.Add(Parm2);

                SqlParameter Parm3 = new SqlParameter();
                Parm3.Direction = ParameterDirection.Input;
                Parm3.DbType = DbType.String;
                Parm3.Value = Param3;
                Parm3.ParameterName = "@Param3";
                cmd.Parameters.Add(Parm3);

                SqlParameter Parm4 = new SqlParameter();
                Parm4.Direction = ParameterDirection.Input;
                Parm4.DbType = DbType.String;
                Parm4.Value = Param4;
                Parm4.ParameterName = "@Param4";
                cmd.Parameters.Add(Parm4);

                SqlParameter Parm5 = new SqlParameter();
                Parm5.Direction = ParameterDirection.Input;
                Parm5.DbType = DbType.String;
                Parm5.Value = Param5;
                Parm5.ParameterName = "@Param5";
                cmd.Parameters.Add(Parm5);


                SqlParameter Parm6 = new SqlParameter();
                Parm6.Direction = ParameterDirection.Input;
                Parm6.DbType = DbType.String;
                Parm6.Value = Param6;
                Parm6.ParameterName = "@Param6";
                cmd.Parameters.Add(Parm6);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                return ds;
            }
            catch (Exception Ex)
            {
                return ds;
            }
            finally
            {
                GC.Collect();
                con.Close();
            }
        }
        #endregion

        #region Method : GetDataWithSevenParam (7 Paraneters) -- string SPName, string Param1, string Param2, string Param3,string Param4, string Param5, string Param6, string Param7
        public DataSet GetDataWithSevenParam(string SPName, string Param1, string Param2, string Param3, string Param4, string Param5, string Param6, string Param7)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(SPName.ToString(), con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = int.MaxValue;
                SqlParameter Parm1 = new SqlParameter();
                Parm1.Direction = ParameterDirection.Input;
                Parm1.DbType = DbType.String;
                Parm1.Value = Param1;
                Parm1.ParameterName = "@Param1";
                cmd.Parameters.Add(Parm1);

                SqlParameter Parm2 = new SqlParameter();
                Parm2.Direction = ParameterDirection.Input;
                Parm2.DbType = DbType.String;
                Parm2.Value = Param2;
                Parm2.ParameterName = "@Param2";
                cmd.Parameters.Add(Parm2);

                SqlParameter Parm3 = new SqlParameter();
                Parm3.Direction = ParameterDirection.Input;
                Parm3.DbType = DbType.String;
                Parm3.Value = Param3;
                Parm3.ParameterName = "@Param3";
                cmd.Parameters.Add(Parm3);

                SqlParameter Parm4 = new SqlParameter();
                Parm4.Direction = ParameterDirection.Input;
                Parm4.DbType = DbType.String;
                Parm4.Value = Param4;
                Parm4.ParameterName = "@Param4";
                cmd.Parameters.Add(Parm4);

                SqlParameter Parm5 = new SqlParameter();
                Parm5.Direction = ParameterDirection.Input;
                Parm5.DbType = DbType.String;
                Parm5.Value = Param5;
                Parm5.ParameterName = "@Param5";
                cmd.Parameters.Add(Parm5);


                SqlParameter Parm6 = new SqlParameter();
                Parm6.Direction = ParameterDirection.Input;
                Parm6.DbType = DbType.String;
                Parm6.Value = Param6;
                Parm6.ParameterName = "@Param6";
                cmd.Parameters.Add(Parm6);

                SqlParameter Parm7 = new SqlParameter();
                Parm6.Direction = ParameterDirection.Input;
                Parm6.DbType = DbType.String;
                Parm6.Value = Param7;
                Parm6.ParameterName = "@Param7";
                cmd.Parameters.Add(Parm7);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                return ds;
            }
            catch (Exception Ex)
            {
                return ds;
            }
            finally
            {
                GC.Collect();
                con.Close();
            }
        }
        #endregion


        #region Method : GetDataWithNineParam (9 Paraneters) -- string SPName, string Param1, string Param2, string Param3,string Param4, string Param5, string Param6, string Param7, string Param8, string Param9
        public DataSet GetDataWithNineParam(string SPName, string Param1, string Param2, string Param3, string Param4, string Param5, string Param6, string Param7, string Param8, string Param9)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(SPName.ToString(), con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = int.MaxValue;
                SqlParameter Parm1 = new SqlParameter();
                Parm1.Direction = ParameterDirection.Input;
                Parm1.DbType = DbType.String;
                Parm1.Value = Param1;
                Parm1.ParameterName = "@Param1";
                cmd.Parameters.Add(Parm1);

                SqlParameter Parm2 = new SqlParameter();
                Parm2.Direction = ParameterDirection.Input;
                Parm2.DbType = DbType.String;
                Parm2.Value = Param2;
                Parm2.ParameterName = "@Param2";
                cmd.Parameters.Add(Parm2);

                SqlParameter Parm3 = new SqlParameter();
                Parm3.Direction = ParameterDirection.Input;
                Parm3.DbType = DbType.String;
                Parm3.Value = Param3;
                Parm3.ParameterName = "@Param3";
                cmd.Parameters.Add(Parm3);

                SqlParameter Parm4 = new SqlParameter();
                Parm4.Direction = ParameterDirection.Input;
                Parm4.DbType = DbType.String;
                Parm4.Value = Param4;
                Parm4.ParameterName = "@Param4";
                cmd.Parameters.Add(Parm4);

                SqlParameter Parm5 = new SqlParameter();
                Parm5.Direction = ParameterDirection.Input;
                Parm5.DbType = DbType.String;
                Parm5.Value = Param5;
                Parm5.ParameterName = "@Param5";
                cmd.Parameters.Add(Parm5);


                SqlParameter Parm6 = new SqlParameter();
                Parm6.Direction = ParameterDirection.Input;
                Parm6.DbType = DbType.String;
                Parm6.Value = Param6;
                Parm6.ParameterName = "@Param6";
                cmd.Parameters.Add(Parm6);

                SqlParameter Parm7 = new SqlParameter();
                Parm7.Direction = ParameterDirection.Input;
                Parm7.DbType = DbType.String;
                Parm7.Value = Param7;
                Parm7.ParameterName = "@Param7";
                cmd.Parameters.Add(Parm7);

                SqlParameter Parm8 = new SqlParameter();
                Parm8.Direction = ParameterDirection.Input;
                Parm8.DbType = DbType.String;
                Parm8.Value = Param8;
                Parm8.ParameterName = "@Param8";
                cmd.Parameters.Add(Parm8);

                SqlParameter Parm9 = new SqlParameter();
                Parm9.Direction = ParameterDirection.Input;
                Parm9.DbType = DbType.String;
                Parm9.Value = Param9;
                Parm9.ParameterName = "@Param9";
                cmd.Parameters.Add(Parm9);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                return ds;
            }
            catch (Exception Ex)
            {
                return ds;
            }
            finally
            {
                GC.Collect();
                con.Close();
            }
        }
        #endregion



        #region Method : GetDataWithTenParam (10 Paraneters) -- string SPName, string Param1, string Param2, string Param3,string Param4, string Param5, string Param6, string Param7, string Param8, string Param9, string Param10
        public DataSet GetDataWithTenParam(string SPName, string Param1, string Param2, string Param3, string Param4, string Param5, string Param6, string Param7, string Param8, string Param9, string Param10)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(SPName.ToString(), con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = int.MaxValue;
                SqlParameter Parm1 = new SqlParameter();
                Parm1.Direction = ParameterDirection.Input;
                Parm1.DbType = DbType.String;
                Parm1.Value = Param1;
                Parm1.ParameterName = "@Param1";
                cmd.Parameters.Add(Parm1);

                SqlParameter Parm2 = new SqlParameter();
                Parm2.Direction = ParameterDirection.Input;
                Parm2.DbType = DbType.String;
                Parm2.Value = Param2;
                Parm2.ParameterName = "@Param2";
                cmd.Parameters.Add(Parm2);

                SqlParameter Parm3 = new SqlParameter();
                Parm3.Direction = ParameterDirection.Input;
                Parm3.DbType = DbType.String;
                Parm3.Value = Param3;
                Parm3.ParameterName = "@Param3";
                cmd.Parameters.Add(Parm3);

                SqlParameter Parm4 = new SqlParameter();
                Parm4.Direction = ParameterDirection.Input;
                Parm4.DbType = DbType.String;
                Parm4.Value = Param4;
                Parm4.ParameterName = "@Param4";
                cmd.Parameters.Add(Parm4);

                SqlParameter Parm5 = new SqlParameter();
                Parm5.Direction = ParameterDirection.Input;
                Parm5.DbType = DbType.String;
                Parm5.Value = Param5;
                Parm5.ParameterName = "@Param5";
                cmd.Parameters.Add(Parm5);


                SqlParameter Parm6 = new SqlParameter();
                Parm6.Direction = ParameterDirection.Input;
                Parm6.DbType = DbType.String;
                Parm6.Value = Param6;
                Parm6.ParameterName = "@Param6";
                cmd.Parameters.Add(Parm6);

                SqlParameter Parm7 = new SqlParameter();
                Parm7.Direction = ParameterDirection.Input;
                Parm7.DbType = DbType.String;
                Parm7.Value = Param7;
                Parm7.ParameterName = "@Param7";
                cmd.Parameters.Add(Parm7);

                SqlParameter Parm8 = new SqlParameter();
                Parm8.Direction = ParameterDirection.Input;
                Parm8.DbType = DbType.String;
                Parm8.Value = Param8;
                Parm8.ParameterName = "@Param8";
                cmd.Parameters.Add(Parm8);

                SqlParameter Parm9 = new SqlParameter();
                Parm9.Direction = ParameterDirection.Input;
                Parm9.DbType = DbType.String;
                Parm9.Value = Param9;
                Parm9.ParameterName = "@Param9";
                cmd.Parameters.Add(Parm9);


                SqlParameter Parm10 = new SqlParameter();
                Parm10.Direction = ParameterDirection.Input;
                Parm10.DbType = DbType.String;
                Parm10.Value = Param10;
                Parm10.ParameterName = "@Param10";
                cmd.Parameters.Add(Parm9);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                return ds;
            }
            catch (Exception Ex)
            {
                return ds;
            }
            finally
            {
                GC.Collect();
                con.Close();
            }
        }
        #endregion


        #region Method : GetDataWithFiveParam (5 Paraneters) -- string SPName, string Param1, string Param2, string Param3,string Param4
        public DataSet GetDataWithOnlyfiveParam(string SPName, string Param1, string Param2, string Param3, string Param4, string Param5)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(SPName.ToString(), con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = int.MaxValue;
                SqlParameter Parm1 = new SqlParameter();
                Parm1.Direction = ParameterDirection.Input;
                Parm1.DbType = DbType.String;
                Parm1.Value = Param1;
                Parm1.ParameterName = "@Param1";
                cmd.Parameters.Add(Parm1);

                SqlParameter Parm2 = new SqlParameter();
                Parm2.Direction = ParameterDirection.Input;
                Parm2.DbType = DbType.String;
                Parm2.Value = Param2;
                Parm2.ParameterName = "@Param2";
                cmd.Parameters.Add(Parm2);

                SqlParameter Parm3 = new SqlParameter();
                Parm3.Direction = ParameterDirection.Input;
                Parm3.DbType = DbType.String;
                Parm3.Value = Param3;
                Parm3.ParameterName = "@Param3";
                cmd.Parameters.Add(Parm3);

                SqlParameter Parm4 = new SqlParameter();
                Parm4.Direction = ParameterDirection.Input;
                Parm4.DbType = DbType.String;
                Parm4.Value = Param4;
                Parm4.ParameterName = "@Param4";
                cmd.Parameters.Add(Parm4);

                SqlParameter Parm5 = new SqlParameter();
                Parm5.Direction = ParameterDirection.Input;
                Parm5.DbType = DbType.String;
                Parm5.Value = Param5;
                Parm5.ParameterName = "@Param5";
                cmd.Parameters.Add(Parm5);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                return ds;
            }
            catch (Exception Ex)
            {
                return ds;
            }
            finally
            {
                GC.Collect();
                con.Close();
            }
        }
        #endregion

        #region Method : GetDataWithTwoParam (1 SPNAME,2 Parameters) string SPName, string Param1, string Param2
        public DataSet GetDataWithTwoParameters(string SPName, string Param1, string Param2)
        {
            DataSet dsWithTwoParamsE = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand(SPName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = int.MaxValue;
                SqlParameter Parm1 = new SqlParameter();
                Parm1.Direction = ParameterDirection.Input;
                Parm1.DbType = DbType.String;
                Parm1.Value = Param1;
                Parm1.ParameterName = "@Param1";
                cmd.Parameters.Add(Parm1);

                SqlParameter Parm2 = new SqlParameter();
                Parm2.Direction = ParameterDirection.Input;
                Parm2.DbType = DbType.String;
                Parm2.Value = Param2;
                Parm2.ParameterName = "@Param2";
                cmd.Parameters.Add(Parm2);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dsWithTwoParamsE);
                return dsWithTwoParamsE;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                con.Close();
            }
        }
        #endregion



        #region Method : GetDataWithTwoParamSingleFlag (1 SPNAME,2 Parameters, 1 Flag) string SPName, string Param1, string Param2, string Flag
        public DataSet GetDataWithTwoParametersSingleFlag(string SPName, string Param1, string Param2, string Flag)
        {
            DataSet dsWithTwoParamsE = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand(SPName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = int.MaxValue;
                SqlParameter Parm1 = new SqlParameter();
                Parm1.Direction = ParameterDirection.Input;
                Parm1.DbType = DbType.String;
                Parm1.Value = Param1;
                Parm1.ParameterName = "@Param1";
                cmd.Parameters.Add(Parm1);

                SqlParameter Parm2 = new SqlParameter();
                Parm2.Direction = ParameterDirection.Input;
                Parm2.DbType = DbType.String;
                Parm2.Value = Param2;
                Parm2.ParameterName = "@Param2";
                cmd.Parameters.Add(Parm2);

                SqlParameter Param3 = new SqlParameter();
                Param3.Direction = ParameterDirection.Input;
                Param3.DbType = DbType.Int32;
                Param3.Value = Flag;
                Param3.ParameterName = "@Flag";
                cmd.Parameters.Add(Flag);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dsWithTwoParamsE);
                return dsWithTwoParamsE;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                con.Close();
            }
        }
        #endregion

        #region Method : GetDataWithTwoParam (1 SPNAME,2 Parameters) string SPName, string Param1, string Param2
        public DataSet GetDataWithTwoParameters1int(string SPName, string Param1, string Param2)
        {
            DataSet dsWithTwoParamsE = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand(SPName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = int.MaxValue;
                SqlParameter Parm1 = new SqlParameter();
                Parm1.Direction = ParameterDirection.Input;
                Parm1.DbType = DbType.String;
                Parm1.Value = Param1;
                Parm1.ParameterName = "@Param1";
                cmd.Parameters.Add(Parm1);

                SqlParameter Parm2 = new SqlParameter();
                Parm2.Direction = ParameterDirection.Input;
                Parm2.DbType = DbType.String;
                Parm2.Value = Param2;
                Parm2.ParameterName = "@Param2";
                cmd.Parameters.Add(Parm2);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dsWithTwoParamsE);
                return dsWithTwoParamsE;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                con.Close();
            }
        }
        #endregion
        #region Method : GetDataWithSingleParam (1 Paramter) -- string SPName, string Param1
        public DataSet GetDataWithSingleParam(string SPName, string Param1)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(SPName.ToString(), con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = int.MaxValue;
                SqlParameter Parm1 = new SqlParameter();
                Parm1.Direction = ParameterDirection.Input;
                Parm1.DbType = DbType.String;
                Parm1.Value = Param1;
                Parm1.ParameterName = "@Param1";
                cmd.Parameters.Add(Parm1);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
            finally
            {
                GC.Collect();
                con.Close();
            }
        }
        #endregion

        #region Method : GetDataWithThreeParameters (1 SPNAME,3 Parameters) string SPName, string Param1, string Param2,string Param3
        public DataSet GetDataWithThreeParameters(string SPName, string Param1, string Param2, string Param3)
        {
            DataSet dsWithTwoParamsE = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand(SPName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = int.MaxValue;
                SqlParameter Parm1 = new SqlParameter();
                Parm1.Direction = ParameterDirection.Input;
                Parm1.DbType = DbType.String;
                Parm1.Value = Param1;
                Parm1.ParameterName = "@Param1";
                cmd.Parameters.Add(Parm1);

                SqlParameter Parm2 = new SqlParameter();
                Parm2.Direction = ParameterDirection.Input;
                Parm2.DbType = DbType.String;
                Parm2.Value = Param2;
                Parm2.ParameterName = "@Param2";
                cmd.Parameters.Add(Parm2);

                SqlParameter Parm3 = new SqlParameter();
                Parm3.Direction = ParameterDirection.Input;
                Parm3.DbType = DbType.String;
                Parm3.Value = Param3;
                Parm3.ParameterName = "@Param3";
                cmd.Parameters.Add(Parm3);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dsWithTwoParamsE);
                return dsWithTwoParamsE;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                con.Close();
            }
        }
        #endregion

        #region  Get Success Message after INSERT/UPDATE/DELETE #BASHA
        public string GetMessage(DataSet ds)
        {
            string Msg = "";
            if (ds.Tables.Contains("Table") == true)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Msg = ds.Tables[0].Rows[0][0].ToString().Trim();
                    }
                    if (ds.Tables.Contains("Table1") == true)
                    {
                        Msg = ds.Tables[1].Rows[0][0].ToString().Trim();
                    }
                }
            }
            if (Msg != null && Msg != "" && Msg != string.Empty)
            {
                return Msg;
            }
            else
            {
                return Msg = "ERROR";
            }
        }
        #endregion


        #region string SPName, string xmlParam, string Param1
        public DataSet SaveDataReturnSingleXMLandSingleParam(string SPName, string xmlParam, string Param1)
        {
            DataSet ds = new DataSet();
            try
            {

                SqlCommand cmd = new SqlCommand(SPName.ToString(), con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param1 = new SqlParameter();
                param1.Direction = ParameterDirection.Input;
                param1.DbType = DbType.String;
                param1.Value = xmlParam;
                param1.ParameterName = "@xmlParam";
                cmd.Parameters.Add(param1);

                SqlParameter param2 = new SqlParameter();
                param2.Direction = ParameterDirection.Input;
                param2.DbType = DbType.String;
                param2.Value = Param1;
                param2.ParameterName = "@Param1";
                cmd.Parameters.Add(param2);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                con.Close();
            }
        }
        #endregion

        #region Method : GetDataWithFiveParam (5 Parameters) -- string SPName, string Param1, string Param2, string Param3,string Param4
        public DataSet GetDataReturnSingleXMLandFiveParam(string SPName, string xmlParam, string Param1, string Param2, string Param3, string Param4)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(SPName.ToString(), con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param1 = new SqlParameter();
                param1.Direction = ParameterDirection.Input;
                param1.DbType = DbType.String;
                param1.Value = xmlParam;
                param1.ParameterName = "@xmlParam";
                cmd.Parameters.Add(param1);


                SqlParameter param2 = new SqlParameter();
                param2.Direction = ParameterDirection.Input;
                param2.DbType = DbType.String;
                param2.Value = Param1;
                param2.ParameterName = "@Param1";
                cmd.Parameters.Add(param2);

                SqlParameter param3 = new SqlParameter();
                param3.Direction = ParameterDirection.Input;
                param3.DbType = DbType.String;
                param3.Value = Param2;
                param3.ParameterName = "@Param2";
                cmd.Parameters.Add(param3);


                SqlParameter param4 = new SqlParameter();
                param4.Direction = ParameterDirection.Input;
                param4.DbType = DbType.String;
                param4.Value = Param3;
                param4.ParameterName = "@Param3";
                cmd.Parameters.Add(param4);


                SqlParameter param5 = new SqlParameter();
                param5.Direction = ParameterDirection.Input;
                param5.DbType = DbType.String;
                param5.Value = Param4;
                param5.ParameterName = "@Param4";
                cmd.Parameters.Add(param5);


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                return ds;
            }
            catch (Exception Ex)
            {
                return ds;
            }
            finally
            {
                GC.Collect();
                con.Close();
            }
        }
        #endregion

        #region string SPName, string xmlParam, int flag
        public DataSet SaveDataReturn(string SPName, string xmlParam, int flag)
        {
            DataSet dsSave = new DataSet();
            try
            {

                SqlCommand cmd = new SqlCommand(SPName.ToString(), con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param1 = new SqlParameter();
                param1.Direction = ParameterDirection.Input;
                param1.DbType = DbType.String;
                param1.Value = xmlParam;
                param1.ParameterName = "@xmlParam";
                cmd.Parameters.Add(param1);

                SqlParameter param2 = new SqlParameter();
                param2.Direction = ParameterDirection.Input;
                param2.DbType = DbType.Int16;
                param2.Value = flag;
                param2.ParameterName = "@flag";
                cmd.Parameters.Add(param2);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dsSave);

                return dsSave;
            }
            catch (Exception)
            {
                return dsSave;
            }
            finally
            {
                GC.Collect();
                con.Close();
            }
        }
        #endregion

        #region string SPName, string xmlParam, int Param1 -- @PVREDDY@
        public DataSet SaveDataReturnSingleXMLwithParam(string SPName, string xmlParam, string Param1)
        {
            DataSet dsSave = new DataSet();
            try
            {

                SqlCommand cmd = new SqlCommand(SPName.ToString(), con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param1 = new SqlParameter();
                param1.Direction = ParameterDirection.Input;
                param1.DbType = DbType.String;
                param1.Value = xmlParam;
                param1.ParameterName = "@xmlParam";
                cmd.Parameters.Add(param1);

                SqlParameter param2 = new SqlParameter();
                param2.Direction = ParameterDirection.Input;
                param2.DbType = DbType.String;
                param2.Value = Param1;
                param2.ParameterName = "@param1";
                cmd.Parameters.Add(param2);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dsSave);

                return dsSave;
            }
            catch (Exception)
            {
                return dsSave;
            }
            finally
            {
                GC.Collect();
                con.Close();
            }
        }
        #endregion


        #region(GET) GetDataWithOneParam(1 SP,1 Parameter)----PVREDDY
        public DataSet GetDataWithOneParameter(string SPName, string Param1)
        {
            DataSet Datawithoneparam = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand(SPName.ToString(), con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter Parm1 = new SqlParameter();
                Parm1.Direction = ParameterDirection.Input;
                Parm1.DbType = DbType.String;
                Parm1.Value = Param1;
                Parm1.ParameterName = "@Param1";
                cmd.Parameters.Add(Parm1);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(Datawithoneparam);
                return Datawithoneparam;
            }
            catch (Exception)
            {
                return ds;
            }
            finally
            {
                GC.Collect();
                con.Close();
            }

        }

        #endregion
        #region string SPName, string xmlParam, string flag
        public DataSet SaveDataReturn1(string SPName, string xmlParam, string flag)
        {
            DataSet dsSave = new DataSet();
            try
            {

                SqlCommand cmd = new SqlCommand(SPName.ToString(), con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param1 = new SqlParameter();
                param1.Direction = ParameterDirection.Input;
                param1.DbType = DbType.String;
                param1.Value = xmlParam;
                param1.ParameterName = "@xmlParam";
                cmd.Parameters.Add(param1);

                SqlParameter param2 = new SqlParameter();
                param2.Direction = ParameterDirection.Input;
                param2.DbType = DbType.String;
                param2.Value = flag;
                param2.ParameterName = "@flag";
                cmd.Parameters.Add(param2);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dsSave);

                return dsSave;
            }
            catch (Exception)
            {
                return dsSave;
            }
            finally
            {
                GC.Collect();
                con.Close();
            }
        }
        #endregion
        #region Method : GetDataWithFourParam (4 Paraneters) -- string SPName, string Param1, string Param2, string Param3,string Param4
        public DataSet GetDataReturnSingleXMLandFourParam(string SPName, string xmlParam, string Param1, string Param2, string Param3)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(SPName.ToString(), con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param1 = new SqlParameter();
                param1.Direction = ParameterDirection.Input;
                param1.DbType = DbType.String;
                param1.Value = xmlParam;
                param1.ParameterName = "@xmlParam";
                cmd.Parameters.Add(param1);


                SqlParameter param2 = new SqlParameter();
                param2.Direction = ParameterDirection.Input;
                param2.DbType = DbType.String;
                param2.Value = Param1;
                param2.ParameterName = "@Param1";
                cmd.Parameters.Add(param2);

                SqlParameter param3 = new SqlParameter();
                param3.Direction = ParameterDirection.Input;
                param3.DbType = DbType.String;
                param3.Value = Param2;
                param3.ParameterName = "@Param2";
                cmd.Parameters.Add(param3);


                SqlParameter param4 = new SqlParameter();
                param4.Direction = ParameterDirection.Input;
                param4.DbType = DbType.String;
                param4.Value = Param3;
                param4.ParameterName = "@Param3";
                cmd.Parameters.Add(param4);


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                return ds;
            }
            catch (Exception Ex)
            {
                return ds;
            }
            finally
            {
                GC.Collect();
                con.Close();
            }
        }
        #endregion


        #region Method : GetDataReturnSingleXMLandthreeParam (4 Paraneters) -- string SPName, string Param1, string Param2, string Param3,string Param4
        public DataSet GetDataReturnSingleXMLandthreeParam(string SPName, string xmlParam, string Param1, string Param2)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(SPName.ToString(), con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param1 = new SqlParameter();
                param1.Direction = ParameterDirection.Input;
                param1.DbType = DbType.String;
                param1.Value = xmlParam;
                param1.ParameterName = "@xmlParam";
                cmd.Parameters.Add(param1);


                SqlParameter param2 = new SqlParameter();
                param2.Direction = ParameterDirection.Input;
                param2.DbType = DbType.String;
                param2.Value = Param1;
                param2.ParameterName = "@Param1";
                cmd.Parameters.Add(param2);

                SqlParameter param3 = new SqlParameter();
                param3.Direction = ParameterDirection.Input;
                param3.DbType = DbType.String;
                param3.Value = Param2;
                param3.ParameterName = "@Param2";
                cmd.Parameters.Add(param3);




                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                return ds;
            }
            catch (Exception Ex)
            {
                return ds;
            }
            finally
            {
                GC.Collect();
                con.Close();
            }
        }
        #endregion


        #region GetsinglexmlParam string SPName, string xmlParam, int flag
        public DataSet GetsinglexmlParam(string SPName, string xmlParam)
        {
            DataSet dsSave = new DataSet();
            try
            {

                SqlCommand cmd = new SqlCommand(SPName.ToString(), con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param1 = new SqlParameter();
                param1.Direction = ParameterDirection.Input;
                param1.DbType = DbType.String;
                param1.Value = xmlParam;
                param1.ParameterName = "@xmlParam";
                cmd.Parameters.Add(param1);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dsSave);

                return dsSave;
            }
            catch (Exception)
            {
                return dsSave;
            }
            finally
            {
                GC.Collect();
                con.Close();
            }
        }
        #endregion

        #region (GET)  GetDataWithPagination (1 SP,2 Parameters) string SPName, string Param1, string Param2--By PVREDDY
        public DataSet GetDataWithPagination(string SPName, int offset, int pageSize)
        {
            DataSet dsWithTwoParamsE = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand(SPName, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter Parm1 = new SqlParameter();
                Parm1.Direction = ParameterDirection.Input;
                Parm1.DbType = DbType.String;
                Parm1.Value = offset;
                Parm1.ParameterName = "@PageNumber";
                cmd.Parameters.Add(Parm1);

                SqlParameter Parm2 = new SqlParameter();
                Parm2.Direction = ParameterDirection.Input;
                Parm2.DbType = DbType.String;
                Parm2.Value = pageSize;
                Parm2.ParameterName = "@PageSize";
                cmd.Parameters.Add(Parm2);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dsWithTwoParamsE);
                return dsWithTwoParamsE;
            }
            catch (Exception)
            {
                return ds;
            }
            finally
            {
                GC.Collect();
                con.Close();
            }
        }
        #endregion



        #region string SPName, string xmlParam, int flag
        public DataSet singleparam1int(string SPName, string Param1, string Param2)
        {
            DataSet dsSave = new DataSet();
            try
            {

                SqlCommand cmd = new SqlCommand(SPName.ToString(), con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param1 = new SqlParameter();
                param1.Direction = ParameterDirection.Input;
                param1.DbType = DbType.String;
                param1.Value = Param1;
                param1.ParameterName = "@Param1";
                cmd.Parameters.Add(param1);

                SqlParameter param2 = new SqlParameter();
                param2.Direction = ParameterDirection.Input;
                param2.DbType = DbType.Int32;
                param2.Value = Param2;
                param2.ParameterName = "@Param2";
                cmd.Parameters.Add(param2);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dsSave);

                return dsSave;
            }
            catch (Exception)
            {
                return dsSave;
            }
            finally
            {
                GC.Collect();
                con.Close();
            }
        }
        #endregion

    }
}
