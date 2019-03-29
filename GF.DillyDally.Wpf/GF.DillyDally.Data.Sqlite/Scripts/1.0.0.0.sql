CREATE TABLE [Currency]
	(
	[RowID] INTEGER PRIMARY KEY AUTOINCREMENT, 
	[CurrencyId] GUID NOT NULL UNIQUE,
	[Name] VARCHAR2(255) NOT NULL,
	[Code] NVARCHAR2(20) NOT NULL
	);

CREATE UNIQUE INDEX [IX_Currency_CurrencyID] ON [Currency]([CurrencyId]);
GO

CREATE TABLE [AccountBalance]
	(
	[RowID] INTEGER PRIMARY KEY AUTOINCREMENT, 
	[AccountBalanceId] GUID NOT NULL UNIQUE,
	[AccountName] VARCHAR2(100) NOT NULL,
	[CurrencyId] GUID NOT NULL REFERENCES [Currency]([CurrencyId]) 
	);

CREATE UNIQUE INDEX [IX_AccountBalance_AccountBalanceId] ON [AccountBalance]([AccountBalanceId]);
GO

CREATE TABLE [AccountBalanceTransaction]
	(
	[RowID] INTEGER PRIMARY KEY AUTOINCREMENT, 
	[AccountBalanceTransactionId] GUID NOT NULL UNIQUE,
	[AccountBalanceId] GUID NOT NULL REFERENCES [AccountBalance]([AccountBalanceId]),
	[CurrencyId] GUID NOT NULL REFERENCES [Currency]([CurrencyId]),
	[Amount] DECIMAL NOT NULL
	);
	
CREATE UNIQUE INDEX [IX_AccountBalanceTransaction_AccountBalanceTransactionId] ON [AccountBalanceTransaction]([AccountBalanceTransactionId]);
GO

CREATE TABLE [RewardTemplate]
	(
	[RowID] INTEGER PRIMARY KEY AUTOINCREMENT, 
	[RewardTemplateId] GUID NOT NULL UNIQUE,
	[Name] VARCHAR2(255) NOT NULL,
	[CurrencyId] GUID NOT NULL REFERENCES [Currency]([CurrencyId])
	);

CREATE UNIQUE INDEX [IX_RewardTemplate_RewardTemplateId] ON [RewardTemplate]([RewardTemplateId]);
GO

CREATE TABLE [Reward]
	(
	[RowID] INTEGER PRIMARY KEY AUTOINCREMENT, 
	[RewardId] GUID NOT NULL UNIQUE,
	[Name] VARCHAR2(255) NOT NULL,
	[CurrencyId] GUID NOT NULL REFERENCES [Currency]([CurrencyId]),
	[RewardTemplateId] GUID NOT NULL REFERENCES [RewardTemplate]([RewardTemplateId])
	);

CREATE UNIQUE INDEX [IX_Reward_RewardId] ON [Reward]([RewardId]);
GO

CREATE TABLE [Task]
	(
	[RowID] INTEGER PRIMARY KEY AUTOINCREMENT, 
	[TaskId] GUID NOT NULL UNIQUE,
	[Name] VARCHAR2(255) NOT NULL,
	[TaskType] int NOT NULL,
	[Description] VARCHAR2 NOT NULL,
	[DueDate] DATETIME NULL
	);

CREATE UNIQUE INDEX [IX_Task_TaskId] ON [Task]([TaskId]);
GO

CREATE TABLE [TaskCompletions]
	(
	[RowID] INTEGER PRIMARY KEY AUTOINCREMENT, 
	[TaskCompletionId] GUID NOT NULL UNIQUE,
	[TaskId] GUID NOT NULL REFERENCES [Task]([TaskId]),
	[CompletedOn] DATETIME NOT NULL
	);

CREATE UNIQUE INDEX [IX_TaskCompletions_TaskCompletionId] ON [TaskCompletions]([TaskCompletionId]);
GO

CREATE TABLE [TaskRewards]
	(
	[RowID] INTEGER PRIMARY KEY AUTOINCREMENT, 
	[TaskRewardId] GUID NOT NULL UNIQUE,
	[TaskId] GUID NOT NULL REFERENCES [Task]([TaskId]),
	[RewardId] GUID NOT NULL REFERENCES [Reward]([RewardId]),	
	[Amount] DECIMAL NOT NULL,
	[ClaimedOn] DATETIME NULL,
	UNIQUE([TaskId] ASC, [RewardId] ASC)
	);

CREATE UNIQUE INDEX [IX_TaskRewards_TaskRewardId] ON [TaskRewards]([TaskRewardId]);
GO