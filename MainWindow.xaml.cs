using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Windows;

namespace siKecil
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string User_ID;
        public MainWindow(string User_ID)
        {
            InitializeComponent();
            this.User_ID = User_ID;

            Connection connectionHelper = new Connection();

            using (SqlConnection sqlCon = connectionHelper.GetConn())
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();

                string user_id = this.User_ID;
                string sqlQuery = $"SELECT FirstName FROM dbo.Users WHERE User_ID = '{user_id}'";

                using (SqlCommand command = new SqlCommand(sqlQuery, sqlCon))
                {

                    // Eksekusi perintah SQL
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Ambil nilai firstname dari hasil query
                            string firstname = reader["FirstName"].ToString();

                            // Tampilkan pesan di XAML
                            greetingText.Text= $"Hello {firstname}!";
                        }
                    }
                }
            }
        }

        private void ToProfileView(object sender, RoutedEventArgs e)
        {
            ProfileView profile = new ProfileView(User_ID);
            profile.Show();
            this.Close();
        }
    }

    
}