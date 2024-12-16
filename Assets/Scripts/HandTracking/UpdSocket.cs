using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Net; //makes us able to use IPEndPoint
using System.Net.Sockets; //makes us able to use UdpClient
using System.Threading; // makes us able to use Thread

public class UdpSocket : MonoBehaviour
{
    [HideInInspector] public bool isTxStarted = false; // Public boolean variable to track whether transmission has started.
    
    [SerializeField] private string IP = "127.0.0.1"; // The IP address to connect to. Defaults to "127.0.0.1" (localhost).
    [SerializeField] private int rxPort = 8000; // port to receive data from Python 
    [SerializeField] private int txPort = 8001; // port to send data to Python

    // Declare the UdpClient and related variables for networking
    private UdpClient client; // UdpClient for handling UDP communication
    private IPEndPoint remoteEndPoint; // Represents the remote endpoint (local IP + port)    
    private Thread receiveThread; // Thread for handling incoming data

    private float[] landmarks; // Array to store landmark positions as floats (X and Y coordinates)
    public int handSignID; // Public variable to store the ID of the detected hand sign

    // Method to return the landmarks array
    public float[] GetLandmarks()
    {
        return landmarks;
    }

    void Awake()
    {
        // Initialize the remote endpoint with the specified IP and transmission port
        remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), txPort); 

        // Initialize the UdpClient for receiving data on the specified receiving port
        client = new UdpClient(rxPort);

        receiveThread = new Thread(new ThreadStart(ReceiveData)); // Create and start a new thread to handle incoming data
        receiveThread.IsBackground = true; // Set as a background thread to stop automatically when the app exits
        receiveThread.Start(); // Start the thread

        // Print a message to Unity's console for debugging
        print("UDP Comms Initialised");
    }

    // Method to receive data from the UDP socket
    private void ReceiveData()
    {
        while (true) // Infinite loop to keep listening for incoming data
        {
            try
            {
                // Create a new endpoint to represent any incoming connection
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);

                // Receive the data as a byte array
                byte[] data = client.Receive(ref anyIP);

                // Convert the byte array into a UTF-8 encoded string
                string receivedData = Encoding.UTF8.GetString(data);

                // Split the received data into parts using the '|' delimiter
                string[] parts = receivedData.Split('|');

                // Extract and clean the landmarks string (removing any unnecessary prefix text)
                string landmarksString = parts[0].Substring(parts[0].IndexOf(": ") + 2);

                // Parse and store the hand sign ID as an integer
                handSignID = int.Parse(parts[1].Trim());

                // Split the landmarks into pairs of X, Y coordinates using ';' as the delimiter
                string[] landmarkPairs = landmarksString.Split(';');

                // Allocate memory for the landmarks array (2 floats per coordinate pair)
                landmarks = new float[landmarkPairs.Length * 2];

                // Loop through each pair of coordinates, parsing and storing them in the landmarks array
                for (int i = 0; i < landmarkPairs.Length; i++)
                {
                    // Split each coordinate pair into X and Y components
                    string[] xy = landmarkPairs[i].Split(',');

                     // Parse and assign the X and Y values to the landmarks array
                    landmarks[2 * i] = float.Parse(xy[0]);
                    landmarks[2 * i + 1] = float.Parse(xy[1]);
                }

            }
            catch (Exception err) // Catch any errors during data reception or parsing
            {
                // Print the error message to the Unity console for debugging
                print(err.ToString());
            }
        }
    }

    // Called when the MonoBehaviour is disabled or the game object is destroyed
    void OnDisable()
    {
        // If the receive thread is running, abort it to stop execution
        if (receiveThread != null)
            receiveThread.Abort();
            
        // Close the UDP client to release the port
        client.Close();
    }
}