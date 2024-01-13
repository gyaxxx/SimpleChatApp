using System;
using System.Collections.Generic;

namespace ChatApp.Models;

public partial class TblUser
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string Email { get; set; } = null!;

    public string HashedPassword { get; set; } = null!;

    public string? ProfilePictureUrl { get; set; }

    public DateTime? LastLogin { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<TblGroup> TblGroups { get; set; } = new List<TblGroup>();

    public virtual ICollection<TblMessage> TblMessages { get; set; } = new List<TblMessage>();

    public virtual ICollection<TblPrivateChat> TblPrivateChatUser1s { get; set; } = new List<TblPrivateChat>();

    public virtual ICollection<TblPrivateChat> TblPrivateChatUser2s { get; set; } = new List<TblPrivateChat>();
}
