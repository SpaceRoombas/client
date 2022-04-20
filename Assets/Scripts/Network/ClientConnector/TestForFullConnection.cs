

using UnityEngine;
using ClientConnector;
using ClientConnector.messages;

public class TestForFullConnection : MonoBehaviour
{
    public string address;
    public int port;

    public RenderWorld renderWorld;
    ServerConnection serverConnection;
    const string PlayerId = "ARoomba";
    // Start is called before the first frame update

    private void Awake()
    {
        PlayerDetails details = new PlayerDetails() {
            PlayerName = PlayerId,
            ServerAddress = "localhost",
            MatchEndTimeMillis = 334563456,
            TokenTimeMillis = 329923929,
            HMACString = "L3KM45LQK234M5LQ2K34M"

        };
        serverConnection = new ServerConnection(details, address, port);

        Debug.Log("Started ServerConnection");
    }
    void Start()
    {
        Debug.Log("Connecting");
        serverConnection.Connect();
    }

    // Update is called once per frame
    void Update()
    {
        if (serverConnection.connectionMode == ServerConnection.ConnectionMode.Connecting) {
            Debug.Log("Establishing Connection...");
        }
        else if (serverConnection.connectionMode == ServerConnection.ConnectionMode.Handshaking) {
            Debug.Log("Handshaking..");
        }
        else if (serverConnection.connectionMode == ServerConnection.ConnectionMode.Disconnected) {
            Debug.Log("Disconnected..");
            Debug.Log("Going to retry connection");
            serverConnection.Connect();
        }
        else {
            if (serverConnection.HasMessage) {
                ICarrierPigeon carrier = serverConnection.DequeueMessage();
                MapSector mapSector = PayloadExtractor.GetMapSector(carrier);
                int[,] map = mapSector.DecodeMap();
                Debug.Log(mapSector.DecodeMap());
                //renderWorld.RenderMap(map,(0,0));
                PlayerFirmwareChange firmwareChange = new PlayerFirmwareChange() {
                    Code = "while(true){move_south() move_west()}",
                    PlayerId = PlayerId,
                    RobotId = "r0"
                };

                // send code change
                serverConnection.EnqueueMessage(firmwareChange);

            }
        }
        serverConnection.Tick();
    }
}