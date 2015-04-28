using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdataBCSSample
{
    public class BRStatusView
    {

        private System.Nullable<decimal> _SmallAC;
        private string _MineArea;
        private string _Notes;
        private System.Nullable<System.DateTime> _DateApproved;
        private System.Nullable<System.DateTime> _DateInitiated;
        private string _BRStatus;
        private System.Nullable<byte> _Phase;
        private int _ID;
        private string _BlockID;

        public int ID { get { return this._ID; } set { this._ID = value; } }
        public string BlockID { get { return this._BlockID; } set { this._BlockID = value; } }

        public System.Nullable<byte> Phase { get { return this._Phase; } set { this._Phase = value; } }
        public string BRStatus { get { return this._BRStatus; } set { this._BRStatus = value; } }
        public System.Nullable<System.DateTime> DateInitiated { get { return this._DateInitiated; } set { this._DateInitiated = value; } }
        public System.Nullable<System.DateTime> DateApproved { get { return this._DateApproved; } set { this._DateApproved = value; } }
        public string Notes { get { return this._Notes; } set { this._Notes = value; } }
        public System.Nullable<decimal> SmallAC { get { return this._SmallAC; } set { this._SmallAC = value; } }
        public string MineArea { get { return this._MineArea; } set { this._MineArea = value; } }

       
        public BRStatusView()
        {
        }
    }
}
