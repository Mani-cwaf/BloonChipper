using Assets.Scripts.Models.Behaviors;
using Assets.Scripts.Models.Bloons.Behaviors;
using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors.Abilities;
using Assets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Assets.Scripts.Models.Towers.Behaviors.Attack;
using Assets.Scripts.Models.Towers.Behaviors.Attack.Behaviors;
using Assets.Scripts.Models.Towers.Filters;
using Assets.Scripts.Models.Towers.Projectiles;
using Assets.Scripts.Models.Towers.Projectiles.Behaviors;
using Assets.Scripts.Models.Towers.Weapons.Behaviors;
using Assets.Scripts.Simulation.Towers.Projectiles;
using Assets.Scripts.Unity;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using HarmonyLib;
using UnhollowerBaseLib;
using UnityEngine;
using System.Linq;
using static BloonChipper.Main;
using Assets.Scripts.Models.Towers.Behaviors.Emissions;
using Assets.Scripts.Models.Towers.Behaviors;

namespace BloonChipper
{
    public class BloonChipper : ModTower
    {

        public override string TowerSet => TowerSetType.Primary;
        public override string BaseTower => "DartMonkey";
        public override int Cost => 750;
        public override int TopPathUpgrades => 5;
        public override int MiddlePathUpgrades => 5;
        public override int BottomPathUpgrades => 0;
        public override string Description => "Rapidly sucks up and shreds bloons, spitting what's left out the back.";
        public override ParagonMode ParagonMode => ParagonMode.Base000;
        public override bool Use2DModel => true;
        public override string Get2DTexture(int[] tiers)
        {
            if (tiers[0] == 1 && tiers[0] < 2 && tiers[1] < 2 && tiers[2] < 2)
            {
                return "SuckierDisplay";
            }
            if (tiers[0] == 2 && tiers[0] < 3 && tiers[1] < 3 && tiers[2] < 3)
            {
                return "HeavyDutySuckageDisplay";
            }
            if (tiers[0] == 3)
            {
                return "DualLayerBladesDisplay";
            }
            if (tiers[0] == 4)
            {
                return "SuperWideFunnelDisplay";
            }
            if (tiers[0] == 5)
            {
                return "UltraWideFunnelDisplay";
            }
            if (tiers[1] == 1 && tiers[0] < 2 && tiers[1] < 2 && tiers[2] < 2)
            {
                return "LongRangeSuckDisplay";
            }
            if (tiers[1] == 2 && tiers[0] < 3 && tiers[1] < 3 && tiers[2] < 3)
            {
                return "FasterShredDisplay";
            }
            if (tiers[1] == 3)
            {
                return "TripleBarrelDisplay";
            }
            if (tiers[1] == 4)
            {
                return "VacuumDisplay";
            }
            if (tiers[1] == 5)
            {
                return "SupaVacDisplay";
            }
            return Name;
        }
        public override float Get2DScale(int[] tiers)
        {
            return 4;
        }
        public override void ModifyBaseTowerModel(TowerModel tower)
        {
            tower.RemoveBehavior<AttackModel>();
            var FUSTY = Game.instance.model.GetTowerFromId("PatFusty 10").GetAbility(1);
            var squeezeModel = FUSTY.GetBehavior<ActivateAttackModel>().attacks[0].Duplicate();
            var squeezeModelWeapon = squeezeModel.weapons[0];
            var squeezeModelProjectile = squeezeModelWeapon.projectile;
            squeezeModel.GetBehavior<AttackFilterModel>().filters = new Il2CppReferenceArray<FilterModel>(new FilterModel[] { new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze", "Moabs", new Il2CppStringArray(0)), new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze", "Ceramic", new Il2CppStringArray(0)), new FilterInvisibleModel("FilterInvisibleModel_", true, false), });
            squeezeModelWeapon.Rate = 5;
            squeezeModelWeapon.GetBehavior<SwitchAnimStateForBloonTypeModel>().nonMoabsAnimId = 4;
            squeezeModelWeapon.GetBehavior<SwitchAnimStateForBloonTypeModel>().moabAnimId = 4;
            squeezeModelWeapon.GetBehavior<SwitchAnimStateForBloonTypeModel>().bfbAnimId = 4;
            squeezeModelWeapon.GetBehavior<SwitchAnimStateForBloonTypeModel>().zomgAnimId = 4;
            squeezeModelWeapon.GetBehavior<SwitchAnimStateForBloonTypeModel>().ddtAnimId = 4;
            squeezeModelProjectile.filters = new Il2CppReferenceArray<FilterModel>(new FilterModel[] { new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze", "Moabs", new Il2CppStringArray(0)), new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze", "Ceramic", new Il2CppStringArray(0)), new FilterInvisibleModel("FilterInvisibleModel_", true, false) });
            squeezeModelProjectile.GetBehavior<ProjectileFilterModel>().filters = new Il2CppReferenceArray<FilterModel>(new FilterModel[] { new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze", "Moabs", new Il2CppStringArray(0)), new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze", "Ceramic", new Il2CppStringArray(0)), new FilterInvisibleModel("FilterInvisibleModel_", true, false) });
            squeezeModelProjectile.maxPierce = 1;
            squeezeModelProjectile.CapPierce(1);
            squeezeModelProjectile.pierce = 1;
            squeezeModelProjectile.GetDamageModel().maxDamage = 1;
            squeezeModelProjectile.GetDamageModel().CapDamage(1);
            squeezeModelProjectile.GetDamageModel().damage = 1;
            squeezeModelProjectile.GetDamageModel().immuneBloonProperties = BloonProperties.Lead;
            squeezeModelProjectile.GetDamageModel().distributeToChildren = true;
            squeezeModelProjectile.GetBehavior<SlowModel>().lifespan = 2.3f;
            squeezeModelProjectile.GetBehavior<SlowModel>().overlayType = "";
            squeezeModelProjectile.collisionPasses = new[] { -1, 0, 1 };
            var squeezeModel2 = squeezeModel.Duplicate();
            var squeezeModel3 = squeezeModel.Duplicate();
            squeezeModelProjectile.GetBehavior<DelayBloonChildrenSpawningModel>().lifespan = 1.5f;
            squeezeModel2.weapons[0].projectile.GetBehavior<DelayBloonChildrenSpawningModel>().lifespan = 1.8f;
            squeezeModel3.weapons[0].projectile.GetBehavior<DelayBloonChildrenSpawningModel>().lifespan = 2.1f;
            tower.AddBehavior(squeezeModel);
            tower.AddBehavior(squeezeModel2);
            tower.AddBehavior(squeezeModel3);
            tower.GetAttackModels().ForEach(attackModel => attackModel.weapons[0].projectile.RemoveBehavior<CreateSoundOnDelayedCollisionModel>());
        }
    }
    public class UpgradedVisors : ModUpgrade<BloonChipper>
    {
        public override string DisplayName => "Upgraded Visors";
        public override string Description => "Can target camo bloons.";
        public override int Cost => 260;
        public override int Path => BOTTOM;
        public override int Tier => 1;
        public override string Icon => Name + "-Icon";
        public override string Portrait => Name + "-Portrait";
        public override void ApplyUpgrade(TowerModel tower)
        {
            tower.GetDescendants<FilterInvisibleModel>().ForEach(model => model.isActive = false);
        }
    }
    public class Shredded : ModUpgrade<BloonChipper>
    {
        public override string DisplayName => "Shredded";
        public override string Description => "Precise shredders cause non-moab bloons to get tears, dealing damage over time.";
        public override int Cost => 875;
        public override int Path => BOTTOM;
        public override int Tier => 2;
        public override string Icon => Name + "-Icon";
        public override string Portrait => Name + "-Portrait";
        public override void ApplyUpgrade(TowerModel tower)
        {
            var DOTmodel = Game.instance.model.GetTowerFromId("GlueGunner-200").GetWeapon().projectile.GetBehavior<AddBehaviorToBloonModel>().Duplicate();
            var DOT = DOTmodel.GetBehavior<DamageOverTimeModel>();
            DOTmodel.lifespan = 100;
            DOTmodel.overlayType = "Bleed";
            DOT.interval = 3;
            tower.GetAttackModels().ForEach(attackModel => attackModel.weapons[0].projectile.AddBehavior(DOTmodel));
        }
    }
    public class Ripping : ModUpgrade<BloonChipper>
    {
        public override string DisplayName => "Ripping";
        public override string Description => "Shredded effect does more damage over time to bloons and causes bloons to take more damage from all atttacks";
        public override int Cost => 2350;
        public override int Path => BOTTOM;
        public override int Tier => 3;
        public override string Icon => Name + "-Icon";
        public override string Portrait => Name + "-Portrait";
        public override void ApplyUpgrade(TowerModel tower)
        {
            var DOTmodel = tower.GetWeapon().projectile.GetBehavior<AddBehaviorToBloonModel>();
            var DOT = DOTmodel.GetBehavior<DamageOverTimeModel>();
            tower.GetAttackModels().ForEach(attackModel => attackModel.weapons[0].projectile.GetBehavior<AddBehaviorToBloonModel>().GetBehavior<DamageOverTimeModel>().damage = 2);
            tower.GetAttackModels().ForEach(attackModel => attackModel.weapons[0].projectile.GetBehavior<AddBehaviorToBloonModel>().GetBehavior<DamageOverTimeModel>().interval = 1.5f);
            tower.GetAttackModels().ForEach(attackModel => attackModel.weapons[0].projectile.AddBehavior(new AddBonusDamagePerHitToBloonModel("AddBonusDamagePerHitToBloonModel", "", 100, 2, 999999, true, false, false)));
        }
    }
    public class LongRangeSuck : ModUpgrade<BloonChipper>
    {
        public override string DisplayName => "Long Range Suck";
        public override string Description => "Increases suction range.";
        public override int Cost => 300;
        public override int Path => MIDDLE;
        public override int Tier => 1;
        public override string Icon => Name + "-Icon";
        public override string Portrait => Name + "-Portrait";
        public override void ApplyUpgrade(TowerModel tower)
        {
            tower.range += 15;
            tower.GetAttackModels().ForEach(attackModel => attackModel.range += 15);
        }
    }
    public class FasterShred : ModUpgrade<BloonChipper>
    {
        public override string DisplayName => "Faster Shred";
        public override string Description => "More efficient internal componentry decreases the time bloons take to be shredded.";
        public override int Cost => 600;
        public override int Path => MIDDLE;
        public override int Tier => 2;
        public override string Icon => Name + "-Icon";
        public override string Portrait => Name + "-Portrait";
        public override void ApplyUpgrade(TowerModel tower)
        {
            tower.GetAttackModels().ForEach(attackModel => attackModel.weapons[0].projectile.GetBehavior<DelayBloonChildrenSpawningModel>().lifespan *= 0.5f);
            tower.GetAttackModels().ForEach(attackModel => attackModel.weapons[0].projectile.GetBehavior<SlowModel>().lifespan *= 2);
        }
    }
    public class TripleBarrel : ModUpgrade<BloonChipper>
    {
        public override string DisplayName => "Triple Barrel";
        public override string Description => "Triple barrels provide the most efficient bloon shredding possible.";
        public override int Cost => 4500;
        public override int Path => MIDDLE;
        public override int Tier => 3;
        public override string Icon => Name + "-Icon";
        public override string Portrait => Name + "-Portrait";
        public override void ApplyUpgrade(TowerModel tower)
        {
            var squeezeModel = tower.GetAttackModel();
            var squeezeModel4 = squeezeModel.Duplicate();
            var squeezeModel5 = squeezeModel.Duplicate();
            var squeezeModel6 = squeezeModel.Duplicate();
            var squeezeModel7 = squeezeModel.Duplicate();
            var squeezeModel8 = squeezeModel.Duplicate();
            var squeezeModel9 = squeezeModel.Duplicate();
            tower.AddBehavior(squeezeModel4);
            tower.AddBehavior(squeezeModel5);
            tower.AddBehavior(squeezeModel6);
            tower.AddBehavior(squeezeModel7);
            tower.AddBehavior(squeezeModel8);
            tower.AddBehavior(squeezeModel9);
        }
    }
    public class Vacuum : ModUpgrade<BloonChipper>
    {
        public override string DisplayName => "Vacuum";
        public override string Description => "Ability: Huge burst of suction power brings all nearby non-MOAB class bloons towards it and holds them there until they have been sucked into the tower. Lasts 8 seconds.";
        public override int Cost => 3800;
        public override int Path => MIDDLE;
        public override int Tier => 4;
        public override string Icon => Name + "-Icon";
        public override string Portrait => Name + "-Portrait";
        public override void ApplyUpgrade(TowerModel tower)
        {
            var abilityModel = new AbilityModel("AbilityModel_Vac", "Vacuum", "Huge burst of suction power brings all nearby non-MOAB class bloons towards it and holds them there until they have been sucked into the tower to then release them to the start of the track. Lasts 12 seconds.", 1, 0, GetSpriteReference(Icon), 20, null, false, false, null, 0, 0, -1, true, false);
            var ability = Game.instance.model.GetTowerFromId("PatFusty 10").GetAbility(1).GetBehavior<ActivateAttackModel>().Duplicate();
            abilityModel.cooldown = 25;
            var squeezeModel = ability.attacks[0];
            var squeezeModelWeapon = squeezeModel.weapons[0];
            var squeezeModelProjectile = squeezeModelWeapon.projectile;
            squeezeModel.range = 5000;
            squeezeModelWeapon.fireWithoutTarget = true;
            squeezeModelProjectile.collisionPasses = new[] { -1, 0, 1 };
            squeezeModelProjectile.radius = 5000;
            squeezeModelProjectile.maxPierce = 1000;
            squeezeModelProjectile.CapPierce(1000);
            squeezeModelProjectile.pierce = 1000;
            squeezeModelProjectile.GetDamageModel().damage = 0;
            squeezeModelProjectile.AddBehavior(new WindModel("WindModel", 1000, 1000, 1, false, "Wind", 0, "", 0.5f));
            squeezeModelProjectile.GetBehavior<SlowModel>().lifespan = 12;
            squeezeModelProjectile.RemoveBehavior<DelayBloonChildrenSpawningModel>();
            squeezeModelProjectile.filters = new Il2CppReferenceArray<FilterModel>(new FilterModel[] { new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze", "Moabs", new Il2CppStringArray(0)) });
            squeezeModelProjectile.GetBehavior<ProjectileFilterModel>().filters = new Il2CppReferenceArray<FilterModel>(new FilterModel[] { new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze", "Moabs", new Il2CppStringArray(0)) });
            squeezeModel.GetBehavior<AttackFilterModel>().filters = new Il2CppReferenceArray<FilterModel>(new FilterModel[] { new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze", "Moabs", new Il2CppStringArray(0)) });
            abilityModel.AddBehavior(ability);
            tower.AddBehavior(abilityModel);
        }
    }
    public class SupaVac : ModUpgrade<BloonChipper>
    {
        public override string DisplayName => "Supa-Vac";
        public override string Description => "Ability: Sucks in all but the biggest bloons for 20 seconds, dealing damage before releasing them to the start";
        public override int Cost => 29500;
        public override int Path => MIDDLE;
        public override int Tier => 5;
        public override string Icon => Name + "-Icon";
        public override string Portrait => Name + "-Portrait";
        public override void ApplyUpgrade(TowerModel tower)
        {
            var abilityModel = tower.GetAbility();
            abilityModel.cooldown = 35;
            var activateAttackModel = abilityModel.GetBehavior<ActivateAttackModel>();
            var squeezeModel = activateAttackModel.attacks[0];
            var squeezeModelWeapon = squeezeModel.weapons[0];
            var squeezeModelProjectile = squeezeModelWeapon.projectile;
            squeezeModelProjectile.name = "SupaVacProjectile";
            squeezeModelProjectile.maxPierce = 999999;
            squeezeModelProjectile.CapPierce(999999);
            squeezeModelProjectile.pierce = 999999;
            squeezeModelProjectile.GetDamageModel().maxDamage = 100;
            squeezeModelProjectile.GetDamageModel().CapDamage(100);
            squeezeModelProjectile.GetDamageModel().damage = 100;
            squeezeModelProjectile.GetDamageModel().distributeToChildren = true;
            squeezeModelProjectile.GetBehavior<SlowModel>().lifespan = 20;
            squeezeModelProjectile.GetBehavior<WindModel>().affectMoab = true;
            squeezeModelProjectile.filters = new Il2CppReferenceArray<FilterModel>(new FilterModel[] { new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze", "Bad", new Il2CppStringArray(0)) });
            squeezeModelProjectile.GetBehavior<ProjectileFilterModel>().filters = new Il2CppReferenceArray<FilterModel>(new FilterModel[] { new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze", "Bad", new Il2CppStringArray(0)) });
            squeezeModel.GetBehavior<AttackFilterModel>().filters = new Il2CppReferenceArray<FilterModel>(new FilterModel[] { new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze", "Bad", new Il2CppStringArray(0)) });

        }
    }
    public class Suckier: ModUpgrade<BloonChipper>
    {
        public override string DisplayName => "Suckier";
        public override string Description => "Increased suction sucks bloons in faster.";
        public override int Cost => 400;
        public override int Path => TOP;
        public override int Tier => 1;
        public override string Icon => Name + "-Icon";
        public override string Portrait => Name + "-Portrait";
        public override void ApplyUpgrade(TowerModel tower)
        {
            tower.GetAttackModels().ForEach(attackModel => attackModel.weapons[0].rate *= 0.5f);
        }
    }
    public class HeavyDutySuction  : ModUpgrade<BloonChipper>
    {
        public override string DisplayName => "Heavy Duty Suction";
        public override string Description => "Higher wattage motor allows for sucking in heavier bloons such as lead and ceramic bloons.";
        public override int Cost => 450;
        public override int Path => TOP;
        public override int Tier => 2;
        public override string Icon => Name + "-Icon";
        public override string Portrait => Name + "-Portrait";
        public override void ApplyUpgrade(TowerModel tower)
        {
            tower.GetAttackModels().ForEach(attackModel => attackModel.weapons[0].projectile.GetDamageModel().immuneBloonProperties = BloonProperties.None);
            if (tower.tiers[2] >= 1)
            {
                tower.GetAttackModels().ForEach(attackModel => attackModel.GetBehavior<AttackFilterModel>().filters = new Il2CppReferenceArray<FilterModel>(new FilterModel[] { new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze", "Moabs", new Il2CppStringArray(0)) }));
                tower.GetAttackModels().ForEach(attackModel => attackModel.weapons[0].projectile.filters = new Il2CppReferenceArray<FilterModel>(new FilterModel[] { new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze", "Moabs", new Il2CppStringArray(0)) }));
                tower.GetAttackModels().ForEach(attackModel => attackModel.weapons[0].projectile.GetBehavior<ProjectileFilterModel>().filters = new Il2CppReferenceArray<FilterModel>(new FilterModel[] { new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze", "Moabs", new Il2CppStringArray(0)) }));
            }
            else
            {
                tower.GetAttackModels().ForEach(attackModel => attackModel.GetBehavior<AttackFilterModel>().filters = new Il2CppReferenceArray<FilterModel>(new FilterModel[] { new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze", "Moabs", new Il2CppStringArray(0)), new FilterInvisibleModel("FilterInvisibleModel_", true, false), }));
                tower.GetAttackModels().ForEach(attackModel => attackModel.weapons[0].projectile.filters = new Il2CppReferenceArray<FilterModel>(new FilterModel[] { new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze", "Moabs", new Il2CppStringArray(0)), new FilterInvisibleModel("FilterInvisibleModel_", true, false) }));
                tower.GetAttackModels().ForEach(attackModel => attackModel.weapons[0].projectile.GetBehavior<ProjectileFilterModel>().filters = new Il2CppReferenceArray<FilterModel>(new FilterModel[] { new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze", "Moabs", new Il2CppStringArray(0)), new FilterInvisibleModel("FilterInvisibleModel_", true, false) }));
            }
        }
    }
    public class DualLayerBlades : ModUpgrade<BloonChipper>
    {
        public override string DisplayName => "Dual Layer Blades";
        public override string Description => "Dual layer blade array doubly shreds bloons, taking off 2 layers instead of 1.";
        public override int Cost => 800;
        public override int Path => TOP;
        public override int Tier => 3;
        public override string Icon => Name + "-Icon";
        public override string Portrait => Name + "-Portrait";
        public override void ApplyUpgrade(TowerModel tower)
        {
            tower.GetAttackModels().ForEach(attackModel => attackModel.weapons[0].projectile.GetDamageModel().maxDamage = 2);
            tower.GetAttackModels().ForEach(attackModel => attackModel.weapons[0].projectile.GetDamageModel().CapDamage(2));
            tower.GetAttackModels().ForEach(attackModel => attackModel.weapons[0].projectile.GetDamageModel().damage = 2);
        }
    }
    public class SuperWideFunnel  : ModUpgrade<BloonChipper>
    {
        public override string DisplayName => "Super Wide Funnel";
        public override string Description => "Wide funnel can suck in MOAB class bloons, which will be shredded for up to 3 seconds before being ejected.";
        public override int Cost => 4500;
        public override int Path => TOP;
        public override int Tier => 4;
        public override string Icon => Name + "-Icon";
        public override string Portrait => Name + "-Portrait";
        public override void ApplyUpgrade(TowerModel tower)
        {
            tower.GetAttackModels().ForEach(attackModel => attackModel.weapons[0].projectile.GetDamageModel().maxDamage = 10);
            tower.GetAttackModels().ForEach(attackModel => attackModel.weapons[0].projectile.GetDamageModel().CapDamage(10));
            tower.GetAttackModels().ForEach(attackModel => attackModel.weapons[0].projectile.GetDamageModel().damage = 10);
            tower.GetAttackModels().ForEach(attackModel => attackModel.weapons[0].projectile.AddBehavior(new DamageModifierForTagModel("DamageModifierForTagModel_Moabs", "Moabs", 1, 350, false, true)));
            if (tower.tiers[2] >= 1)
            {
                tower.GetAttackModels().ForEach(attackModel => attackModel.GetBehavior<AttackFilterModel>().filters = new Il2CppReferenceArray<FilterModel>(new FilterModel[] { new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze", "Bad", new Il2CppStringArray(0)), new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze", "Zomg", new Il2CppStringArray(0)), new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze", "Ddt", new Il2CppStringArray(0)) }));
                tower.GetAttackModels().ForEach(attackModel => attackModel.weapons[0].projectile.filters = new Il2CppReferenceArray<FilterModel>(new FilterModel[] { new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze", "Bad", new Il2CppStringArray(0)), new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze", "Zomg", new Il2CppStringArray(0)), new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze", "Ddt", new Il2CppStringArray(0)) }));
                tower.GetAttackModels().ForEach(attackModel => attackModel.weapons[0].projectile.GetBehavior<ProjectileFilterModel>().filters = new Il2CppReferenceArray<FilterModel>(new FilterModel[] { new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze", "Bad", new Il2CppStringArray(0)), new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze", "Zomg", new Il2CppStringArray(0)), new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze", "Ddt", new Il2CppStringArray(0)) }));
            }
            else
            {
                tower.GetAttackModels().ForEach(attackModel => attackModel.GetBehavior<AttackFilterModel>().filters = new Il2CppReferenceArray<FilterModel>(new FilterModel[] { new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze", "Bad", new Il2CppStringArray(0)), new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze", "Zomg", new Il2CppStringArray(0)), new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze", "Ddt", new Il2CppStringArray(0)), new FilterInvisibleModel("FilterInvisibleModel_", true, false) }));
                tower.GetAttackModels().ForEach(attackModel => attackModel.weapons[0].projectile.filters = new Il2CppReferenceArray<FilterModel>(new FilterModel[] { new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze", "Bad", new Il2CppStringArray(0)), new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze", "Zomg", new Il2CppStringArray(0)), new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze", "Ddt", new Il2CppStringArray(0)), new FilterInvisibleModel("FilterInvisibleModel_", true, false) }));
                tower.GetAttackModels().ForEach(attackModel => attackModel.weapons[0].projectile.GetBehavior<ProjectileFilterModel>().filters = new Il2CppReferenceArray<FilterModel>(new FilterModel[] { new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze", "Bad", new Il2CppStringArray(0)), new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze", "Zomg", new Il2CppStringArray(0)), new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze", "Ddt", new Il2CppStringArray(0)), new FilterInvisibleModel("FilterInvisibleModel_", true, false) }));
            }
        }
    }
    public class UltraWideFunnel : ModUpgrade<BloonChipper>
    {
        public override string DisplayName => "Ultra Wide Funnel";
        public override string Description => "Even wider funnel can suck even bigger bloons.";
        public override int Cost => 24000;
        public override int Path => TOP;
        public override int Tier => 5;
        public override string Icon => Name + "-Icon";
        public override string Portrait => Name + "-Portrait";
        public override void ApplyUpgrade(TowerModel tower)
        {
            tower.GetAttackModels().ForEach(attackModel => attackModel.weapons[0].projectile.GetDamageModel().maxDamage = 30);
            tower.GetAttackModels().ForEach(attackModel => attackModel.weapons[0].projectile.GetDamageModel().CapDamage(30));
            tower.GetAttackModels().ForEach(attackModel => attackModel.weapons[0].projectile.GetDamageModel().damage = 30);
            tower.GetAttackModels().ForEach(attackModel => attackModel.weapons[0].projectile.GetBehavior<DamageModifierForTagModel>().damageAddative = 1200);
            if (tower.tiers[2] >= 1)
            {
                tower.GetAttackModels().ForEach(attackModel => attackModel.GetBehavior<AttackFilterModel>().filters = new Il2CppReferenceArray<FilterModel>(new FilterModel[] { new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze", "Bad", new Il2CppStringArray(0)) }));
                tower.GetAttackModels().ForEach(attackModel => attackModel.weapons[0].projectile.filters = new Il2CppReferenceArray<FilterModel>(new FilterModel[] { new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze", "Bad", new Il2CppStringArray(0)) }));
                tower.GetAttackModels().ForEach(attackModel => attackModel.weapons[0].projectile.GetBehavior<ProjectileFilterModel>().filters = new Il2CppReferenceArray<FilterModel>(new FilterModel[] { new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze", "Bad", new Il2CppStringArray(0)) }));
            }
            else
            {
                tower.GetAttackModels().ForEach(attackModel => attackModel.GetBehavior<AttackFilterModel>().filters = new Il2CppReferenceArray<FilterModel>(new FilterModel[] { new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze", "Bad", new Il2CppStringArray(0)), new FilterInvisibleModel("FilterInvisibleModel_", true, false) }));
                tower.GetAttackModels().ForEach(attackModel => attackModel.weapons[0].projectile.filters = new Il2CppReferenceArray<FilterModel>(new FilterModel[] { new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze", "Bad", new Il2CppStringArray(0)), new FilterInvisibleModel("FilterInvisibleModel_", true, false) }));
                tower.GetAttackModels().ForEach(attackModel => attackModel.weapons[0].projectile.GetBehavior<ProjectileFilterModel>().filters = new Il2CppReferenceArray<FilterModel>(new FilterModel[] { new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze", "Bad", new Il2CppStringArray(0)), new FilterInvisibleModel("FilterInvisibleModel_", true, false) }));
            }
        }
    }
}