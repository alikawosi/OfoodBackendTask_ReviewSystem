using System;
using System.Collections.Generic;

namespace OFoodBackendTask.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? StoreRef { get; set; }

    public DateTime? OrderDate { get; set; }

    public virtual ICollection<DeliveryFeedback> DeliveryFeedbacks { get; set; } = new List<DeliveryFeedback>(); 

    public virtual ICollection<OrderFeedback> OrderFeedbacks { get; set; } =new List<OrderFeedback>();

    public virtual Store? StoreRefNavigation { get; set; }
}
