using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bourse.Models
{
    public class BourseModel : SqlExpressUtilities.SqlExpressWrapper
    {
        public String Symbol { get; set; }

        public String Name { get; set; }


        public BourseModel(Object connexionString)
            : base(connexionString)
        {
            SQLTableName = "BOURSE";
        }

        public BourseModel()
            : base("")
        {
        }
        public override void GetValues()
        {
            Symbol = this["SYMBOL"];
            Name = this["NAME"];
        }

        public override void Insert()
        {
            InsertRecord(Symbol,Name);
        }

        public override void Update()
        {
            UpdateRecord(Symbol, Name);
        }
    }
}