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

CREATE TABLE [Reward]
	(
	[RowID] INTEGER PRIMARY KEY AUTOINCREMENT, 
	[RewardId] GUID NOT NULL UNIQUE,
	[Name] VARCHAR2(255) NOT NULL,
	[CurrencyId] GUID NOT NULL REFERENCES [Currency]([CurrencyId]),
	[Rarity] INTEGER NOT NULL,
	[AmountRangeBegin] DECIMAL NOT NULL,
	[AmountRangeEnd] DECIMAL NOT NULL,
	[ExcludeFromRandomization] BOOL NOT NULL,
	[ExcludeFromLootboxRandomization] BOOL NOT NULL
	);

CREATE UNIQUE INDEX [IX_Reward_RewardId] ON [Reward]([RewardId]);
GO

CREATE TABLE [Task]
	(
	[RowID] INTEGER PRIMARY KEY AUTOINCREMENT, 
	[TaskId] GUID NOT NULL UNIQUE,
	[Name] VARCHAR2(255) NOT NULL,
	[TaskType] int NOT NULL,
	[Description] VARCHAR2 NULL,
	[DueDate] DATETIME NULL,
	[CreatedOn] DATETIME NOT NULL
	);

CREATE UNIQUE INDEX [IX_Task_TaskId] ON [Task]([TaskId]);
GO

CREATE TABLE [TaskCompletion]
	(
	[RowID] INTEGER PRIMARY KEY AUTOINCREMENT, 
	[TaskCompletionId] GUID NOT NULL UNIQUE,
	[TaskId] GUID NOT NULL REFERENCES [Task]([TaskId]),
	[CompletedOn] DATETIME NOT NULL
	);

CREATE UNIQUE INDEX [IX_TaskCompletion_TaskCompletionId] ON [TaskCompletion]([TaskCompletionId]);
GO

CREATE TABLE [TaskReward]
	(
	[RowID] INTEGER PRIMARY KEY AUTOINCREMENT, 
	[TaskRewardId] GUID NOT NULL UNIQUE,
	[TaskId] GUID NOT NULL REFERENCES [Task]([TaskId]),
	[RewardId] GUID NOT NULL REFERENCES [Reward]([RewardId]),	
	[Amount] DECIMAL NULL,
	[ClaimedOn] DATETIME NULL,
	UNIQUE([TaskId] ASC, [RewardId] ASC)
	);

CREATE UNIQUE INDEX [IX_TaskReward_TaskRewardId] ON [TaskReward]([TaskRewardId]);
GO

CREATE VIEW [OpenTasksView]
AS
SELECT 
       [Task].[TaskId], 
       [Task].[Name], 
       [TaskType], 
       [Description], 
       [DueDate], 
       COUNT ([TaskReward].[TaskRewardId]) AS [RewardCount]
FROM   [Task]
       LEFT JOIN [TaskCompletion] ON [TaskCompletion].[TaskId] = [Task].[TaskId]
       LEFT JOIN [TaskReward] ON [TaskReward].[TaskId] = [Task].[TaskId]
WHERE  [TaskCompletion].[CompletedOn] IS NULL
       OR  [Task].[TaskType] = 1
GROUP  BY [Task].[TaskId];
GO