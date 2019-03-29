CREATE TABLE [Currency]
	(
	[RowID] INTEGER PRIMARY KEY AUTOINCREMENT, 
	[CurrencyId] GUID NOT NULL UNIQUE,
	[Name] VARCHAR2(255) NOT NULL,
	[Code] NVARCHAR2(20) NOT NULL)
	);

CREATE UNIQUE INDEX [IX_Currency_CurrencyID] ON [Currency]([CurrencyId]);
GO

CREATE TABLE [AccountBalance]
	(
	[RowID] INTEGER PRIMARY KEY AUTOINCREMENT, 
	[AccountBalanceId] GUID NOT NULL UNIQUE,
	[AccountName] VARCHAR2(100) NOT NULL,
	[CurrencyId] GUID NOT NULL REFERENCES [Currency]([CurrencyId]), 
	);

CREATE UNIQUE INDEX [IX_AccountBalance_AccountBalanceId] ON [AccountBalance]([AccountBalanceId]);
GO