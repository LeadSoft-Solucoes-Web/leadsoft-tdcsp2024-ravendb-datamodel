namespace LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Products
{
    public partial class Product
    {
        public static IList<Product> GetSamples()
        =>
        [
            // Lightsaber - Sabre de luz icônico dos Jedi
            new Product("Lightsaber").AddPrice(new Price("Preço de Lançamento", 450190, 365255))
                                     .AddPrice(new Price("Promoção de Natal", 400000, 350000, new DateTime(2024, 12, 15), new DateTime(2024, 12, 31))),
        
            // Turbolaser - Potente arma de nave
            new Product("Turbolaser").AddPrice(new Price("Preço de Lançamento", 900000, 750000)),

            // Ion Cannon - Canhão de íons usado para desativar naves
            new Product("Ion Cannon").AddPrice(new Price("Preço de Lançamento", 600000, 500000))
                                     .AddPrice(new Price("Oferta de Black Friday", 550000, 495000, new DateTime(2024, 11, 20), new DateTime(2024, 11, 27))),

            // Tractor Beam - Feixe trator usado para imobilizar naves
            new Product("Tractor Beam").AddPrice(new Price("Preço de Lançamento", 750000, 620000))
                                       .AddPrice(new Price("Desconto de Ano Novo", 700000, 615000, new DateTime(2025, 1, 1), new DateTime(2025, 1, 10))),

            // Concussion Missile - Míssil de concussão, explosão pesada
            new Product("Concussion Missile").AddPrice(new Price("Preço de Lançamento", 300000, 270000)),

            // Proton Torpedo - Torpedo de próton, alta precisão
            new Product("Proton Torpedo").AddPrice(new Price("Preço de Lançamento", 350000, 300000)),

            // DC-15A Blaster Rifle - Rifle blaster padrão da República
            new Product("DC-15A Blaster Rifle").AddPrice(new Price("Preço de Lançamento", 120000, 100000))
                                               .AddPrice(new Price("Promoção de Setembro", 110000, 95000, new DateTime(2024, 9, 20), new DateTime(2024, 9, 30))),

            // Z-6 Rotary Blaster Cannon - Canhão blaster rotativo pesado
            new Product("Z-6 Rotary Blaster Cannon").AddPrice(new Price("Preço de Lançamento", 350000, 300000)),

            // E-11 Blaster Rifle - Rifle blaster padrão do Império
            new Product("E-11 Blaster Rifle").AddPrice(new Price("Preço de Lançamento", 85000, 70000))
                                             .AddPrice(new Price("Desconto Relâmpago", 80000, 68000, new DateTime(2024, 10, 15), new DateTime(2024, 10, 20))),

            // DLT-19 Heavy Blaster Rifle - Rifle blaster pesado usado por stormtroopers
            new Product("DLT-19 Heavy Blaster Rifle").AddPrice(new Price("Preço de Lançamento", 150000, 130000)),

            // DL-44 Heavy Blaster Pistol - Pistola blaster pesada de Han Solo
            new Product("DL-44 Heavy Blaster Pistol").AddPrice(new Price("Preço de Lançamento", 95000, 85000))
                                                     .AddPrice(new Price("Desconto do Caçador", 90000, 83000, new DateTime(2024, 11, 1), new DateTime(2024, 11, 10))),

            // Wookiee Bowcaster - Arco-blaster tradicional dos Wookiees
            new Product("Wookiee Bowcaster").AddPrice(new Price("Preço de Lançamento", 250000, 220000)),

            // Vibro-blade - Lâmina vibro, arma corpo a corpo mortal
            new Product("Vibro-blade").AddPrice(new Price("Preço de Lançamento", 70000, 60000)),

            // Force Pike - Pique energético usado pela Guarda Imperial
            new Product("Force Pike").AddPrice(new Price("Preço de Lançamento", 130000, 115000)),

            // Thermal Detonator - Detonador térmico, explosivo altamente destrutivo
            new Product("Thermal Detonator").AddPrice(new Price("Preço de Lançamento", 50000, 40000))
                                            .AddPrice(new Price("Promoção de Fim de Ano", 45000, 38000, new DateTime(2024, 12, 20), new DateTime(2024, 12, 31))),

            // CR-28 - Arma incendiária
            new Product("CR-28").AddPrice(new Price("Preço de Lançamento", 100000, 85000)),

            // Merr-Sonn - Lançador de projéteis pesado
            new Product("Merr-Sonn").AddPrice(new Price("Preço de Lançamento", 180000, 160000))
        ];
    }
}
