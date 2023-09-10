using UnityEngine;

namespace GunNamespace
{
    public interface IAmmo{
        public GameObject GetBB();
        public void DecreaseAmmo();
        public int GetAmmoLeft();
        public int GetAmmoCapacity();
        public AmmoType GetAmmoType();
        public void DropAmmo();
    }
}