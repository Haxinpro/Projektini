using System.Collections.Generic;

namespace Game
{
    public class ReputationManager
    {
        private Dictionary<string, int> reputations;

        public ReputationManager()
        {
            reputations = new Dictionary<string, int>();
        }
        
        public void SetReputation(string npcName, int reputation)
        {
            reputations[npcName] = reputation;
        }

        public int GetReputation(string npcName)
        {
            return reputations.ContainsKey(npcName) ? reputations[npcName] : 0;
        }

        public void IncreaseReputation(string npcName, int amount)
        {
            if (reputations.ContainsKey(npcName))
            {
                reputations[npcName] += amount;
            }
            else
            {
                reputations[npcName] = amount;
            }
        }

        public void DecreaseReputation(string npcName, int amount)
        {
            if (reputations.ContainsKey(npcName))
            {
                reputations[npcName] -= amount;
            }
            else
            {
                reputations[npcName] = -amount;
            }
        }

        // Uusi metodi, joka tarkistaa maineen tason
        public string CheckReputationLevel(Npc npc)
        {
            int reputation = GetReputation(npc.Name);
            Npc.Pronouns pronouns = npc.GetPronouns();

            if (reputation > 150)
            {
                return $"{npc.Name} greatly trusts you. You receive special discounts. (Your reputation {reputation}))";
            }
            else if (reputation > 50)
            {
                return $"{npc.Name} thinks highly of you. More wares are available for you. (Your reputation {reputation})";
            }
            else if (reputation > 0)
            {
                return $"{npc.Name} knows you, but you haven't made a strong impression yet. (Your reputation {reputation})";
            }
            else if (reputation == 0)
            {
                return $"{npc.Name} doesn't know you well yet. (Your reputation {reputation})";
            }
            else if (reputation < 0 && reputation > -50)
            {
                return $"{npc.Name} does not smile to you. Maybe you have done something wrong to {pronouns.Objective}. (Your reputation {reputation})";
            }
            else
            {
                return $"{npc.Name} dislikes you. You might face higher prices or fewer wares. (Your reputation {reputation})";
            }
        }
    }
}
