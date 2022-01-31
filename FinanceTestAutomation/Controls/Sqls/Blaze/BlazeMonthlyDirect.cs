using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTestAutomation.Controls.Sqls
{
    public class BlazeMonthlyDirect : BlazeBaseDbRequests
    {
        private string getMonthlyDirectCountSynonymSnowflake = "SELECT COUNT(*) FROM SynonymSnowflake.NewBusiness WHERE paymentfrequency = 'Monthly'";

        private string getMDReceiptCount = "SELECT COUNT(*) FROM SynonymSnowflake.Receipt Rep JOIN SynonymSnowflake.NewBusiness Nb ON Nb.policyid = Rep.policyid WHERE Rep.paymentfrequency = 'Monthly'";


        //private string getInvoiceJournalCountInLedger = "SELECT COUNT(JNL.DocumentNumber) FROM Ledger.AccountingEntryJournalMaster AEJM INNER JOIN Ledger.Journal JNL ON JNL.AccountingEntryJournalMasterSK = AEJM.AccountingEntryJournalMasterSK WHERE AEJM.PaymentFrequencySK = 2 AND AEJM.EventTypeSK = 1";

        //mod by anggi
        private string getInvoiceJournalCountInLedger = "SELECT COUNT(DISTINCT(documentnumber)) FROM ledger.vw_ledger WHERE paymentfrequency = 'Monthly'" + "" +
"AND EventTypeSK = 1";

        //private string getDuplicateInvoiceJournal = "SELECT CASE WHEN SUM(COUNT1) IS NULL THEN 0 ELSE 1 END FROM(SELECT CASE WHEN COUNT(*)>=1 THEN 1 ELSE 0 END AS count1 FROM Ledger.vw_Ledger WHERE PaymentFrequencySK = 2 AND EventTypeSK = 1 GROUP BY policyid HAVING COUNT(distinct(DocumentNumber)) > 1) a";

        //mod by anggi
        private string getDuplicateInvoiceJournal = "SELECT CASE WHEN SUM(COUNT1) IS NULL THEN 0 ELSE 1 END FROM(SELECT CASE WHEN COUNT(*)>=1 THEN 1 ELSE 0 END AS count1 FROM Ledger.vw_Ledger WHERE StreamCode = 'MonthlyDirect'" +
"AND EventName = 'New business' AND EventTypeSK = 1 GROUP BY policyid HAVING COUNT(distinct(DocumentNumber)) > 1) a";

        private string getReceiptJournalCountInLedger = "SELECT COUNT(JNL.DocumentNumber) FROM Ledger.AccountingEntryJournalMaster AEJM INNER JOIN Ledger.Journal JNL ON JNL.AccountingEntryJournalMasterSK = AEJM.AccountingEntryJournalMasterSK INNER JOIN Ledger.AccountingEntryJournalMasterAttribute AJMA ON AJMA.AccountingEntryJournalMasterSK = AEJM.AccountingEntryJournalMasterSK INNER JOIN OpenItemManagement.CustomerReceiptLog CRL ON CRL.CustomerReceiptLogSK = AJMA.CustomerReceiptLogSK INNER JOIN OpenItemManagement.CustomerReceivable RCV ON RCV.CustomerReceivableSK = CRL.CustomerReceivableSK WHERE AEJM.EventTypeSK = 2 AND AEJM.PaymentFrequencySK = 2 AND RCV.EventTypeSK = 1";

        private string getDuplicateReceiptJournal = "SELECT CASE WHEN SUM(COUNT1) IS NULL THEN 0 ELSE 1 END FROM(SELECT CASE WHEN COUNT(*)>=1 THEN 1 ELSE 0 END AS count1 FROM Ledger.vw_Ledger lr JOIN OpenItemManagement.CustomerReceivable cr ON cr.PolicyId = lr.PolicyId WHERE lr.EventName = 'Receipt' AND cr.EventTypeSK = 1 GROUP BY lr.policyid, lr.DocumentNumber HAVING COUNT(distinct(lr.DocumentNumber)) > 1) a";

        private string getCustomerReceivableCount = "SELECT COUNT(*) FROM OpenItemManagement.CustomerReceivable WHERE EventTypeSK = 1 AND PaymentFrequencySK = 2 ";

        private string getUnpaidCustomerLogCount = "SELECT COUNT(*) FROM OpenItemManagement.CustomerReceiptLog crl JOIN OpenItemManagement.CustomerReceivable cr ON cr.CustomerReceivableSK = crl.CustomerReceivableSK WHERE cr.EventTypeSK = 1 AND cr.PaymentFrequencySK = 2 AND crl.CustomerReceiptStatusTypeSK = 1";

        private string getUnderpaidCustomerLogCount = "SELECT COUNT(DISTINCT crl.CustomerReceivableSK) FROM OpenItemManagement.CustomerReceiptLog crl JOIN OpenItemManagement.CustomerReceivable cr ON cr.CustomerReceivableSK = crl.CustomerReceivableSK WHERE cr.EventTypeSK = 1 AND crl.CustomerReceiptStatusTypeSK = 2 AND cr.PaymentFrequencySK = 2";



        public int GetNBCountInSynonymSnowflake()
        {
            return GetCountOfRecordsAUBlaze(getMonthlyDirectCountSynonymSnowflake);
        }

        public int GetMDReceiptCount()
        {
            return GetCountOfRecordsAUBlaze(getMDReceiptCount);
        }


        public int GetInvoiceCountInFITT()
        {
            return GetCountOfRecordsAUBlaze(getInvoiceJournalCountInLedger);
        }

        public int GetDuplicateInvoiceCountInFITT()
        {
            return GetCountOfRecordsAUBlaze(getDuplicateInvoiceJournal);
        }

        public int GetReceiptCountInFITT()
        {
            return GetCountOfRecordsAUBlaze(getReceiptJournalCountInLedger);
        }

        public int GetDuplicateReceiptCountInFITT()
        {
            return GetCountOfRecordsAUBlaze(getDuplicateReceiptJournal);
        }

        public int GetCustomerReceivableCount()
        {
            return GetCountOfRecordsAUBlaze(getCustomerReceivableCount);
        }


        public int GetUnpaidCustomerLogCount()
        {
            return GetCountOfRecordsAUBlaze(getUnpaidCustomerLogCount);
        }

        public int GetUnderpaidCustomerLogCount()
        {
            return GetCountOfRecordsAUBlaze(getUnderpaidCustomerLogCount);
        }


    }
}
