using UnityEngine;
using FPS.WeaponSO;

//Todo
//스크립터블 오브젝트에서 enum WeaponType과 string weaponName을 받아서 저장하는 오브젝트. 그리고 어떻게 생겨먹었는지 알리기 위한 프리팹만 저장
//상호작용 하면 WeaponType을 이용해서 WeaponSwap의 WeaponList의 몇 번 인덱스에 넣을지 지정하고, 그 지정한 곳에 weaponName을 넣어준다.

//+기능1 무기 교체했으니 기존에 들고 있던 무기는 떨군다.
//+기능2 만약 1번 슬롯 무기 들고 있는데 1번 슬롯 무기를 교체했다면 WeaponSwap() 기능을 다시 실행하게 한다.

namespace FPS.WeaponSwap
{
    public class WeaponPickup : MonoBehaviour
    {

        WeaponDataSO weaponData = WeaponSettingAssets.instance["weaponName"];




    }
}