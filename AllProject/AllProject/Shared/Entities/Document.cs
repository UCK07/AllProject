using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllProject.Shared.Entities
{
    public abstract class Document : IDocument
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime? Created { get; set; }
    }
}
