using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApi.Models;

namespace TestApi
{
    public class CitiesDataStore
    {
        public static CitiesDataStore Current { get; } = new CitiesDataStore();
        public List<CityDto> Cities { get; set; }

        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto
                {
                    Id  = 1,
                    Name = "New York City",
                    Description = "The one with big park",
                    PointOfInterests = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto
                        {
                            Id = 1,
                            Name = "Central Park",
                            Descriptino = "The most visited place in US"
                        },
                        new PointOfInterestDto
                        {
                            Id = 2,
                            Name = "Empire State Building",
                            Descriptino = "A 102 story skyscrapper"
                        }
                    }
                },
                new CityDto
                {
                    Id  = 2,
                    Name = "New Jersey",
                    Description = "The one where our house is",
                    PointOfInterests = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto
                        {
                            Id = 1,
                            Name = "Sea Side Height",
                            Descriptino = "Ok beach"
                        },
                        new PointOfInterestDto
                        {
                            Id = 2,
                            Name = "Edison",
                            Descriptino = "Indian restaurants"
                        }
                    }
                }
            };
        }
    }
}
