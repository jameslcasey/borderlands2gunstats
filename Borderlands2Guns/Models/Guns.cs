using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Borderlands2Guns.Models
{
    public class Guns
    {
        public int Id {get;set;}
        public string Name { get; set; }
        public int Level { get; set; }
        public GunType Type { get; set; }
        public Brand Brand { get; set; }

        private int _damage;
        public int Damage {
            get { return _damage; }
            set {
                _damage = value;
                DamageTimesFireRate = value * FireRate;
            }

        }

        private decimal _fireRate;
        public decimal FireRate {
            get { return _fireRate; }
            set {
                _fireRate = value;
                DamageTimesFireRate = value * Damage;
            }
        }

        private decimal _accuracy;
        public decimal Accuracy {
            get {
                return _accuracy;
            }
            set {
                //calc DamageOnTarget...
                _accuracy = (value/100);
                DamageOnTarget = (value/100) * DamageTimesFireRateTimesMagazineSizePerReloadSpeed;
            }
        }

        private decimal _reloadSpeed;
        public decimal ReloadSpeed {
            get {
                return _reloadSpeed;
            }
            set {
                //calc DamageTimesFireRateTimesMagazineSizePerReloadSpeed

                _reloadSpeed = value;

                if (value > 0)
                {
                    DamageTimesFireRateTimesMagazineSizePerReloadSpeed =
                        DamageTimesFireRateTimesMagazineSize / value;
                }
            }
        }

        private int _magazineSize;
        public int MagazineSize {
            get {
                return _magazineSize;
            }
            set {
                //calc DamageTimesFireRateTimesMagazineSize

                _magazineSize = value;

                DamageTimesFireRateTimesMagazineSize = DamageTimesFireRate * value;

            }
        }

        private decimal _damageTimesFireRate;
        public decimal DamageTimesFireRate {
            get {
                return _damageTimesFireRate;
            }
            set {
                //calc DamageTimesFireRateTimesMagazineSize
                _damageTimesFireRate = value;
                DamageTimesFireRateTimesMagazineSize = value * MagazineSize;
            }
        }

        private decimal _damageTimesFireRateTimesMagazineSize;
        public decimal DamageTimesFireRateTimesMagazineSize {
            get {
                return this._damageTimesFireRateTimesMagazineSize;
            }
            set {
                //calc DamageTimesFireRateTimesMagazineSizePerReloadSpeed
                _damageTimesFireRateTimesMagazineSize = value;
                if (value > 0)
                {
                    DamageTimesFireRateTimesMagazineSizePerReloadSpeed =
                        value / ReloadSpeed;
                }
            }
        }

        private decimal _damageTimesFireRateTimesMagazineSizePerReloadSpeed;
        public decimal DamageTimesFireRateTimesMagazineSizePerReloadSpeed {
            get {
                return this._damageTimesFireRateTimesMagazineSizePerReloadSpeed;
            }
            set {
                //calc DamageOnTarget
                _damageTimesFireRateTimesMagazineSizePerReloadSpeed = value;
                DamageOnTarget = Accuracy * value;

            }
        }

        private decimal _damageOnTarget;
        public decimal DamageOnTarget {
            get {
                return this._damageOnTarget;
            }
            set {
                _damageOnTarget = value;
                ElementalDamageOnTargetTimesDamagePerSecondTimesChance =
                    ElementalDamagePerSecondTimesChance * value;
            }
        }

        public ElementalEffect? ElementalEffect { get; set; }

        private decimal _elementalDamagePerSecond;
        public decimal ElementalDamagePerSecond {
            get {
                return _elementalDamagePerSecond;
            }
            set {
                _elementalDamagePerSecond = value;
                ElementalDamagePerSecondTimesChance = value * Chance;
            }
        }

        private decimal _chance;
        public decimal Chance {
            get {
                return _chance;
            }
            set {
                _chance = (value/100);
                ElementalDamagePerSecondTimesChance = ElementalDamagePerSecond * (value/100);
            }
        }


        private decimal _elementalDamagePerSecondTimesChance;
        public decimal ElementalDamagePerSecondTimesChance
        {
            get
            {
                return _elementalDamagePerSecondTimesChance;
            }
            set
            {
                _elementalDamagePerSecondTimesChance = value;
                ElementalDamageOnTargetTimesDamagePerSecondTimesChance = value * DamageOnTarget;
            }
        }

        private decimal _elementalDamageOnTargetTimesDamagePerSecondTimesChance;
        public decimal ElementalDamageOnTargetTimesDamagePerSecondTimesChance
        {
            get
            {
                return _elementalDamageOnTargetTimesDamagePerSecondTimesChance;
            }
            set
            {
                _elementalDamageOnTargetTimesDamagePerSecondTimesChance = value;
            }
        }





    }


    public enum GunType
    {
        Pistol = 0,
        SubmachineGun = 1,
        Shotgun = 2,
        AssaultRifle = 3,
        SniperRifle = 4,
        RocketLaucher = 5
    }

    public enum Brand
    {
        Bandit =0,
        Dahl = 1,
        Hyperion = 2,
        Jakobs=3,
        Maliwan=4,
        Tediore=5,
        Torgue=6,
        Vladof=7,
        ETech=8
    }

    public enum ElementalEffect
    {
        Corrosive=0,
        Explosive=1,
        Incendiary=2,
        Shock=3,
        Slag=4
    }

}
