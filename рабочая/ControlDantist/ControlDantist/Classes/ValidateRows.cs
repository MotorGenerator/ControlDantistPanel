using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ControlDantist.Classes
{
    /// <summary>
    /// ����� ��������� ������ ������� �� ������� ������
    /// </summary>
    public class ValidateRows
    {
        /// <summary>
        /// ���������� �������� �� ����� ������ ����������� � ������ ����������
        /// </summary>
        /// <param name="query"></param>
        /// <param name="con"></param>
        /// <param name="transact"></param>
        /// <returns></returns>
        public static int Row(string query, SqlConnection con, SqlTransaction transact)
        {
            SqlCommand com = new SqlCommand(query, con);
            com.Transaction = transact;

            int iCountRow = (int)com.ExecuteScalar();
            return iCountRow;
        }

        /// <summary>
        /// ���������� ������ �������� �� ������� 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="con"></param>
        /// <returns></returns>
        public static int Row(string query, SqlConnection con)
        {
            SqlCommand com = new SqlCommand(query, con);
            //com.Transaction = transact;

            int iCountRow = (int)com.ExecuteScalar();
            return iCountRow;
        }
    }
}
