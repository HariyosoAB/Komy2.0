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

namespace BismillahTA.User_Control
{
    /// <summary>
    /// Interaction logic for levelTherapy.xaml
    /// </summary>
    public partial class levelTherapy : UserControl
    {
        MainWindow parent;
        private bool clickSatu2 = false,
                     clickSatu3 = false,
                     clickDua1 = false,
                     clickDua2 = false,
                     clickDua3 = false,
                     clickTiga1 = false,
                     clickTiga2 = false,
                     clickTiga3 = false,
                     clickEmpat1 = false,
                     clickEmpat2 = false,
                     clickEmpat3 = false;
             

        SqlConnector con = new SqlConnector();
        
        public levelTherapy()
        {
            InitializeComponent();
            satu1.Visibility = Visibility.Hidden;
            satu2.Visibility = Visibility.Hidden;
            satu3.Visibility = Visibility.Hidden;
            dua1.Visibility = Visibility.Hidden;
            dua2.Visibility = Visibility.Hidden;
            dua3.Visibility = Visibility.Hidden;
            tiga1.Visibility = Visibility.Hidden;
            tiga2.Visibility = Visibility.Hidden;
            tiga3.Visibility = Visibility.Hidden;
            empat1.Visibility = Visibility.Hidden;
            empat2.Visibility = Visibility.Hidden;
            empat3.Visibility = Visibility.Hidden;
        }

        public levelTherapy(MainWindow parent): this()
        {
            this.parent = parent;
            backLabel.Visibility = System.Windows.Visibility.Hidden;
            nextLabel.Visibility = System.Windows.Visibility.Hidden;
        }
        
        private void task1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            int countSatu1 = con.checkTaskAndLevel(1, 1);
            int countSatu2 = con.checkTaskAndLevel(1, 2);

            if (countSatu1 >= 3)
            {
                satu2.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/stage2.png"));
                clickSatu2 = true;

                if (countSatu2 >= 3)
                {
                    satu3.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/stage3.png"));
                    clickSatu3 = true;
                }
            }

            satu1.Visibility = Visibility.Visible;
            satu2.Visibility = Visibility.Visible;
            satu3.Visibility = Visibility.Visible;
        }

        private void satu1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            (parent as MainWindow).getUserControl(new User_Control.Task1(parent, 1));
        }

        private void satu2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (clickSatu2 == true)
            {
                clickSatu2 = false;
                (parent as MainWindow).getUserControl(new User_Control.Task1(parent, 2));
            }
            else
                return;
        }

        private void satu3_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (clickSatu3 == true)
            {
                clickSatu3 = false;
                (parent as MainWindow).getUserControl(new User_Control.Task1(parent, 3));
            }
            else
            {
                MessageBox.Show("Pelatihan masih terkunci");
                return;
            }
        }

        private void backButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            (parent as MainWindow).getUserControl(new User_Control.chooseObject(parent));
        }

        private void task2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            int countSatu3 = con.checkTaskAndLevel(1, 3);
            int countDua1 = con.checkTaskAndLevel(2, 1);
            int countDua2 = con.checkTaskAndLevel(2, 2);

            if (countSatu3 >= 3)
            {
                dua1.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/stage1.png"));
                clickDua1 = true;

                if (countDua1 >= 3)
                {
                    dua2.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/stage2.png"));
                    clickDua2 = true;

                    if(countDua2 >= 3)
                    {
                        dua3.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/stage3.png"));
                        clickDua3 = true;
                    }
                }
            }

            dua1.Visibility = Visibility.Visible;
            dua2.Visibility = Visibility.Visible;
            dua3.Visibility = Visibility.Visible;
        }

        private void dua1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (clickDua1 == true)
            {
                clickDua1 = false;
                (parent as MainWindow).getUserControl(new User_Control.Task2(parent, 1));
            }
            else
            {
                MessageBox.Show("Pelatihan masih terkunci");
                return;
            }
        }

        private void dua2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //if (clickDua2 == true)
            //{
                clickDua2 = false;
                (parent as MainWindow).getUserControl(new User_Control.Task2(parent, 2));
            //}
            //else
            //{
            //    MessageBox.Show("Pelatihan masih terkunci");
            //    return;
            //}
        }

        private void dua3_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (clickDua3 == true)
            {
                clickDua3 = false;
                (parent as MainWindow).getUserControl(new User_Control.Task2(parent, 3));
            }
            else
            {
                MessageBox.Show("Pelatihan masih terkunci");
                return;
            }
        }

        private void task3_MouseUp(object sender, MouseButtonEventArgs e)
        {
            int countDua3 = con.checkTaskAndLevel(2, 3);
            int countTiga1 = con.checkTaskAndLevel(3, 1);
            int countTiga2 = con.checkTaskAndLevel(3, 2);

            if (countDua3 >= 3)
            {
                dua1.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/stage1.png"));
                clickTiga1 = true;

                if (countTiga1 >= 3)
                {
                    dua2.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/stage2.png"));
                    clickTiga2 = true;

                    if (countTiga2 >= 3)
                    {
                        dua3.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/stage3.png"));
                        clickTiga3 = true;
                    }
                }
            }

            tiga1.Visibility = Visibility.Visible;
            tiga2.Visibility = Visibility.Visible;
            tiga3.Visibility = Visibility.Visible;
        }

        private void tiga1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (clickTiga1 == true)
            {
                clickTiga1 = false;
                (parent as MainWindow).getUserControl(new User_Control.Task3(parent, 1));
            }
            else
            {
                MessageBox.Show("Pelatihan masih terkunci");
                return;
            }
        }

        private void tiga2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //if (clickTiga2 == true)
            //{
                clickTiga2 = false;
                (parent as MainWindow).getUserControl(new User_Control.Task3(parent, 2));
            //}
            //else
            //{
            //    MessageBox.Show("Pelatihan masih terkunci");
            //    return;
            //}
        }

        private void tiga3_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (clickTiga3 == true)
            {
                clickTiga3 = false;
                (parent as MainWindow).getUserControl(new User_Control.Task3(parent, 3));
            }
            else
            {
                MessageBox.Show("Pelatihan masih terkunci");
                return;
            }
        }

        private void task4_MouseUp(object sender, MouseButtonEventArgs e)
        {
            int countTiga3 = con.checkTaskAndLevel(3, 3);
            int countEmpat1 = con.checkTaskAndLevel(4, 1);
            int countEmpat2 = con.checkTaskAndLevel(4, 2);

            if (countTiga3 >= 3)
            {
                dua1.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/stage1.png"));
                clickEmpat1 = true;

                if (countEmpat1 >= 3)
                {
                    dua2.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/stage2.png"));
                    clickEmpat2 = true;

                    if (countEmpat2 >= 3)
                    {
                        dua3.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/stage3.png"));
                        clickEmpat3 = true;
                    }
                }
            }

            empat1.Visibility = Visibility.Visible;
            empat2.Visibility = Visibility.Visible;
            empat3.Visibility = Visibility.Visible;
        }

        private void empat1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //if (clickEmpat1 == true)
            //{
                clickEmpat1 = false;
                (parent as MainWindow).getUserControl(new User_Control.Task4(parent, 1));
            //}
            //else
            //{
            //    MessageBox.Show("Pelatihan masih terkunci");
            //    return;
            //}
        }

        private void empat2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //if (clickEmpat2 == true)
            //{
                clickEmpat2 = false;
                (parent as MainWindow).getUserControl(new User_Control.Task4(parent, 2));
            //}
            //else
            //{
            //    MessageBox.Show("Pelatihan masih terkunci");
            //    return;
            //}
        }

        private void empat3_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //if (clickEmpat3 == true)
            //{
                clickEmpat3 = false;
                (parent as MainWindow).getUserControl(new User_Control.Task4(parent, 3));
            //}
            //else
            //{
            //    MessageBox.Show("Pelatihan masih terkunci");
            //    return;
            //}
        }

        private void backButton_MouseEnter(object sender, MouseEventArgs e)
        {
            backLabel.Visibility = System.Windows.Visibility.Visible;
        }

        private void backButton_MouseLeave(object sender, MouseEventArgs e)
        {
            backLabel.Visibility = System.Windows.Visibility.Hidden;
        }
        private void nextButton_MouseUp(object sender, MouseEventArgs e)
        {
            (parent as MainWindow).getUserControl(new User_Control.chooseBalance(parent));
        }
        private void nextButton_MouseEnter(object sender, MouseEventArgs e)
        {
            nextLabel.Visibility = System.Windows.Visibility.Visible;
        }

        private void nextButton_MouseLeave(object sender, MouseEventArgs e)
        {
            nextLabel.Visibility = System.Windows.Visibility.Hidden;
        }

        #region Over-Leave Mouse
        private void satu1_MouseEnter(object sender, MouseEventArgs e)
        {
            satu1.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/stage1Dark.png"));
        }

        private void satu1_MouseLeave(object sender, MouseEventArgs e)
        {
            satu1.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/stage1.png"));
        }

        private void satu2_MouseEnter(object sender, MouseEventArgs e)
        {
            if (clickSatu2 == true)
                satu2.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/stage2Dark.png"));
            else
                return;
        }

        private void satu2_MouseLeave(object sender, MouseEventArgs e)
        {
            if (clickSatu2 == true)
                satu2.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/stage2.png"));
            else
                return;
        }

        private void satu3_MouseEnter(object sender, MouseEventArgs e)
        {
            if (clickSatu3 == true)
                satu3.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/stage3Dark.png"));
            else
                return;
        }

        private void satu3_MouseLeave(object sender, MouseEventArgs e)
        {
            if (clickSatu3 == true)
                satu3.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/stage3.png"));
            else
                return;
        }

        private void dua1_MouseEnter(object sender, MouseEventArgs e)
        {
            if (clickDua1 == true)
                dua1.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/stage1Dark.png"));
            else
                return;
        }

        private void dua1_MouseLeave(object sender, MouseEventArgs e)
        {
            if (clickDua1 == true)
                dua1.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/stage1.png"));
            else
                return;
        }

        private void dua2_MouseEnter(object sender, MouseEventArgs e)
        {
            if (clickDua2 == true)
                dua2.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/stage2Dark.png"));
            else
                return;
        }

        private void dua2_MouseLeave(object sender, MouseEventArgs e)
        {
            if (clickDua2 == true)
                dua2.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/stage2.png"));
            else
                return;
        }

        private void dua3_MouseEnter(object sender, MouseEventArgs e)
        {
            if (clickDua3 == true)
                dua3.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/stage3Dark.png"));
            else
                return;
        }

        private void dua3_MouseLeave(object sender, MouseEventArgs e)
        {
            if (clickDua3 == true)
                dua3.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/stage3.png"));
            else
                return;
        }

        private void tiga1_MouseEnter(object sender, MouseEventArgs e)
        {
            if (clickTiga1 == true)
                tiga1.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/stage1Dark.png"));
            else
                return;
        }

        private void tiga1_MouseLeave(object sender, MouseEventArgs e)
        {
            if (clickTiga1 == true)
                tiga1.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/stage1.png"));
            else
                return;
        }

        private void tiga2_MouseEnter(object sender, MouseEventArgs e)
        {
            if (clickTiga2 == true)
                tiga2.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/stage2Dark.png"));
            else
                return;
        }

        private void tiga2_MouseLeave(object sender, MouseEventArgs e)
        {
            if (clickTiga2 == true)
                tiga2.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/stage2.png"));
            else
                return;
        }

        private void tiga3_MouseEnter(object sender, MouseEventArgs e)
        {
            if (clickTiga3 == true)
                tiga3.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/stage3Dark.png"));
            else
                return;
        }

        private void tiga3_MouseLeave(object sender, MouseEventArgs e)
        {
            if (clickTiga3 == true)
                tiga3.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/stage3.png"));
            else
                return;
        }

        private void empat1_MouseEnter(object sender, MouseEventArgs e)
        {
            if (clickEmpat1 == true)
                empat1.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/stage1Dark.png"));
            else
                return;
        }

        private void empat1_MouseLeave(object sender, MouseEventArgs e)
        {
            if (clickEmpat1 == true)
                empat1.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/stage1.png"));
            else
                return;
        }

        private void empat2_MouseEnter(object sender, MouseEventArgs e)
        {
            if (clickEmpat2 == true)
                empat2.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/stage2Dark.png"));
            else
                return;
        }

        private void empat2_MouseLeave(object sender, MouseEventArgs e)
        {
            if (clickEmpat2 == true)
                empat2.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/stage2.png"));
            else
                return;
        }

        private void empat3_MouseEnter(object sender, MouseEventArgs e)
        {
            if (clickEmpat3 == true)
                empat3.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/stage3Dark.png"));
            else
                return;
        }

        private void empat3_MouseLeave(object sender, MouseEventArgs e)
        {
            if (clickEmpat3 == true)
                empat3.Source = new BitmapImage(new Uri("pack://application:,,,/BismillahTA;component/Resources/stage3.png"));
            else
                return;
        }
        #endregion

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
