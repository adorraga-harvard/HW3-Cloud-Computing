using Microsoft.WindowsAzure.Mobile.Service;

namespace Voice.DataObjects
{
    public class UserVote : EntityData
    {
        public string userId { get; set; }
        public string featureId { get; set; }
        public string votedatetime { get; set; }
    }
}