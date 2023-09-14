
using Microsoft.EntityFrameworkCore;
using Dinotrack.Shared.Entities;
using Dinotrack.Backend.Services;
using Dinotrack.Shared.Responses;

namespace Dinotrack.Backend.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IApiService _apiService;

        public SeedDb(DataContext context, IApiService apiService)
        {
            _context = context;
            _apiService = apiService;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
            await CheckBrandsAsync();
            await CheckWorkshopsAsync();
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
                        new Ref() { Name = "V80_2000"},
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
                    Name = "BMW",
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
                    Name = "Ducati",
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
                    Name = "Triumph",
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
                    Name = "AKT",
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
                    Name = "Harley Davidson",
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
                    Name = "Kymico",
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
                    Name = "Bajaj",
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
                    Name = "TVS",
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
                    Name = "Victory",
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
