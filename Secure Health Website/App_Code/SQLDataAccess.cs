using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Test_Website
{
/// <summary>
/// Class used for SQL Database connections, getting and sending information
/// </summary>
    public static class SQLDataAccess
    {
        // Insert information into PersonalInformation table
        public static bool InsertPersonalInfo(string userid,string name,string dob,string email,string phone,bool gender, int age)
        {
             string query = "EXECUTE [dbo].[InsertPersonalInfo] '" + userid + "','" + name + "','" + dob + "','" + email + "','" + phone + "',"+ gender + "," +age; 

             int success = SqlInsert(query, new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SHWConnection"].ConnectionString));
             if (success!= 0)
             {
                 return true;
             }
             return false;

        }

        // Get the case Details
        public static DataTable GetCaseDetails(string caseId) 
        {
            string query = "SELECT P.[HashUserId],[PatientDescription],[DrPrescription],[AlertPatient],[AlertDr],[CreationDate],[LastModification], [LastModifiedBy], P.[Name]"
            + " FROM [Case] C INNER JOIN [PersonalInformation] P ON P.[HashUserId] = C.[HashUserId]  WHERE [CaseId] = " + caseId;
            SqlDataReader reader = sqlRead(query, new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SHWConnection"].ConnectionString));
            DataTable dt = new DataTable();

            if (reader.HasRows)
            {
                dt.Load(reader);
            }
            return dt;
        }

        // Update case details
        public static bool UpdateCaseDetails(string PatientText, string DoctorText, string caseId, bool PatientUpdate) 
        {
            string query = (PatientUpdate) ?
                "UPDATE [CASE] SET PatientDescription = '" + PatientText + "', AlertPatient=0, AlertDr = 1, LastModification=GETDATE(), LastModifiedby = 0 WHERE [CaseId] = " + caseId :
                "UPDATE [CASE] SET DrPrescription = '" + DoctorText + "', AlertPatient=1, AlertDr = 0, LastModification=GETDATE(), LastModifiedby = 1 WHERE [CaseId] = " + caseId;
            int resultInt = SqlInsert(query, new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SHWConnection"].ConnectionString));
            bool result = (resultInt != 0) ? true : false;

            return result;
        }

        // Remove the flag of patient
        public static bool ClearPatientFlag(string caseId)
        {
            string query = "UPDATE [CASE] SET AlertPatient=0 WHERE [CaseId] = " + caseId;                
            int resultInt = SqlInsert(query, new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SHWConnection"].ConnectionString));
            bool result = (resultInt != 0) ? true : false;

            return result;
        }

        //Register a case and get the new case ID
        public static string RegisterCaseAndGetID(string userName,string description)
        {
            string query = "EXECUTE [dbo].[RegisterCaseID] '" + userName + "','" + description + "'";
            SqlDataReader reader = sqlRead(query, new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SHWConnection"].ConnectionString));
            string caseID = "";
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    caseID = reader["CaseId"].ToString();
                }
            }
            return caseID;
        }

        //Get the data from Personal Information Table
        public static DataTable GetPersonalInformation(string UserId) 
        {
            string query = "SELECT [Name],[DateofBirth],[Email],[PhoneNumber],[Gender],[Age] "
            + " FROM [dbo].[PersonalInformation] WHERE [HashUserId] = '" + UserId + "'";
            SqlDataReader reader = sqlRead(query, new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SHWConnection"].ConnectionString));
            DataTable ReadTable = new DataTable();

            if (reader.HasRows)
            {
                ReadTable.Load(reader);
            }
            return ReadTable;
        }

        //Update the personal information table
        public static bool UpdatePersonalInfo(string UserID, string UserName,string dob, string email, string phone, bool gender, int age)
        {
            string query = "EXECUTE [dbo].[UpdatePersonalInfo] '" + UserID + "','" + UserName + "','" + dob + "','" + email + "','" + phone + "'," + gender + "," + age;
    
            SqlDataReader reader1 = sqlRead(query, new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SHWConnection"].ConnectionString));
        DataTable ReadTable = new DataTable();

            if (reader1.HasRows)
            {
                return true;
            }
            return false ;
        
        }

        #region SQL functions
        // Open/Close SQL connection and send Query for inserting
        private static SqlDataReader sqlRead(string query, SqlConnection conn)
        {
            SqlDataReader qrr = null;
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                SqlCommand qr = new SqlCommand(query, conn);
                qr.CommandTimeout = 600;
                qrr = qr.ExecuteReader();
                return qrr;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                return qrr;
            }
        }

        //Open connection and insert query to SQLDB
        private static int SqlInsert(string query, SqlConnection conn)
        {
            int Arows = 0;
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                SqlCommand qr = new SqlCommand(query, conn);
                Arows = qr.ExecuteNonQuery();
                return Arows;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                return Arows;
            }
        }
        #endregion
	    
    }
}