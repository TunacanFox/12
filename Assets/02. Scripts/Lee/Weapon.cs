using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

using FPS.WeaponSO; //스크립터블 오브젝트 데이터 끌고 오기 위해서 사용

public class Weapon : MonoBehaviour
{

    public int damage;
    public float fireRate;

    public Camera camera;

    private float nextFire;

    [Header("VFX EFFECT")]
    public GameObject hitVFX;
    public float removeFireHole;
    [Header("Ammo")]
    public int mag = 5;
    public int ammo = 30;
    public int magAmmo = 30;

    [Header("UI")]
    public TextMeshProUGUI magText;
    public TextMeshProUGUI ammoText;

    [Header("Animation")]
    public Animation animation;
    public AnimationClip reload;

    [Header("Recoil Settings")]
    /*[Range(0, 1)]
    public float recoilPercent = 0.3f;*/

    [Range(0, 2)]
    public float recoverPercent = 0.7f;

    [Space]
    public float recoilUp = 1f;

    public float recoilBack = 0f;

    private Vector3 originalPosition;
    private Vector3 recoilVelocity = Vector3.zero;
    
    private float recoilLength;
    private float recoverLength;

    private bool recoiling;
    public bool recovering;

    private PhotonView _photonView;
    // Start is called before the first frame update
    void Start()
    {

        _photonView = GetComponent<PhotonView>();
        //magText.text = mag.ToString(); //아래로 대체되었지만, 인지하라고 주석처리
        //ammoText.text = ammo + "/" + magAmmo;
        magText = GameObject.Find("MagText").GetComponent<TextMeshProUGUI>();
        ammoText = GameObject.Find("AmmoText").GetComponent<TextMeshProUGUI>();



        originalPosition = transform.localPosition;

        recoilLength  = 0;
        recoverLength = 1 / fireRate * recoverPercent;
    }

    // Update is called once per frame
    void Update()
    {
        if(nextFire > 0)
           nextFire -= Time.deltaTime;
        
        if(Input.GetButton("Fire1") && nextFire <= 0 && ammo > 0 && animation.isPlaying == false)
        {
            nextFire = 1 / fireRate;
            ammo--;

            magText.text = mag.ToString();
            ammoText.text = ammo + "/" + magAmmo;

            Fire();
        }
        if(Input.GetKeyDown(KeyCode.R) && mag > 0 && ammo < 30)
        {
            Reload();
        }

        if(recoiling)
        {
            Recoil();
        }
       if(recovering)
        {
            Recovering();
        }
    }


    public void WeaponUISetup(WeaponDataSO weaponData)
    {
        Debug.Log("호출은 되나??"); ///호출조차 안 된다

        magText.text = weaponData.magazineSize.ToString();
        ammoText.text = weaponData.ammo.ToString();
        Debug.Log("WeaponUISetup 함수 실행됐다, weaponData: " + weaponData);
    }


    void Reload()
    {
        animation.Play(reload.name);
        if(mag > 0)
        {
            mag--;

            ammo = magAmmo;
        }
        magText.text = mag.ToString();
        ammoText.text = ammo + "/" + magAmmo;
    }

    public void Fire()
    {
        recoiling = true;
        recovering = false;

        Ray ray = new Ray(camera.transform.position, camera.transform.forward); //오류: UnassignedReferenceException: The variable camera of Weapon has not been assigned.
                                                                                //You probably need to assign the camera variable of the Weapon script in the inspector.
        RaycastHit hit;
        int playermask = 1 << gameObject.layer;

        if (Physics.Raycast(ray.origin, ray.direction, out hit, 100f, playermask))
        {
            if (hit.transform.TryGetComponent(out Health health))
            {
                health.TakeDamageClientRpc(damage);
            }
        }
        else if (Physics.Raycast(ray.origin, ray.direction, out hit, 100f, ~playermask))
        {
            EffectSpawner.instance.SpawnBulletEffectClientRpc(hit.point, hit.normal);
        }

        return;

    

        //Ray ray = new Ray(camera.transform.position, camera.transform.forward);
        //
        //RaycastHit hit;
        //int playermask = 1 << gameObject.layer;
        //
        //
        //if (Physics.Raycast( ray.origin, ray.direction, out hit, 100f,playermask))
        //{
        //    if(hit.transform.gameObject.GetComponent<Health>())
        //    {
        //        hit.transform.gameObject.GetComponent<PhotonView>().RPC("TakeDamage",RpcTarget.All,damage);
        //    }
        //}
        //else if(Physics.Raycast(ray.origin, ray.direction, out hit, 100f, ~playermask))
        //{
        //    GameObject hitInfo = PhotonNetwork.Instantiate(hitVFX.name, hit.point, Quaternion.LookRotation(hit.normal));
        //    Destroy(hitInfo, 5f);
        //    hitInfo.transform.position += hitInfo.transform.forward / 1000;
        //}
        
    }

    void Recoil()
    {
        Vector3 finalPosition = new Vector3(originalPosition.x,
                                            originalPosition.y + recoilUp,
                                            originalPosition.z + recoilBack);

        transform.localPosition = Vector3.SmoothDamp(transform.localPosition,
                                                     finalPosition,
                                                     ref recoilVelocity,
                                                     recoilLength);
        if(transform.localPosition == finalPosition)
        {
            recoiling = false;
            recovering = true;
        }
    }
    void Recovering()
    {
        Vector3 finalPosition = originalPosition;

        transform.localPosition = Vector3.SmoothDamp(transform.localPosition,
                                                     finalPosition,
                                                     ref recoilVelocity,
                                                     recoverLength);
        if (transform.localPosition == finalPosition)
        {
            recoiling = false;
            recovering = false;
        }
    }


}
