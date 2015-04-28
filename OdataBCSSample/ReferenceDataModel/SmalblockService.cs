using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OdataBCSSample;

namespace OdataBCSSample.ReferenceDataModel
{
    public partial class SmalblockService
    {
        public static IEnumerable<SmallBlock> ReadList()
        {
            

           const string ServerName = @"WIN-3QNLGCNIT6M\SHAREPOINT";
           
           
            TWCCClasses1DataContext dataContentTWCC = new TWCCClasses1DataContext
               ("Data Source=" + ServerName + ";" +
               "Initial Catalog=TWCC;Integrated Security=True");
                    IEnumerable<SmallBlock> cust = (from SmallBlock in dataContentTWCC.SmallBlocks.AsEnumerable().Take(20)
                                  select SmallBlock);


                    ULSLoggingService.LogMessage(string.Format("found one item from smallblocks"));
              

            ULSLoggingService.LogMessage("finished readlist");

            return cust;
        }

        public static void Update(SmallBlock smalblock)
        {
            throw new System.NotImplementedException();
        }

        public static SmallBlock Create(SmallBlock newSmalblock)
        {
            throw new System.NotImplementedException();
        }

        public static void Delete(long id)
        {
            throw new System.NotImplementedException();
        }

        public static IEnumerable<BRStatusView> SmalblockToBRStatusView(string smallBlock1)
        {
            throw new System.NotImplementedException();
        }

        public static SmallBlock ReadItem(string smallBlock1)
        {
            const string ServerName = @"WIN-3QNLGCNIT6M\SHAREPOINT";


            
            TWCCClasses1DataContext dataContentTWCC = new TWCCClasses1DataContext
            ("Data Source=" + ServerName + ";" +
            "Initial Catalog=TWCC;Integrated Security=True");
            SmallBlock smallBlockentity = (from SmallBlock in dataContentTWCC.SmallBlocks.AsEnumerable().Take(20)
                                           where SmallBlock.SmallBlock1 == smallBlock1
                                           select SmallBlock).Single();
           

            return smallBlockentity;


        }
    }
}
