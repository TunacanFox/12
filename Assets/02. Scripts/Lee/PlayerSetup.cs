using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerSetup : MonoBehaviour
{
    public static Dictionary<string, PlayerSetup> spawned = new Dictionary<string, PlayerSetup>();
    public string nickName;

    public TextMeshPro nickNameText;
    private PhotonView _photonView;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
        spawned.Add(_photonView.Owner.UserId, this);
    }

    private void OnDestroy()
    {
        spawned.Remove(_photonView.Owner.UserId);
    }

    [PunRPC]
    public void SetNickname(string _name)
    {
        nickName = _name;

        nickNameText.text = nickName;
    }
    
}
