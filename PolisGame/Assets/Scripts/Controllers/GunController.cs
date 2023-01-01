using Data.UnityObjects;
using Enums;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class GunController : MonoBehaviour
    {
        private BulletController _bulletController;
        private GunData _gunData;
        private GunTypes _gunTypes;

        [SerializeField] private GameObject player;
        private  float _myBulletSpeed;
        public int _bulletCount;
        private void Awake()
        {
            _gunData = Resources.Load<CD_Gun>("Data/CD_Gun").GunData;
            _gunTypes = GunTypes.Ak47;
            _myBulletSpeed = _gunData.GunDatas[_gunTypes].BulletSpeed;
            _bulletCount = _gunData.GunDatas[_gunTypes].MaxBulletCount;
        }
        private void SendDataToControllers()
        {
            _bulletController.SetGunTypeData(_gunTypes,_gunData);
        }
        // ReSharper disable Unity.PerformanceAnalysis
        [ContextMenu("Fire")]
        public void Fire()
        {
            GameObject obj = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.Bullet.ToString(), transform);
            var transformEuler = obj.transform.eulerAngles;
            transformEuler.y = player.transform.eulerAngles.y;
            obj.transform.eulerAngles = transformEuler;
            obj.GetComponent<BulletController>().SetGunTypeData(_gunTypes,_gunData);
            var rbVelocity = obj.GetComponent<Rigidbody>().velocity;
            rbVelocity.x = transform.forward.normalized.x * _myBulletSpeed;
            rbVelocity.z = transform.forward.normalized.z * _myBulletSpeed;
            obj.GetComponent<Rigidbody>().velocity = rbVelocity;
            _bulletCount--;
        }
    }
}
