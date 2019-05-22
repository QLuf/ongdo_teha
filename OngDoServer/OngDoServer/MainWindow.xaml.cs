using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.Net.Sockets;
using System.Net;
using Newtonsoft.Json;
using System.Data;
using System.IO;

namespace OngDoServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        const int PORT = 12345;
        //const string IP_ADDRESS = IPAddress.Any;

        public MainWindow()
        {
            InitializeComponent();
            //outTextBox.Text = string.Empty;

            InitWordDataTable();
            InitCountDataTable();
            Task.Factory.StartNew(() => StartSever());
        }

        DataTable wordDataTable = new DataTable();
        DataTable countDataTable = new DataTable();

        private void CreateNewDataTable()
        {
            DataColumn id = new DataColumn("ID", typeof(int))
            {
                AutoIncrement = true,
                AutoIncrementSeed = 1,
                AutoIncrementStep = 1
            };
            DataColumn code = new DataColumn("Code", typeof(string));
            DataColumn word = new DataColumn("Word", typeof(string));

            wordDataTable.Columns.Add(id);
            wordDataTable.Columns.Add(code);
            wordDataTable.Columns.Add(word);
        }

        public void InitWordDataTable()
        {
            try
            {
                string json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/db.json");

                if (string.IsNullOrEmpty(json))
                {
                    CreateNewDataTable();
                }
                else
                {
                    wordDataTable = JsonConvert.DeserializeObject<DataTable>(json);
                    if (wordDataTable == null)
                    {
                        CreateNewDataTable();
                    }
                    else
                    {
                        if (wordDataTable.Rows.Count == 0)
                        {
                            CreateNewDataTable();
                        }
                        else
                        {
                            wordDataTable.Columns["ID"].AutoIncrement = true;

                            DataRow lastRow = wordDataTable.Rows[wordDataTable.Rows.Count - 1];

                            wordDataTable.Columns["ID"].AutoIncrementSeed = (long) lastRow["ID"] + 1;
                            wordDataTable.Columns["ID"].AutoIncrementStep = 1;
                        }
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                CreateNewDataTable();
            }

            wordDataGrid.ItemsSource = wordDataTable.DefaultView;
        }

        void InitCountDataTable()
        {
            DataColumn word = new DataColumn("Word", typeof(string));
            DataColumn count = new DataColumn("Count", typeof(string));

            countDataTable.Columns.Add(word);
            countDataTable.Columns.Add(count);

            CountWord();

            countDataGrid.ItemsSource = countDataTable.DefaultView;
        }

        void CountWord()
        {
            var query = from row in wordDataTable.AsEnumerable()
                        group row by row.Field<string>("Word") into words
                        orderby words.Key
                        select new
                        {
                            Word = words.Key,
                            Count = words.Count()
                        };

            if (countDataTable.Rows.Count > 0)
            {
                countDataTable.Rows.Clear();
            }

            foreach (var w in query)
            {
                countDataTable.Rows.Add(w.Word, w.Count);
            }
        }

        TcpListener server = null;

        public void StartSever()
        {
            //TextBoxStreamWriter streamTextBoxWriter = new TextBoxStreamWriter(outTextBox);
            //Console.SetOut(streamTextBoxWriter);
            Console.WriteLine("Starting server up...");

            try
            {
                //IPAddress localAddr = IPAddress.Parse(IPAddress.Any);

                server = new TcpListener(IPAddress.Any, PORT);
                server.Start();

                byte[] bytes = new byte[256];
                string data = null;

                while (true)
                {
                    Console.Write("Waiting for a connection... ");

                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    data = null;

                    NetworkStream stream = client.GetStream();

                    int i;

                    // Loop to receive all the data sent by the client.
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // Translate data bytes to a UTF8 string.
                        data = System.Text.Encoding.UTF8.GetString(bytes, 0, i);

                        Console.WriteLine("Received: {0}", data);

                        byte[] msg = System.Text.Encoding.UTF8.GetBytes(data);

                        // Send back a response.
                        stream.Write(msg, 0, msg.Length);
                        Console.WriteLine("Sent: {0}", data);

                        // load data to gui
                        CodeWord result = JsonConvert.DeserializeObject<CodeWord>(data);

                        Console.WriteLine("Add data to DataTable");
                        AddData(result, wordDataTable);
                    }

                    stream.Close();
                    client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }
        }

        private void AddData(CodeWord item, DataTable dt)
        {
            DataRow row = dt.NewRow();
            row["Code"] = item.Code;
            row["Word"] = item.Word;

            dt.Rows.Add(row);

            string json = JsonConvert.SerializeObject(wordDataTable);
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "/db.json", json);

            CountWord();

            //countDataGrid.ItemsSource = null;
            //countDataGrid.ItemsSource = countDataTable.DefaultView;
            countDataGrid.Dispatcher.BeginInvoke((Action)(() =>
            {
                countDataGrid.ItemsSource = null;
                countDataGrid.ItemsSource = countDataTable.DefaultView;
            }));

            wordDataGrid.Dispatcher.BeginInvoke((Action)(() =>
            {
                wordDataGrid.ItemsSource = null;
                wordDataGrid.ItemsSource= wordDataTable.DefaultView;
            }));
        }

        private void MainWindows_Closed(object sender, EventArgs e)
        {
            if (server != null)
                server.Stop();
        }

        private void FilterTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {           
            try
            {               
                //DataView dataView = wordDataTable.DefaultView;
                //var results = from row in wordDataTable.AsEnumerable()
                //             where row.Field<string>("Code").Contains(filterTxtBox.Text) && row.Field<string>("Code").EndsWith(filterTxtBox.Text) ||
                //                   row.Field<string>("Word").Contains(filterTxtBox.Text) && row.Field<string>("Word").EndsWith(filterTxtBox.Text)
                //             select row;
                //dataView = results.AsDataView();

                DataView dv = wordDataTable.DefaultView;
                string query = string.Format("Code LIKE '*{0}*' OR Word LIKE '*{1}*'", filterTxtBox.Text, filterTxtBox.Text);
                dv.RowFilter = query;

                wordDataGrid.ItemsSource = null;
                wordDataGrid.ItemsSource = dv;
            }
            catch (SyntaxErrorException exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch (EvaluateException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }

}
