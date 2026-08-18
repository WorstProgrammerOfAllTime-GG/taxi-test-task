using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverySystem.Services
{
    public interface IRandomNumberService
    {
        public Task<int> GetRandomNumber();
    }
}
