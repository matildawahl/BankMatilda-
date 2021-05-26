using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoFixture;
using BankMatilda.Data;
using Microsoft.EntityFrameworkCore;

namespace UnitTest
{
    public class BaseTest
    {
        protected AutoFixture.Fixture fixture = new AutoFixture.Fixture();
        public BankAppDataContext ctxInMemmory;

        public BaseTest()
        {
            var options = new DbContextOptionsBuilder<BankAppDataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            ctxInMemmory = new BankAppDataContext(options);

            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));


        }
    }
}
