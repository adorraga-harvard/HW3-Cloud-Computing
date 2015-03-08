
using Microsoft.WindowsAzure.Mobile.Service;

namespace Voice.DataObjects
{
    public class User : EntityData
    {
        //public string Id { get; set; }
        public string IDPId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string PictureURL { get; set; }
        public int VoteBalance { get; set; }
        public string RegistrationStatus { get; set; }
        public string Role { get; set; }

    }
}