using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FishInABox.Models
{
    /*
     * https://ole.michelsen.dk/blog/grouping-data-with-linq-and-mvc.html
     * create a generic GroupModel class to hold the data and a key of your own choosing to group by
     */
    public class GroupModel<T, K>
    {
        public K Key;
        public IEnumerable<T> Values;
    }
}