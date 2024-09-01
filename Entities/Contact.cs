using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Contact
    {
        public int Id { get; set; }
        public string Adress { get; set; }
        public string FirstPhone { get; set; }
        public string Email { get; set; }
        public string SecondPhone { get; set; }
        public string HomePhone { get; set; }
        public string OfficePhone { get; set; }
        public int Status { get; set; }
    }
}
