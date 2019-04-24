CREATE TABLE [Files](
  [RowID] INTEGER PRIMARY KEY AUTOINCREMENT, 
  [FileId] GUID NOT NULL UNIQUE,
  [Binary] BLOB NOT NULL, 
  [HashCode] TEXT NOT NULL, 
  [Size] INTEGER NOT NULL,
  [Name] TEXT(255) NOT NULL, 
  [Extension] TEXT NOT NULL);

CREATE UNIQUE INDEX [IX_Files_FileId] ON [Files]([FileId]);
GO

CREATE TABLE [RunningNumberCounters](
  [RowID] INTEGER PRIMARY KEY AUTOINCREMENT, 
  [RunningNumberCounterId] GUID NOT NULL UNIQUE,
  [CurrentNumber] INTEGER NOT NULL, 
  [CounterArea] INTEGER NOT NULL,
  [Prefix] VARCHAR2(10) NOT NULL);

CREATE UNIQUE INDEX [IX_RunningNumberCounters_RunningNumberCounterId] ON [RunningNumberCounters]([RunningNumberCounterId]);
GO

CREATE TABLE [RunningNumbers](
  [RowID] INTEGER PRIMARY KEY AUTOINCREMENT, 
  [RunningNumberId] GUID NOT NULL UNIQUE,
  [RunningNumber] VARCHAR2(100) NOT NULL, 
  [RunningNumberCounterId] GUID NOT NULL REFERENCES [RunningNumberCounters]([RunningNumberCounterId]));

CREATE UNIQUE INDEX [IX_RunningNumbers_RunningNumberId] ON [RunningNumbers]([RunningNumberId]);
GO

CREATE TABLE [Category]
	(
	[RowID] INTEGER PRIMARY KEY AUTOINCREMENT, 
	[CategoryId] GUID NOT NULL UNIQUE,
	[RunningNumberId] GUID NOT NULL REFERENCES [RunningNumbers]([RunningNumberId]),
	[Name] VARCHAR2(255) NOT NULL,
	[ColorCode] NVARCHAR2(9) NOT NULL
	);


CREATE UNIQUE INDEX [IX_Category_CategoryId] ON [Category]([CategoryId]);
GO

CREATE TABLE [Lane]
	(
	[RowID] INTEGER PRIMARY KEY AUTOINCREMENT, 
	[LaneId] GUID NOT NULL UNIQUE,
	[RunningNumberId] GUID NOT NULL REFERENCES [RunningNumbers]([RunningNumberId]),
	[Name] VARCHAR2(255) NOT NULL,
	[ColorCode] NVARCHAR2(9) NOT NULL,
	[IsCompletedLane] BOOL NOT NULL,
	[IsRejectedLane] BOOL NOT NULL
	);

CREATE UNIQUE INDEX [IX_Lane_LaneId] ON [Lane]([LaneId]);
GO






CREATE TABLE [Images](
  [RowID] INTEGER PRIMARY KEY AUTOINCREMENT, 
  [ImageId] GUID NOT NULL UNIQUE,
  [Binary] BLOB NOT NULL, 
  [OriginalFileId] GUID NOT NULL REFERENCES [Files]([FileId]),
  [SizeType] INTEGER NOT NULL);

CREATE UNIQUE INDEX [IX_Images_ImageId] ON [Images]([ImageId]);
GO






CREATE TABLE [Achievement]
	(
	[RowID] INTEGER PRIMARY KEY AUTOINCREMENT, 
	[AchievementId] GUID NOT NULL UNIQUE,
	[Name] VARCHAR2(255) NOT NULL,
	[Type] INTEGER NOT NULL,
	[CategoryId] GUID NOT NULL REFERENCES [Category]([CategoryId]),
	[LaneId] GUID NOT NULL REFERENCES [Lane]([LaneId]),
	[RewardCount] INTEGER NOT NULL,
	[LinkAchievementId] GUID NULL REFERENCES [Achievement]([AchievementId]),
	[DueDate] DATETIME NULL,
	[CreatedOn] DATETIME NOT NULL,
	[Description] VARCHAR2 NULL,
	[PreviewImageId] GUID NULL REFERENCES [Images]([ImageId])
	);

CREATE UNIQUE INDEX [IX_Achievement_AchievementId] ON [Achievement]([AchievementId]);
GO





--CREATE TABLE [Currency]
--	(
--	[RowID] INTEGER PRIMARY KEY AUTOINCREMENT, 
--	[CurrencyId] GUID NOT NULL UNIQUE,
--	[Name] VARCHAR2(255) NOT NULL,
--	[Code] NVARCHAR2(20) NOT NULL
--	);

--CREATE UNIQUE INDEX [IX_Currency_CurrencyID] ON [Currency]([CurrencyId]);
--GO

--CREATE TABLE [AccountBalance]
--	(
--	[RowID] INTEGER PRIMARY KEY AUTOINCREMENT, 
--	[AccountBalanceId] GUID NOT NULL UNIQUE,
--	[AccountName] VARCHAR2(100) NOT NULL,
--	[CurrencyId] GUID NOT NULL REFERENCES [Currency]([CurrencyId]) 
--	);

--CREATE UNIQUE INDEX [IX_AccountBalance_AccountBalanceId] ON [AccountBalance]([AccountBalanceId]);
--GO

--CREATE TABLE [AccountBalanceTransaction]
--	(
--	[RowID] INTEGER PRIMARY KEY AUTOINCREMENT, 
--	[AccountBalanceTransactionId] GUID NOT NULL UNIQUE,
--	[AccountBalanceId] GUID NOT NULL REFERENCES [AccountBalance]([AccountBalanceId]),
--	[CurrencyId] GUID NOT NULL REFERENCES [Currency]([CurrencyId]),
--	[Amount] DECIMAL NOT NULL
--	);
	
--CREATE UNIQUE INDEX [IX_AccountBalanceTransaction_AccountBalanceTransactionId] ON [AccountBalanceTransaction]([AccountBalanceTransactionId]);
--GO

--CREATE TABLE [Reward]
--	(
--	[RowID] INTEGER PRIMARY KEY AUTOINCREMENT, 
--	[RewardId] GUID NOT NULL UNIQUE,
--	[Name] VARCHAR2(255) NOT NULL,
--	[CurrencyId] GUID NOT NULL REFERENCES [Currency]([CurrencyId]),
--	[Rarity] INTEGER NOT NULL,
--	[AmountRangeBegin] DECIMAL NOT NULL,
--	[AmountRangeEnd] DECIMAL NOT NULL,
--	[ExcludeFromRandomization] BOOL NOT NULL,
--	[ExcludeFromLootboxRandomization] BOOL NOT NULL
--	);

--CREATE UNIQUE INDEX [IX_Reward_RewardId] ON [Reward]([RewardId]);
--GO

--CREATE TABLE [Task]
--	(
--	[RowID] INTEGER PRIMARY KEY AUTOINCREMENT, 
--	[TaskId] GUID NOT NULL UNIQUE,
--	[Name] VARCHAR2(255) NOT NULL,
--	[TaskType] int NOT NULL,
--	[Description] VARCHAR2 NULL,
--	[DueDate] DATETIME NULL,
--	[CreatedOn] DATETIME NOT NULL
--	);

--CREATE UNIQUE INDEX [IX_Task_TaskId] ON [Task]([TaskId]);
--GO

--CREATE TABLE [TaskCompletion]
--	(
--	[RowID] INTEGER PRIMARY KEY AUTOINCREMENT, 
--	[TaskCompletionId] GUID NOT NULL UNIQUE,
--	[TaskId] GUID NOT NULL REFERENCES [Task]([TaskId]),
--	[CompletedOn] DATETIME NOT NULL
--	);

--CREATE UNIQUE INDEX [IX_TaskCompletion_TaskCompletionId] ON [TaskCompletion]([TaskCompletionId]);
--GO

--CREATE TABLE [TaskReward]
--	(
--	[RowID] INTEGER PRIMARY KEY AUTOINCREMENT, 
--	[TaskRewardId] GUID NOT NULL UNIQUE,
--	[TaskId] GUID NOT NULL REFERENCES [Task]([TaskId]),
--	[RewardId] GUID NOT NULL REFERENCES [Reward]([RewardId]),	
--	[Amount] DECIMAL NULL,
--	[ClaimedOn] DATETIME NULL,
--	UNIQUE([TaskId] ASC, [RewardId] ASC)
--	);

--CREATE UNIQUE INDEX [IX_TaskReward_TaskRewardId] ON [TaskReward]([TaskRewardId]);
--GO

--CREATE VIEW [OpenTasksView]
--AS
--SELECT 
--       [Task].[TaskId], 
--       [Task].[Name], 
--       [TaskType], 
--       [Description], 
--       [DueDate], 
--       COUNT ([TaskReward].[TaskRewardId]) AS [RewardCount]
--FROM   [Task]
--       LEFT JOIN [TaskCompletion] ON [TaskCompletion].[TaskId] = [Task].[TaskId]
--       LEFT JOIN [TaskReward] ON [TaskReward].[TaskId] = [Task].[TaskId]
--WHERE  [TaskCompletion].[CompletedOn] IS NULL
--       OR  [Task].[TaskType] = 1
--GROUP  BY [Task].[TaskId];
--GO