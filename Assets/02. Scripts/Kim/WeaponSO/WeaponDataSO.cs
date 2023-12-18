using System;
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

        [Header("WeaponName")]
        public string weaponName; //스크립터블 오브젝트를 부르기 위한 이름
        public string weaponNameFinder;//Find를 하기 위한 이름

        //기초적인 변수들
        [Header("Elementry Variable")]
        public int damage; //뎀
        public float fireRate; //발사 주기
        //Main Camera는 알아서 Find로 Weapon.cs에 넣어주자

        public float range; //사거리

        [Header("Ammo")]
        public int ammo; //총에 들어가는 총알 개수
        public int magazineSize; //탄창 개수
        public int fullMagazine; //주어지는 총 탄창 크기

        [Header("Prefab")]
        public GameObject weaponPrefab;

        [Header("VFX EFFECT")]
        public GameObject hitVFX;
        public float removeFireHole;

        [Header("Animation")]
        //public Animation animationForWeapon; //ak74 프리팹의 경우 ak74를 넣었다. 해당 총기의 프리팹을 넣어야 하는 듯 하다.
        //그러니 Weapon.cs에서 Find이용해서 넣어주자
        public AnimationClip reloadAnimation; //미리 만든 애니메이션 클립을 넣는다

        //반동
        [Header("Recoil Settings")]
        public float recoverPercent;
        public float recoilUp;
        public float recoilBack;
        public bool recovering;

        // 기타 필요한 총기 특성들 추가...

    }
}