using ESimConnect;
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

namespace ESimConnectDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Model Model { get; set; } = new();
        private readonly ESimConnect.ESimConnect simCon = new();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = Model;

            this.simCon.Connected += SimCon_Connected;
            this.simCon.Disconnected += SimCon_Disconnected;
            this.simCon.ThrowsException += SimCon_ThrowsException;
            this.simCon.DataReceived += SimCon_DataReceived;
            this.simCon.SystemEventInvoked += SimCon_EventInvoked;
        }

        private void SimCon_EventInvoked(ESimConnect.ESimConnect _, ESimConnect.ESimConnect.ESimConnectSystemEventInvokedEventArgs e)
        {
            Model.SimVarEvent? se = Model.Events.FirstOrDefault(q => q.EventId == e.EventId);
            if (se == null) return;
            se.LastInvoked = DateTime.Now;
            Model.FiredEvents.Insert(0, $"System event '{se.Name}' invoked at '{se.LastInvoked}' with argument '{e.Value}'.");
        }

        private void SimCon_DataReceived(ESimConnect.ESimConnect _, ESimConnect.ESimConnect.ESimConnectDataReceivedEventArgs e)
        {
            Model.SimVarValue? svv = Model.Values.FirstOrDefault(q => q.RequestId == e.RequestId);
            if (svv == null) return;
            svv.Value = (double)e.Data;
        }

        private void SimCon_ThrowsException(ESimConnect.ESimConnect _, ESimConnect.SimConnectException ex)
        {
            MessageBox.Show($"SimCon exception: {ex}", "SimCon throw exception", MessageBoxButton.OK, MessageBoxImage.Error);
            btnConnectDisconnect.IsEnabled = true;
        }

        private void SimCon_Disconnected(ESimConnect.ESimConnect _)
        {
            Model.ConnectionStatus = Model.EConnectionStatus.DISCONNECTED;
            btnConnectDisconnect.IsEnabled = true;
        }

        private void SimCon_Connected(ESimConnect.ESimConnect _)
        {
            Model.ConnectionStatus = Model.EConnectionStatus.CONNECTED;
            btnConnectDisconnect.IsEnabled = true;
        }

        private void btnConnectDisconnect_Click(object sender, RoutedEventArgs e)
        {
            btnConnectDisconnect.IsEnabled = false;
            if (simCon.IsOpened)
                simCon.Close();
            else
                try
                {
                    simCon.Open();
                }
                catch (ESimConnectException ex)
                {
                    MessageBox.Show("Failed to connect: " + ex.Message, "Error");
                    btnConnectDisconnect.IsEnabled = true;
                }
        }

        private void btnValueSimVarAdd_Click(object sender, RoutedEventArgs e)
        {
            if (simCon.IsOpened == false)
            {
                MessageBox.Show("SimCon not connected.", "Error");
                return;
            }

            string simVar = txtValueSimVar.Text.Trim().ToUpper();
            Model.SimVarValue svv = new()
            {
                Name = simVar,
                Value = 0,
                Period = SimConnectPeriod.SECOND,
                OnlyOnChange = true,
                TypeId = simCon.Values.Register<double>(simVar)
            };
            svv.RequestId = simCon.Values.RequestRepeatedly(svv.TypeId, svv.Period, sendOnlyOnChange: svv.OnlyOnChange);

            svv.UpdateRequest += Svv_UpdateRequest;

            this.Model.Values.Add(svv);

            txtValueSimVar.Text = "";
            txtValueSimVar.Focus();
        }

        private void Svv_UpdateRequest(Model.SimVarValue sender)
        {
            simCon.Values.RequestRepeatedly(sender.RequestId, sender.TypeId, sender.Period, sender.OnlyOnChange, skipBetweenFrames: sender.FrameSkip);
        }

        private void btnValueDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Model.SimVarValue svv = (Model.SimVarValue)btn.Tag;
            Model.Values.Remove(svv);
        }

        private void btnAddSystemEvent_Click(object sender, RoutedEventArgs e)
        {
            if (simCon.IsOpened == false)
            {
                MessageBox.Show("SimCon not connected.", "Error");
                return;
            }

            string eventName = cmbSystemEvent.Text;
            EventId eid = simCon.SystemEvents.Register(eventName);
            Model.SimVarEvent se = new()
            {
                Name = eventName,
                EventId = eid
            };
            this.Model.Events.Add(se);

            cmbSystemEvent.SelectedIndex = -1;
        }

        private void btnDeleteSystemEvent_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Model.SimVarEvent? se = (Model.SimVarEvent)btn.Tag;
            if (se == null) return;
            simCon.SystemEvents.Unregister(se.EventId);
            Model.Events.Remove(se);
        }

        private void btnSimVarSetValue_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Model.SimVarValue svv = (Model.SimVarValue)btn.Tag;
            simCon.Values.Send<double>(svv.TypeId, svv.ValueToSet);
        }
    }
}