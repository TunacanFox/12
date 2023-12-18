using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomManager : MonoBehaviourPunCallbacks
{

    public static RoomManager instance;
    // Start is called before the first frame update
    public GameObject player;

    public Transform spawnPoint;
    
    [Space]
    public GameObject roomCam;

    public GameObject connectionUI;

    public GameObject nameUI;

    private string nickname = "unnamed";
    private void Awake()
    {
        instance = this;
    }
    public void ChangeNickname(string _name)
    {
        nickname = _name;
    }

    public void JoinRoomButtonPressed()
    {
        Debug.Log("Connecting....");

        PhotonNetwork.ConnectUsingSettings();

        nameUI.SetActive(false);
        connectionUI.SetActive(true);
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        Debug.Log("Connected to Server");

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        PhotonNetwork.JoinOrCreateRoom("test", null, null);

        Debug.Log("We're connecte and in a room now");
    }
    public override void OnJoinedRoom() //Client가 다른방에 조인했을때 방장 X 
    {
        base.OnJoinedRoom();
        Debug.Log("Joined a room");

        roomCam.SetActive(false);

        SpawnPlayer();
    }
    public void SpawnPlayer()
    {
        GameObject _player = PhotonNetwork.Instantiate(player.name,
                                   spawnPoint.position,
                                   Quaternion.identity);

        _player.GetComponent<PhotonView>().RPC("SetNickname", RpcTarget.AllBuffered, nickname);
    }

    
}
