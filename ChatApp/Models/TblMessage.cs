using System;
using System.Collections.Generic;

namespace ChatApp.Models;

public partial class TblMessage
{
    public int MessageId { get; set; }

    public int SenderUserId { get; set; }

    public int ChatId { get; set; }

    public string MessageText { get; set; } = null!;

    public DateTime Timestampt { get; set; }

    public virtual TblPrivateChat Chat { get; set; } = null!;

    public virtual TblUser SenderUser { get; set; } = null!;

    public virtual ICollection<TblPrivateChat> TblPrivateChats { get; set; } = new List<TblPrivateChat>();
}
