using System;
using System.Collections.Generic;

namespace ChatApp.Models;

public partial class TblUserGroup
{
    public int UserId { get; set; }

    public int GroupId { get; set; }

    public DateTime? JoinedDate { get; set; }

    public string? UserRole { get; set; }

    public virtual TblUser User { get; set; } = null!;
}
