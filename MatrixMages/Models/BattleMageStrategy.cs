using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

#nullable disable

namespace MatrixMages.Models
{
    public partial class BattleMageStrategy
    {
        public BattleMageStrategy()
        {
            Victories = new HashSet<Victory>();
        }

        public long Id { get; set; }
        public string StrategyName { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Victory> Victories { get; set; }
    }
}
