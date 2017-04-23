CREATE TABLE [dbo].[Users] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Username]       NVARCHAR (30)  NOT NULL,
    [HashedPassword] NVARCHAR (100) NOT NULL,
    [Salt]           NVARCHAR (50)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Username] ASC)
);

CREATE TABLE [dbo].[Worlds] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (30) NOT NULL,
    [IsMonitored] BIT           DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Name] ASC)
);

CREATE TABLE [dbo].[Creatures] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (30) NOT NULL,
    [IsMonitored] BIT           DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Name] ASC)
);

CREATE TABLE [dbo].[Scrapes] (
    [Id]   INT  IDENTITY (1, 1) NOT NULL,
    [Date] DATE NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Date] ASC)
);

CREATE TABLE [dbo].[Spawns] (
    [Id]         INT IDENTITY (1, 1) NOT NULL,
    [WorldId]    INT NOT NULL,
    [CreatureId] INT NOT NULL,
    [ScrapeId]   INT NOT NULL,
    [Amount]     INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Spawns_Scrapes] FOREIGN KEY ([ScrapeId]) REFERENCES [dbo].[Scrapes] ([Id]),
    CONSTRAINT [FK_Spawns_Worlds] FOREIGN KEY ([WorldId]) REFERENCES [dbo].[Worlds] ([Id]),
    CONSTRAINT [FK_Spawns_Creatures] FOREIGN KEY ([CreatureId]) REFERENCES [dbo].[Creatures] ([Id])
);

CREATE TABLE [dbo].[Positions] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (30) NOT NULL,
    [X]    INT           NOT NULL,
    [Y]    INT           NOT NULL,
    [Z]    INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Name] ASC)
);

CREATE TABLE [dbo].[TagCategories] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (30) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Name] ASC)
);

CREATE TABLE [dbo].[Tags] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (30) NOT NULL,
    [TagCategoryId] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Name] ASC, [TagCategoryId] ASC),
    CONSTRAINT [FK_Tags_TagCategories] FOREIGN KEY ([TagCategoryId]) REFERENCES [dbo].[TagCategories] ([Id])
);

CREATE TABLE [dbo].[WorldTags] (
    [Id]      INT IDENTITY (1, 1) NOT NULL,
    [WorldId] INT NOT NULL,
    [TagId]   INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([WorldId] ASC, [TagId] ASC),
    CONSTRAINT [FK_WorldTags_Worlds] FOREIGN KEY ([WorldId]) REFERENCES [dbo].[Worlds] ([Id]),
    CONSTRAINT [FK_WorldTags_Tags] FOREIGN KEY ([TagId]) REFERENCES [dbo].[Tags] ([Id])
);

CREATE TABLE [dbo].[CreatureTags] (
    [Id]         INT IDENTITY (1, 1) NOT NULL,
    [CreatureId] INT NOT NULL,
    [TagId]      INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([CreatureId] ASC, [TagId] ASC),
    CONSTRAINT [FK_CreatureTags_Creatures] FOREIGN KEY ([CreatureId]) REFERENCES [dbo].[Creatures] ([Id]),
    CONSTRAINT [FK_CreatureTags_Tags] FOREIGN KEY ([TagId]) REFERENCES [dbo].[Tags] ([Id])
);

CREATE TABLE [dbo].[PositionTags] (
    [Id]         INT IDENTITY (1, 1) NOT NULL,
    [PositionId] INT NOT NULL,
    [TagId]      INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([PositionId] ASC, [TagId] ASC),
    CONSTRAINT [FK_PositionTags_Positions] FOREIGN KEY ([PositionId]) REFERENCES [dbo].[Positions] ([Id]),
    CONSTRAINT [FK_PositionTags_Tags] FOREIGN KEY ([TagId]) REFERENCES [dbo].[Tags] ([Id])
);