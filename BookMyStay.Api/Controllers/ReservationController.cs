using BookMyStay.Api.Common;
using BookMyStay.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookMyStay.Api.Controllers
{
    public class ReservationController : BaseController
    {
        #region Individual reservation actions

        // GET /reservation
        [HttpGet]
        [ActionName("Index")]
        public IActionResult Get(ReservationViewModel model)
        {
            if (model == null) return NotFound();

            try
            {
                model.GetReservation();

                return Json(model);
            }
            catch (ReservationNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                // log exception...
                return Problem();
            }
        }

        // POST /reservation
        [HttpPost]
        [ActionName("Index")]
        public IActionResult Create(ReservationViewModel? model)
        {
            if (model == null) return BadRequest();

            if (model.Id.HasValue) ModelState.AddModelError("Id", Strings.IdForbidden);
            
            if (!ModelState.IsValid) return ModelValidationErrorResult();

            try
            {
                model.CreateReservation();

                return Json(model);
            }
            catch (Exception)
            {
                // log exception...
                return Problem();
            }
        }

        // PUT /reservation
        [HttpPut]
        [ActionName("Index")]
        public IActionResult Update(ReservationViewModel model)
        {
            if (model == null) return BadRequest();

            if (!model.Id.HasValue) ModelState.AddModelError("Id", Strings.IdRequired);

            if (!ModelState.IsValid) return ModelValidationErrorResult();

            try
            {
                model.UpdateReservation();

                return Json(model);
            }
            catch (ReservationNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                // log exception...
                return Problem();
            }
        }

        // DELETE /reservation
        [HttpDelete]
        [ActionName("Index")]
        public IActionResult Delete(ReservationViewModel model)
        {
            if (model == null) return BadRequest();

            try
            {
                model.DeleteReservation();

                return NoContent();
            }
            catch (ReservationNotFoundException)
            {
                return NotFound();
            }
            catch (IdIsNullException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                // log exception...
                return Problem();
            }
        }

        #endregion

        #region List reservation actions

        // GET /reservation/all
        [HttpGet]
        public IActionResult All()
        {
            try
            {
                return Json(new ReservationViewModel().GetAllReservations());
            }
            catch (Exception)
            {
                // log exception...
                return Problem();
            }
        }

        // GET /reservation/current
        [HttpGet]
        public IActionResult Current()
        {
            try
            {
                return Json(new ReservationViewModel().GetCurrentReservations());
            }
            catch (Exception)
            {
                // log exception...
                return Problem();
            }
        }

        // GET /reservation/past
        [HttpGet]
        public IActionResult Past()
        {
            try
            {
                return Json(new ReservationViewModel().GetPastReservations());
            }
            catch (Exception)
            {
                // log exception...
                return Problem();
            }
        }

        #endregion
    }
}
