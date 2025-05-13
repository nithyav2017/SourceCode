using clsArthritisPatient;
using System.Text.Json;

namespace SampleMAUIApp
{
    public partial class MainPage : ContentPage
    {
      //  int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnRegisterClicked(object sender, EventArgs e)
        {
            var patient = new Patient
            {
                FirstName = FirstName.Text,
                LastName=LastName.Text,
                Email = Email.Text,
                Phone = Phone.Text,
                PinHash = Pin.Text,
                HcpSpecialty = "Rheumatologist",
                Indication = "RA",
                InsuranceType = 1,
                ConsentToEmail = true,
                ConsentToText = true
            };

            var json = JsonSerializer.Serialize(patient);
            DisplayAlert("Message", "You clicked me!", "OK");
        }
    }

}
