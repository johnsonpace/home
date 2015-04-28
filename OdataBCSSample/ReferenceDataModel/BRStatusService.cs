using System;
using System.Collections.Generic;
using System.Linq;
using OdataBCSSample;
using System.Data.Linq;

namespace OdataBCSSample.ReferenceDataModel
{
    public partial class BRStatusService
    {
        public static BRStatusView ReadItem(int iD)
        {
            const string ServerName = @"WIN-3QNLGCNIT6M\SHAREPOINT";

            
            ReferenceDataClasses1DataContext dataContext = new ReferenceDataClasses1DataContext
            ("Data Source=" + ServerName + ";" +
            "Initial Catalog=ReferenceData;Integrated Security=True");
            Recl_BRstatus BRStatus = (from Recl_BRstatus in dataContext.Recl_BRstatus.AsEnumerable().Take(20)
                                        where Recl_BRstatus.ID == iD
                                        select Recl_BRstatus).Single();
            TWCCClasses1DataContext dataContentTWCC = new TWCCClasses1DataContext
            ("Data Source=" + ServerName + ";" +
            "Initial Catalog=TWCC;Integrated Security=True");
            SmallBlock smallBlock = (from SmallBlock in dataContentTWCC.SmallBlocks.AsEnumerable().Take(20)
                                     where SmallBlock.SmallBlock1 == BRStatus.BlockID
                                     select SmallBlock).Single();
            BRStatusView view = new BRStatusView();
            if (BRStatus != null)
            {
                view.ID = BRStatus.ID;
                view.BlockID = BRStatus.BlockID;
               /* view.Phase = BRStatus.Phase;
                view.BRStatus = BRStatus.BRstatus;
                view.DateInitiated = BRStatus.DateInitiated;
                view.DateApproved = BRStatus.DateApproved;
                view.Notes = BRStatus.Notes;*/
            }

            if (smallBlock != null)
            {
               // view.SmallAC = smallBlock.SmallAC;
                view.MineArea = smallBlock.MineArea;
            }

            return view;



        }

        public static IEnumerable<BRStatusView> ReadList()
        {
            const string ServerName = @"WIN-3QNLGCNIT6M\SHAREPOINT";

            ULSLoggingService.LogMessage(string.Format("server name is {0}", ServerName));
            ReferenceDataClasses1DataContext dataContext = new ReferenceDataClasses1DataContext
                  ("Data Source=" + ServerName + ";" +
                   "Initial Catalog=ReferenceData;Integrated Security=True");

            IEnumerable<Recl_BRstatus> cust = from Recl_BRstatus in dataContext.Recl_BRstatus.Take(20)

                                              select Recl_BRstatus;

            ULSLoggingService.LogMessage(string.Format("found one item from brstatuts"));

            List<BRStatusView> lbrstatusview = new List<BRStatusView>();
            SmallBlock smallBlock = null;
            foreach (Recl_BRstatus stat in cust)
            {
                try
                {
                    TWCCClasses1DataContext dataContentTWCC = new TWCCClasses1DataContext
               ("Data Source=" + ServerName + ";" +
               "Initial Catalog=TWCC;Integrated Security=True");
                    smallBlock = (from SmallBlock in dataContentTWCC.SmallBlocks.AsEnumerable().Take(20)
                                  where SmallBlock.SmallBlock1 == stat.BlockID
                                  select SmallBlock).Single();
                    ULSLoggingService.LogMessage(string.Format("found one item from smallblocks {0}",stat.ID));
                }
                catch (Exception ex)
                {
                    ULSLoggingService.LogUIException(string.Format("error in read list bdc {0}", ex.Message),ex);
                }

                BRStatusView view = new BRStatusView();
                if (stat != null)
                {
                    view.ID = stat.ID;
                    view.BlockID = stat.BlockID;
                   /* view.Phase = stat.Phase;
                    view.BRStatus = stat.BRstatus;
                    view.DateInitiated = stat.DateInitiated;
                    view.DateApproved = stat.DateApproved;
                    view.Notes = stat.Notes;*/
                }

                if (smallBlock != null)
                {
                   // view.SmallAC = smallBlock.SmallAC;
                    view.MineArea = smallBlock.MineArea;
                }
                lbrstatusview.Add(view);
            }

            IEnumerable<BRStatusView> myEnumerable = lbrstatusview;

            ULSLoggingService.LogMessage("finished readlist");

            return myEnumerable;
        }



        public static Recl_BRstatus Create(BRStatusView newBRStatus)
        {
            throw new System.NotImplementedException();
        }

        public static void Delete(int iD)
        {
            throw new System.NotImplementedException();
        }

        public static void Update(BRStatusView bRStatus)
        {
            throw new System.NotImplementedException();
        }
    }
}
