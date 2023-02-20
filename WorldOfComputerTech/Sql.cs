using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfComputerTech
{
    class Sql
    {
        //---- Исполнительные функции для работы с базой данных
        public static string ConnectionString()
        {

            //string startupPath = Environment.CurrentDirectory;

            return @"Data Source=PK312-9;Initial Catalog=DB_UP02;Integrated Security=True";

            //return @"Data Source=HOME-PC;Initial Catalog=DB_UP02;Integrated Security=True";

        }

        public static string Querry(string QuerryText)
        {
            SqlConnection CurrentConnection = new SqlConnection(Sql.ConnectionString());
            CurrentConnection.Open();
            SqlCommand CurrentCommand = CurrentConnection.CreateCommand();
            CurrentCommand.CommandText = ($@"{QuerryText}");

            if (CurrentCommand.ExecuteScalar() == null)
            {
                string BadOutput = ($@"!beam");
                CurrentConnection.Close();
                return BadOutput;
            }
            else
            {
                string Output = (string)CurrentCommand.ExecuteScalar();
                CurrentConnection.Close();
                return Output;
            }
        }

        public static DateTime QuerryDate(string QuerryText)
        {
            SqlConnection CurrentConnection = new SqlConnection(Sql.ConnectionString());
            CurrentConnection.Open();
            SqlCommand CurrentCommand = CurrentConnection.CreateCommand();
            CurrentCommand.CommandText = ($@"{QuerryText}");

            if (CurrentCommand.ExecuteScalar() == null)
            {
                DateTime BadOutput = DateTime.MinValue;
                CurrentConnection.Close();
                return BadOutput;
            }
            else
            {
                DateTime Output = (DateTime)CurrentCommand.ExecuteScalar();
                CurrentConnection.Close();
                return Output;
            }
        }

        public static string[] QuerryArrString(string QuerryText)
        {
            SqlConnection CurrentConnection = new SqlConnection(Sql.ConnectionString());
            CurrentConnection.Open();
            SqlCommand CurrentCommand = CurrentConnection.CreateCommand();
            CurrentCommand.CommandText = ($@"{QuerryText}");

            if (CurrentCommand.ExecuteScalar() == null)
            {
                string[] BadOutput = {"bad", "output"};
                CurrentConnection.Close();
                return BadOutput;
            }
            else
            {
                string[] Output = (string[])CurrentCommand.ExecuteScalar();
                CurrentConnection.Close();
                return Output;
            }
        }

        public static int[] QuerryArrInt(string QuerryText)
        {
            SqlConnection CurrentConnection = new SqlConnection(Sql.ConnectionString());
            CurrentConnection.Open();
            SqlCommand CurrentCommand = CurrentConnection.CreateCommand();
            CurrentCommand.CommandText = ($@"{QuerryText}");

            if (CurrentCommand.ExecuteScalar() == null)
            {
                int[] BadOutput = { 0, 0 };
                CurrentConnection.Close();
                return BadOutput;
                
            }
            else
            {
                int[] Output = (int[])CurrentCommand.ExecuteScalar();
                CurrentConnection.Close();
                return Output;
            }
        }

        public static int QuerryInt(string QuerryText)
        {
            SqlConnection CurrentConnection = new SqlConnection(Sql.ConnectionString());
            CurrentConnection.Open();
            SqlCommand CurrentCommand = CurrentConnection.CreateCommand();
            CurrentCommand.CommandText = ($@"{QuerryText}");

            if (CurrentCommand.ExecuteScalar() == null)
            {
                int BadOutput = -1;
                CurrentConnection.Close();
                return BadOutput;
            }
            else
            {
                int Output = (int)CurrentCommand.ExecuteScalar();
                CurrentConnection.Close();
                return Output;
            }
        }

        public static decimal QuerryDecimal(string QuerryText)
        {
            SqlConnection CurrentConnection = new SqlConnection(Sql.ConnectionString());
            CurrentConnection.Open();
            SqlCommand CurrentCommand = CurrentConnection.CreateCommand();
            CurrentCommand.CommandText = ($@"{QuerryText}");

            if (CurrentCommand.ExecuteScalar() == null)
            {
                decimal BadOutput = -1;
                CurrentConnection.Close();
                return BadOutput;
            }
            else
            {
                decimal Output = (decimal)CurrentCommand.ExecuteScalar();
                CurrentConnection.Close();
                return Output;
            }
        }

        public static DataSet QuerryForTable(string InputQuerry)
        {
            SqlConnection CurrentConnection = new SqlConnection(Sql.ConnectionString());
            SqlDataAdapter Adapter;
            DataSet DataSet;
            CurrentConnection.Open();
            Adapter = new SqlDataAdapter(InputQuerry, CurrentConnection);
            DataSet = new DataSet();
            Adapter.Fill(DataSet);
            return DataSet;
        }
    }
}
