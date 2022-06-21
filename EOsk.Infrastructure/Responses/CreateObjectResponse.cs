using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOsk.Infrastructure.Responses
{
    public class CreateObjectResponse
    {
        public Guid Id { get; set; }

        public bool IsSucces { get; set; } = true;

        public List<string> Errors { get; set; }

        public string Message { get; set; }
    }
}
