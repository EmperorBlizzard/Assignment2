using Assignment2.Models;
using Assignment2.Services;
using Newtonsoft.Json;
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
        private ObservableCollection<ContactPerson> _contacts;
        private IFileService _fileService = new FileService();
        private string _filePath = @$"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\Address_book.json";
        private Guid id;
        public MainWindow()
        {
            InitializeComponent();

            UpdateProductCatalog();
        }
        //Uppdaterar listview genom att läsa av json fil
        private void UpdateProductCatalog()
        {
            try
            {
                var list =

                _contacts = JsonConvert.DeserializeObject<ObservableCollection<ContactPerson>>(_fileService.Read(_filePath));
                lv_Contacts.ItemsSource = _contacts.OrderByDescending(x => x.Created);
            }
            catch { }
        }
        //lägger till ny kontakt och kollar om Email redan existerar i systemet så att inte samma person läggs till fler än en gång
        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            var _contact = new ContactPerson();
            _contacts = JsonConvert.DeserializeObject<ObservableCollection<ContactPerson>>(_fileService.Read(_filePath))!;
            var contact = _contacts.FirstOrDefault(x => x.Email == tb_Email.Text);
            
            if(contact == null)
            {

                _contact.FirstName = tb_FirstName.Text;
                _contact.LastName = tb_LastName.Text;
                _contact.Email = tb_Email.Text;
                _contact.StreetName = tb_StreetName.Text;
                _contact.PostalCode = tb_PostalCode.Text;
                _contact.City = tb_City.Text;

                _contacts.Add(_contact);

                _fileService.Save(_filePath, JsonConvert.SerializeObject(_contacts));
                
            }
            else
            {
                MessageBox.Show("Contact already exist.");
            }
            ClearFields();
            UpdateProductCatalog();
        }
        //Rensar alla textbox
        private void ClearFields()
        {
            tb_FirstName.Text = "";
            tb_LastName.Text = "";
            tb_Email.Text = "";
            tb_StreetName.Text = "";
            tb_PostalCode.Text = "";
            tb_City.Text = "";
        }
        //Tar bort en kontakt
        private void btn_Remove_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var contact = (ContactPerson)button!.DataContext;

            _contacts.Remove(contact);

            _fileService.Save(_filePath, JsonConvert.SerializeObject(_contacts));

            UpdateProductCatalog();
        }

        //Gör så att om en kontakt klickas så fyller den alla textbox med den kontacktens information. 
        //Sparar kontaktens id.
        //Gömer också Add contact knappen och gör Edit contact knappen synlig.
        private void lv_Contacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var contact = (ContactPerson)lv_Contacts.SelectedItems[0]!;
            tb_FirstName.Text = contact.FirstName;
            tb_LastName.Text = contact.LastName;
            tb_Email.Text = contact.Email;
            tb_StreetName.Text = contact.StreetName;
            tb_PostalCode.Text = contact.PostalCode;
            tb_City.Text = contact.City;

            id = contact.Id;

            btn_Add.Visibility = Visibility.Hidden;
            btn_Edit.Visibility = Visibility.Visible;
            
        }
        //När knappen klickar skickar den över Index och contact till Edit metoden.
        //Gömer också Edit contact knappen och gör Add contact knappen synlig. 
        private void btn_Edit_Click(object sender, RoutedEventArgs e)
        {


            var contact = _contacts.FirstOrDefault(x => x.Id == id);
            var Index = _contacts.IndexOf(contact);
            
            Edit(Index, contact);

            btn_Add.Visibility = Visibility.Visible;
            btn_Edit.Visibility = Visibility.Hidden;

            ClearFields();
            UpdateProductCatalog();
        }
        //Änndrar innehålet av specifik kontakt
        private void Edit(int Index, ContactPerson contact)
        {
            
            if(contact != null)
            {

                contact.FirstName = tb_FirstName.Text;

                contact.LastName = tb_LastName.Text;
                contact.Email = tb_Email.Text;
                contact.StreetName = tb_StreetName.Text;
                contact.PostalCode = tb_PostalCode.Text;
                contact.City = tb_City.Text;

                _contacts[Index] = contact;
                _fileService.Save(_filePath, JsonConvert.SerializeObject(_contacts));
            }
            
        }
    }
}
