using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bourse.Models
{
    public class AchatModel : SqlExpressUtilities.SqlExpressWrapper
    {
        public long ID { get; set; }

        public long UserId { get; set; }

        public string SymbolId { get; set; }

        public int QteAction { get; set; }

        public double PrixAchat { get; set; }


        public AchatModel(Object connexionString)
            : base(connexionString)
        {
            SQLTableName = "ACHAT";
        }

        public AchatModel()
            : base("")
        {
        }
        public override void GetValues()
        {
            ID = long.Parse(this["ID"]);
            UserId = long.Parse(this["USERID"]);
            SymbolId = this["SYMBOLID"];
            QteAction = int.Parse(this["QTE_ACTION"]);
            PrixAchat = double.Parse(this["PRIX_ACHAT"]);
        }

        public override void Insert()
        {
            InsertRecord(UserId, SymbolId, QteAction, PrixAchat);
        }
        public override void Update()
        {
            UpdateRecord(ID, UserId, SymbolId, QteAction, PrixAchat);
        }
    }
}