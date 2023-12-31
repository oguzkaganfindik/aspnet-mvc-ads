using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ads.Persistence.ViewModels
{
    public class ClaimViewModel
    {
        public string Issuer { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Value { get; set; } = null!;
    }
}
