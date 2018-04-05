using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows;
using System.Windows.Controls;

using MySql.Data;
using MySql.Data.MySqlClient;

namespace BismillahTA
{
    class SqlConnector
    {
        public MySqlConnection myConn;

        public void getConnection()
        {
            string stringConn = "server=localhost;port=3306;user id=root;pwd=;database=tav2;";
            myConn = new MySqlConnection(stringConn);
        }

        public bool Login(string username, string password)
        {
            if (myConn == null)
                getConnection();

            int count = 0;

            DataTable dt = new DataTable();

            using (MySqlCommand comm = new MySqlCommand("SELECT * FROM patient WHERE Username ='" + username + "' AND Password='" + password + "';", myConn))
            {
                if (myConn.State == ConnectionState.Closed)
                {
                    //MessageBox.Show("in");
                    myConn.Close();
                    myConn.Open();
                }

                using (var myReader = new MySqlDataAdapter(comm))
                {
                    myReader.Fill(dt);
                    count = dt.Rows.Count;
                }
            }


            myConn.Close();

            if (count == 1)
                return true;
            else
                return false;
        }

        public string GetName()
        {
            string name;

            if (myConn == null)
                getConnection();

            MySqlCommand comm = new MySqlCommand("SELECT PatientName FROM patient WHERE Username='" + Properties.Settings.Default.user + "';", myConn);
            myConn.Open();
            MySqlDataReader reader = comm.ExecuteReader();

            while(reader.Read())
            {
                name = reader.GetString(0);
                return name;
            }

            return null;
        }

        public int SignUp(string name, int age, bool gender, string address, string username, string password)
        {
            if (myConn == null)
                getConnection();

            int count = 0;

            DataTable dt = new DataTable();

            using (MySqlCommand comm = new MySqlCommand("SELECT * FROM patient WHERE Username ='" + username + "';", myConn))
            {
                if (myConn.State == ConnectionState.Closed)
                {
                    myConn.Close();
                    myConn.Open();
                }

                using (var myReader = new MySqlDataAdapter(comm))
                {
                    myReader.Fill(dt);
                    count = dt.Rows.Count;
                }
            }

            if (count != 0)
                return 2;
            else
            {
                MySqlCommand comm = new MySqlCommand("INSERT INTO patient (`PatientName`, `Age`, `Gender`, `Address`, `Username`, `Password`) VALUES('" + name + "'," + age + "," + gender + ",'" + address + "','" + username + "','" + password + "');", myConn);
                int row = comm.ExecuteNonQuery();
                myConn.Close();

                if (row != 1)
                    return 0;
                else
                    return 1;
            }

        }

        public int SignUpTherapist(string name, string username, int age, bool gender, string address, string contact)
        {
            if (myConn == null)
                getConnection();

            int count = 0;

            DataTable dt = new DataTable();

            using (MySqlCommand comm = new MySqlCommand("SELECT * FROM therapist WHERE Username ='" + username + "';", myConn))
            {
                if (myConn.State == ConnectionState.Closed)
                {
                    myConn.Close();
                    myConn.Open();
                }

                using (var myReader = new MySqlDataAdapter(comm))
                {
                    myReader.Fill(dt);
                    count = dt.Rows.Count;
                }

                myConn.Close();
            }

            if (count != 0)
                return 2;
            else
            {
                MySqlCommand comm = new MySqlCommand("INSERT INTO therapist (`Name`, `Username`, `Age`, `Gender`, `Address`, `Contact`) VALUES('" + name + "','" + username + "'," + age + "," + gender + ",'" + address + "','" + contact + "');", myConn);
                myConn.Open();
                int row = comm.ExecuteNonQuery();
                myConn.Close();

                if (row != 1)
                    return 0;
                else
                    return 1;
            }

        }

        public void getItemComboBox(ComboBox comboName)
        {
            if (myConn == null)
                getConnection();

            MySqlCommand comm = new MySqlCommand("SELECT `Username` FROM therapist", myConn);
            myConn.Open();

            MySqlDataReader myReader = comm.ExecuteReader();
            while(myReader.Read())
            {
                string name = myReader.GetString(0);
                comboName.Items.Add(name);
            }

            comboName.Items.Add("--Keluarga--");
            myConn.Close();
        }

        public int getLastHistory()
        {
            if (myConn == null)
                getConnection();

            MySqlCommand comm = new MySqlCommand("SELECT IdHistory FROM history ORDER BY IdHistory DESC LIMIT 1", myConn);
            //myConn.Open();
            int idH = Convert.ToInt32(comm.ExecuteScalar());
            //myConn.Close();

            return idH;

        }

        public bool saveScore(int task, int level, int score)
        {
            MySqlCommand comm;

            if (myConn == null)
                getConnection();

            if (Properties.Settings.Default.terapis == "--Keluarga--")
            {
                comm = new MySqlCommand("INSERT INTO history (IdPatient, Date, Task, Level, Score) VALUES((SELECT IdPatient FROM patient WHERE Username='" + Properties.Settings.Default.user + "'),'"
                                                     + DateTime.Now.ToString("yyyy-MM-dd") + "'," + task + "," + level + "," + score + ");", myConn);

                myConn.Open();
                int row = comm.ExecuteNonQuery();
                myConn.Close();

                if (row == 0)
                    return false;
                else
                    return true;
            }
            else
            {
                comm = new MySqlCommand("INSERT INTO history (IdPatient, Date, Task, Level, Score) VALUES((SELECT IdPatient FROM patient WHERE Username='" + Properties.Settings.Default.user + "'), sysdate() , " + task + ", " + level + ", " + score + ");", myConn);

                myConn.Open();
                int row = comm.ExecuteNonQuery();

                if (Properties.Settings.Default.note == "" || Properties.Settings.Default.note == null)
                {
                    myConn.Close();
                    if (row != 0)
                        return true;
                    else
                        return false;
                }
                else
                {
                    int idH = getLastHistory();

                    MySqlCommand comm2 = new MySqlCommand("INSERT INTO detilHistory(IdHistory, IdTherapist, Notes) VALUES (" + idH + ", (SELECT IdTherapist FROM therapist WHERE Username='" + Properties.Settings.Default.terapis + "'), '"
                                                            + Properties.Settings.Default.note + "');", myConn);

                    int row2 = comm2.ExecuteNonQuery();

                    myConn.Close();

                    if (row != 0 && row2 != 0)
                        return true;
                    else
                        return false;
                }

            }
        }

        private int getIdPatient()
        {
            if (myConn == null)
                getConnection();

            MySqlCommand comm = new MySqlCommand("SELECT IdPatient FROM patient WHERE Username='" + Properties.Settings.Default.user + "';", myConn);
            myConn.Open();
            int id = Convert.ToInt32(comm.ExecuteScalar());
            myConn.Close();

            return id;
        }

        public int checkTaskAndLevel(int task, int level)
        {
            int id = getIdPatient();

            if (myConn == null)
                getConnection();

            MySqlCommand comm = new MySqlCommand("SELECT COUNT(Task) FROM history WHERE Task =" + task + " AND Level =" + level + " AND IdPatient =" + id , myConn);
            myConn.Open();
            int count = Convert.ToInt32(comm.ExecuteScalar());
            myConn.Close();

            return count;
            
        }

        public void fillDgvHistory(DataGrid dgv)
        {
            int id = getIdPatient();

            if (myConn == null)
                getConnection();

            MySqlCommand comm = new MySqlCommand("SELECT h.Date AS Tanggal, h.Task AS Pelatihan, h.Level AS Level, h.Score AS Skor FROM history h WHERE  h.IdPatient=" + id , myConn);

            myConn.Open();
            comm.ExecuteNonQuery();
            MySqlDataAdapter da = new MySqlDataAdapter(comm);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgv.ItemsSource = dt.DefaultView;
            da.Update(dt);

            myConn.Close();

        }

        public void fillDgvHistoryByTaskAndLevel(DataGrid dgv, int task, int level)
        {
            int id = getIdPatient();

            if (myConn == null)
                getConnection();

            MySqlCommand comm = new MySqlCommand("SELECT h.Date AS Tanggal, h.Task AS Pelatihan, h.Level AS Level, h.Score AS Skor FROM history h WHERE  h.IdPatient = " + id 
                                                    +" AND h.Task = " + task + " AND h.Level = " + level , myConn);

            myConn.Open();
            comm.ExecuteNonQuery();
            MySqlDataAdapter da = new MySqlDataAdapter(comm);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgv.ItemsSource = dt.DefaultView;
            da.Update(dt);

            myConn.Close();

        }

        public void fillDgvNote(DataGrid dgv)
        {
            int id = getIdPatient();

            if (myConn == null)
                getConnection();

            MySqlCommand comm = new MySqlCommand("SELECT h.Date AS Tanggal, t.Name AS Terapis, dH.Notes AS Catatan FROM history h, therapist t, detilHistory dH WHERE  h.IdPatient=" + id + " AND h.IdHistory = dH.IdHistory AND dH.IdTherapist = t.IdTherapist", myConn);

            myConn.Open();
            comm.ExecuteNonQuery();
            MySqlDataAdapter da = new MySqlDataAdapter(comm);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgv.ItemsSource = dt.DefaultView;
            da.Update(dt);

            myConn.Close();
        }

        public void fillDgvNoteByDate(DataGrid dgv, String day, String month, String year)
        {
            int id = getIdPatient();

            if (myConn == null)
                getConnection();

            MySqlCommand comm = new MySqlCommand("SELECT h.Date AS Tanggal, t.Name AS Terapis, dH.Notes AS Catatan FROM history h, therapist t, detilHistory dH WHERE  h.IdPatient=" + id + 
                                                " AND h.IdHistory = dH.IdHistory AND dH.IdTherapist = t.IdTherapist AND YEAR(h.Date) = '" + year + "' AND MONTH(h.Date) = '" + month + 
                                                "' AND DAY(h.Date) = '" + day + "' ", myConn);

            myConn.Open();
            comm.ExecuteNonQuery();
            MySqlDataAdapter da = new MySqlDataAdapter(comm);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgv.ItemsSource = dt.DefaultView;
            da.Update(dt);

            myConn.Close();
        }

        public DataTable getPatientProfile()
        {
            if(myConn == null)
                getConnection();

            MySqlCommand comm = new MySqlCommand("SELECT PatientName, Age, Gender, Address FROM patient WHERE Username = '" +Properties.Settings.Default.user+"';", myConn);
            myConn.Open();
            comm.ExecuteNonQuery();
            MySqlDataAdapter da = new MySqlDataAdapter(comm);
            DataTable dt = new DataTable();
            da.Fill(dt);

            myConn.Close();

            return dt;
        }

        public DataTable getTherapistProfile()
        {
            if (myConn == null)
                getConnection();

            MySqlCommand comm = new MySqlCommand("SELECT Name, Age, Gender, Address, Contact FROM therapist WHERE Username = '" + Properties.Settings.Default.terapis +"';", myConn);
            myConn.Open();
            comm.ExecuteNonQuery();
            MySqlDataAdapter da = new MySqlDataAdapter(comm);
            DataTable dt = new DataTable();
            da.Fill(dt);

            myConn.Close();

            return dt;
        }

        public DataTable getDataGraphic(int task, int level)
        {
            if (myConn == null)
                getConnection();

            int id = getIdPatient();

            MySqlCommand comm = new MySqlCommand("SELECT Date, Score FROM history WHERE IdPatient = "+ id +" AND Task = "+ task +" AND Level = " + level + " ORDER BY IdHistory DESC LIMIT 5", myConn);
            myConn.Open();
            comm.ExecuteNonQuery();
            MySqlDataAdapter da = new MySqlDataAdapter(comm);
            DataTable dt = new DataTable();
            da.Fill(dt);

            myConn.Close();

            return dt;
        }

        public bool updatePatient(string name, int age, string address)
        {
            if (myConn == null)
                getConnection();

            int id = getIdPatient();

            MySqlCommand comm = new MySqlCommand("UPDATE patient SET PatientName = '" + name + "' , Age = " + age + " , Address = '" + address 
                                                + "' WHERE IdPatient = " + id, myConn);

            myConn.Open();
            int row = comm.ExecuteNonQuery();
            myConn.Close();

            if (row != 1)
                return false;
            else
                return true;
        }

        public bool updateTherapist(string name, int age, string address, string contact)
        {
            if (myConn == null)
                getConnection();

            int id = getIdPatient();

            MySqlCommand comm = new MySqlCommand("UPDATE therapist SET Name = '" + name + "' , Age = " + age + " , Address = '" + address
                                                + "' , Contact = '" + contact + "' WHERE Username = '" + Properties.Settings.Default.terapis +"'", myConn);

            myConn.Open();
            int row = comm.ExecuteNonQuery();
            myConn.Close();

            if (row != 1)
                return false;
            else
                return true;
        }

    }

}
