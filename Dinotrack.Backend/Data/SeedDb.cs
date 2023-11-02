
using Microsoft.EntityFrameworkCore;
using Dinotrack.Shared.Entities;
using Dinotrack.Backend.Services;
using Dinotrack.Shared.Responses;
using Dinotrack.Backend.Helper;
using Dinotrack.Shared.Enums;

namespace Dinotrack.Backend.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IApiService _apiService;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IApiService apiService, IUserHelper userHelper)
        {
            _context = context;
            _apiService = apiService;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
            await CheckBrandsAsync();
            await CheckWorkshopsAsync();
            await CheckRolesAsync();
            await CheckUserAsync("1010", "David", "Gómez", "dagomez@yopmail.com", "3014278799", "Carrera 38", UserType.Admin);

        }

        private async Task<User> CheckUserAsync(string document, string firstName, string lastName, string email, string phone, string address, UserType userType)
        {
            var user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                var city = await _context.Cities.FirstOrDefaultAsync(x => x.Name == "Medellín");
                if (city == null)
                {
                    city = await _context.Cities.FirstOrDefaultAsync();
                }

                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    City = city,
                    UserType = userType,
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());
                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);

            }

            return user;

        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
            await _userHelper.CheckRoleAsync(UserType.MechanicAdmin.ToString());
        }


        private async Task CheckBrandsAsync()
        {
            if (!_context.Brands.Any())
            {
                _context.Brands.Add(new Brand 
                { 
                    Name = "Suzuki",
                    Refs = new List<Ref>() 
                    {
                        new Ref() { Name = "DR150"}, 
                        new Ref() { Name = "V-STROM 250 SX"}, 
                        new Ref() { Name = "GIXXER 150 ABS"}, 
                        new Ref() { Name = "GIXXER 150 FI"}, 
                        new Ref() { Name = "GIXXER SF 150 ABS"}, 
                        new Ref() { Name = "GSX S150 ABS"}, 
                        new Ref() { Name = "GSX-R150 ABS"}, 
                        new Ref() { Name = "GIXXER 250"}, 
                        new Ref() { Name = "GIXXER SF 250"}, 
                        new Ref() { Name = "AX4 EVOLUTION EURO 3"}, 
                        new Ref() { Name = "AX4 EURO 3"}, 
                        new Ref() { Name = "GSXS 1000 GT"}, 
                    }
                });
                _context.Brands.Add(new Brand 
                { 
                    Name = "Yamaha",
                    Refs = new List<Ref>()
                    {
                        new Ref() { Name = "V80_2000"},
                        new Ref() { Name = "V80_2000-PRO"},
                        new Ref() { Name = "Axis_2001"},
                        new Ref() { Name = "BWS 100_2007"},
                        new Ref() { Name = "BWS 125_2012"},
                        new Ref() { Name = "Crypton 115_2010"},
                        new Ref() { Name = "Crypton 115_2014"},
                        new Ref() { Name = "FZ8 S_2013"},
                        new Ref() { Name = "MT07_2014"},
                        new Ref() { Name = "2015_FAZER"},
                        new Ref() { Name = "SZ-RR_B391_2016"},
                        new Ref() { Name = "Tenere_250_2015"},
                    }
                });
                _context.Brands.Add(new Brand 
                { 
                    Name = "Honda",
                    Refs = new List<Ref>()
                    {
                        new Ref() { Name = "Honda CB110"},
                        new Ref() { Name = "Navi"},
                        new Ref() { Name = "Wave 110s"},
                        new Ref() { Name = "Honda VFR 1200 XD"},
                        new Ref() { Name = "CBR 1000RR ABS"},
                        new Ref() { Name = "Pioneer 1000"},
                        new Ref() { Name = "PCX 160 ABS 2024"},
                        new Ref() { Name = "PCX 150 DLX"},
                        new Ref() { Name = "AFRICA TWIN ADVENTURE SPORTS SE"},
                        new Ref() { Name = "X-ADV"},
                        new Ref() { Name = "CB 125F"},
                        new Ref() { Name = "X-Blade 160"},
                    }
                });
                _context.Brands.Add(new Brand 
                { 
                    Name = "Kawasaki",
                    Refs = new List<Ref>() 
                    {
                        new Ref() { Name = "K-100"},
                        new Ref() { Name = "K-150"},
                        new Ref() { Name = "KW 200"},
                        new Ref() { Name = "KW 400"},
                        new Ref() { Name = "NAS 250"},
                        new Ref() { Name = "RSX 350"},
                        new Ref() { Name = "RX 600"},
                        new Ref() { Name = "RX 1000"},
                        new Ref() { Name = "RZ X-1000"},
                        new Ref() { Name = "AX EURO PLUS"},
                        new Ref() { Name = "ROAD PT 500"},
                        new Ref() { Name = "GTRACK 1000 GT"},
                    }
                });
                _context.Brands.Add(new Brand 
                { 
                    Name = "BMW",
                    Refs = new List<Ref>() 
                    {
                        new Ref() { Name = "BMW - 100"},
                        new Ref() { Name = "BMW - 200"},
                        new Ref() { Name = "BMW - 400"},
                        new Ref() { Name = "BMW - 600"},
                        new Ref() { Name = "BMW - 1000"},
                        new Ref() { Name = "BMW - 1200"},
                        new Ref() { Name = "BMW - 100 PRO"},
                        new Ref() { Name = "BMW - 200 PRO"},
                        new Ref() { Name = "BMW - 400 PRO"},
                        new Ref() { Name = "BMW - 600 PRO"},
                        new Ref() { Name = "BMW - 1000 PRO"},
                        new Ref() { Name = "BMW - 1200 PRO"},
                    }
                });
                _context.Brands.Add(new Brand 
                { 
                    Name = "Ducati",
                    Refs = new List<Ref>()
                    {
                        new Ref() { Name = "DUCATI-R1000"},
                        new Ref() { Name = "DUCATI-R600 PRO"},
                        new Ref() { Name = "DUCATI-R600"},
                        new Ref() { Name = "DUCATI-R400 PRO"},
                        new Ref() { Name = "DUCATI-R400"},
                        new Ref() { Name = "DUCATI-R200 PRO"},
                        new Ref() { Name = "DUCATI-R200"},
                        new Ref() { Name = "DUCATI-R100 PRO"},
                        new Ref() { Name = "DUCATI-R100"},
                        new Ref() { Name = "DUCATI-R250 PRO"},
                        new Ref() { Name = "DUCATI-R250"},
                        new Ref() { Name = "DUCATI-R1"},
                    }            });
                _context.Brands.Add(new Brand 
                { 
                    Name = "Triumph",
                    Refs = new List<Ref>()
                    {
                        new Ref() { Name = "TR - GT 1000 PRO" },
                        new Ref() { Name = "TR - GT 1000" },
                        new Ref() { Name = "TR - GT 800 PRO" },
                        new Ref() { Name = "TR - GT 800" },
                        new Ref() { Name = "TR - GT 600 PRO" },
                        new Ref() { Name = "TR - GT 600" },
                        new Ref() { Name = "TR - GT 400 PRO" },
                        new Ref() { Name = "TR - GT 400" },
                        new Ref() { Name = "TR - GT 200 PRO" },
                        new Ref() { Name = "TR - GT 200" },
                        new Ref() { Name = "TR - GT 10" },
                        new Ref() { Name = "TR - GT 1" },
                    }
                });
                _context.Brands.Add(new Brand 
                { 
                    Name = "AKT",
                    Refs = new List<Ref>()
                    {
                        new Ref() { Name = "AK-100 GT" },
                        new Ref() { Name = "AK-200 LS" },
                        new Ref() { Name = "AK-200 GT" },
                        new Ref() { Name = "AK-400 LS" },
                        new Ref() { Name = "AK-400 GT" },
                        new Ref() { Name = "AK-600 LS" },
                        new Ref() { Name = "AK-600 GT" },
                        new Ref() { Name = "AK-1000 LS" },
                        new Ref() { Name = "AK-1000 GT" },
                        new Ref() { Name = "AK-125 GT" },
                        new Ref() { Name = "AK-125 PRO" },
                        new Ref() { Name = "AK-125 LS" },
                    }
                });
                _context.Brands.Add(new Brand 
                { 
                    Name = "Harley Davidson",
                    Refs = new List<Ref>()
                    {
                        new Ref() { Name = "HD - 1600 GT" },
                        new Ref() { Name = "HD - 1200 GT" },
                        new Ref() { Name = "HD - 1000 GT" },
                        new Ref() { Name = "HD - 900 GT" },
                        new Ref() { Name = "HD - 800 GT" },
                        new Ref() { Name = "HD - 700 GT" },
                        new Ref() { Name = "HD - 600 GT" },
                        new Ref() { Name = "HD - 500 GT" },
                        new Ref() { Name = "HD - 400 GT" },
                        new Ref() { Name = "HD - 300 GT" },
                        new Ref() { Name = "HD - 200 GT" },
                        new Ref() { Name = "HD - 100 GT" },
                    }
                });
                _context.Brands.Add(new Brand 
                { 
                    Name = "Kymico",
                    Refs = new List<Ref>()
                    {
                        new Ref() { Name = "K-110 A" },
                        new Ref() { Name = "K-110 B" },
                        new Ref() { Name = "K-110 C" },
                        new Ref() { Name = "K-110 D" },
                        new Ref() { Name = "K-110 E" },
                        new Ref() { Name = "K-110 F" },
                        new Ref() { Name = "K-110 G" },
                        new Ref() { Name = "K-110 H" },
                        new Ref() { Name = "K-110 I" },
                        new Ref() { Name = "K-110 J" },
                        new Ref() { Name = "K-110 K" },
                        new Ref() { Name = "K-110 L" },
                    }
                });
                _context.Brands.Add(new Brand 
                { 
                    Name = "Bajaj",
                    Refs = new List<Ref>() 
                    {
                        new Ref() { Name = "B-DR150"},
                        new Ref() { Name = "B-V-STROM 250 SX"},
                        new Ref() { Name = "B-GIXXER 150 ABS"},
                        new Ref() { Name = "B-GIXXER 150 FI"},
                        new Ref() { Name = "B-GIXXER SF 150 S"},
                        new Ref() { Name = "B-GSX S150 ABS"},
                        new Ref() { Name = "B-GSX-R150 ABS"},
                        new Ref() { Name = "B-GIXXER 250"},
                        new Ref() { Name = "B-GIXXER SF 250"},
                        new Ref() { Name = "B-AX4 EVOLUTION 3"},
                        new Ref() { Name = "B-AX4 EURO 3"},
                        new Ref() { Name = "B-GSXS 1000 GT"},
                    }  
                });
                _context.Brands.Add(new Brand 
                { 
                    Name = "TVS",
                    Refs = new List<Ref>()
                    {
                        new Ref() { Name = "150 X"},
                        new Ref() { Name = "STROM 250 SX"},
                        new Ref() { Name = "XXER 150 ABS"},
                        new Ref() { Name = "XXER 150 FI"},
                        new Ref() { Name = "XXER SF 150 ABS"},
                        new Ref() { Name = "X S150 ABS"},
                        new Ref() { Name = "X-R150 ABS"},
                        new Ref() { Name = "XXER 250"},
                        new Ref() { Name = "XXER SF 250"},
                        new Ref() { Name = "4 EVOLUTION EURO 3"},
                        new Ref() { Name = "4 EURO 3"},
                        new Ref() { Name = "XS 1000 GT"},
                    }
                });
                _context.Brands.Add(new Brand 
                { 
                    Name = "Victory",
                    Refs = new List<Ref>()
                    {
                        new Ref() { Name = "V-DR150"},
                        new Ref() { Name = "V-STROM S" },
                        new Ref() { Name = "V-TRACK 1" },
                        new Ref() { Name = "V-TRACK 2" },
                        new Ref() { Name = "V-TRACK S" },
                        new Ref() { Name = "V-GSX-S150" },
                        new Ref() { Name = "V-GSX-R150" },
                        new Ref() { Name = "V-GIXXER 2" },
                        new Ref() { Name = "V-GIXXER S" },
                        new Ref() { Name = "V-AX4 EVOL" },
                        new Ref() { Name = "V-AX4 EURO" },
                        new Ref() { Name = "V-GSXS 100" },
                    }
                });
                await _context.SaveChangesAsync();
            }
        }


        private async Task CheckWorkshopsAsync()
        {
            if (!_context.Workshops.Any())
            {
                _context.Workshops.Add(new Workshop { Name = "Taller la 45" });
                _context.Workshops.Add(new Workshop { Name = "Yamaha Motors" });
                _context.Workshops.Add(new Workshop { Name = "Taller Honda" });
                _context.Workshops.Add(new Workshop { Name = "Concesionario Motoland" });
                _context.Workshops.Add(new Workshop { Name = "Workshop BMW" });
                _context.Workshops.Add(new Workshop { Name = "Ducati profesional shop" });
                _context.Workshops.Add(new Workshop { Name = "Taller de motos XYZ" });
                _context.Workshops.Add(new Workshop { Name = "Motos y Motos" });
                _context.Workshops.Add(new Workshop { Name = "MotoPlus" });
                _context.Workshops.Add(new Workshop { Name = "Susuki Motor" });
                _context.Workshops.Add(new Workshop { Name = "Centro de servicios zulu" });
                _context.Workshops.Add(new Workshop { Name = "Tallerama" });
                _context.Workshops.Add(new Workshop { Name = "Todo en motos Medellín" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                var responseCountries = await _apiService.GetAsync<List<CountryResponse>>("/v1", "/countries");
                if (responseCountries.WasSuccess)
                {
                    var countries = responseCountries.Result!;
                    foreach (var countryResponse in countries)
                    {
                        var country = await _context.Countries.FirstOrDefaultAsync(c => c.Name == countryResponse.Name!)!;
                        if (country == null)
                        {
                            country = new() { Name = countryResponse.Name!, States = new List<State>() };
                            var responseStates = await _apiService.GetAsync<List<StateResponse>>("/v1", $"/countries/{countryResponse.Iso2}/states");
                            if (responseStates.WasSuccess)
                            {
                                var states = responseStates.Result!;
                                foreach (var stateResponse in states!)
                                {
                                    var state = country.States!.FirstOrDefault(s => s.Name == stateResponse.Name!)!;
                                    if (state == null)
                                    {
                                        state = new() { Name = stateResponse.Name!, Cities = new List<City>() };
                                        var responseCities = await _apiService.GetAsync<List<CityResponse>>("/v1", $"/countries/{countryResponse.Iso2}/states/{stateResponse.Iso2}/cities");
                                        if (responseCities.WasSuccess)
                                        {
                                            var cities = responseCities.Result!;
                                            foreach (var cityResponse in cities)
                                            {
                                                if (cityResponse.Name == "Mosfellsbær" || cityResponse.Name == "Șăulița")
                                                {
                                                    continue;
                                                }
                                                var city = state.Cities!.FirstOrDefault(c => c.Name == cityResponse.Name!)!;
                                                if (city == null)
                                                {
                                                    state.Cities.Add(new City() { Name = cityResponse.Name! });
                                                }
                                            }
                                        }
                                        if (state.CitiesNumber > 0)
                                        {
                                            country.States.Add(state);
                                        }
                                    }
                                }
                            }
                            if (country.StatesNumber > 0)
                            {
                                _context.Countries.Add(country);
                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                }
            }
        }
    }
}
