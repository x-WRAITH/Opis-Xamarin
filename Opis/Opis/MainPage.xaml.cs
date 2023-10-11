using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using static Android.Print.PrintAttributes;

namespace Opis
{
    public partial class MainPage : ContentPage
    {
        public string plec;
        public string imageurl;
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(firstName.Text)) {
                DisplayAlert("Error", "Pole Imię nie moze byc puste!", "OK");
                firstName.PlaceholderColor = Color.Red;
                return;
            }
            if (string.IsNullOrEmpty(lastName.Text))
            {
                DisplayAlert("Error", "Pole Nazwisko nie moze byc puste!", "OK");
                lastName.PlaceholderColor = Color.Red;
                return;

            }
            if (string.IsNullOrEmpty(phoneNumber.Text) || phoneNumber.Text.Length != 9)
            {
                DisplayAlert("Error", "Pole Numer telefonu nie może być puste i musi zawierać 9 cyfr!", "OK");
                phoneNumber.PlaceholderColor = Color.Red;
                return;
            }

            if (wojewodztwoPicker.SelectedItem == null)
            {
                DisplayAlert("Error", "Pole wojewodztwo nie moze byc puste!", "OK");
                wojewodztwoPicker.BackgroundColor = Color.Red;
                return;
            }
            if (miastoPicker.SelectedItem == null)
            {
                DisplayAlert("Error", "Pole miasto nie moze byc puste!", "OK");
                miastoPicker.BackgroundColor = Color.Red;
                return;
            }
            if (string.IsNullOrEmpty(imageurl))
            {
                DisplayAlert("Error", "Brak zdjecia!", "OK");
                buttonPhoto.BorderColor = Color.Red;
                return;
            }
            if (radio1.IsChecked == true) { plec = "Kobieta"; } else { plec = "Mezczyzna"; }

            string text = "Imie: " + firstName.Text + "\nNazwisko: " + lastName.Text + "\nNumer telefonu: " + phoneNumber.Text + "\nPlec: " + plec + "\nWojewodztwo: " + wojewodztwoPicker.SelectedItem + "\nMiasto: " + miastoPicker.SelectedItem + "\nData: " + datePicker.Date.ToString("dd, MM, yyyy") + "\nZainteresowania:";
            if (programowanie.IsChecked == true) text += "\n -" + "Programowanie";
            if (taniec.IsChecked == true) text += "\n -" + "Taniec";
            if (muzyka.IsChecked == true) text += "\n -" + "Muzyka";

            photo.Source = imageurl;

            DisplayAlert("Opis", text, "OK"); 

        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            var valid = Regex.IsMatch(e.NewTextValue, "^[a-zA-Z]+$");
            if(e.NewTextValue.Length > 0)
            {

                ((Entry)sender).Text = valid ? e.NewTextValue : e.NewTextValue.Remove(e.NewTextValue.Length - 1);
            }
        }

        private void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedRegion = ((Picker)sender).SelectedItem as string;

            if (selectedRegion == null)
            {
                miastoPicker.IsVisible = false;
                miastoPicker.ItemsSource = null;
            }
            else if (selectedRegion == "Małopolskie")
            {
                miastoPicker.IsVisible= true;
                miastoPicker.ItemsSource = new List<string> { "Limanowa", "Kraków", "Słopnice" };
            }
            else if (selectedRegion == "Śląskie")
            {
                miastoPicker.IsVisible = true;
                miastoPicker.ItemsSource = new List<string> { "Katowice", "Bytom", "Gliwice" };
            }
            else if (selectedRegion == "Świętokrzyskie")
            {
                miastoPicker.IsVisible = true;
                miastoPicker.ItemsSource = new List<string> { "Kielce", "Sandomierz", "Busko-Zdrój" };
            }
        }

        private async void Button_Clicked_Photo(object sender, EventArgs e)
        {
            try
            {
                var photourl = await MediaPicker.PickPhotoAsync();

                if (photourl != null)
                {
                    imageurl = photourl.FullPath;
                }
            }
            catch (Exception ex)
            {
                // Obsłuż błędy, np. jeśli użytkownik nie wybierze zdjęcia.
                await DisplayAlert("Błąd", "Nie udało się wybrać zdjęcia.", "OK");
            }
        }
    }
}
