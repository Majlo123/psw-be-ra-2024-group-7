using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class Reward
    {
        public RewardType Type { get; set; }
        public int Amount { get; set; }
    }

    public enum RewardType
    {
        XP,
        Coupon,
        AC
    }
}
