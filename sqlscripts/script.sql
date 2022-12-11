use bugtracker;

create table if not exists Projects
(
    Id          int auto_increment
        primary key,
    Name        longtext not null,
    Description longtext not null
);

create table if not exists Issues
(
    Id                int auto_increment
        primary key,
    IssueName         longtext    not null,
    Description       longtext    not null,
    Type              int         not null,
    Priority          int         not null,
    TimeForCompletion datetime(6) null,
    DueDate           datetime(6) null,
    RootProjectID     int         not null,
    constraint FK_Issues_Projects_RootProjectID
        foreign key (RootProjectID) references Projects (Id)
            on delete cascade,
    constraint CK_Issues_Priority_Enum
        check (`Priority` in (0, 1, 2, 3, 4)),
    constraint CK_Issues_Type_Enum
        check (`Type` in (0, 1, 2, 3))
);

create index if not exists IX_Issues_RootProjectID
    on Issues (RootProjectID);

create table if not exists Labels
(
    Id            int auto_increment
        primary key,
    Name          longtext not null,
    RootProjectId int      not null,
    constraint FK_Labels_Projects_RootProjectId
        foreign key (RootProjectId) references Projects (Id)
            on delete cascade
);

create table if not exists IssueLabel
(
    IssuesId int not null,
    LabelsId int not null,
    primary key (IssuesId, LabelsId),
    constraint FK_IssueLabel_Issues_IssuesId
        foreign key (IssuesId) references Issues (Id)
            on delete cascade,
    constraint FK_IssueLabel_Labels_LabelsId
        foreign key (LabelsId) references Labels (Id)
            on delete cascade
);

create index if not exists IX_IssueLabel_LabelsId
    on IssueLabel (LabelsId);

create index if not exists IX_Labels_RootProjectId
    on Labels (RootProjectId);

create table if not exists LinkedIssues
(
    Id      int auto_increment
        primary key,
    IssueId int not null,
    Reason  int not null,
    constraint FK_LinkedIssues_Issues_IssueId
        foreign key (IssueId) references Issues (Id)
            on delete cascade,
    constraint CK_LinkedIssues_Reason_Enum
        check (`Reason` in (0, 1, 2))
);

create index if not exists IX_LinkedIssues_IssueId
    on LinkedIssues (IssueId);

create table if not exists Users
(
    Id                  int auto_increment
        primary key,
    Username            longtext    not null,
    Email               longtext    not null,
    FirstName           longtext    not null,
    LastName            longtext    not null,
    PasswordHash        longblob    not null,
    PasswordSalt        longblob    not null,
    RefreshToken        longtext    null,
    RefreshTokenCreated datetime(6) null,
    RefreshTokenExpires datetime(6) null
);

create table if not exists IssueUser
(
    AssignedIssuesId int not null,
    AssigneesId      int not null,
    primary key (AssignedIssuesId, AssigneesId),
    constraint FK_IssueUser_Issues_AssignedIssuesId
        foreign key (AssignedIssuesId) references Issues (Id)
            on delete cascade,
    constraint FK_IssueUser_Users_AssigneesId
        foreign key (AssigneesId) references Users (Id)
            on delete cascade
);

create index if not exists IX_IssueUser_AssigneesId
    on IssueUser (AssigneesId);

create table if not exists ProjectUser
(
    ProjectsId int not null,
    UsersId    int not null,
    primary key (ProjectsId, UsersId),
    constraint FK_ProjectUser_Projects_ProjectsId
        foreign key (ProjectsId) references Projects (Id)
            on delete cascade,
    constraint FK_ProjectUser_Users_UsersId
        foreign key (UsersId) references Users (Id)
            on delete cascade
);

create index if not exists IX_ProjectUser_UsersId
    on ProjectUser (UsersId);

create table if not exists __EFMigrationsHistory
(
    MigrationId    varchar(150) not null
        primary key,
    ProductVersion varchar(32)  not null
);


