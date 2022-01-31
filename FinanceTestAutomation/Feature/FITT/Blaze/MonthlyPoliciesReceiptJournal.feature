Feature: MonthlyPoliciesReceiptJournal
	Create automation test script for Monthly Policies Receipt Journal
	As a FITT QA
	I want to be able to compare the data in the synonym tables with data in the ledger 

@Blaze
Scenario: Verify every monthly direct policy has a invoice journal created in Ledger
	Given monthly direct policies are created in Blaze and loaded in snowflake
	When I check the data between synonyms and FITT ledger view
	Then the invoice ledger should be created for each monthly direct policy in FITT

@Blaze
Scenario: Verify no duplicate invoice journals created in Ledger for monthly direct policies
	Given Invoice journals for monthly direct policies are created in the FITT ledger
	Then there should not be any duplicate journals for a any monthly direct policy


@Blaze
Scenario: Verify every monthly direct policy has a Receipt journal created in Ledger
	Given Underpaid log entries are created in customer recipt log for MD
	Then the Receipt journal should be created for each monthly direct policy in FITT

@Blaze
Scenario: Verify no duplicate receipt journals created in Ledger for monthly direct policies
	Given Receipt journals for monthly direct policies are created in the FITT ledger
	Then there should not be any duplicate Receipt journals for monthly direct policies