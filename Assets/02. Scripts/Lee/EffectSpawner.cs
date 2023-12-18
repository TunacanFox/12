using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class EffectSpawner : MonoBehaviour
{
    public static EffectSpawner instance;
    private PhotonView _photonView;

    [Header("VFX EFFECT")]
    public GameObject hitVFX;

    private void Awake()
    {
        instance = this;
        _photonView = GetComponent<PhotonView>();
    }

    public void SpawnBulletEffectClientRpc(Vector3 point, Vector3 normal)
    {
        _photonView.RPC("SpawnBulletEffect", RpcTarget.All, new object[] { point, normal });
    }

    [PunRPC]
    private void SpawnBulletEffect(Vector3 point, Vector3 normal)
    {
        GameObject effect = Instantiate(hitVFX, point, Quaternion.LookRotation(normal));
    }
}

