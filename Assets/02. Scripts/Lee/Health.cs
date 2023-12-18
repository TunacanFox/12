using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{

    public int health;
    public bool isLocalPlayer;

    [Header("UI")]
    public TextMeshProUGUI healthText;

    private PhotonView _photonView;
    // Start is called before the first frame update
    void Start()
    {
        _photonView = GetComponent<PhotonView>();
    }

    public void TakeDamageClientRpc(int damage)
    {
        _photonView.RPC("TakeDamage", RpcTarget.All, new object[] { damage });
    }

    // RPC 함수가 이름으로 함수를 검색해서 호출하는 로직 예제
    public void RPC(string methodName, RpcTarget target, object[] parameters)
    {
        var monos = gameObject.GetComponentsInChildren<MonoBehaviour>();

        foreach (var mono in monos)
        {
            var methods = mono.GetType().GetMethods();
            foreach (var method in methods)
            {
                if (method.Name.Equals(methodName))
                {
                    method.Invoke(mono, parameters);
                    return;
                }

            }
        }
    }



    [PunRPC]
    public void TakeDamage(int damgage)
    {
        health -= damgage;

        healthText.text = health.ToString();
            if (health <= 0)
        {
            if(isLocalPlayer)
                RoomManager.instance.SpawnPlayer();

            Destroy(gameObject);
        }
    }
}
