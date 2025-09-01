using Microsoft.Maui.Controls;

namespace MauiHello.Views
{
    public partial class CustomRegisterPage : ContentPage
    {
        public CustomRegisterPage()
        {
            InitializeComponent();
        }

        private void OnRegisterClicked(object sender, EventArgs e)
        {
            // Example: Retrieve values from controls
            string userId = userIdEntry.Text;
            string password = passwordEntry.Text;
            string motivation = motivationEditor.Text;
            DateTime selectedDate = datePicker.Date;
            TimeSpan selectedTime = timePicker.Time;

            // Simple validation example
            if (string.IsNullOrWhiteSpace(userId) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(motivation))
            {
                messageLabel.Text = "Please fill in all fields.";
                return;
            }

            // Registration logic goes here
            messageLabel.Text = "Registration successful!";
        }
    }
}