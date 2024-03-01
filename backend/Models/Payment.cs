using System;
using System.Collections.Generic;

namespace RepositryAssignement.Models;

public partial class Payment
{
    public int Id { get; set; }

    public int PolicyId { get; set; }

    public DateOnly DateOfPayment { get; set; }

    public long AmountPaid { get; set; }

    public long FineAmount { get; set; }

    public virtual PolicyEnrollment Policy { get; set; } = null!;
}
