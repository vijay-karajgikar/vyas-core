drop table dbo.users
GO

drop sequence dbo.UserIdSequence
GO

create sequence dbo.UserIdSequence 
    as bigint
    start with 1
    increment by 1
GO

create table dbo.users (
    id bigint not null constraint default_userId default (next value for dbo.UserIdSequence),
    email nvarchar(100) not null,
    fullname nvarchar(100) not null,
    token nvarchar(100) not null,    
    is_active bit not null constraint users_default_is_active default 0,
    activation_id nvarchar(100) null,
    last_login datetime2(7) null,
    create_date datetime2(7) not null constraint users_default_create_date default getdate(),
    
    constraint users_primary_key primary key (id)
)
GO