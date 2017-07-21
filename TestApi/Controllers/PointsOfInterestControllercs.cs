﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApi.Controllers
{
    [Route("api/cities")]
    public class PointsOfInterestControllercs : Controller
    {
        [HttpGet("{cityId}/pointofinterest")]
        public IActionResult GetPointOfInterests(int cityId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if(city == null)
            {
                NotFound();
            }

            return Ok(city.PointOfInterests);
        }

        [HttpGet("{cityId}/pointofinterest/{id}")]
        public IActionResult GetPointOfInterests(int cityId, int id)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                NotFound();
            }

            var pointOfInterest = city.PointOfInterests.First(p => p.Id == id);

            if(pointOfInterest == null)
            {
                NotFound();
            }

            return Ok(pointOfInterest);
        }
    }
}
