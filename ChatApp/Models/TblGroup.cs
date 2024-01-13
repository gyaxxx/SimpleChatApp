using System;
using System.Collections.Generic;

namespace ChatApp.Models;

public partial class TblGroup
{
    public int GroupId { get; set; }

    public string GroupName { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }

    public int CreatedByUserId { get; set; }

    public string? GroupDescription { get; set; }

    public bool? IsActive { get; set; }

    public virtual TblUser CreatedByUser { get; set; } = null!;
}
