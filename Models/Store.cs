using System;
using System.Collections.Generic;

namespace OFoodBackendTask.Models;

public partial class Store
{
    public int StoreId { get; set; }

    public string? StoreName { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
