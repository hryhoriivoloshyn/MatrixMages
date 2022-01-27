using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MatrixMages.Models;

namespace MatrixMages.ViewModels
{
    public class MatrixViewModel
    {
        public IEnumerable<Victory> Victories { get; set; }
        public IEnumerable<BattleMageStrategy> BattleMageStrategies { get; set; }

        public IEnumerable<SpaceMageStrategy> SpaceMageStrategies { get; set; }

        public IEnumerable<BattleMageStrategy> MaxMinResult { get; set; }
    }
}
