using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OFoodBackendTask.Controller.Helpers;
using OFoodBackendTask.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OFoodBackendTask.Controller
{
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly OfoodContext _context;

        #region [- ctor -]

        public FeedbackController(OfoodContext context)
        {
            _context = context;
        }
        #endregion

        #region [- GetReviewsByStoreId(int storeId) -]

        [HttpGet("{id}")]
        [Route("api/getReviews/{storeId}")]
        public async Task<ActionResult> GetReviewsByStoreId(int storeId)
        {
            if (_context.Stores == null)
            {
                return NotFound();
            }
            var orderList = await _context.Orders.Where(p => p.StoreRef == storeId)
                                                 .Include(o=>o.OrderFeedbacks)
                                                 .Include(o=>o.DeliveryFeedbacks)
                                                 .ToListAsync();
            if (orderList == null)
            {
                return NotFound();
            }
            
            var orderFeedbacks = orderList.SelectMany(o => o.OrderFeedbacks).ToList();
            var deliveryFeedbacks = orderList.SelectMany(o => o.DeliveryFeedbacks).ToList();

            var allRatings = orderFeedbacks.Select(f => f.Rating).Concat(deliveryFeedbacks.Select(f => f.Rating)).Where(r => r.HasValue).Select(r => r.Value).ToList();
            var commentsCount = orderFeedbacks.Count(f => !string.IsNullOrEmpty(f.FeedbackComment)) + deliveryFeedbacks.Count(f => !string.IsNullOrEmpty(f.FeedbackComment));
            var ratesCount = allRatings.Count;

            var aggregatedRating = new AggregatedRating
            {
                Rating = allRatings.Average(),
                CommentsCount = commentsCount,
                RatesCount = ratesCount,
                RatingOneCount = allRatings.Count(r => r == 1),
                RatingTwoCount = allRatings.Count(r => r == 2),
                RatingThreeCount = allRatings.Count(r => r == 3),
                RatingFourCount = allRatings.Count(r => r == 4),
                RatingFiveCount = allRatings.Count(r => r == 5)
            };
            return Ok("aggregatedRating");
        }
        #endregion

        #region [- PostFeedback([FromBody] ReviewHelper review) -]

        [HttpPost]
        [Route("api/postFeedback")]
        public async Task<ActionResult<Order>> PostFeedback([FromBody] ReviewHelper review)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'Orders'  is null.");
            }
            DeliveryFeedback newDeliveryFeedback = new DeliveryFeedback() { OrderRef = review.Order_Id, Rating = review.Delivery_Feedback?.Rating, FeedbackComment = review.Delivery_Feedback?.Comment };
            OrderFeedback newOrderFeedback = new OrderFeedback() { OrderRef = review.Order_Id, Rating = review.Order_Feedback?.Rating, FeedbackComment = review.Order_Feedback?.Comment };

            _context.DeliveryFeedbacks.Add(newDeliveryFeedback);
            _context.OrderFeedbacks.Add(newOrderFeedback);

            await _context.SaveChangesAsync();

            return Ok("Successfully Done");
        }
        #endregion
    }
}

