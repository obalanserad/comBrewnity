using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    abstract public class BaseType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
    }

    public class Hop:BaseType
    {

    }

    public class Malt:BaseType
    {

    }

    public class Yeast:BaseType
    {

    }

    public class Other:BaseType
    {

    }
}
