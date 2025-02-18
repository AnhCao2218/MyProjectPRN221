using System;
using System.Collections.Generic;

namespace AnNaHomeStay.Models;

public partial class Image
{
    public int ImageId { get; set; }

    public int HomstayId { get; set; }

    public string Link { get; set; } = null!;

    public virtual Homestay Homstay { get; set; } = null!;
}
