using System;
using System.Collections.Generic;

namespace RepositryAssignement.Models;

public partial class ClientKyc
{
    public int ClientId { get; set; }

    public DateOnly ProofDob { get; set; }

    public string ProofIdentity { get; set; } = null!;

    public string BankAccount { get; set; } = null!;

    public virtual Client Client { get; set; } = null!;
}
