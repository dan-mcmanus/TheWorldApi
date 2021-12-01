using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public class WorldContextSeedData
    {
        private WorldContext _context;
        private UserManager<WorldUser> _userManager;

        public WorldContextSeedData(WorldContext context, UserManager<WorldUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task  EnsureSeedDataAsync()
        {
            if (await _userManager.FindByEmailAsync("danmcmanus11@outlook.com") == null)
            {
                var newUser = new WorldUser()
                {
                    UserName = "danmcmanus",
                    Email = "danmcmanus11@outlook.com"
                };

                await _userManager.CreateAsync(newUser, "P@ssword123");
            }
            if (!_context.Trips.Any())
            {
                // Add new Data
                var usTrip = new Trip()
                {
                    Name = "US Trip",
                    Created = DateTime.UtcNow,
                    UserName = "danmcmanus",
                    Stops = new List<Stop>()
                    {
                      new Stop() {  Name = "Milwaukee, WI", Arrival = new DateTime(2015, 6, 4), Latitude = 43.063348, Longitude = -87.966695, Order = 0 },
                      new Stop() {  Name = "New York, NY", Arrival = new DateTime(2015, 6, 9), Latitude = 40.712784, Longitude = -74.005941, Order = 1 },
                      new Stop() {  Name = "Boston, MA", Arrival = new DateTime(2015, 7, 1), Latitude = 42.360082, Longitude = -71.058880, Order = 2 },
                      new Stop() {  Name = "Seattle, WA", Arrival = new DateTime(2015, 8, 13), Latitude = 47.606209, Longitude = -122.332071, Order = 4 },
                      new Stop() {  Name = "Chicago, IL", Arrival = new DateTime(2015, 7, 10), Latitude = 41.878114, Longitude = -87.629798, Order = 3 },
                      new Stop() {  Name = "Milwaukee, WI", Arrival = new DateTime(2015, 8, 23), Latitude = 43.063348, Longitude = -87.966695, Order = 5 },
                    }
                };

                _context.Trips.Add(usTrip);
                _context.Stops.AddRange(usTrip.Stops);

                var worldTrip = new Trip()
                {
                    Name = "World Trip",
                    Created = DateTime.UtcNow,
                    UserName = "danmcmanus",
                    Stops = new List<Stop>()
          {
            new Stop() { Order = 0, Latitude =  51.508515, Longitude =  -0.125487, Name = "London, UK", Arrival = DateTime.Parse("Aug 10, 2015") },
            new Stop() { Order = 1, Latitude =  53.349805, Longitude =  -6.260310, Name = "Dublin, Ireland", Arrival = DateTime.Parse("Sep 9, 2015") },
            new Stop() { Order = 2, Latitude =  47.368650, Longitude =  8.539183, Name = "Zurich, Switzerland", Arrival = DateTime.Parse("Sep 16, 2015") },
            new Stop() { Order = 3, Latitude =  48.135125, Longitude =  11.581981, Name = "Munich, Germany", Arrival = DateTime.Parse("Sep 19, 2015") },
          }
                };

                _context.Trips.Add(worldTrip);
                _context.Stops.AddRange(worldTrip.Stops);

                _context.SaveChanges();
            }
        }
    }
}
