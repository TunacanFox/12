using FPS.WeaponSO;
using UnityEngine;

namespace FPS.WeaponHandler
{
    public class WeaponStat : MonoBehaviour
    {
        //enum 타입인 WeaponEffectType weaponType 이거는 namespace로 불러와서 쓰면 된다.

        //기초적인 변수들
        public string weaponName; //무기 네임
        public float damage; //뎀
        public float range; //사거리
        public float fireRate; //발사 주기
        public int magazineSize; //탄창 크기
        public int totalMagazine; //주어지는 총 탄창 큰기

        public GameObject weaponPrefab;


        public void Setup(WeaponDataSO weaponData)
        {
            //총기 스탯 초기화 -> WeaponFactory에서 이 스크립트 붙여넣게 하고, 이 함수 불러서 초기화하게 한다.
            weaponName = weaponData.weaponName;
            damage = weaponData.damage;
            range = weaponData.range;
            fireRate = weaponData.fireRate;
            magazineSize = weaponData.magazineSize;
            totalMagazine = weaponData.fullMagazine;

            weaponPrefab = weaponData.weaponPrefab;
        }

        public void TestPrint() //값 잘 들어갔나 체크 용 (망할 생각해보니 에디터에서 확인해도 무방하네)
        {
            Debug.Log($"weaponName: {weaponName}");
            Debug.Log($"damage: {damage}");
            Debug.Log($"range: {range}");
            Debug.Log($"fireRate: {fireRate}");
            Debug.Log($"magazineSize: {magazineSize}");
            Debug.Log($"totalMagazine: {totalMagazine}");
        }

    }
}