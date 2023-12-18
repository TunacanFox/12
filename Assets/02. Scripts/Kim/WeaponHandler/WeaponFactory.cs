using UnityEngine;
using FPS.WeaponSO;
using FPS.WeaponHandler;
using FPS.Lee.WeaponDetail;

namespace FPS.WeaponHandler
{
    //총기의 Vector값과 Quaternion 기반으로 총기를 만들어줌, 만들어서 1인칭 화면으로 띄워주는 클래스
    //총기의 세부 스탯은 WeaponStat으로 이관, 이 클래스로 불러온다
    public class WeaponFactory : MonoBehaviour
    {
        //Instantiate를 위한 변수
        //Canvas의 자식인 Image의 자식으로 생성하기 위해 정보2개를 씀.
        public GameObject foundationPrefab; //부모1
        public Transform foundationTransform; //자식1 다른거 추가하지 않는 이상 이 녀석의 자식으로 Instantiate를 넣을 것이다.
        
        //Instantiate를 위한 Vector, Rotation
        public Vector3 weaponVector; //투명 프리팹의 위치
        public Quaternion weaponRotation; //투명 프리팹의 각도
        public Transform trackingPositionSettingPrefab; //투명 프리팹 정확히 Find해서 Vector, Rotation 설정할 수 있게 해줌.

        //Instantiate를 위한 프리팹
        public GameObject weaponPrefab; //생성할 프리팹


        private void Start()
        {
            //foundationPrefab = GameObject.Find("Player"); //가장 높은 부모 
            //원래는 하이어라키 창에 빈 오브젝트에 들어있는 있는게 WeaponFactory임, 하지만 이거는 초기화를 Start때 하는데, 플레이어는 나중에 들어온다
            //그래서 값이 안들어가서 바꿔주기가 쉽지 않음. -> 따라서 플레이어 안에 그냥 넣어줌 그래서 플레이어의 자식으로 들어간다.
            //부모 찾는 코드는 아니니 작동 안함 그러니 빼야함

            //foundationTransform = foundationPrefab.transform.Find("Main Camera"); //갑자기 오류
            //trackingPositionSettingPrefab = foundationTransform.transform.Find("PositionSettingPrefab"); //얘도 오류나서 직접 넣는걸로 변경

        }

        private void Update()
        {
            /*
            if (Input.GetKeyUp(KeyCode.Space))
            {
                
                CreateWeapon("(SG) Bennelli_M4"); //테스트 용이니 그냥 주석 처리
            }
             */
    }

        public GameObject CreateWeapon(string weaponName)
        {
            //Instantiate를 위한 Vector, Rotation 초기화. 이 녀석은 플레이어의 위치에 따라서 계속 변하니 호출되면 초기화 해야한다.
            //float distanceFromCamera = 1.0f; // 이 값은 게임에 따라 조정해야 합니다. 임시로 값 넣었는데, 정확한 값을 모르겠다.
            weaponVector = new Vector3(trackingPositionSettingPrefab.transform.position.x, trackingPositionSettingPrefab.transform.position.y, trackingPositionSettingPrefab.transform.position.z);
            weaponRotation = trackingPositionSettingPrefab.transform.rotation;

            WeaponDataSO weaponData = WeaponSettingAssets.instance[weaponName];
            //SO를 가져오긴 하는데, 모든 SO 하나하나 다 하기는 그러니까 딕셔너리로 한 꺼번에 가져오는건가?


            //테스트
            Debug.Log("CreateWeapon weaponName: " + weaponName); //이거 오류는 절대 안남
                                                                 //테스트파트
            Debug.Log("WeaponData.magazineSize: " + weaponData.magazineSize); //값 존재함
            Debug.Log("WeaponData.ammo: " + weaponData.ammo); //값 존재함
            Debug.Log("WeaponUISetup 실행되기 직전");

            

            if (weaponData != null)
            {
                this.weaponPrefab = weaponData.weaponPrefab; //들어간 것 확인

                GameObject weaponInstance = Instantiate(weaponPrefab, weaponVector, weaponRotation, foundationTransform);
                //WeaponStat weaponStat = weaponInstance.GetComponent<WeaponStat>();
                WeaponStat weaponStat = weaponInstance.AddComponent<WeaponStat>();
                Weapon forWeaponScriptAdd = weaponInstance.AddComponent<Weapon>(); //Weapon 스크립트를 붙여넣기 위해 존재하는 녀석

                forWeaponScriptAdd.WeaponUISetup(weaponData); //테스트 무기 UI의 TextMeshPro를 바꿀 수 있나 테스트 
                                                       //호출 조차 안된다.



                //새로 만든 weaponInstance에 weaponStat 스크립트 붙이기
                if (weaponStat != null)
                {
                    Debug.Log("weaponStat.Setup(weaponData) 실행");
                    weaponStat.Setup(weaponData); //이 함수는 SO이용해 데이터 자동으로 초기화해준다.
                                                  //weaponStat.TestPrint(); //테스트 용 함수


                }
                else
                {
                    Debug.LogError("WeaponStat 컴포넌트를 찾을 수 없습니다.");
                }
                return weaponInstance;
            }
            else
            {
                Debug.LogError("Weapon data not found for: " + weaponName);
                return null;
            }
        }
    }
}