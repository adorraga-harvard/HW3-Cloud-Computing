using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Voice
{
    public class Pattern
    {
        public class featureList
        {
            public string featureId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public int Votes { get; set; }
        }

        public class profileList
        {
            public string name { get; set; }
            public string gender { get; set; }
            public string profilePictureURL { get; set; }
            public string registrationStatus { get; set; }
            public int voteBalance { get; set; }
        }
    }

   
}