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
    /// Interaction logic for CallPage.xaml
    /// </summary>
    public partial class CallPage : Page
    {
        public CallPage()
        {
            InitializeComponent();
            AvtorizationWindow.bd.Calls.Load();
            dtgEmpl.ItemsSource = AvtorizationWindow.bd.Calls.Local.OrderBy(x => x.id);
        }

        private void sortPos_Checked(object sender, RoutedEventArgs e)
        {
            AvtorizationWindow.bd.Calls.Load();
            dtgEmpl.ItemsSource = AvtorizationWindow.bd.Calls.Local.OrderBy(x => int.Parse(x.number));
        }

        private void sortPhone_Checked(object sender, RoutedEventArgs e)
        {
            AvtorizationWindow.bd.Calls.Load();
            dtgEmpl.ItemsSource = AvtorizationWindow.bd.Calls.Local.OrderBy(x => x.phone);
        }

        private void sortName_Checked(object sender, RoutedEventArgs e)
        {
            AvtorizationWindow.bd.Calls.Load();
            dtgEmpl.ItemsSource = AvtorizationWindow.bd.Calls.Local.OrderBy(x => x.adress);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            spRed.Visibility = Visibility.Visible;
            btnSave.Visibility = Visibility.Hidden;
            btnAd.Visibility = Visibility.Visible;
            tbName.Text = "";
            tbSalary.Text = "";
            tbPhone.Text = "";
        }
        Call selectEntites = new Call();

        private void btnRed_Click(object sender, RoutedEventArgs e)
        {
            AvtorizationWindow.bd.Calls.Load();
            selectEntites = (Call)dtgEmpl.SelectedItem;
            if (selectEntites != null)
            {
                try
                {
                    tbName.Text = selectEntites.number.ToString();
                    tbSalary.Text = selectEntites.adress.ToString();
                    tbPhone.Text = selectEntites.phone.ToString();
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
            AvtorizationWindow.bd.Calls.Load();
            selectEntites = (Call)dtgEmpl.SelectedItem;
            if (selectEntites != null)
            {
                try
                {
                    if (MessageBox.Show("Вы действительно хотите стереть вызов?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        AvtorizationWindow.bd.Calls.Remove(selectEntites);
                        AvtorizationWindow.bd.SaveChanges();
                        dtgEmpl.ItemsSource = AvtorizationWindow.bd.Calls.Local.OrderBy(x => x.id);
                        AvtorizationWindow.Inf("Вызов удалён");
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
            Regex input = new Regex(@"[0-9]");
            Match match = input.Match(e.Text);
            if (!match.Success)
            {
                e.Handled = true;
            }
        }

        private void tbSalary_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex input = new Regex(@"[А-Яа-я0-9]");
            Match match = input.Match(e.Text);
            if (!match.Success)
            {
                e.Handled = true;
            }
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            AvtorizationWindow.bd.Calls.Load();
            Call currentProduct = new Call();
            if (tbName.Text != "" && tbSalary.Text != "" && tbPhone.Text != "")
            {
                try
                {
                    currentProduct.number = tbName.Text;
                    currentProduct.adress = tbSalary.Text;
                    currentProduct.phone = tbPhone.Text;
                    AvtorizationWindow.bd.Calls.Remove(selectEntites);
                    AvtorizationWindow.bd.Calls.Add(currentProduct);
                    AvtorizationWindow.bd.SaveChanges();
                    AvtorizationWindow.Inf("Данные сохранены");
                    spRed.Visibility = Visibility.Hidden;
                    dtgEmpl.ItemsSource = AvtorizationWindow.bd.Calls.Local.OrderBy(x => x.id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnAd_Click(object sender, RoutedEventArgs e)
        {
            AvtorizationWindow.bd.Calls.Load();
            Call currentProduct = new Call();
            if (tbName.Text != "" && tbSalary.Text != "" && tbPhone.Text != "")
            {
                try
                {
                    currentProduct.number = tbName.Text;
                    currentProduct.adress = tbSalary.Text;
                    currentProduct.phone = tbPhone.Text;
                    AvtorizationWindow.bd.Calls.Add(currentProduct);
                    AvtorizationWindow.bd.SaveChanges();
                    AvtorizationWindow.Inf("Данные сохранены");
                    spRed.Visibility = Visibility.Hidden;
                    dtgEmpl.ItemsSource = AvtorizationWindow.bd.Calls.Local.OrderBy(x => x.id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void tbPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$
            Regex input = new Regex(@"[0-9+]");
            Match match = input.Match(e.Text);
            if (!match.Success)
            {
                e.Handled = true;
            }
        }
        }
    }
