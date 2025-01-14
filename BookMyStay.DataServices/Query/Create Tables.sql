
if not object_id('TblReservation', 'U') is null
	drop table TblReservation
go
create table TblReservation
(
	[Id] int primary key clustered identity(1,1) not null,
	[FirstName] varchar(30),
	[LastName] varchar(30),
	[Start] date,
	[End] date,
	[Created] datetime,
	[Modified] datetime
)