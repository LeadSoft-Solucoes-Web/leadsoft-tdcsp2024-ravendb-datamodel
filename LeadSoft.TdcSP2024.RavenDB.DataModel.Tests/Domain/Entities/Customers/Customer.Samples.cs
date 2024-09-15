using static LeadSoft.Common.Library.Enumerators.Enums;

namespace LeadSoft.TdcSP2024.RavenDB.DataModel.Tests.Domain.Entities.Customers
{
    public partial class Customer
    {
        public static IList<Customer> GetSamples()
        =>
        [
            // Darth Vader (Anakin Skywalker)
        new Customer("Darth Vader", new DateTime(1977, 1, 1))  // 1977, ano de estreia de Star Wars: Uma Nova Esperança
            .AddEmail("darthvader@siths.glx", true)
            .AddAddress("Mustafar Castle", new Address { Street = "Vulcano Ave", Number = "666", City = "Mustafar", UF = UF.SP }),

        // Anakin Skywalker
        new Customer("Anakin Skywalker", new DateTime(1980, 5, 4))  // 1980, ano de lançamento de O Império Contra-Ataca
            .AddEmail("anakin@jeditemple.org")
            .AddAddress("Coruscant Temple", new Address { Street = "Temple Rd", Number = "77", City = "Coruscant", UF = UF.MG })
            .Deactivate(),

        // Obi-Wan Kenobi
        new Customer("Obi-Wan Kenobi", new DateTime(1956, 3, 25))  // Data criativa, reflete um Jedi mais velho e sábio
            .AddEmail("kenobi@tatooine.res")
            .AddAddress("Hut no Deserto", new Address { Street = "Dune Sea", Number = "101", City = "Tatooine", UF = UF.MG }),

        // Luke Skywalker
        new Customer("Luke Skywalker", new DateTime(1983, 5, 25))  // 1983, lançamento de O Retorno de Jedi
            .AddEmail("luke@rebelalliance.org")
            .AddAddress("Fazenda Lars", new Address { Street = "Moisture Farm", Number = "12", City = "Tatooine", UF = UF.RJ }),

        // Princesa Leia
        new Customer("Leia Organa", new DateTime(1983, 5, 25))  // Gêmea de Luke
            .AddEmail("leia@rebellion.gal")
            .AddAddress("Alderaan Palace", new Address { Street = "Royal Rd", Number = "1", City = "Alderaan", UF = UF.PR }),

        // Mestre Yoda
        new Customer("Mestre Yoda", new DateTime(1920, 9, 1))  // Refletindo um ser muito mais velho
            .AddEmail("yoda@dagobah.master")
            .AddAddress("Swamp Hut", new Address { Street = "Dagobah Marshes", Number = "108", City = "Dagobah", UF = UF.SP })
            .Deactivate(),

        // Imperador Palpatine / Darth Sidious
        new Customer("Imperador Palpatine", new DateTime(1950, 4, 1))  // Data criativa para representar sua idade avançada
            .AddEmail("emperor@theempire.gal")
            .AddAddress("Imperial Throne", new Address { Street = "Death Star", Number = "1", City = "Space", UF = UF.PR })
            .Deactivate(),

        // C-3PO
        new Customer("C-3PO", new DateTime(1977, 5, 25))  // 1977, primeira aparição de C-3PO
            .AddEmail("c3po@protocol.droid")
            .AddAddress("Rebel Base", new Address { Street = "Echo Base", Number = "C3", City = "Hoth", UF = UF.PR }),

        // R2-D2
        new Customer("R2-D2", new DateTime(1977, 5, 25))  // Mesmo "nascimento" de C-3PO
            .AddEmail("r2d2@astromech.droid")
            .AddAddress("Rebel Base", new Address { Street = "Echo Base", Number = "R2", City = "Hoth", UF = UF.MG }),

        // Han Solo
        new Customer("Han Solo", new DateTime(1977, 5, 25))  // 1977, ano de estreia de Han
            .AddEmail("han@millenniumfalcon.gal")
            .AddAddress("Millennium Falcon", new Address { Street = "Hangar 94", Number = "MF", City = "Mos Eisley", UF = UF.SP }),

        // Chewbacca
        new Customer("Chewbacca", new DateTime(1950, 1, 1))  // Chewbacca é muito mais velho, então um ano mais antigo faz sentido
            .AddEmail("chewie@kashyyyk.wookiee")
            .AddAddress("Kachirho", new Address { Street = "Treehouse", Number = "W1", City = "Kashyyyk", UF = UF.ES }),

        // Padmé Amidala
        new Customer("Padmé Amidala", new DateTime(1983, 5, 25))  // 1983, data criativa
            .AddEmail("padme@naboosenate.org")
            .AddAddress("Naboo Palace", new Address { Street = "Queen Ave", Number = "33", City = "Theed", UF = UF.RJ }),

        // Conde Dookan
        new Customer("Conde Dookan", new DateTime(1940, 10, 1))  // Representando uma figura mais velha e sábia
            .AddEmail("dooku@sithlords.gal")
            .AddAddress("Serenno Estate", new Address { Street = "Dark Tower", Number = "99", City = "Serenno", UF = UF.SP })
            .Deactivate(),

        // Lando Calrissian
        new Customer("Lando Calrissian", new DateTime(1977, 6, 1))  // Um pouco mais jovem que Han Solo
            .AddEmail("lando@cloudcity.biz")
            .AddAddress("Cloud City", new Address { Street = "Lando Rd", Number = "B7", City = "Bespin", UF = UF.SP }),

        // Mace Windu
        new Customer("Mace Windu", new DateTime(1955, 8, 10))  // Outra figura mais velha, líder no Conselho Jedi
            .AddEmail("macewindu@jeditemple.org")
            .AddAddress("Coruscant Temple", new Address { Street = "Temple Rd", Number = "55", City = "Coruscant", UF = UF.MG })
            .Deactivate(),

        // Qui-Gon Jinn
        new Customer("Qui-Gon Jinn", new DateTime(1945, 12, 15))  // Mentor de Obi-Wan, mais velho e sábio
            .AddEmail("quigon@jeditemple.org")
            .AddAddress("Coruscant Temple", new Address { Street = "Temple Rd", Number = "66", City = "Coruscant", UF = UF.SP })
        ];
    }
}
