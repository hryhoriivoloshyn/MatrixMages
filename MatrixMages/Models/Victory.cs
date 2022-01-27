using System;
using System.Collections.Generic;

#nullable disable

namespace MatrixMages.Models
{
    public partial class Victory
    {
        public long SpaceMageId { get; set; }
        public long BattleMageId { get; set; }
        public int Victory1 { get; set; }

        public virtual BattleMageStrategy BattleMage { get; set; }
        public virtual SpaceMageStrategy SpaceMage { get; set; }
    }
}
