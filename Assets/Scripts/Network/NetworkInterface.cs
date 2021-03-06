using UnityEngine;
using TMPro;
using ClientConnector.messages;
using ClientConnector;
using StaticContext;

public class NetworkInterface : MonoBehaviour
{
    public RobotMaster robotMaster;
    public TextMeshProUGUI scoreText;
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
            PlayerName = GameConnectionContext.Username,
            ServerAddress = GameConnectionContext.Host,
            MatchEndTimeMillis = 0,
            TokenTimeMillis = 0,
            HMACString = GameConnectionContext.Token

        };
        serverConnection = new ServerConnection(details, GameConnectionContext.Host, GameConnectionContext.Port);

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

                if(carrier.GetPayloadType() == "PlayerRobotMoveMessage")
                {
                    this.MoveRobotPosition(carrier);
                }

                if (carrier.GetPayloadType() == "RobotListingMessage")
                {
                    this.LogRobotListing(carrier);
                }

                if (carrier.GetPayloadType() == "PlayerRobotErrorMessage")
                {
                    this.RobotError(carrier);
                }

                if(carrier.GetPayloadType() == "MapSectorListing")
                {
                    this.HandleIncomingMapSectorListing(carrier);
                }

                if(carrier.GetPayloadType() == "ScoreUpdateMessage")
                {
                    this.LogScoreUpdate(carrier);
                }

                if (carrier.GetPayloadType() == "PlayerRobotMineMessage")
                {
                    this.LogMineEvent(carrier);
                }

                if(carrier.GetPayloadType() == "PlayerFirmwareChange")
                {
                    HandleServerFirmwareChange(carrier);
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



    private void MoveRobotPosition(ICarrierPigeon carrier)
    {
        RobotMovementEvent movementEvent = PayloadExtractor.GetRobotMovementEvent(carrier);
        //Debug.Log($"Player \"{movementEvent.PlayerId}\" Robot \"{movementEvent.RobotId}\"" +
        //    $" moved to -> X: {movementEvent.X} Y: {movementEvent.Y} sector: {movementEvent.NewLocation.SectorId}");

        string sectorID = movementEvent.NewLocation.SectorId;
        robotMaster.MoveRobot(movementEvent.RobotId, (movementEvent.X, movementEvent.Y), sectorID);
        
    }

    private void LogRobotListing(ICarrierPigeon carrier)
    {
        RobotListing listing = PayloadExtractor.GetRobotListing(carrier);
        foreach (Robot r in listing.robots) {
            Debug.Log(r == null);
            robotMaster.MoveRobot(r.RobotId, (r.Location.X, r.Location.Y), r.Location.SectorId,r.Firmware);
            Debug.Log($" Robot \"{r.RobotId}\" made at -> X: {r.Location.X} Y: {r.Location.Y} sector: {r.Location.SectorId}");

        }
        Debug.Log("Caught robot listing");
     
    }

    private void RobotError(ICarrierPigeon carrier)
    {
        RobotErrorEvent err = PayloadExtractor.GetRobotErrorEvent(carrier);

        robotMaster.ErrorRobot(err.RobotId);

        Debug.LogError($"Robot {err.PlayerId}:{err.RobotId} had error \"{err.Error} \"");
    }


    private void HandleIncomingMapSectorListing(ICarrierPigeon carrier)
    {
        MapSectorListingUpdate listing = PayloadExtractor.GetMapSectorListingUpdate(carrier);
        int[,] map;

        foreach (MapSector sector in listing.MapSectors)
        {
            map = sector.DecodeMap();
            renderWorld.RenderMap(map, sector.SectorId);
        }
        Debug.Log("Caught MapSector listing");

    }

    private void LogMineEvent(ICarrierPigeon carrier)
    {
        RobotMineEvent e = PayloadExtractor.GetRobotMineEvent(carrier);

        Debug.Log($"Robot {e.PlayerId}:{e.RobotId} mined at {e.SectorId}:X:{e.X}Y:{e.Y}");
        GameObject r = GameObject.Find("Robots/" + e.RobotId);
        (int x,int y) cord = RenderWorld.GetCordinates((e.X,e.Y),e.SectorId);
        r.GetComponent<RobotController>().Mine(cord.x,cord.y);
        renderWorld.RemoveTile((e.X, e.Y), e.SectorId);
    }

    private void LogScoreUpdate(ICarrierPigeon carrier)
    {
        ScoreUpdateMessage e = PayloadExtractor.GetScoreUpdateMessage(carrier);

        scoreText.text = $"{e.score}";

        Debug.Log($"Robot {e.PlayerId}:{e.RobotId} Score is {e.score}");

    }

    private void HandleServerFirmwareChange(ICarrierPigeon carrier)
    {
        PlayerFirmwareChange e = PayloadExtractor.GetPlayerFirmwareChange(carrier);

        robotMaster.AddFirmwareFromServer(e.RobotId, e.Code);
    }
}
