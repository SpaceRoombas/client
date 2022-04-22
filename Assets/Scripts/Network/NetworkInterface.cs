using UnityEngine;
using ClientConnector.messages;
using ClientConnector;

public class NetworkInterface : MonoBehaviour
{
    public RobotMaster robotMaster;
    public RenderWorld renderWorld;

    public string address;
    public int port;
    ServerConnection serverConnection;
    const string PlayerId = "TheBetterRoomba";
    // Start is called before the first frame update

    private void Awake()
    {
        PlayerDetails details = new PlayerDetails()
        {
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
        if (serverConnection.connectionMode == ServerConnection.ConnectionMode.Connecting)
        {
            Debug.Log("Establishing Connection...");
        }
        else if (serverConnection.connectionMode == ServerConnection.ConnectionMode.Handshaking)
        {
            Debug.Log("Handshaking..");
        }
        else if (serverConnection.connectionMode == ServerConnection.ConnectionMode.Disconnected)
        {
            Debug.Log("Disconnected..");
            Debug.Log("\n\nGoing to retry connection");
            serverConnection.Connect();
        }
        else
        {
            if (serverConnection.HasMessage)
            {
                ICarrierPigeon carrier = serverConnection.DequeueMessage();
                if(carrier.GetPayloadType() == "MapSector")
                {
                    Debug.Log("Got our MapSector, shooting code change");
                    MapSector mapSector = PayloadExtractor.GetMapSector(carrier);
                    int[,] map = mapSector.DecodeMap();
                    Debug.Log(mapSector.DecodeMap());
                    
                    renderWorld.RenderMap(map, "0,0");
                }

                if(carrier.GetPayloadType() == "PlayerRobotMoveMessage")
                {
                    this.MoveRobotPosition(carrier);
                }

                if (carrier.GetPayloadType() == "RobotListingMessage")
                {
                    this.LogRobotListing(carrier);
                }
            }
        }
   
        serverConnection.Tick();
    }

    public void SendFirmwareChange(string rID,string newFirmware)
    {
        PlayerFirmwareChange firmwareChange = new PlayerFirmwareChange()
        {
            Code = newFirmware,
            PlayerId = PlayerId,
            RobotId = rID
        };

        // send code change
        serverConnection.EnqueueMessage(firmwareChange);
    }

    private void LogRobotPosition(ICarrierPigeon carrier)
    {
        RobotMovementEvent movementEvent = PayloadExtractor.GetRobotMovementEvent(carrier);
        Debug.Log($"Player \"{movementEvent.PlayerId}\" Robot \"{movementEvent.RobotId}\" moved to -> X: {movementEvent.X} Y: {movementEvent.Y}");
    }

    private void MoveRobotPosition(ICarrierPigeon carrier)
    {
        //Debug.Log("que size:"+serverConnection.quesize);
        RobotMovementEvent movementEvent = PayloadExtractor.GetRobotMovementEvent(carrier);
        Debug.Log($"Player \"{movementEvent.PlayerId}\" Robot \"{movementEvent.RobotId}\" moved to -> X: {movementEvent.X} Y: {movementEvent.Y}");
      
        string sectorID = "0,0";
        robotMaster.MoveRobot(movementEvent.RobotId, (movementEvent.X, movementEvent.Y),sectorID);
        
    }

    private void LogRobotListing(ICarrierPigeon carrier)
    {
        RobotListing listing = PayloadExtractor.GetRobotListing(carrier);
        foreach (Robot r in listing.robots) {
            robotMaster.MoveRobot(r.RobotId, (r.Location.X, r.Location.Y), r.Location.SectorId);
        }
        Debug.Log("Caught robot listing");
     
    }
}