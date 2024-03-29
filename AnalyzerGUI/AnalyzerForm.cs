// Code für die ANALYZER APP (Klient)

using Logger;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalyzerGUI
{
    public partial class AnalyzerForm : Form
    {
        private byte[] buffer = new byte[1024];
        private NetworkStream stream;
        private TcpClient client;
        private bool isConnected = false;
        private ILogger logger;

        public AnalyzerForm()
        {
            InitializeComponent();
            logger = new FullLogger(AnalyzerReadOnlyTextBox, "C:\\Users\\OverL\\Desktop\\AnalyzerLog.txt");
        }

        // Implementierung eines ZEITSTEMPELS für jede eingehende Nachricht.
        public static string GetTimestamp()
        {
            return DateTime.Now.ToString("[HH:mm:ss.fff]");
        }

        // Code für die Logik hinter der bifunktionalen Init-Schaltfläche „Verbinden/Trennen“(Connect/Disconnect).
        private async void ConnectDisconnectButton_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                try
                {
                    client = new TcpClient();
                    await client.ConnectAsync("127.0.0.1", 8888);

                    stream = client.GetStream();

                    isConnected = true;
                    logger.Log("Analzyer connected with LIS");

                    ConnectDisconnectButton.Text = "Disconnect";

                    // Beginnen Sie mit der Überwachung eingehender Nachrichten, wenn der Analyzer mit LIS verbunden ist.
                    ListenForMessages();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error connecting to LIS: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    // Schließen Sie den Stream und den Client ordnungsgemäß, wenn Sie die Verbindung trennen
                    client.Client.Shutdown(SocketShutdown.Both);

                    isConnected = false;
                    ConnectDisconnectButton.Text = "Connect";

                    // Zeigt eine Meldung in der ReadOnlyTextBox der Analyzer-GUI an, wenn die Verbindung getrennt wird
                    AnalyzerReadOnlyTextBox.Clear();
                    logger.Log("Analyzer disconnected from LIS");

                    GC.Collect();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error disconnecting from LIS: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        // Code für die Logik hinter der Schaltfläche „Result“.
        private async void SendResultButton_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                string message = ResultTextBox.Text;

                byte[] messageBytes = Encoding.UTF8.GetBytes(message+"R");
                string messageWithTimestamp = $"Send: {message}";

                logger.Log(messageWithTimestamp + "");
                ResultTextBox.Clear();

                await stream.WriteAsync(messageBytes, 0, messageBytes.Length);
            }
        }

        // Code für die Logik hinter der Schaltfläche „Query“.
        private async void QueryButton_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                string queryMessage = ResultTextBox.Text;

                byte[] queryBytes = Encoding.UTF8.GetBytes(queryMessage+"Q");
                string messageWithTimestamp = $"Send: {queryMessage}";

                logger.Log(messageWithTimestamp + "");
                ResultTextBox.Clear();

                await stream.WriteAsync(queryBytes, 0, queryBytes.Length);
            }
        }

        // Hier ist der Code, der für die Verarbeitung der eingehenden Nachrichten vom LIS verwendet und implementiert wird.
        private async void ListenForMessages()
        {
            try
            {
                while (true)
                {
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        stream.Close();
                        client.Close();

                        isConnected = false;
                        ConnectDisconnectButton.Text = "Connect";

                        AnalyzerReadOnlyTextBox.Clear();
                        logger.Log("Analyzer disconnected from LIS");

                        break;
                    }

                    string pushMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();

                    // Zeitstempel und empfangene Nachricht anzeigen.
                    logger.Log($"Received: {pushMessage}");

                    if (pushMessage == "<EOT>")
                    {
                        stream.Flush();
                        Array.Clear(buffer, 0, buffer.Length);

                        continue;
                    }

                    if (pushMessage == "<ACK>" || pushMessage == "<NAK>")
                    {
                        stream.Flush();
                        Array.Clear(buffer, 0, buffer.Length);

                        await SendEOT();

                        continue;
                    }
                    // Umgang mit leeren Nachrichten.
                    if (string.IsNullOrWhiteSpace(pushMessage))
                    {
                        logger.Log("Analyzer cannot read empty messages");
                        await SendNotAcknowledgment();
                        
                        Array.Clear(buffer, 0, buffer.Length);

                        continue;
                    }
                    // Umgang mit richtigen Nachricht Format.
                    if (pushMessage.StartsWith("P| "))
                    {
                        await SendAcknowledgment();
                    }
                    else
                    {
                        logger.Log("Message format is wrong");
                        await SendNotAcknowledgment();
                    }

                    stream.Flush();

                    Array.Clear(buffer, 0, buffer.Length);
                }
            }
            catch (Exception ex)
            {
                // Ausnahmen während des Nachrichtenempfangs ordnungsgemäß behandeln.
                MessageBox.Show("Error receiving Push message: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // CODE FÜR DIE METHODEN ACKNOWLEDGED (<ACK>) UND NOT ACKNOWLEDGED(<NAK>)
        // Hier ist der Teil, in dem die automatischen Antworten gesendet werden, wenn die Nachricht „Akzeptiert“ oder „Nicht akzeptiert“ ist.

        // Hier schickt der Analyzer <ACK>
        private async Task SendAcknowledgment()
        {
            byte[] acknowledgment = Encoding.UTF8.GetBytes("<ACK>");
            logger.Log("Send: <ACK>");
            await stream.WriteAsync(acknowledgment, 0, acknowledgment.Length);

           
        }

        // Hier schickt der Analyzer <NAK>
        private async Task SendNotAcknowledgment()
        {
            byte[] notAcknowledgment = Encoding.UTF8.GetBytes("<NAK>");
            logger.Log("Send: <NAK>");
            await stream.WriteAsync(notAcknowledgment, 0, notAcknowledgment.Length);

            
        }
        // EOT CODE(End of transmission)
        private async Task SendEOT()
        {
            byte[] acknowledgment = Encoding.UTF8.GetBytes("<EOT>");
            logger.Log("Send: <EOT>");
            await stream.WriteAsync(acknowledgment, 0, acknowledgment.Length);

            
        }

        private void AnalyzerForm_Load(object sender, EventArgs e)
        {

        }
    }
}