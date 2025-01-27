using System.Diagnostics.Metrics;
using System.Linq.Expressions;
using TouristAgency.Data;
using TouristAgency.Model;

namespace TouristAgency
{
    public class Seed
    {
        // unapred popunjava Database podacima, treba popuniti podacima kao u springboot sql bazu

        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.Tourists.Any())
            {
                var categories = new List<Category>
                {
                    new Category { Name = "Letovanje" },
                    new Category { Name = "Zimovanje" },
                    new Category { Name = "Krstarenje" },
                    new Category { Name = "Evropski gradovi" },
                    new Category { Name = "Daleke destinacije" }
                };

                var destinations = new List<Destination>
                {
                    new Destination
                    {
                        Name = "Tokio",
                        Description = "APA Hotel Asakusa Tawaramachi Ekimae 3* u Tokiju je hotel sa tri zvezdice po lokalnoj kategorizaciji.",
                        Hotel = "APA Hotel Asakusa Tawaramachi Ekimae ",
                        HotelImages = new List<string> { "hotel1.jpg", "hotel2.jpg" }
                    },
                    new Destination
                    {
                        Name = "Krstarenje",
                        Description = "Brod- Icon of the Seas",
                        Hotel = "",
                        HotelImages = new List<string> { "brod3.jpg", "brod4.jpg" }
                    },
                     new Destination
                    {
                        Name = "Hanioti",
                        Description = " Vila Penny  je smeštena ispod magistrale, na 450m od plaže, a 600m od centralnog trga. ",
                        Hotel = "Vila Penny",
                        HotelImages = new List<string> { "han3.jpg", "han4.jpg" }
                    },
                       new Destination
                    {
                        Name = "Montekatini",
                        Description = "Hotel se nalazi u centru Montekatinija. Hotel ima restoran i bar, kao i besplatan WIFi. Svaka soba ima ima kupatilo, fen, TV, klimu. ",
                        Hotel = "Hotel Piccadilly 3* ",
                        HotelImages = new List<string> { "han3.jpg", "han4.jpg" }
                    },
                       new Destination
                    {
                        Name = "Atina",
                        Description = "Hotel Αchillion se nalazi u poslovnom centru Atine, na 200 metara od stanice metroa Omonija. U ponudi ima klimatizovane sobe sa balkonom. Besplatan kablovski internet dostupan je u čitavom hotelu. ",
                        Hotel = "Hotel Achillion 3* ",
                        HotelImages = new List<string> { "han3.jpg", "han4.jpg" }
                    }
                };
                var tourists = new List<Tourist>
                {
                    new Tourist
                    {
                        Name = "Nikola",
                        LastName = "Jokic",
                        UserName = "joker",
                        Email = "jok@example.com",
                        PhoneNumber = "123456789",
                        UserPhoto = "akdh.jpg",
                        Role= Role.Tourist,
                        Password="nik",
                        reservations = new List<Reservation>()

                    }
                };
                var organizers = new List<Organizer>
                {
                    new Organizer
                    {

                        Name = "Mila",
                        LastName = "Balic",
                        UserName = "mila12",
                        Email = "mila@example.com",
                        PhoneNumber = "987652221",
                        UserPhoto = "akdh.jpg",
                        Role= Role.Organizer,
                        Password="lozinka123"
                    }
                };

                var touristPackages = new List<TouristPackage>
                {
                    new TouristPackage
                    {
                        Organizer=organizers[0],
                        Name = "Japan i Kina",
                        Description = "Otkrijte magiju Dalekog Istoka kroz nezaboravan put u Japan i Kinu!" +
                            " Posetite moderne metropole poput Tokija i Šangaja, istražite mistične hramove," +
                            " slikovite vrtove i drevne tradicije koje spajaju prošlost i budućnost. Prepustite " +
                            "se jedinstvenoj avanturi uz pažljivo osmišljen program i udobnost na svakom koraku!",
                        Duration = 13,
                        DateOfDeparture = new DateTime(2025, 9, 1),
                        ReturnDate = new DateTime(2025, 9, 14),
                        BasePrice = 2299,
                        Capacity = 15,
                        Images = new List<string> { "Japan1.jpg", "Japan2.jpg" },
                        Schedule = "1.DAN - Polazak; 2. DAN - Dolazak u Tokio; obilazak Asakusa kvarta i hrama Senso-ji; 3. DAN - Tokio; poseta Šibuji i Šindžuku; 4. DAN - Tokio; istraživanje Akihabara kvarta i parka Ueno; 5. DAN - Tokio; jednodnevni izlet na Odaibu; 6. DAN - Putovanje u Kjoto; poseta svetištu Fušimi Inari Taiša; 7. DAN - Kjoto; obilazak Kinkaku-džija i Rjoan-džija; 8. DAN - Kjoto; poseta Gion distriktu i hramu Kijomizu-dera; 9. DAN - Putovanje u Osaku; poseta dvorcu Osaka; 10. DAN - Osaka; istraživanje Dotomborija i Šinsaibašija; 11. DAN - Jednodnevni izlet u Himeđi; poseta dvorcu Himeđi; 12. DAN - Povratak kući.",
                        PriceIncludes = "Smestaj, prevoz, hrana, osam obilazaka gratis(: Asakusa district, Sensoji hram," +
                             " Nakamise ulica, Ueno park, Carska palata, Tokyo Metropolitan Government (TMG), Shinjuku district, " +
                             "Kabukicho district)",
                        PriceDoesNotIncludes = "Medjunarnodno zdravstveno osiguranje, fakultativni izleti," +
                            "troskovi vadjena vize za putnike koji nisu drzavljani Republike Srbije, doplata za jednokrevetnu sobu",
                        Category = categories[4],
                        Destination = destinations[0],
                        Transportation= Transportation.Airplane,
                        RoomPrices = new List<double> { 2299 },
                        Trips = new List<Trip>
                        {
                            new Trip
                            {
                                Name="Obilazak Sangaja",
                                Description="Cena ukljucuje troškove transfera, usluge lokalnog vodiča na engleskom jeziku, uslugu pratioca grupe, ulaznicu za Yu vrtove",
                                Price=50
                            },
                            new Trip
                            {
                                Name="Obilazak Tokija 2",
                                Description="Drugi dan nastavljamo obilazak Tokija posetom Tsukidzi distriktu, japanskom ‘’gradu hrane’’ gde ćemo se susresti sa svim vrstama tradicionalne japanske hrane. Uživamo u ukusu originalnog japanskog sušija i ostalih specijaliteta od sveže ribe. Posle ručka put nastavljamo prema Ginzi, najstarijem i najpoznatijem šoping disktriktu Tokija. ",
                                Price=30
                            },
                            new Trip
                            {
                                Name="Paket izleta",
                                Description="Obilazak Hirosime, Kjota, Osake",
                                Price=359
                            }

                        }
                    },
                    new TouristPackage
                    {
                        Organizer=organizers[0],
                        Name = "Zapadni Karibi",
                        Description = "Krstarite Karibima i posetite Majami, Honduras, Meksiko i privatno Royal Caribbean ostrvo " +
                            "na Bahamima sa organizovanim povratnim avio prevozom, svim transferima," +
                            " tri noćenja u Majamiju, vodičem, 8-dnevnim krstarenjem i lučkim taksama.",
                        Duration = 10,
                        DateOfDeparture = new DateTime(2025, 8, 10),
                        ReturnDate = new DateTime(2025, 8, 20),
                        BasePrice = 3899,
                        Capacity = 25,
                        Images = new List<string> { "karibi1.jpg", "karibi2.jpg" },
                        Schedule = "1DAN - Polazak; 2. DAN - Dolazak u Miami; slobodno vreme za istraživanje grada; 3. DAN - Krstarenje; dan proveden na brodu uživajući u sadržajima i aktivnosti; 4. DAN - Pristanak na Bahamima; poseta Nassau i uživanje na plažama; 5. DAN - Krstarenje; dan na brodu sa opuštanjem i zabavom; 6. DAN - Pristanak u Jamajci; obilazak Montego Bay i uživanje u lokalnim atrakcijama; 7. DAN - Krstarenje; dan proveden na brodu, uživanje u zabavi i wellnessu; 8. DAN - Pristanak u Kajmanovim Ostrvima; istraživanje George Town i uživanje u vodi; 9. DAN - Krstarenje; opuštanje na brodu i uživanje u luksuznoj ponudi; 10. DAN - Povratak u Miami i povratak kući.",
                        PriceIncludes = "Smestaj, prevoz, hrana",
                        PriceDoesNotIncludes = "Medjunarnodno zdravstveno osiguranje, fakultativni izleti,troskovi vadjena vize za putnike koji nisu drzavljani Republike Srbije, doplata za jednokrevetnu sobu",
                        Category = categories[4],
                        Destination = destinations[1],
                        Transportation= Transportation.Airplane,
                        RoomPrices = new List<double> { 3899 },

                    },
                     new TouristPackage
                    {
                         Organizer=organizers[0],
                        Name = "Hanioti, Grcka",
                        Description = "Hanioti je poznato grčko letovalište smešteno na unutrašnjoj strani poluostrva Kasandra. Nalazi se na oko 105 km južno od Soluna, između takođe popularnih letovališta Pefkohori i Polihrono  i predstavlja jedno od najlepših turističkih mesta prvog prsta Halkidikija.",
                        Duration = 12,
                        DateOfDeparture = new DateTime(2025, 8, 10),
                        ReturnDate = new DateTime(2025, 8, 22),
                        BasePrice = 150,
                        Capacity = 20,
                        Images = new List<string> { "hanio.jpg", "hanio2.jpg" },
                        Schedule = "1DAN - Polazak; 2. DAN - Dolazak, smestaj u apartman; 3-11. DAN - individualne aktivnosti; 12. DAN - Povratak;",
                        PriceIncludes = "Smestaj, prevoz",
                        PriceDoesNotIncludes = "Medjunarnodno zdravstveno osiguranje",
                        Category = categories[0],
                        Destination = destinations[2],
                        Transportation= Transportation.Bus,
                        RoomPrices = new List<double> {175,150},
                        Trips = new List<Trip>
                        {
                            new Trip
                            {
                                Name="Pefkohori",
                                Description="Cena ukljucuje troškove transfera, usluge lokalnog vodiča",
                                Price=10
                            },
                            new Trip
                            {
                                Name="Solun",
                                Description="Cena ukljucuje troškove transfera, usluge lokalnog vodiča ",
                                Price=15
                            }
                        

                        }
                    },
                      new TouristPackage
                    {
                        Organizer=organizers[0],
                        Name = "Toskana, Italija",
                        Description = " Mesto gde se krije sunce. Savršeno izvajani predeli puni raznobojne flore pripadaju jednom od najmirnijih i najčešće posećenih predela u samoj Italiji. Domaćinske kućice u širokim razmacima bruje od priča starih Italijana, smeha, igre i naravno nezaobilaznog kuvanja. Ova predivna regija svojim mirisnim čempresima priča priče datirane hiljadama godina unazad. ",
                        Duration = 6,
                        DateOfDeparture = new DateTime(2025, 8, 10),
                        ReturnDate = new DateTime(2025, 8, 16),
                        BasePrice = 150,
                        Capacity = 50,
                        Images = new List<string> { "tos.jpg", "tos2.jpg" },
                        Schedule = "1. DAN - BEOGRAD,2. DAN. - MONTECATINI TERME 3. DAN. - MONTECATINI - SIENA - S. ĐIMINJANO 4. DAN. - MONTECATINI TERME – LUCA - PIZA 5. DAN. - MONTECATINI - FIRENCA 6. DAN - BEOGRAD",
                        PriceIncludes = "Smestaj, prevoz, dorucak",
                        PriceDoesNotIncludes = "Medjunarnodno zdravstveno osiguranje, fakultativni izleti, doplata za jednokrevetnu sobu",
                        Category = categories[3],
                        Destination = destinations[3],
                        Transportation= Transportation.Bus,
                        RoomPrices = new List<double> {150},
                        Trips = new List<Trip>
                        {
                            new Trip
                            {
                                Name="Siena i San Djimijano",
                                Description="Po dolasku u Sienu, grad koji je poznat po svojim kontradama koje imaju imena životinja, krećemo u šetnju sa lokalnim vodičem: Bazilika San Domeniko, Piazza Salimbeni, Katedrala Duomo, Piazza del Campo na kome se dva puta godišnje održava čuvena trka konja „Palio“.. Po čemu je specifičan i drugačiji ovaj gradić u dolinama čempresa? Po tome što ga zovu “Grad tornjeva”, nekada ih je imao 72, a danas samo 14 kao pečat tog srednjevekovnog perioda. ",
                                Price=35
                            },
                            new Trip
                            {
                                Name="Firenca",
                                Description="Krenućemo od Piazza Santa Maria Novele, Piazza della Signoria, katedrale Santa Maria del Fiore, krstionice i Đotovog zvonika do Danteove rodne kuće, Ponte Vechio, Palazzo Uffici.  ",
                                Price=30
                            },
                             new Trip
                            {
                                Name="Bolonja",
                                Description="Cena ukljucuje troškove transfera, usluge lokalnog vodiča ",
                                Price=20
                            }


                        }
                    },
                        new TouristPackage
                    {
                        Organizer=organizers[0],
                        Name = "Atina, Grcka",
                        Description = "Koracima stare civilizacije tamo gde su bile legende istorije, koje opeva Homer, kuda je Odisej plovio, tamo gde su ispisane čitave stranice svetske istorije, tamo gde su bogovi živeli i gde mitovi i danas žive...",
                        Duration = 6,
                        DateOfDeparture = new DateTime(2025, 8, 10),
                        ReturnDate = new DateTime(2025, 8, 16),
                        BasePrice = 140,
                        Capacity = 50,
                        Images = new List<string> { "tos.jpg", "tos2.jpg" },
                        Schedule = "1. DAN BEOGRAD, 2. DAN ATINA,3. DAN ATINA - PELOPONEZ - EPIDAURUS - NAFPLIO - ATINA, 4. DAN ATINA - PIREJ - RT SUNION,5. DAN ATINA - AKROPOLJ, 6. DAN BEOGRAD",
                        PriceIncludes = "Smestaj, prevoz, dorucak",
                        PriceDoesNotIncludes = "Medjunarnodno zdravstveno osiguranje, fakultativni izleti, doplata za jednokrevetnu sobu",
                        Category = categories[3],
                        Destination = destinations[4],
                        Transportation= Transportation.Bus,
                        RoomPrices = new List<double> {140},
                        Trips = new List<Trip>
                        {
                            new Trip
                            {
                                Name="Akropolis",
                                Description="Cena ukljucuje vodica i ulaznicu.",
                                Price=30
                            },
                         

                        }
                    }
                };

               
                var reservations = new List<Reservation>
                {
                    new Reservation
                            {
                                TouristPackage = touristPackages[2],
                                BedCount = 2,
                                ReservationDate = DateTime.Now,
                                Name = "Milan",
                                LastName = "Jaric",
                                Email = "milan@example.com",
                                PhoneNumber = "123456789",
                                JMBG = "0101999123422",
                                PaymentMethod = PaymentMethod.Cache,
                                
                            },
                    new Reservation
                            {
                                TouristPackage = touristPackages[2],
                                BedCount = 3,
                                ReservationDate = DateTime.Now,
                                Name = "Milana",
                                LastName = "Jeric",
                                Email = "milanaaa@example.com",
                                PhoneNumber = "123456789",
                                JMBG = "0101992123422",
                                PaymentMethod = PaymentMethod.Cache,

                            },


                };

              
                //dataContext.Categories.AddRange(categories);
                //dataContext.Destinations.AddRange(destinations);
                //dataContext.TouristPackages.AddRange(touristPackages);
                //dataContext.Tourists.AddRange(tourists);
                //dataContext.Organizers.AddRange(organizers);
                dataContext.Reservations.AddRange(reservations);
                dataContext.SaveChanges();
            }

        }
    }
}
