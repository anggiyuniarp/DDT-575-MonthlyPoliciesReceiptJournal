using System;
using TechTalk.SpecFlow;
using FinanceTestAutomation.Controls.Sqls;
using NUnit.Framework;

namespace FinanceTestAutomation.Steps.FITT.Blaze
{
    [Binding]
    public class MonthlyPoliciesReceiptJournalSteps
    {
        private readonly BlazeMonthlyDirect _monthlydirectjournal = new BlazeMonthlyDirect();

        [Given(@"Underpaid log entries are created in customer recipt log for MD")]
        public void GivenUnderpaidLogEntriesAreCreatedInCustomerReciptLogForMD()
        {
            var countOfUnderPaidPolicies = _monthlydirectjournal.GetUnderpaidCustomerLogCount();
            ScenarioContext.Current["countOfUnderPaidPolicies"] = countOfUnderPaidPolicies;
        }


        [Then(@"the Receipt journal should be created for each monthly direct policy in FITT")]
        public void ThenTheReceiptJournalShouldBeCreatedForEachMonthlyDirectPolicyInFITT()
        {
            var countOfUnderPaidPolicies = ScenarioContext.Current["countOfUnderPaidPolicies"];
            var countInFITT = _monthlydirectjournal.GetReceiptCountInFITT();
            ScenarioContext.Current["countInFITT"] = countInFITT;
            Assert.AreEqual(countOfUnderPaidPolicies, countInFITT);
            Console.WriteLine("Number of records in SnowFlake Synonym is " + countOfUnderPaidPolicies +
                "Number of records in FITT ledger is" + countInFITT);
        }



        [Given(@"Receipt journals for monthly direct policies are created in the FITT ledger")]
        public void GivenReceiptJournalsForMonthlyDirectPoliciesAreCreatedInTheFITTLedger()
        {
            var duplicateReceiptCountInFITT = _monthlydirectjournal.GetDuplicateReceiptCountInFITT();
            ScenarioContext.Current["duplicateReceiptCountInFITT"] = duplicateReceiptCountInFITT;
        }
         
        [Then(@"there should not be any duplicate Receipt journals for monthly direct policies")]
        public void ThenThereShouldNotBeAnyDuplicateReceiptJournalsForMonthlyDirectPolicies()
        {
            var duplicateReceiptCountInFITT = ScenarioContext.Current["duplicateReceiptCountInFITT"];
            Assert.AreEqual(duplicateReceiptCountInFITT, 0);
            Console.WriteLine("Number of duplicate records for monthly direct invoice journals in" + duplicateReceiptCountInFITT);
        }
    }
}
