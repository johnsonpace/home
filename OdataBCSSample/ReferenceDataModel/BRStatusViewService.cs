using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OdataBCSSample;

namespace OdataBCSSample.ReferenceDataModel
{
    public partial class BRStatusViewService
    {
        public static IEnumerable<BRStatusView> ReadList()
        {
            const string ServerName = @"WIN-3QNLGCNIT6M\SHAREPOINT";
            string connection = Common.GetSQLonnectionString("ReferenceData");

            ULSLoggingService.LogMessage(string.Format("server name is {0}", ServerName));
            ULSLoggingService.LogMessage(string.Format("secure store connection string is {0}", connection));
            ReferenceDataClasses1DataContext dataContext = new ReferenceDataClasses1DataContext
                  (connection);

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
                    ULSLoggingService.LogMessage(string.Format("found one item from smallblocks {0}", stat.ID));
                }
                catch (Exception ex)
                {
                    ULSLoggingService.LogUIException(string.Format("error in read list bdc {0}", ex.Message), ex);
                }

                BRStatusView view = new BRStatusView();
                if (stat != null)
                {
                    view.ID = stat.ID;
                    view.BlockID = stat.BlockID;
                     view.Phase = stat.Phase;
                     view.BRStatus = stat.BRstatus;
                     view.DateInitiated = stat.DateInitiated;
                     view.DateApproved = stat.DateApproved;
                     view.Notes = stat.Notes;
                }

                if (smallBlock != null)
                {
                    view.SmallAC = smallBlock.SmallAC;
                    view.MineArea = smallBlock.MineArea;
                }
                lbrstatusview.Add(view);
            }

            

            ULSLoggingService.LogMessage("finished readlist");

            return lbrstatusview;
        }

      /*  public static IEnumerable<BRStatusView> ReadList()
        {
            List<BRStatusView> viewlist = new List<BRStatusView>();
            BRStatusView view = new BRStatusView();
            view.ID = 1;
            view.MineArea = "minearea";
            view.Notes = "notes";
            viewlist.Add(view);

            return viewlist;
        }*/

        public static BRStatusView ReadItem(int iD)
        {
            const string ServerName = @"WIN-3QNLGCNIT6M\SHAREPOINT";
            string connection = Common.GetSQLonnectionString("ReferenceData");

            ULSLoggingService.LogMessage(string.Format("server name is {0}", ServerName));
            ULSLoggingService.LogMessage(string.Format("secure store connection string is {0}", connection));
            ReferenceDataClasses1DataContext dataContext = new ReferenceDataClasses1DataContext
                  (connection);
            Recl_BRstatus BRStatus = (from Recl_BRstatus in dataContext.Recl_BRstatus.AsEnumerable().Take(20)
                                      where Recl_BRstatus.ID == iD
                                      select Recl_BRstatus).Single();
            string connection1 = Common.GetSQLonnectionString("TWCC");

            ULSLoggingService.LogMessage(string.Format("server name is {0}", connection1));
            TWCCClasses1DataContext dataContentTWCC = new TWCCClasses1DataContext
            (connection1);
            SmallBlock smallBlockentity = (from SmallBlock in dataContentTWCC.SmallBlocks.AsEnumerable().Take(20)
                                           where SmallBlock.SmallBlock1 == BRStatus.BlockID
                                           select SmallBlock).Single();
            BRStatusView view = new BRStatusView();
            if (BRStatus != null)
            {
                view.ID = BRStatus.ID;
                view.BlockID = BRStatus.BlockID;
                view.Phase = BRStatus.Phase;
                view.BRStatus = BRStatus.BRstatus;
                view.DateInitiated = BRStatus.DateInitiated;
                view.DateApproved = BRStatus.DateApproved;
                view.Notes = BRStatus.Notes;
            }

            if (smallBlockentity != null)
            {
                view.SmallAC = smallBlockentity.SmallAC;
                view.MineArea = smallBlockentity.MineArea;
            }

            return view;
        }

        public static BRStatusView Create(BRStatusView newBRStatusView)
        {
            const string ServerName = @"WIN-3QNLGCNIT6M\SHAREPOINT";
            string connection = Common.GetSQLonnectionString("ReferenceData");

            ULSLoggingService.LogMessage(string.Format("server name is {0}", ServerName));
            ULSLoggingService.LogMessage(string.Format("secure store connection string is {0}", connection));
            ReferenceDataClasses1DataContext dataContext = new ReferenceDataClasses1DataContext
                  (connection);
            Recl_BRstatus BRStatus = new Recl_BRstatus();
            BRStatus.ID = newBRStatusView.ID;
            BRStatus.Notes = newBRStatusView.Notes;
            BRStatus.Phase = newBRStatusView.Phase;
            BRStatus.BlockID = newBRStatusView.BlockID;
            BRStatus.BRstatus = newBRStatusView.BRStatus;
            BRStatus.DateApproved = newBRStatusView.DateApproved;
            BRStatus.DateInitiated = newBRStatusView.DateApproved;
            dataContext.Recl_BRstatus.InsertOnSubmit(BRStatus);
            dataContext.SubmitChanges();

            string connection1 = Common.GetSQLonnectionString("TWCC");

            ULSLoggingService.LogMessage(string.Format("server name is {0}", connection1));
            TWCCClasses1DataContext dataContentTWCC = new TWCCClasses1DataContext
            (connection1);
            SmallBlock smallBlock = new SmallBlock();
            smallBlock.MineArea = newBRStatusView.MineArea;
            smallBlock.SmallAC = newBRStatusView.SmallAC;
            dataContentTWCC.SmallBlocks.InsertOnSubmit(smallBlock);
            dataContentTWCC.SubmitChanges();

            return newBRStatusView;
          
        }

        public static void Delete(int iD)
        {
            throw new System.NotImplementedException();
        }

        public static void Update(BRStatusView bRStatusView, int ID)
        {
            const string ServerName = @"WIN-3QNLGCNIT6M\SHAREPOINT";
            string connection = Common.GetSQLonnectionString("ReferenceData");

            ULSLoggingService.LogMessage(string.Format("server name is {0}", ServerName));
            ULSLoggingService.LogMessage(string.Format("secure store connection string is {0}", connection));
            ReferenceDataClasses1DataContext dataContext = new ReferenceDataClasses1DataContext
                  (connection);
            Recl_BRstatus BRStatus = (from Recl_BRstatus in dataContext.Recl_BRstatus.AsEnumerable().Take(20)
                                      where Recl_BRstatus.ID == ID
                                      select Recl_BRstatus).Single();

            if (BRStatus != null)
            {
                //BRStatus.ID = bRStatusView.ID;
                BRStatus.BlockID = bRStatusView.BlockID;
                BRStatus.Phase = bRStatusView.Phase;
                BRStatus.BRstatus = bRStatusView.BRStatus;
                BRStatus.DateInitiated = bRStatusView.DateInitiated;
                BRStatus.DateApproved = bRStatusView.DateApproved;
                BRStatus.Notes = bRStatusView.Notes;
            }

            dataContext.SubmitChanges();

        }

        public static BRStatusView ReadItem(int iD, string smallBlock)
        {
            const string ServerName = @"WIN-3QNLGCNIT6M\SHAREPOINT";
            string connection = Common.GetSQLonnectionString("ReferenceData");

            ULSLoggingService.LogMessage(string.Format("server name is {0}", ServerName));
            ULSLoggingService.LogMessage(string.Format("secure store connection string is {0}", connection));
            ReferenceDataClasses1DataContext dataContext = new ReferenceDataClasses1DataContext
                  (connection);
            Recl_BRstatus BRStatus = (from Recl_BRstatus in dataContext.Recl_BRstatus.AsEnumerable().Take(20)
                                      where Recl_BRstatus.ID == iD
                                      select Recl_BRstatus).Single();

            string connection1 = Common.GetSQLonnectionString("TWCC");

            ULSLoggingService.LogMessage(string.Format("server name is {0}", connection1));
            TWCCClasses1DataContext dataContentTWCC = new TWCCClasses1DataContext
            (connection1);
            SmallBlock smallBlockentity = (from SmallBlock in dataContentTWCC.SmallBlocks.AsEnumerable().Take(20)
                                     where SmallBlock.SmallBlock1 == BRStatus.BlockID
                                     select SmallBlock).Single();
            BRStatusView view = new BRStatusView();
            if (BRStatus != null)
            {
                view.ID = BRStatus.ID;
                view.BlockID = BRStatus.BlockID;
                 view.Phase = BRStatus.Phase;
                 view.BRStatus = BRStatus.BRstatus;
                 view.DateInitiated = BRStatus.DateInitiated;
                 view.DateApproved = BRStatus.DateApproved;
                 view.Notes = BRStatus.Notes;
            }

            if (smallBlockentity != null)
            {
                view.SmallAC = smallBlockentity.SmallAC;
                view.MineArea = smallBlockentity.MineArea;
            }

            return view;


        }
    }
}
