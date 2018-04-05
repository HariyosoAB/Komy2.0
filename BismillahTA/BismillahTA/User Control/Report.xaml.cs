using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Data;

using System.Windows.Controls.DataVisualization.Charting;

namespace BismillahTA.User_Control
{
    /// <summary>
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class Report : UserControl
    {
        SqlConnector con = new SqlConnector();

        public Report()
        {
            InitializeComponent();
            List<string> dataTaskCombo = new List<string>();
            List<string> dataLevelCombo = new List<string>();
            dataTaskCombo.Add("1");
            dataTaskCombo.Add("2");
            dataTaskCombo.Add("3");
            dataTaskCombo.Add("4");
            dataLevelCombo.Add("1");
            dataLevelCombo.Add("2");
            dataLevelCombo.Add("3");

            taskComboBox.ItemsSource = dataTaskCombo;
            taskComboHistory.ItemsSource = dataTaskCombo;
            levelComboBox.ItemsSource = dataLevelCombo;
            levelComboHistory.ItemsSource = dataLevelCombo;

            taskComboBox.SelectedIndex = taskComboHistory.SelectedIndex = levelComboBox.SelectedIndex = levelComboHistory.SelectedIndex = 0;
            
            DateTime now = DateTime.Now;

            for (int i = 1; i <= 31; i++)
                comboDay.Items.Add(i);

            for (int i = 1; i <= 12; i++)
                comboMonth.Items.Add(i);

            for (int i = int.Parse(now.Year.ToString()); i >= 2000; i--)
                comboYear.Items.Add(i);

            comboDay.SelectedIndex = now.Day - 1;
            comboMonth.SelectedIndex = now.Month - 1;
            comboYear.Text = now.Year.ToString();
        }

        private void reportGrid_Loaded(object sender, RoutedEventArgs e)
        {
            con.fillDgvHistory(reportGrid);
        }

        private void MyChart_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable dtChart1 = con.getDataGraphic(1, 1);
            List<KeyValuePair<string, int>> values = new List<KeyValuePair<string,int>>();

            for (int i = dtChart1.Rows.Count - 1; i >= 0; i--)
            {
                values.Add(new KeyValuePair<string,int>(dtChart1.Rows[i].ItemArray[0].ToString(), int.Parse(dtChart1.Rows[i].ItemArray[1].ToString())));
            }

            myLine.ItemsSource = values;
        }

        private void noteGrid_Loaded(object sender, RoutedEventArgs e)
        {
            con.fillDgvNote(noteGrid);
        }

        private void chooseButton_Click(object sender, RoutedEventArgs e)
        {
            if (myLine != null)
                myLine.ItemsSource = null;

            DataTable dtChart1 = new DataTable();
            dtChart1 = con.getDataGraphic(int.Parse(taskComboBox.Text), int.Parse(levelComboBox.Text));
            List<KeyValuePair<string, int>> values = new List<KeyValuePair<string, int>>();

            for (int i = dtChart1.Rows.Count - 1; i >= 0; i--)
            {
                values.Add(new KeyValuePair<string, int>(dtChart1.Rows[i].ItemArray[0].ToString(), int.Parse(dtChart1.Rows[i].ItemArray[1].ToString())));
            }

            myLine.ItemsSource = values;
        }

        private void chooseButtonHistory_Click(object sender, RoutedEventArgs e)
        {
            con.fillDgvHistoryByTaskAndLevel(reportGrid, int.Parse(taskComboHistory.Text), int.Parse(levelComboHistory.Text));
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            con.fillDgvNoteByDate(noteGrid, comboDay.Text, comboMonth.Text, comboYear.Text);
        }
    }

}
