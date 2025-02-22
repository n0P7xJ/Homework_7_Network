using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfClient.Models;
using WpfClient.Services;

namespace Wpf_Message_Sender;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private MessageService _service;
    public MainWindow()
    {
        InitializeComponent();
        _service = new MessageService();
    }

    private async void SendButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            MessagePostRequests message = new MessagePostRequests()
            {
                Recipient = this.PhoneNumberTextBox.Text,
                Text = this.MessageTextBox.Text
            };

            MessageResponse response = await _service.SendMessage(message);

            MessageBox.Show($"Id message: {response.Data?.MessageId}", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

    }
}