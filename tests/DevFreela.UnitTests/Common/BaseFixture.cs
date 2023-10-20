using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;

namespace DevFreela.UnitTests.Common;
public class BaseFixture
{
    public Faker Faker { get; set; }

    public BaseFixture()
        => Faker = new Faker("pt_BR");

    public bool GetRandomBoolean()
        => new Random().NextDouble() < 0.5;
}
