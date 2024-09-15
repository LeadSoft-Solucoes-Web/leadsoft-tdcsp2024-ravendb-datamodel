namespace LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Products.Categories
{
    public partial class Category
    {
        public static IList<Category> GetSamples()
            =>
            [
                new("Starship Weapons"),
                new("Personal Ranged Weapons"),
                new("Repeating Blasters"),
                new("Heavy Launchers"),
                new("Melee Weapons"),
                new("Demolition Weapons"),
                new("Incendiary Weapons")
            ];

        public static string GetNameByProduct(string productName)
        {
            if (!ProductCategoryMap.TryGetValue(productName.ToLower(), out string category))
                return string.Empty;

            return category;
        }

        private static readonly Dictionary<string, string> ProductCategoryMap = new()
        {
            // Starship Weapons
            { "Turbolaser".ToLower(), "Starship Weapons" },
            { "Ion cannon".ToLower(), "Starship Weapons" },
            { "Tractor beam".ToLower(), "Starship Weapons" },
            { "Gravity well projector".ToLower(), "Starship Weapons" },
            { "Concussion missile".ToLower(), "Starship Weapons" },
            { "Proton torpedo".ToLower(), "Starship Weapons" },

            // Personal Ranged Weapons
            { "Accelerated Charged Particle Array Gun".ToLower(), "Personal Ranged Weapons" },
            { "Accelerated Charged Particle Repeater Gun".ToLower(), "Personal Ranged Weapons" },
            { "A280 blaster rifle".ToLower(), "Personal Ranged Weapons" },
            { "DC-15A blaster rifle".ToLower(), "Personal Ranged Weapons" },
            { "DC-15s side arm blaster".ToLower(), "Personal Ranged Weapons" },
            { "DC-15S blaster rifle".ToLower(), "Personal Ranged Weapons" },
            { "DC-17 blaster rifle".ToLower(), "Personal Ranged Weapons" },
            { "DC-17m Interchangeable Weapon System".ToLower(), "Personal Ranged Weapons" },
            { "Z-6 rotary blaster cannon".ToLower(), "Personal Ranged Weapons" },
            { "E-11 blaster rifle".ToLower(), "Personal Ranged Weapons" },
            { "WESTAR-M5 blaster rifle".ToLower(), "Personal Ranged Weapons" },
            { "DLT-19 Heavy Blaster Rifle".ToLower(), "Personal Ranged Weapons" },
            { "DLT-20A blaster rifle".ToLower(), "Personal Ranged Weapons" },
            { "DL-44 heavy blaster pistol".ToLower(), "Personal Ranged Weapons" },
            { "DH-17 blaster pistol".ToLower(), "Personal Ranged Weapons" },
            { "T-21 light repeating blaster".ToLower(), "Personal Ranged Weapons" },
            { "CDEF pistol/rifle/carbine".ToLower(), "Personal Ranged Weapons" },
            { "Wookiee Bowcaster".ToLower(), "Personal Ranged Weapons" },
            { "Tenloss Disruptor Rifle".ToLower(), "Personal Ranged Weapons" },
            { "Imperial Repeater".ToLower(), "Personal Ranged Weapons" },
            { "Imperial Heavy Repeater".ToLower(), "Personal Ranged Weapons" },
            { "Golan Arms FC-1 Flechette".ToLower(), "Personal Ranged Weapons" },
            { "Stouker Concussion Rifle".ToLower(), "Personal Ranged Weapons" },
            { "LJ-50 concussion rifle".ToLower(), "Personal Ranged Weapons" },
            { "Disruptor".ToLower(), "Personal Ranged Weapons" },
            { "Modified Bryar Pistol".ToLower(), "Personal Ranged Weapons" },
            { "WESTAR-34 blaster pistol".ToLower(), "Personal Ranged Weapons" },
            { "DT-57 heavy blaster pistol".ToLower(), "Personal Ranged Weapons" },
            { "SE-14R light repeating blaster".ToLower(), "Personal Ranged Weapons" },
            { "Jeron Fusion Cutter".ToLower(), "Personal Ranged Weapons" },
            { "Destructive EMP 2 gun".ToLower(), "Personal Ranged Weapons" },
            { "Training Remote".ToLower(), "Personal Ranged Weapons" },
            { "Carbonite Freeze Gun".ToLower(), "Personal Ranged Weapons" },

            // Repeating Blasters
            { "E WHB-10".ToLower(), "Repeating Blasters" },
            { "EWHB-12".ToLower(), "Repeating Blasters" },
            { "E-web heavy repeating blaster".ToLower(), "Repeating Blasters" },
            { "Medium repeating blaster cannon".ToLower(), "Repeating Blasters" },
            { "E-Web(15) Heavy Blaster".ToLower(), "Repeating Blasters" },
            { "F-Web Repeating Blaster".ToLower(), "Repeating Blasters" },
            { "M-Web Repeating Blaster".ToLower(), "Repeating Blasters" },
            { "Repeating-blaster".ToLower(), "Repeating Blasters" },

            // Heavy Launchers
            { "Merr-Sonn".ToLower(), "Heavy Launchers" },
            { "PLX-2M Portable Missile System".ToLower(), "Heavy Launchers" },
            { "Rail charges launcher".ToLower(), "Heavy Launchers" },
            { "Packered Mortar Gun".ToLower(), "Heavy Launchers" },
            { "Assault Cannon".ToLower(), "Heavy Launchers" },
            { "HH-15 projectile launcher".ToLower(), "Heavy Launchers" },
            { "MiniMag PTL missile launcher".ToLower(), "Heavy Launchers" },

            // Melee Weapons
            { "Energy Sword".ToLower(), "Melee Weapons" },
            { "Lightsaber".ToLower(), "Melee Weapons" },
            { "Vibro-blade".ToLower(), "Melee Weapons" },
            { "Vibrosword".ToLower(), "Melee Weapons" },
            { "Vibro-ax".ToLower(), "Melee Weapons" },
            { "Vibrostave".ToLower(), "Melee Weapons" },
            { "Vibro-whip".ToLower(), "Melee Weapons" },
            { "Force Pike".ToLower(), "Melee Weapons" },
            { "Stun baton".ToLower(), "Melee Weapons" },
            { "Electrostaff".ToLower(), "Melee Weapons" },
            { "Morgukai".ToLower(), "Melee Weapons" },
            { "Cortosis Staff".ToLower(), "Melee Weapons" },
            { "Lightwhip".ToLower(), "Melee Weapons" },
            { "Double-Bladed Sword".ToLower(), "Melee Weapons" },

            // Demolition Weapons
            { "Thermal detonator".ToLower(), "Demolition Weapons" },
            { "Flash detonator".ToLower(), "Demolition Weapons" },
            { "Sonic detonator".ToLower(), "Demolition Weapons" },
            { "Cryoban grenade".ToLower(), "Demolition Weapons" },
            { "I.M. Mines".ToLower(), "Demolition Weapons" },
            { "Trip Mine".ToLower(), "Demolition Weapons" },
            { "Sequencer charge".ToLower(), "Demolition Weapons" },
            { "Detonation pack".ToLower(), "Demolition Weapons" },
            { "Concussion grenade".ToLower(), "Demolition Weapons" },
            { "Plasticine Thermite Gel".ToLower(), "Demolition Weapons" },

            // Incendiary Weapons
            { "CR-28".ToLower(), "Incendiary Weapons" }
        };
    }
}
