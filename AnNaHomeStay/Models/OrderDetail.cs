﻿using System;
using System.Collections.Generic;

namespace AnNaHomeStay.Models;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public int OrderId { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal PriceWhenSell { get; set; }

    public bool IsPayment { get; set; }

    public virtual Order Order { get; set; } = null!;
}
