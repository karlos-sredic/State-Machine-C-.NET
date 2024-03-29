// Code für die LIS APP(Server)

using Logger;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LISGUI
{
    public partial class LISForm : Form
    {
        private byte[] buffer = new byte[1024];
        private NetworkStream stream;
        private TcpListener listener;
        private TcpClient client;
        internal TextBox AnalyzerReadOnlyTextBox;
        private bool isServerRunning = false;
        private ILogger logger;

        // Implementierung eines ZEITSTEMPELS für jede eingehende Nachricht.
        public static string GetTimestamp()
        {
            return DateTime.Now.ToString("[HH:mm:ss.fff]"); 
        }

        public LISForm()
        {
            InitializeComponent();
            AnalyzerReadOnlyTextBox = LISReadOnlyTextBox;
            listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8888);
            logger = new FullLogger(LISReadOnlyTextBox, "C:\\Users\\OverL\\Desktop\\LISLog.txt");
        }

        // Code für die Logik hinter der bifunktionalen Init-Schaltfläche „Start/Stop“ für die TCP/IP Socket Kommunikation.
        private async void StartStopButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isServerRunning)
                {
                    // Starte den Server
                    listener.Start();

                    isServerRunning = true;
                    StartStopButton.Text = "Stop";

                    client = await listener.AcceptTcpClientAsync();
                    stream = client.GetStream();

                    ListenForIncomingMessages();
                    
                    logger.Log("TCP Server Socket Communication started");

                    stream.Flush();                                                                                                                            
                }
                else
                {
                    // Stoppen des Servers
                    if (client.Connected)
                    {
                        client?.Client.Shutdown(SocketShutdown.Both);
                    }

                    isServerRunning = false;
                    StartStopButton.Text = "Start";

                    // Alle ausstehenden Nachrichten in der LISReadOnlyTextBox löschen
                    LISReadOnlyTextBox.Clear();
                    logger.Log("TCP Server Socket Communication stopped ");

                    GC.Collect();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Hier ist der Code, der für die Verarbeitung der eingehenden Nachrichten vom Analyzer verwendet und implementiert wird.
        private async void ListenForIncomingMessages()
        {
            try
            {
                while (true)
                {
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        stream?.Dispose();
                        stream?.Close();

                        client?.Dispose();
                        client?.Close();

                        listener.Stop();
                        
                        isServerRunning = false;
                        StartStopButton.Text = "Start";

                        LISReadOnlyTextBox.Clear();
                        logger.Log("TCP Server Socket Communication stopped ");

                        break;
                    }

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
                    char sentWithButton = message[message.Length - 1];
                    string messageWithoutButton = message.Substring(0, message.Length - 1);

                    if(message=="<EOT>")
                    {
                        logger.Log($"Received: {message}");

                        stream.Flush();
                        Array.Clear(buffer, 0, buffer.Length);

                        continue;
                    }

                    if (message == "<ACK>" || message == "<NAK>")
                    {
                        logger.Log($"Received: {message}");

                        stream.Flush();
                        Array.Clear(buffer, 0, buffer.Length);

                        await SendEOT();

                        continue;
                    } else
                    {
                        logger.Log($"Received: {messageWithoutButton}");
                    }

                    // Umgang mit leeren Nachrichten.
                    if (string.IsNullOrWhiteSpace(messageWithoutButton))
                    {
                        logger.Log("LIS cannot read empty messages");
                        await SendNotAcknowledgment();

                        Array.Clear(buffer, 0, bytesRead);
                        continue;
                    }

                    // Ergebnis- oder Abfragenachricht verarbeiten(RESULT AND QUERY)
                    if (sentWithButton == 'R')
                    {
                        if (messageWithoutButton.StartsWith("R| "))
                        {
                            await SendAcknowledgment();
                        }
                        else if(messageWithoutButton.StartsWith("Q| "))
                        {
                            logger.Log("Result doesn't accept query format");
                            await SendNotAcknowledgment();
                        }
                        else
                        {
                            logger.Log("Message format is wrong");
                            await SendNotAcknowledgment();
                        }
                    }else
                    {
                        if (messageWithoutButton.StartsWith("Q| "))
                        {
                            await SendAcknowledgment();
                        }
                        else if (messageWithoutButton.StartsWith("R| "))
                        {
                            logger.Log("Query doesn't accept result format");
                            await SendNotAcknowledgment();
                        }
                        else
                        {
                            logger.Log("Message format is wrong");
                            await SendNotAcknowledgment();
                        }
                    }

                    // Den Stream nach der Verarbeitung jeder Nachricht leeren
                    stream.Flush();
                    Array.Clear(buffer, 0, buffer.Length);

                    // Nach der Verarbeitung jeder Nachricht eine Speicherbereinigung durchführen
                    GC.Collect();
                }
            }
            catch (Exception ex)
            {
                logger.Log($"[ERROR] {ex.Message}");
                logger.Log($"[ERROR] Error receiving message");

                await SendNotAcknowledgment();
            }
        }

        // Code für die Logik hinter der Schaltfläche „Push“.
        private async void PushButton_Click(object sender, EventArgs e)
        {
            string pushMessage = PushTextBox.Text.Length != 0 ? PushTextBox.Text : " ";
            byte[] data = Encoding.UTF8.GetBytes(pushMessage);

            string messageWithTimestamp = $"Send: {pushMessage}";
            logger.Log(messageWithTimestamp + "");
            PushTextBox.Clear();

            // Daten direkt in den Stream schreiben, ohne zu warten
            await stream.WriteAsync(data, 0, data.Length);
        }

        // CODE FÜR DIE METHODEN ACKNOWLEDGED (<ACK>) UND NOT ACKNOWLEDGED(<NAK>)
        // Hier ist der Teil, in dem die automatischen Antworten gesendet werden, wenn die Nachricht „Akzeptiert“ oder „Nicht akzeptiert“ ist.


        // Hier schickt der LIS <ACK>
        private async Task SendAcknowledgment()
        {
            byte[] acknowledgment = Encoding.UTF8.GetBytes("<ACK>");
            logger.Log("Send: <ACK>");
            await stream.WriteAsync(acknowledgment, 0, acknowledgment.Length);

            
        }

        // Hier schickt der LIS <NAK>
        private async Task SendNotAcknowledgment()
        {
            byte[] notAcknowledgment = Encoding.UTF8.GetBytes("<NAK>");
            logger.Log("Send: <NAK>");
            await stream.WriteAsync(notAcknowledgment, 0, notAcknowledgment.Length);

           
        }

        // Schicke die Übertragungssteuerzeichen <EOT> , EndOfTransmission.
        private async Task SendEOT()
        {
            byte[] acknowledgment = Encoding.UTF8.GetBytes("<EOT>");
            logger.Log("Send: <EOT>");
            await stream.WriteAsync(acknowledgment, 0, acknowledgment.Length);

            
        }

        private void LISForm_Load(object sender, EventArgs e)
        {

        }
    }
}