using CELSavings.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CELSavings.Controllers.Api
{
    public class PaymentsController : ApiController
    {
        // POST /api/payments
        [HttpPost]
        public IHttpActionResult CreateRentals(PaymentDto newPaymentDto)
        {

            //var customer = _context.Customers.First(x => x.Id == newRentalsDto.CustomerId);

            //var movies = _context.Movies.Where(x => newRentalsDto.MovieIds.Contains(x.Id));

            //foreach (var movie in movies)
            //{
            //    if (movie.NumberAvailable == 0)
            //        return BadRequest("Movie is not Available");

            //    movie.NumberAvailable--;
            //    _context.Rentals.Add(
            //        new Rental
            //        {
            //            CustomerId = customer.Id,
            //            MovieId = movie.Id,
            //            DateRented = DateTime.Today
            //        });
            //}
            //_context.SaveChanges();


            return Ok();

        }
    }
}
