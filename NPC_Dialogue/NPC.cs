using System;
using System.Collections.Generic;

namespace Game
{
    public class Npc
    {
        // Perustiedot (properties)
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public int Strength { get; set; }
        public int Reputation { get; set; }
        public string Title { get; set; }

        // Konstruktori (Constructor)
        public Npc(string name, int age, string gender, int strength, int reputation, string title)
        {
            Name = name;
            Age = age;
            Gender = gender;
            Strength = strength;
            Reputation = reputation;
            Title = title;
        }

        // Määritetään oikea pronomini sukupuolelle keskusteluissa.
        public struct Pronouns
        {
            public string Subjective { get; }
            public string Objective { get; }
            public string Possessive { get; }

            public Pronouns(string subjective, string objective, string possessive)
            {
                Subjective = subjective;
                Objective = objective;
                Possessive = possessive;
            }
        }

        public Pronouns GetPronouns()
        {
            return Gender.ToLower() switch
            {
                "male" => new Pronouns("he", "him", "his"),
                "female" => new Pronouns("she", "her", "hers"),
                _ => new Pronouns("they", "them", "their"),
            };
        }

        // todennäköisesti tarpeeton. Käytetty ohjeena/pohjana.
        public void Meet()
        {
            Console.WriteLine($"You approach the {Title} {Name}.");
        }
    }
}
