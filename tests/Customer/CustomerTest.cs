using System;

namespace Notificator.Tests.Customer
{
    public class CustomerTest
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public CustomerTest(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}