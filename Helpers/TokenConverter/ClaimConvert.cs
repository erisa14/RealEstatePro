using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;


namespace Helpers.TokenConverter
{
    public class ClaimConvert
    {
        public static Guid ConvertGuid(Claim claimId)
        {
            Guid userId = Guid.Empty;
            if (claimId != null && Guid.TryParse(claimId.Value, out var parsedGuid))
            {
                userId = parsedGuid;
            }
            return userId;
        }
    }
}
