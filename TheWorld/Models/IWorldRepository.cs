using System.Collections.Generic;

namespace TheWorld.Models
{
  public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();
        IEnumerable<Trip> GetAllTripsWithStops();
        void AddTrip(Trip newTrip);
        void AddStop(string tripName, Stop newStop);
        bool saveAll();
        Trip GetTripByName(string tripName);
        IEnumerable<Trip> GetUserTripsWithStops(string name);
    }
}