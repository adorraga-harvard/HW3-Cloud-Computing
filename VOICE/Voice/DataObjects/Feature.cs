using Microsoft.WindowsAzure.Mobile.Service;

namespace Voice.DataObjects
{
    public class Feature : EntityData
    {
        //public string Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public int Votes { get; set; }
        public string VotesModDate { get; set; }
    }
 
}