using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballRanking.ExtensionMethods
{
    public static class Methods
    {
        public static Boolean crawlPart(List<String>parts,String part)
        {
            foreach (var item in parts)
            {
                if(item.Equals(part))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
