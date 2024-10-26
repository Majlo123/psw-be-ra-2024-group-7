using Explorer.BuildingBlocks.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Shopping.Tests
{
    public class BaseShoppingIntegrationTest : BaseWebIntegrationTest<ShoppingTestFactory>
    {
        public BaseShoppingIntegrationTest(ShoppingTestFactory factory) : base(factory)
        {
        }
    }
}
