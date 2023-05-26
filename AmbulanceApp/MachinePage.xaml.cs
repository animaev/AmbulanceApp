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
    /// Interaction logic for MachinePage.xaml
    /// </summary>
    public partial class MachinePage : Page
    {
        public MachinePage()
        {
            InitializeComponent();
            List<string> a = new List<string>() { "На вызове","Свободна"}; 
            cmbActual.ItemsSource=a;
            cmbActual.SelectedIndex=1;
            AvtorizationWindow.bd.Machines.Load();
            dtgEmpl.ItemsSource = AvtorizationWindow.bd.Machines.Local.OrderBy(x => x.id);
        }

        private void sortPos_Checked(object sender, RoutedEventArgs e)
        {
            AvtorizationWindow.bd.Machines.Load();
            dtgEmpl.ItemsSource = AvtorizationWindow.bd.Machines.Local.OrderBy(x => int.Parse(x.number));
        }

        private void sortId_Checked(object sender, RoutedEventArgs e)
        {
            AvtorizationWindow.bd.Machines.Load();
            dtgEmpl.ItemsSource = AvtorizationWindow.bd.Machines.Local.OrderBy(x => x.id);
        }

        private void sortName_Checked(object sender, RoutedEventArgs e)
        {
            AvtorizationWindow.bd.Machines.Load();
            dtgEmpl.ItemsSource = AvtorizationWindow.bd.Machines.Local.OrderBy(x => x.busy);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            spRed.Visibility = Visibility.Visible;
            btnSave.Visibility = Visibility.Hidden;
            btnAd.Visibility = Visibility.Visible;
            tbName.Text = "";
        }
        Machine selectEntites = new Machine();

        private void btnRed_Click(object sender, RoutedEventArgs e)
        {
            AvtorizationWindow.bd.Machines.Load();
            selectEntites = (Machine)dtgEmpl.SelectedItem;
            if (selectEntites != null)
            {
                try
                {
                    tbName.Text = selectEntites.number.ToString();
                    if (selectEntites.busy == true)
                    {
                        cmbActual.SelectedIndex = 0;
                    }
                    else cmbActual.SelectedIndex = 1;
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
            AvtorizationWindow.bd.Machines.Load();
            selectEntites = (Machine)dtgEmpl.SelectedItem;
            if (selectEntites != null)
            {
                try
                {
                    if (MessageBox.Show("Вы действительно хотите удалить машину?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        AvtorizationWindow.bd.Machines.Remove(selectEntites);
                        AvtorizationWindow.bd.SaveChanges();
                        dtgEmpl.ItemsSource = AvtorizationWindow.bd.Machines.Local.OrderBy(x => x.id);
                        AvtorizationWindow.Inf("Машина удалена");
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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            AvtorizationWindow.bd.Machines.Load();
            Machine currentProduct = new Machine();
            if (tbName.Text != "")
            {
                try
                {
                    currentProduct.number = tbName.Text;
                    if (cmbActual.SelectedIndex == 0)
                    {
                        currentProduct.busy = true;
                    }
                    else currentProduct.busy = false;
                    AvtorizationWindow.bd.Machines.Remove(selectEntites);
                    AvtorizationWindow.bd.Machines.Add(currentProduct);
                    AvtorizationWindow.bd.SaveChanges();
                    AvtorizationWindow.Inf("Данные сохранены");
                    spRed.Visibility = Visibility.Hidden;
                    dtgEmpl.ItemsSource = AvtorizationWindow.bd.Machines.Local.OrderBy(x => x.id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnAd_Click(object sender, RoutedEventArgs e)
        {
            AvtorizationWindow.bd.Machines.Load();
            Machine currentProduct = new Machine();
            if (tbName.Text != "")
            {
                try
                {
                    currentProduct.number = tbName.Text;
                    if (cmbActual.SelectedIndex == 0)
                    {
                        currentProduct.busy = true;
                    }
                    else currentProduct.busy = false;
                    AvtorizationWindow.bd.Machines.Add(currentProduct);
                    AvtorizationWindow.bd.SaveChanges();
                    AvtorizationWindow.Inf("Данные сохранены");
                    spRed.Visibility = Visibility.Hidden;
                    dtgEmpl.ItemsSource = AvtorizationWindow.bd.Machines.Local.OrderBy(x => x.id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void tbName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex input = new Regex(@"[0-9]");
            Match match = input.Match(e.Text);
            if (!match.Success)
            {
                e.Handled = true;
            }
        }
    }
}
