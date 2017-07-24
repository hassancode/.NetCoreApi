using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApi.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace TestApi.Controllers
{
    [Route("api/cities")]
    public class PointsOfInterestController : Controller
    {
        [HttpGet("{cityId}/pointsofinterest")]
        public IActionResult GetPointOfInterests(int cityId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if(city == null)
            {
                return NotFound();
            }

            return Ok(city.PointOfInterests);
        }

        [HttpGet("{cityId}/pointsofinterest/{id}", Name = "GetPointsOfInterest")]
        public IActionResult GetPointOfInterests(int cityId, int id)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterest = city.PointOfInterests.FirstOrDefault(p => p.Id == id);

            if(pointOfInterest == null)
            {
                return NotFound();
            }

            return Ok(pointOfInterest);
        }

        [HttpPost("{cityId}/pointsofinterest/")]
        public IActionResult CreatePointOfInterest(int cityId, [FromBody] PointOfInterestForCreationDto pointOfInterest){
            if(pointOfInterest == null){
                return BadRequest();
            }

            if(pointOfInterest.Name == pointOfInterest.Description){
                ModelState.AddModelError("Description", "The provided Description should be different than name");
            }
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

			var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

			if (city == null)
			{
				return NotFound();
			}

            var maxPointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(c => c.PointOfInterests)
                                                      .Max(p => p.Id);
            var finalPointOfInterest = new PointOfInterestDto
            {
                Id = ++maxPointOfInterestId,
                Name = pointOfInterest.Name,
                Descriptino = pointOfInterest.Description
            };

            city.PointOfInterests.Add(finalPointOfInterest);

            return CreatedAtRoute("GetPointsOfInterest",
                                  new { cityId = cityId, id = finalPointOfInterest.Id},
                                  finalPointOfInterest);
		}

        [HttpPut("{cityId}/pointsofinterest/{id}")]
        public IActionResult UpdatePointOfInterest(int cityId, int id, [FromBody] PointOfInterestForUpdateDto pointOfInterest){
			if (pointOfInterest == null)
			{
				return BadRequest();
			}

			if (pointOfInterest.Name == pointOfInterest.Description)
			{
				ModelState.AddModelError("Description", "The provided Description should be different than name");
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

            //Code Redundancy can be improved  by FluidValidation third party library
			var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

			if (city == null)
			{
				return NotFound();
			}

			var pointOfInterestFromStore = city.PointOfInterests.FirstOrDefault(p => p.Id == id);

			if (pointOfInterestFromStore == null)
			{
				return NotFound();
			}

            pointOfInterestFromStore.Name = pointOfInterest.Name;
            pointOfInterestFromStore.Descriptino = pointOfInterest.Description;

            return NoContent();
		}

        [HttpPatch("{cityId}/pointsofinterest/{id}")]
        public IActionResult PartiallyUpdatePointOfInterest(int cityId, int id,
                                                            [FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> patchDoc){
            if(patchDoc == null){
                return BadRequest();
            }

			var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

			if (city == null)
			{
				return NotFound();
			}

			var pointOfInterestFromStore = city.PointOfInterests.FirstOrDefault(p => p.Id == id);

			if (pointOfInterestFromStore == null)
			{
				return NotFound();
			}

            var pointOfInterestToPatch = new PointOfInterestForUpdateDto
            {
                Name = pointOfInterestFromStore.Name,
                Description = pointOfInterestFromStore.Descriptino
            };

            patchDoc.ApplyTo(pointOfInterestToPatch, ModelState);

            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

			if (pointOfInterestToPatch.Name == pointOfInterestToPatch.Description)
			{
				ModelState.AddModelError("Description", "The provided Description should be different than name");
			}

            TryValidateModel(pointOfInterestToPatch);

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
			pointOfInterestFromStore.Descriptino = pointOfInterestToPatch.Description;

			return NoContent();
        }

        [HttpDelete("{cityId}/pointsofinterest/{id}")]
        public IActionResult DeletePointOfInterest(int cityId, int id){
            
			var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

			if (city == null)
			{
				return NotFound();
			}

			var pointOfInterestFromStore = city.PointOfInterests.FirstOrDefault(p => p.Id == id);

			if (pointOfInterestFromStore == null)
			{
				return NotFound();
			}

            city.PointOfInterests.Remove(pointOfInterestFromStore);

            return NoContent();
        }
    }
}
