using Assignment2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Assignment2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<ContactPerson> contacts;
        public MainWindow()
        {
            InitializeComponent();
            contacts = new ObservableCollection<ContactPerson>();
            lv_Contacts.ItemsSource = contacts;
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            var contact = contacts.FirstOrDefault(x => x.Email == tb_Email.Text);
        }

        private void ClearFields()
        {
            tb_FirstName.Text = "";
            tb_LastName.Text = "";
            tb_Email.Text = "";
            tb_StreetName.Text = "";
            tb_PostalCode.Text = "";
            tb_City.Text = "";
        }

        private void lv_Contacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btn_Remove_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
