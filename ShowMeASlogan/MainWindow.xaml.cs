using System.Windows;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System;

namespace ShowMeASlogan
{
    public partial class MainWindow : Window
    {
        // wichtig für den Aufbau einer Verbindung zum SQL Server bzw. zur Datenbank
        SqlConnection sqlCon;

        public MainWindow()
        {
            InitializeComponent();
            // über welche Datenverbindung soll die Anwendung eine Verbindung mit der Datenbank erstellen?

            string conString = ConfigurationManager.ConnectionStrings["ShowMeASlogan.Properties.Settings.SlogansConnectionString"].ConnectionString;
            sqlCon = new SqlConnection(conString);
        }

        public void ShowSlogans()
        {
            DataTable dataSlogan = new DataTable();
            DataSet dataSet = new DataSet();
            
            Random rdnum = new Random();
            int raNum = rdnum.Next(1, 58);

            string query = "SELECT * FROM SlogansTable WHERE Id = " + raNum;
            SqlDataAdapter sqlData = new SqlDataAdapter(query, sqlCon);

            using(sqlData)
            {  
                sqlData.Fill(dataSlogan);
                libx_Data.DisplayMemberPath = "Slogan";
                libx_Data.SelectedValuePath = "Id";
                libx_Data.ItemsSource = dataSlogan.DefaultView;
            }
        }

        private void Btn_Show_Click(object sender, RoutedEventArgs e)
        {
            ShowSlogans();
        }
    }
}
