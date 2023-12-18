using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

using FPS.WeaponSO; //스크립터블 오브젝트 데이터 끌고 오기 위해서 사용

namespace FPS.Lee.WeaponDetail
{
    public class Weapon : MonoBehaviour
    {

        public int damage;
        public float fireRate;

        public Camera mainCamera;

        private float nextFire;

        [Header("Ammo")]
        public int mag;
        public int ammo;
        public int fullMagazine;

        [Header("VFX EFFECT")]
        public GameObject hitVFX;
        public float removeFireHole;

        [Header("UI")]
        public TextMeshProUGUI magText;
        public TextMeshProUGUI ammoText;

        [Header("Animation")]
        public Animation weaponAnimation; //<-- 이놈 쉬키 뭐임??
        public AnimationClip reloadAnimation;

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

            recoilLength = 0;
            recoverLength = 1 / fireRate * recoverPercent;
        }

        // Update is called once per frame
        void Update()
        {
            if (nextFire > 0)
                nextFire -= Time.deltaTime;

            if (Input.GetButton("Fire1") && nextFire <= 0 && ammo > 0 && weaponAnimation.isPlaying == false)
            {
                nextFire = 1 / fireRate;
                ammo--;

                magText.text = mag.ToString(); //n개의 개수 UI에 수정
                ammoText.text = ammo + "/" + fullMagazine; //mn개의 총알 개수 UI에 수정

                Fire();
            }
            if (Input.GetKeyDown(KeyCode.R) && mag > 0 && ammo < fullMagazine) //30
            {
                Reload();
            }

            if (recoiling)
            {
                Recoil();
            }
            if (recovering)
            {
                Recovering();
            }
        }


        public void WeaponUISetup(WeaponDataSO weaponData)
        {
            //Elementry Variable
            damage = weaponData.damage; //뎀
            fireRate = weaponData.fireRate; //발사 주기
            mainCamera = GetComponentInParent<Camera>();
            //mainCamera = Camera.main;//메인카메라 찾기. 씬에 있는 메인 카메라 태그 가진 카메라에 접근하는 방법.
            //인데... 태그가 MainCamera인 첫 번째 활성 카메라를 찾아서 모든 플레이어가 같이 움직이는 문제가 발생한다.

            //Ammo
            mag = weaponData.magazineSize; //초기화
            ammo = weaponData.ammo; //초기화
            fullMagazine = weaponData.fullMagazine; //초기화

            //VFX Effect
            hitVFX = weaponData.hitVFX;
            removeFireHole = weaponData.removeFireHole;

            //UI

            //애니메이션
            Camera clonedObject = GetComponentInParent<Camera>(); //Camera.main 제거
            string cloneAnimationFinder = weaponData.weaponNameFinder + "(Clone)";
            Transform gunTransform = clonedObject.transform.Find(cloneAnimationFinder); //설마했는데 (Clone)빠졌다고 이름이 틀리니까 Find를 못하네 ㅋㅋㅋㅋㅋㅋ
            //Transform gunTransform = clonedObject.transform.Find(weaponData.weaponNameFinder);
            Debug.Log($"weaponData네임파인더: {weaponData.weaponNameFinder}");
            weaponAnimation = gunTransform.GetComponent<Animation>(); //총기 애니메이션 없으면 에러난다.


            if(weaponAnimation == null)
            {

            }
            else
            {

            }


            //weaponAnimation = GameObject.Find(weaponData.weaponNameFinder); //ak74 프리팹의 경우 ak74를 넣었다. 그래서 일단 주석처리 필요하면 후에 넣는다
            reloadAnimation = weaponData.reloadAnimation; //미리 만든 애니메이션 클립 넣어줌.

            //RecoilSettings
            recoverPercent = weaponData.recoverPercent;
            recoilUp = weaponData.recoilUp;
            recoilBack = weaponData.recoilBack;
            //recovering = weaponData.recovering; //에러 떠서 주석처리
        }

    void Reload()
        {
            weaponAnimation.Play(reloadAnimation.name);
            if (mag > 0)
            {
                mag--;

                ammo = fullMagazine;
            }
            magText.text = mag.ToString();
            ammoText.text = ammo + "/" + fullMagazine;
        }

        public void Fire()
        {
            recoiling = true;
            recovering = false;

            Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward); //오류: UnassignedReferenceException: The variable camera of Weapon has not been assigned.
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
            if (transform.localPosition == finalPosition)
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
    }//class
}//namespace
