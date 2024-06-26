using System;
namespace OFoodBackendTask.Controller.Helpers
{
	public class ReviewHelper
	{
        public int Order_Id { get; set; }
        public int Store_Id { get; set; }
        public FeedbackHelper? Order_Feedback { get; set; }
        public FeedbackHelper? Delivery_Feedback { get; set; }
    }
}

