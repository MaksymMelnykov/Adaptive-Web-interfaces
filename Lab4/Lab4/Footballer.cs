using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    public class Footballer
    {
        public string Name {  get; set; }
        private int Age { get; set; }
        protected string Position { get; set; }
        internal string Club { get; set; }
        protected internal int Goals { get; set; }

        public Footballer(string name, int age, string position, string club, int goals)
        {
            Name = name;
            Age = age;
            Position = position;
            Club = club;
            Goals = goals;
        }

        public void ScoreGoal()
        {
            Goals++;
            Console.WriteLine($"{Name} has scored a goal his {Goals} in season!");
        }

        private void Celebrate()
        {
            Console.WriteLine($"{Name} is celebrating his goal!");
        }

        protected internal void Transfer(string newClub)
        {
            Club = newClub;
            Console.WriteLine($"{Name} has transferred to {newClub}!");
        }

        public bool Retirement()
        {
            if (Age >= 35)
            {
                Console.WriteLine($"{Name} is ready for retirement.");
                return true;
            }
            else
            {
                Console.WriteLine($"{Name} is not ready for retirement. He is playing");
                return false;
            }
        }
    }
}
