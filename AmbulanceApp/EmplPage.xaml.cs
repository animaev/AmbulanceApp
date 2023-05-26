using AmbulanceApp.ModelBD;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace AmbulanceApp
{
    /// <summary>
    /// Interaction logic for EmplPage.xaml
    /// </summary>
    public partial class EmplPage : Page
    {
        public EmplPage()
        {
            InitializeComponent();
            AvtorizationWindow.bd.Employees.Load();
            dtgEmpl.ItemsSource = AvtorizationWindow.bd.Employees.Local.OrderBy(x => x.id);
        }

        private void sortPos_Checked(object sender, RoutedEventArgs e)
        {
            AvtorizationWindow.bd.Employees.Load();
            dtgEmpl.ItemsSource = AvtorizationWindow.bd.Employees.Local.OrderBy(x => x.position);
        }

        private void sortId_Checked(object sender, RoutedEventArgs e)
        {
            AvtorizationWindow.bd.Employees.Load();
            dtgEmpl.ItemsSource = AvtorizationWindow.bd.Employees.Local.OrderBy(x => x.id);
        }

        private void sortName_Checked(object sender, RoutedEventArgs e)
        {
            AvtorizationWindow.bd.Employees.Load();
            dtgEmpl.ItemsSource = AvtorizationWindow.bd.Employees.Local.OrderBy(x => x.name);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            spRed.Visibility = Visibility.Visible;
            btnSave.Visibility = Visibility.Hidden;
            btnAd.Visibility = Visibility.Visible;
            tbName.Text = "";
            tbPos.Text = "";
        }
        Employee selectEntites = new Employee();
        private void btnRed_Click(object sender, RoutedEventArgs e)
        {
            AvtorizationWindow.bd.Employees.Load();
            selectEntites = (Employee)dtgEmpl.SelectedItem;
            if (selectEntites != null)
            {
                try
                {
                    tbName.Text = selectEntites.name.ToString();
                    tbPos.Text = selectEntites.position.ToString();
                    spRed.Visibility = Visibility.Visible;
                    btnAd.Visibility = Visibility.Hidden;
                    btnSave.Visibility = Visibility.Visible;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                AvtorizationWindow.Exp("Вы ничего не выбрали!");
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            AvtorizationWindow.bd.Employees.Load();
            selectEntites = (Employee)dtgEmpl.SelectedItem;
            if (selectEntites != null)
            {
                try
                {
                    if (MessageBox.Show("Вы действительно хотите уволить сотрудника?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        AvtorizationWindow.bd.Employees.Remove(selectEntites);
                        AvtorizationWindow.bd.SaveChanges();
                        dtgEmpl.ItemsSource = AvtorizationWindow.bd.Employees.Local.OrderBy(x => x.id);
                        AvtorizationWindow.Inf("Сотрудник уволен");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                AvtorizationWindow.Exp("Вы ничего не выбрали!");
            }
        }

        private void tbName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex input = new Regex(@"[А-Яа-я]");
            Match match = input.Match(e.Text);
            if (!match.Success)
            {
                e.Handled = true;
            }
        }

        private void tbPos_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex input = new Regex(@"[А-Яа-я]");
            Match match = input.Match(e.Text);
            if (!match.Success)
            {
                e.Handled = true;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            AvtorizationWindow.bd.Employees.Load();
            Employee currentProduct = new Employee();
            if (tbName.Text != "" && tbPos.Text != "")
            {
                try
                {
                    currentProduct.name = tbName.Text;
                    currentProduct.position = tbPos.Text;
                    AvtorizationWindow.bd.Employees.Remove(selectEntites);
                    AvtorizationWindow.bd.Employees.Add(currentProduct);
                    AvtorizationWindow.bd.SaveChanges();
                    AvtorizationWindow.Inf("Данные сохранены");
                    spRed.Visibility = Visibility.Hidden;
                    dtgEmpl.ItemsSource = AvtorizationWindow.bd.Employees.Local.OrderBy(x => x.id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnAd_Click(object sender, RoutedEventArgs e)
        {
            AvtorizationWindow.bd.Employees.Load();
            Employee currentProduct = new Employee();
            if (tbName.Text != "" && tbPos.Text != "")
            {
                try
                {
                    currentProduct.name = tbName.Text;
                    currentProduct.position = tbPos.Text;
                    AvtorizationWindow.bd.Employees.Add(currentProduct);
                    AvtorizationWindow.bd.SaveChanges();
                    AvtorizationWindow.Inf("Данные сохранены");
                    spRed.Visibility = Visibility.Hidden;
                    dtgEmpl.ItemsSource = AvtorizationWindow.bd.Employees.Local.OrderBy(x => x.id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
