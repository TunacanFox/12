using FPS.WeaponSO;
//using FPS.Datum;
using UnityEngine;

//enum 형 데이터를 이용하여 키 값과 일치하는 함수를 호출해준다.
//ScriptableObject를 만드는 CardDataSO에 enum데이터 있음
public class WeaponTypeManager : MonoBehaviour
{
    public GameObject weaponPrefab;//카드 생성을 위한 기본 데이터
    Transform weaponSettingHandImageTransform;


    // 카드의 고유 효과를 실행하는 메소드
    public void ExecuteEffect(string weaponName)
    {
        //WeaponDataSO cardData = WeaponSettingAssets.instance[cardName]; //나중에 구현한다~
    }

    public void ActivateWeaponEffect(WeaponType effectType, Vector3 dragEndPosition) //카드겜 만들 때 쓴거라 dragEndPosition 없애도 무방
    {
        //weaponData에 따라 일치하는 메서드를 호출한다.

        // 여기서 cardData를 사용하여 카드의 고유 효과를 구현합니다.
        // 예를 들어, 카드가 유닛이면 특정 행동을 하거나, 
        // 벽 카드면 특정 위치에 벽을 생성하는 등의 로직을 구현할 수 있습니다.
        switch (effectType)
        {
            case WeaponType.MainArm:
                Debug.Log("Acorn");
                GameObject cardInstance = Instantiate(weaponPrefab, dragEndPosition, Quaternion.identity); //카드 겜 할 때 구현한거라 없애도 무방
                //이런 식으로 프리팹 소환 가능
                //혹은 함수 부르든가
                break;

            case WeaponType.SideArm:
                Debug.Log("SideArm");
                break;

            case WeaponType.Melee:
                Debug.Log("Melee");
                break;

            case WeaponType.Explosive:
                Debug.Log("Explosive");
                break;

            case WeaponType.SpecialWeapon:
                Debug.Log("SpecialWeapon");
                break;

            case WeaponType.Grappling:
                Debug.Log("Grappling");
                break;


                // 기타 효과 처리...
        }
    }

    #region weaponData에 따라 호출되는 메서드
    private void ShotgunPallet(WeaponDataSO weaponData)
    {
        // 샷건 처럼 팰릿이 여러 개 나가는 것일 경우
    }

    private void DestroyWall(WeaponDataSO cardDatweaponDataa)
    {
        // 벽 부수는 폭발형 무기인데 범위 내에 폭발형 무기의 폭발이 닿을 경우
    }

    // 기타 카드 효과 관련 메소드들...

    #endregion
}
