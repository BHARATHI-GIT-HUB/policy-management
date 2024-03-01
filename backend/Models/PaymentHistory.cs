using System;
using System.Collections.Generic;

namespace RepositryAssignement.Models;

public partial class PaymentHistory
{
    public int Id { get; set; }

    public int PolicyHistoryId { get; set; }

    public long DateOfPayment { get; set; }

    public long AmountPaid { get; set; }

    public long FineAmount { get; set; }
}
