




using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Player.Weapon.Suriken
{
    [System.Serializable]
    public class Suriken : Projectile
    {
        [SerializeField] private Transform _sprite;
        private SurikenWeapon _surikenWeapon;

        protected override void OnEnable()
        {
            base.OnEnable();
            Timer = new WaitForSeconds(_surikenWeapon.Duration);
            Damage = _surikenWeapon.Damage;
        }

        private void Update()
        {
            _sprite.transform.Rotate(xAngle: 0, yAngle: 0, zAngle: 500f * Time.deltaTime);
            transform.position += transform.right * (_surikenWeapon.Speed * Time.deltaTime);
        }


        [Inject] 
        private void Construct(SurikenWeapon surikenWeapon) =>
            _surikenWeapon = surikenWeapon;



    }
}
