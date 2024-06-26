using System;
using System.Collections.Generic;

namespace OFoodBackendTask.Models;

public partial class DeliveryFeedback
{
    public int Id { get; set; }

    public int OrderRef { get; set; }

    public int? Rating { get; set; }

    public string? FeedbackComment { get; set; }

    public virtual Order? OrderRefNavigation { get; set; }
}
