using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingTechniques
{
    public class ValueSamples
    {
        public string FullName = "Andrija Mitrovic";

        public int Age = 28;

        public DateTime DateOfBirth = new(1993, 6, 9);

        public User AppUser = new()
        {
            FullName = "Andrija Mitrovic",
            Age = 21,
            DateOfBirth = new(2000, 6, 9)
        };

        public IEnumerable<User> Users = new[]
        {
        new User()
        {
            FullName = "Andrija Mitrovic",
            Age = 21,
            DateOfBirth = new (2000, 6, 9)
        },
        new User()
        {
            FullName = "Tom Scott",
            Age = 37,
            DateOfBirth = new (1984, 6, 9)
        },
        new User()
        {
            FullName = "Steve Mould",
            Age = 43,
            DateOfBirth = new (1978, 10, 5)
        }
    };

        public IEnumerable<int> Numbers = new[] { 1, 5, 10, 15 };

        public event EventHandler ExampleEvent;

        internal int InternalSecretNumber = 42;

        public virtual void RaiseExampleEvent()
        {
            ExampleEvent(this, EventArgs.Empty);
        }
    }
}
