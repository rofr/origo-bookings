using System;

namespace Bookit.Domain
{
    [Serializable]
    public class House : Resource
    {
        public bool NeedsCleaning { get; set; }

        public House(int id):base(id)
        {
       
        }
    }
}