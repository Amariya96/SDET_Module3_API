using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy_API
{
    public class Booking
    {
            [JsonProperty("token")]
            public string? Token { get; set; }

            [JsonProperty("bookingid")]
            public string? BookingId { get; set; }

          
        }

    }
