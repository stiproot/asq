using System.Collections.Generic;
using System.Text.Json;

namespace DTO.Tracking
{
    public class BaseTracking
    {
        public long Id{ get; set; }
        public System.Guid UniqueId{ get; set; }
        public System.DateTime CreationDateUtc{ get; set; }
        public long CreationUserId{ get; set; }
        public bool Inactive{ get; set; }

        public string Request{ get; set; }
        public string Tracking{ get; set; }
        public bool Failed{ get; set; }

        public IEnumerable<TrackingComponent> TrackingComponents
        { 
            get => JsonSerializer.Deserialize<List<TrackingComponent>>(this.Tracking);
            set => this.Tracking = JsonSerializer.Serialize(value);
        }
    }
}