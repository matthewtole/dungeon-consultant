using System;
using System.Collections.Generic;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace Code.Scripts.Characters
{
    public class NameGenerator
    {
        private readonly string[] _tmpNames =
        {
            "Blumph", "Cloozjub", "Didiotfoog", "Bidiot", "Thug", "Dug", "Gunb", "Golt", "Clungloaf", "Foof",
            "Woofloron", "Bloronwimp", "Munhoof", "Pubjump", "Noogskull", "Tholtoog", "Glormknock", "Thuck", "Goog",
            "Muckmuck", "Hooz", "Punkghoo", "Thunph", "Brolphtwit", "Glob", "Junbglumb", "Moobuzz", "Buck", "Blunt",
            "Nidiot", "Funphwong", "Klook", "Clolphlunk", "Kunblub", "Blormskull", "Nuckbrung", "Kuck", "Joog",
            "Gogkoo", "Thootwerp", "Pookfunk", "Bundoof", "Pooknit", "Blorg", "Buckolph", "Brork", "Fruckhead", "Blorm",
            "Glunk", "Punph", "Pooum", "Grult", "Bumpsnark", "Munbidiot", "Gorgog", "Humhead", "Hoboof", "Duntair",
            "Grorm", "Bunkmunch", "Goog", "Widiot", "Blunbwooz", "Jungbone", "Gidiotunk", "Mundulf", "Didiotflum",
            "Tholt", "Komph", "Golphdork", "Folt", "Jugdumph", "Woron", "Moozook", "Clulfmorm", "Pum", "Dorkunph",
            "Huntorm", "Hidiot", "Clumph", "Poo", "Blongjoog", "Glorgcheese", "Thum", "Fuck", "Gluzzgoof", "Wungdumb",
            "Boomong", "Gorm", "Grum", "Womphknuckle", "Moo", "Blolphklork", "Kook", "Numbgrumble", "Mok", "Goodorm",
            "Nun", "Thubunk", "Pubkork", "Flomphoof", "Flunggult", "Goo", "Doo", "Blumhung", "Noogoron", "Punphgook",
            "Gunbgonk", "Mormknocker", "Klubdoof", "Frung", "Glunjuck", "Mooz", "Hunphthimble", "Broltflelch", "Nook",
            "Pongclolph", "Gook", "Dolph"
        };
        
        public NameGenerator()
        {
        }

        public string GenerateName()
        {
            return _tmpNames[Random.Range(0, _tmpNames.Length)];
        }
    }
}
