using System;
using System.Collections.Generic;

namespace ChatApp.Models;

public partial class TblPrivateChat
{
    public int ChatId { get; set; }

    public int User1Id { get; set; }

    public int User2Id { get; set; }

    public int? LastMessageId { get; set; }

    public virtual TblMessage? LastMessage { get; set; }

    public virtual ICollection<TblMessage> TblMessages { get; set; } = new List<TblMessage>();

    public virtual TblUser User1 { get; set; } = null!;

    public virtual TblUser User2 { get; set; } = null!;
}
