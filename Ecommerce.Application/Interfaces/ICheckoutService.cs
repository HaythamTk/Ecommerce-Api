﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Interfaces
{
    public interface ICheckoutService
    {
        public bool ProcessCheckout(int shoppingCart);
    }
}
