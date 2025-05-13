using System;
using System.Collections.Generic;
using System.Text;

namespace ZipCode.Data.Entity
{
    public class State
    {
        public int StateId { get; set; }
        public string Abbreviation { get; set; }
        public string Name { get; set; }
        public bool IsPrimaryState { get; set; }

        public int EntityId
        {
            get { return StateId; }
            set { StateId = value; }
        }
    }
}
