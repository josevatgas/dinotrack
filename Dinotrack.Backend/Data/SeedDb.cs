using Dinotrack.Backend.Helper;
using Dinotrack.Backend.Services;
using Dinotrack.Shared.Entities;
using Dinotrack.Shared.Enums;
using Dinotrack.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dinotrack.Backend.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IApiService _apiService;
        private readonly IUserHelper _userHelper;
        private readonly IFileStorage _fileStorage;

        public SeedDb(DataContext context, IApiService apiService, IUserHelper userHelper, IFileStorage fileStorage)
        {
            _context = context;
            _apiService = apiService;
            _userHelper = userHelper;
            _fileStorage = fileStorage;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            //await CheckCountriesAsync();
            await CheckCountriesAsync2();
            await CheckBrandsAsync();
            await CheckWorkshopsAsync();
            await CheckRolesAsync();
            await CheckUserAsync("0001", "David", "Gomez", "david@yopmail.com", "3003786703", "CALLE 40 # 40 - 11", "David Gomez.png", UserType.Admin);
            await CheckUserAsync("0002", "Abigail", "Aristizabal", "abigail@yopmail.com", "3003779951", "CALLE 53 # 74 - 58", "Abigail Aristizabal.png", UserType.User);
            await CheckUserAsync("0003", "Alexander", "Vega", "alexander@yopmail.com", "3005845731", "CARRERA 51 # 46 - 58", "Alexander Vega.png", UserType.User);
            await CheckUserAsync("0004", "Allison", "Smith", "allison@yopmail.com", "3002488471", "CALLE 93 # 50 - 34", "Allison Smith.png", UserType.User);
            await CheckUserAsync("0005", "Ana", "Cruz", "ana@yopmail.com", "3004401588", "CARRERA 41 # 63 - 5", "Ana Cruz.png", UserType.User);
            await CheckUserAsync("0006", "Carlos", "Sanchez", "carlos@yopmail.com", "3005471401", "CALLE 86 # 26 - 13", "Carlos Sanchez.png", UserType.User);
            await CheckUserAsync("0007", "David", "Gomez", "david@yopmail.com", "3002644737", "CALLE 81 # 10 - 43", "David Gomez.png", UserType.User);
            await CheckUserAsync("0008", "Haily", "Sunders", "haily@yopmail.com", "3005315718", "CALLE 98 # 37 - 74", "Haily Sunders.png", UserType.User);
            await CheckUserAsync("0009", "Juliana", "Restrepo", "juliana@yopmail.com", "3004897218", "CARRERA 85 # 82 - 50", "Juliana Restrepo.png", UserType.User);
            await CheckUserAsync("0010", "Maria", "Zuluaga", "maria@yopmail.com", "3003888772", "CARRERA 33 # 72 - 62", "Maria Zuluaga.png", UserType.User);
            await CheckUserAsync("0011", "Maximiliano", "Jurado", "maximiliano@yopmail.com", "3002890192", "CALLE 52 # 59 - 24", "Maximiliano Jurado.png", UserType.User);
            await CheckUserAsync("0012", "Michell", "Sanders", "michell@yopmail.com", "3006292495", "CARRERA 50 # 23 - 36", "Michell Sanders.png", UserType.User);
            await CheckUserAsync("0013", "Veronica", "Cadavid", "veronica@yopmail.com", "3003025616", "CARRERA 61 # 46 - 66", "Veronica Cadavid.png", UserType.User);
            await CheckUserAsync("0014", "Yin", "Zakata", "yin@yopmail.com", "3005168863", "CARRERA 77 # 76 - 57", "Yin Zakata.png", UserType.User);
            await CheckRefsAsync();
        }

        private async Task CheckRefsAsync()
        {
            if (!_context.Refs.Any())
            {
                await AddRefAsync("AKT TT Dual Sport", "Moto marca AKT, referencia AKT TT Dual Sport", 2023, "AKT", "AKT TT Dual Sport.png");
                await AddRefAsync("CR4 125", "Moto marca AKT, referencia CR4 125", 2023, "AKT", "CR4 125.png");
                await AddRefAsync("CR4 162 CARBON", "Moto marca AKT, referencia CR4 162 CARBON", 2023, "AKT", "CR4 162 CARBON.png");
                await AddRefAsync("CR4 162", "Moto marca AKT, referencia CR4 162", 2023, "AKT", "CR4 162.png");
                await AddRefAsync("DINAMIC K", "Moto marca AKT, referencia DINAMIC K", 2023, "AKT", "DINAMIC K.png");
                await AddRefAsync("DINAMIC PRO", "Moto marca AKT, referencia DINAMIC PRO", 2023, "AKT", "DINAMIC PRO.png");
                await AddRefAsync("FLEX LED", "Moto marca AKT, referencia FLEX LED", 2023, "AKT", "FLEX LED.png");
                await AddRefAsync("NKD 125 CLASSIC", "Moto marca AKT, referencia NKD 125 CLASSIC", 2023, "AKT", "NKD 125 CLASSIC.png");
                await AddRefAsync("NKD 125", "Moto marca AKT, referencia NKD 125", 2023, "AKT", "NKD 125.png");
                await AddRefAsync("SPECIAL 110 X", "Moto marca AKT, referencia SPECIAL 110 X", 2023, "AKT", "SPECIAL 110 X.png");
                await AddRefAsync("VOGE 3000S", "Moto marca AKT, referencia VOGE 3000S", 2023, "AKT", "VOGE 3000S.png");
                await AddRefAsync("BMW F 900 XR", "Moto marca BMW, referencia BMW F 900 XR", 2023, "BMW", "BMW F 900 XR.png");
                await AddRefAsync("BMW K 1600 GT", "Moto marca BMW, referencia BMW K 1600 GT", 2023, "BMW", "BMW K 1600 GT.png");
                await AddRefAsync("BMW K 1600 GTL", "Moto marca BMW, referencia BMW K 1600 GTL", 2023, "BMW", "BMW K 1600 GTL.png");
                await AddRefAsync("BMW M 1000 R", "Moto marca BMW, referencia BMW M 1000 R", 2023, "BMW", "BMW M 1000 R.png");
                await AddRefAsync("BMW M 1000 RR - 2023", "Moto marca BMW, referencia BMW M 1000 RR - 2023", 2023, "BMW", "BMW M 1000 RR - 2023.png");
                await AddRefAsync("BMW M 1000 RR", "Moto marca BMW, referencia BMW M 1000 RR", 2023, "BMW", "BMW M 1000 RR.png");
                await AddRefAsync("BMW R 1250 RS", "Moto marca BMW, referencia BMW R 1250 RS", 2023, "BMW", "BMW R 1250 RS.png");
                await AddRefAsync("BMW R 1250 RT", "Moto marca BMW, referencia BMW R 1250 RT", 2023, "BMW", "BMW R 1250 RT.png");
                await AddRefAsync("BMW S 1000 RR", "Moto marca BMW, referencia BMW S 1000 RR", 2023, "BMW", "BMW S 1000 RR.png");
                await AddRefAsync("BMW S 1000 XR", "Moto marca BMW, referencia BMW S 1000 XR", 2023, "BMW", "BMW S 1000 XR.png");
                await AddRefAsync("Desert-X", "Moto marca DUCATI, referencia Desert-X", 2023, "DUCATI", "Desert-X.png");
                await AddRefAsync("DesertX-Rally", "Moto marca DUCATI, referencia DesertX-Rally", 2023, "DUCATI", "DesertX-Rally.png");
                await AddRefAsync("Diavel-V4", "Moto marca DUCATI, referencia Diavel-V4", 2023, "DUCATI", "Diavel-V4.png");
                await AddRefAsync("Hypermotard", "Moto marca DUCATI, referencia Hypermotard", 2023, "DUCATI", "Hypermotard.png");
                await AddRefAsync("Monster", "Moto marca DUCATI, referencia Monster", 2023, "DUCATI", "Monster.png");
                await AddRefAsync("Multistrada-V4", "Moto marca DUCATI, referencia Multistrada-V4", 2023, "DUCATI", "Multistrada-V4.png");
                await AddRefAsync("Panigale", "Moto marca DUCATI, referencia Panigale", 2023, "DUCATI", "Panigale.png");
                await AddRefAsync("Scrambler", "Moto marca DUCATI, referencia Scrambler", 2023, "DUCATI", "Scrambler.png");
                await AddRefAsync("SuperLeggera", "Moto marca DUCATI, referencia SuperLeggera", 2023, "DUCATI", "SuperLeggera.png");
                await AddRefAsync("Supersport", "Moto marca DUCATI, referencia Supersport", 2023, "DUCATI", "Supersport.png");
                await AddRefAsync("XDiavel", "Moto marca DUCATI, referencia XDiavel", 2023, "DUCATI", "XDiavel.png");
                await AddRefAsync("breakout-117-f22", "Moto marca HARLEY DAVIDSON, referencia breakout-117-f22", 2023, "HARLEY DAVIDSON", "breakout-117-f22.png");
                await AddRefAsync("electra-glide-highway-king-f51", "Moto marca HARLEY DAVIDSON, referencia electra-glide-highway-king-f51", 2023, "HARLEY DAVIDSON", "electra-glide-highway-king-f51.png");
                await AddRefAsync("fat-bob-114-f85", "Moto marca HARLEY DAVIDSON, referencia fat-bob-114-f85", 2023, "HARLEY DAVIDSON", "fat-bob-114-f85.png");
                await AddRefAsync("fat-boy-114-f89", "Moto marca HARLEY DAVIDSON, referencia fat-boy-114-f89", 2023, "HARLEY DAVIDSON", "fat-boy-114-f89.png");
                await AddRefAsync("heritage-classic-114-f91b", "Moto marca HARLEY DAVIDSON, referencia heritage-classic-114-f91b", 2023, "HARLEY DAVIDSON", "heritage-classic-114-f91b.png");
                await AddRefAsync("low-rider-s-f57", "Moto marca HARLEY DAVIDSON, referencia low-rider-s-f57", 2023, "HARLEY DAVIDSON", "low-rider-s-f57.png");
                await AddRefAsync("nightster-special-016", "Moto marca HARLEY DAVIDSON, referencia nightster-special-016", 2023, "HARLEY DAVIDSON", "nightster-special-016.png");
                await AddRefAsync("pan-america-1250-special-f99", "Moto marca HARLEY DAVIDSON, referencia pan-america-1250-special-f99", 2023, "HARLEY DAVIDSON", "pan-america-1250-special-f99.png");
                await AddRefAsync("sport-glide-f87", "Moto marca HARLEY DAVIDSON, referencia sport-glide-f87", 2023, "HARLEY DAVIDSON", "sport-glide-f87.png");
                await AddRefAsync("sportster-s-f89", "Moto marca HARLEY DAVIDSON, referencia sportster-s-f89", 2023, "HARLEY DAVIDSON", "sportster-s-f89.png");
                await AddRefAsync("street-bob-114-f53", "Moto marca HARLEY DAVIDSON, referencia street-bob-114-f53", 2023, "HARLEY DAVIDSON", "street-bob-114-f53.png");
                await AddRefAsync("ultra-limited-010", "Moto marca HARLEY DAVIDSON, referencia ultra-limited-010", 2023, "HARLEY DAVIDSON", "ultra-limited-010.png");
                await AddRefAsync("CB-1000R", "Moto marca HONDA, referencia CB-1000R", 2023, "HONDA", "CB-1000R.png");
                await AddRefAsync("CB125F", "Moto marca HONDA, referencia CB125F", 2023, "HONDA", "CB125F.png");
                await AddRefAsync("CB125FDLX", "Moto marca HONDA, referencia CB125FDLX", 2023, "HONDA", "CB125FDLX.png");
                await AddRefAsync("CB190R", "Moto marca HONDA, referencia CB190R", 2023, "HONDA", "CB190R.png");
                await AddRefAsync("CB-300F", "Moto marca HONDA, referencia CB-300F", 2023, "HONDA", "CB-300F.png");
                await AddRefAsync("CB-650R", "Moto marca HONDA, referencia CB-650R", 2023, "HONDA", "CB-650R.png");
                await AddRefAsync("CBR650R", "Moto marca HONDA, referencia CBR650R", 2023, "HONDA", "CBR650R.png");
                await AddRefAsync("XBlade", "Moto marca HONDA, referencia XBlade", 2023, "HONDA", "XBlade.png");
                await AddRefAsync("XR-150L", "Moto marca HONDA, referencia XR-150L", 2023, "HONDA", "XR-150L.png");
                await AddRefAsync("XR190L", "Moto marca HONDA, referencia XR190L", 2023, "HONDA", "XR190L.png");
                await AddRefAsync("XRE190", "Moto marca HONDA, referencia XRE190", 2023, "HONDA", "XRE190.png");
                await AddRefAsync("KLX 110R L", "Moto marca KAWASAKI, referencia KLX 110R L", 2023, "KAWASAKI", "KLX 110R L.png");
                await AddRefAsync("NINJA 400 KRT", "Moto marca KAWASAKI, referencia NINJA 400 KRT", 2023, "KAWASAKI", "NINJA 400 KRT.png");
                await AddRefAsync("NINJA ZR-10X", "Moto marca KAWASAKI, referencia NINJA ZR-10X", 2023, "KAWASAKI", "NINJA ZR-10X.png");
                await AddRefAsync("VERSYS 1000 S", "Moto marca KAWASAKI, referencia VERSYS 1000 S", 2023, "KAWASAKI", "VERSYS 1000 S.png");
                await AddRefAsync("VERSYS 300ABS", "Moto marca KAWASAKI, referencia VERSYS 300ABS", 2023, "KAWASAKI", "VERSYS 300ABS.png");
                await AddRefAsync("VERSYS 650", "Moto marca KAWASAKI, referencia VERSYS 650", 2023, "KAWASAKI", "VERSYS 650.png");
                await AddRefAsync("Z400", "Moto marca KAWASAKI, referencia Z400", 2023, "KAWASAKI", "Z400.png");
                await AddRefAsync("ADDRESS NZ", "Moto marca SUZUKI, referencia ADDRESS NZ", 2023, "SUZUKI", "ADDRESS NZ.png");
                await AddRefAsync("AVENIS", "Moto marca SUZUKI, referencia AVENIS", 2023, "SUZUKI", "AVENIS.png");
                await AddRefAsync("BURGMAN", "Moto marca SUZUKI, referencia BURGMAN", 2023, "SUZUKI", "BURGMAN.png");
                await AddRefAsync("GIXXER FI 150 ABS", "Moto marca SUZUKI, referencia GIXXER FI 150 ABS", 2023, "SUZUKI", "GIXXER FI 150 ABS.png");
                await AddRefAsync("Gixxer-250", "Moto marca SUZUKI, referencia Gixxer-250", 2023, "SUZUKI", "Gixxer-250.png");
                await AddRefAsync("GSX 125R", "Moto marca SUZUKI, referencia GSX 125R", 2023, "SUZUKI", "GSX 125R.png");
                await AddRefAsync("GSX S1000 GT", "Moto marca SUZUKI, referencia GSX S1000 GT", 2023, "SUZUKI", "GSX S1000 GT.png");
                await AddRefAsync("GSX S1000", "Moto marca SUZUKI, referencia GSX S1000", 2023, "SUZUKI", "GSX S1000.png");
                await AddRefAsync("INTRUDER", "Moto marca SUZUKI, referencia INTRUDER", 2023, "SUZUKI", "INTRUDER.png");
                await AddRefAsync("SV650", "Moto marca SUZUKI, referencia SV650", 2023, "SUZUKI", "SV650.png");
                await AddRefAsync("V-STROM-250", "Moto marca SUZUKI, referencia V-STROM-250", 2023, "SUZUKI", "V-STROM-250.png");
                await AddRefAsync("AEROX155", "Moto marca YAMAHA, referencia AEROX155", 2023, "YAMAHA", "AEROX155.png");
                await AddRefAsync("CRYPTON", "Moto marca YAMAHA, referencia CRYPTON", 2023, "YAMAHA", "CRYPTON.jpg");
                await AddRefAsync("FZ", "Moto marca YAMAHA, referencia FZ", 2023, "YAMAHA", "FZ.png");
                await AddRefAsync("MT03", "Moto marca YAMAHA, referencia MT03", 2023, "YAMAHA", "MT03.jpg");
                await AddRefAsync("MT07", "Moto marca YAMAHA, referencia MT07", 2023, "YAMAHA", "MT07.jpg");
                await AddRefAsync("MT09", "Moto marca YAMAHA, referencia MT09", 2023, "YAMAHA", "MT09.png");
                await AddRefAsync("MT09SP", "Moto marca YAMAHA, referencia MT09SP", 2023, "YAMAHA", "MT09SP.png");
                await AddRefAsync("MT15", "Moto marca YAMAHA, referencia MT15", 2023, "YAMAHA", "MT15.png");
                await AddRefAsync("NMAX", "Moto marca YAMAHA, referencia NMAX", 2023, "YAMAHA", "NMAX.png");
                await AddRefAsync("SZRR", "Moto marca YAMAHA, referencia SZRR", 2023, "YAMAHA", "SZRR.jpg");
                await AddRefAsync("TMAXTechmax", "Moto marca YAMAHA, referencia TMAXTechmax", 2023, "YAMAHA", "TMAXTechmax.png");
                await AddRefAsync("XMAX", "Moto marca YAMAHA, referencia XMAX", 2023, "YAMAHA", "XMAX.jpg");
                await AddRefAsync("XTZ125", "Moto marca YAMAHA, referencia XTZ125", 2023, "YAMAHA", "XTZ125.jpg");
                await AddRefAsync("XTZ150", "Moto marca YAMAHA, referencia XTZ150", 2023, "YAMAHA", "XTZ150.png");
                await AddRefAsync("XTZ250", "Moto marca YAMAHA, referencia XTZ250", 2023, "YAMAHA", "XTZ250.jpg");
                await AddRefAsync("YBRZ125", "Moto marca YAMAHA, referencia YBRZ125", 2023, "YAMAHA", "YBRZ125.jpg");
                await AddRefAsync("YCZ110", "Moto marca YAMAHA, referencia YCZ110", 2023, "YAMAHA", "YCZ110.png");
                await _context.SaveChangesAsync();
            }
        }

        private async Task AddRefAsync(string name, string description, int model, string brand, string image)
        {
            Ref refe = new()
            {
                Name = name,
                Description = description,
                Model = model,
                RefImages = new List<RefImage>(),
            };

            var brandName = await _context.Brands.FirstOrDefaultAsync(r => r.Name == brand);
            if (brandName != null)
            {
                refe.BrandId = brandName.Id;
            }

            var filePath = $"{Environment.CurrentDirectory}\\Images\\Refs\\{image}";
            var fileBytes = File.ReadAllBytes(filePath);
            var imagePath = await _fileStorage.SaveFileAsync(fileBytes, "jpg", "motorcycles");
            refe.RefImages!.Add(new RefImage { Image = imagePath });

            _context.Refs.Add(refe);
        }

        private async Task<User> CheckUserAsync(string document, string firstName, string lastName, string email, string phone, string address, string image, UserType userType)
        {
            var user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                var city = await _context.Cities.FirstOrDefaultAsync(x => x.Name == "Medellín");
                if (city == null)
                {
                    city = await _context.Cities.FirstOrDefaultAsync();
                }

                var filePath = $"{Environment.CurrentDirectory}\\Images\\users\\{image}";
                var fileBytes = File.ReadAllBytes(filePath);
                var imagePath = await _fileStorage.SaveFileAsync(fileBytes, "jpg", "users");

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
                    Photo = imagePath,
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
                _context.Brands.Add(new Brand { Name = "Suzuki" });
                _context.Brands.Add(new Brand { Name = "Yamaha" });
                _context.Brands.Add(new Brand { Name = "Honda" });
                _context.Brands.Add(new Brand { Name = "Kawasaki" });
                _context.Brands.Add(new Brand { Name = "BMW" });
                _context.Brands.Add(new Brand { Name = "Ducati" });
                _context.Brands.Add(new Brand { Name = "AKT" });
                _context.Brands.Add(new Brand { Name = "Harley Davidson" });

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

        private async Task CheckCountriesAsync2()
        {
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(new Country
                {
                    Name = "Colombia",
                    States = new List<State>
            {
                new State
                {
                    Name = "Antioquia",
                    Cities = new List<City>
                    {
                        new City
                        {
                            Name = "Medellín"
                        }
                    }
                }
            }
                });
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