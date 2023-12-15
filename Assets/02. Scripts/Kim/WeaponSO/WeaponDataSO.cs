using UnityEngine;

namespace FPS.WeaponSO
{
    public enum WeaponType
    {
        MainArm, //1�� ���� ���� ���� ����
        SideArm, //2�� ���� ���� ���� ������
        Melee, //3�� ���� ���� ���� ����
        Explosive, //4�� ���� ���߹�, ���̼���ź ���
        SpecialWeapon, //5�� ���� Ư������: ��ô���� ������, ��帧, ������ ������
        Grappling, //6�� ����: �θ��� �׷��ø� ��!!
    }

    [CreateAssetMenu(fileName = "Weapon", menuName = "WeaponSO")]
    public class WeaponDataSO : ScriptableObject
    {
        /// <summary>
        /// ��� ���: 
        /// Ÿ�Կ� ���� ��ġ�ϴ� �Լ��� ȣ���ϰ� �� �� �ִ�. ex) �׷��ø� �� �Լ� �ҷ�����, ���� ���� �ҷ�����,
        /// ��ô��, �� �߿� ���� ����ź�̸� ȭ�� ���� ��ȯ ����
        /// ���Ÿ� ���� �ƴϸ� ����
        /// </summary>
        public WeaponType weaponType; 

        //�������� ������
        public string weaponName; //���� ����
        public float damage; //��
        public float range; //��Ÿ�
        public float fireRate; //�߻� �ֱ�
        public int magazineSize; //źâ ũ��
        public int totalMagazine; //�־����� �� źâ ū��

        public GameObject weaponPrefab;
        // ��Ÿ �ʿ��� �ѱ� Ư���� �߰�...

        

    }
}