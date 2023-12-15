using UnityEngine;

namespace FPS.WeaponSO
{
    public enum WeaponType
    {
        MainArm, //1번 슬롯 무기 메인 무기
        SideArm, //2번 슬롯 무기 권총 생각중
        Melee, //3번 슬롯 무기 근접 무기
        Explosive, //4번 슬롯 폭발물, 소이수류탄 등등
        SpecialWeapon, //5번 슬롯 특수무기: 투척무기 슈리켄, 고드름, 수리검 같은거
        Grappling, //6번 슬롯: 로망인 그래플링 건!!
    }

    [CreateAssetMenu(fileName = "Weapon", menuName = "WeaponSO")]
    public class WeaponDataSO : ScriptableObject
    {
        /// <summary>
        /// 사용 방법: 
        /// 타입에 따라 일치하는 함수를 호출하게 할 수 있다. ex) 그래플링 건 함수 불러오기, 샷건 점프 불러오기,
        /// 투척물, 그 중에 소이 수류탄이면 화염 지대 소환 등등등
        /// 쓸거면 쓰고 아니면 말고
        /// </summary>
        public WeaponType weaponType; 

        //기초적인 변수들
        public string weaponName; //무기 네임
        public float damage; //뎀
        public float range; //사거리
        public float fireRate; //발사 주기
        public int magazineSize; //탄창 크기
        public int totalMagazine; //주어지는 총 탄창 큰기

        public GameObject weaponPrefab;
        // 기타 필요한 총기 특성들 추가...

        

    }
}