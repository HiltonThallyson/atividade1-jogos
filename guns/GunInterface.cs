using GunNamespace;

public interface IGun {

    public void AdjustHopUP(float newValue);
    public void SetUpGun();
    public void Shoot();
    public void ReloadGun();
    public void SwitchFireMode();
    public void SetUpCartridge(IAmmo cartridge);
}