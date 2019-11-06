create database GvcSportsBetting
go

use GvcSportsBetting
go

create table GvcSportsBetting.dbo.[SportEvents]
(
	SportEventID bigint not null identity(1,1),
	EventName varchar(1024) null,
	OddsForFirstTeam decimal(5,2) null,
	OddsForDraw decimal(5,2) null,
	OddsForSecondTeam decimal(5,2) null,
	EventStartDate datetime not null,
	IsDeleted bit not null default 0,
	constraint [PK_SportEvents_EventID] primary key(SportEventID),
	constraint [Chk_OddsForFirstTeam] check(OddsForFirstTeam >= 1),
	constraint [Chk_OddsForDraw] check(OddsForDraw >= 1),
	constraint [Chk_OddsForSecondTeam] check(OddsForDraw >= 1),
)

go


insert into GvcSportsBetting.dbo.[SportEvents] 
(EventName, OddsForFirstTeam, OddsForDraw, OddsForSecondTeam, EventStartDate)
values ('Liverpool - Juventus', 1.95, 3.15, 2.20, '2019-12-25 20:50'),
	   ('Grigor Dimitrov - Rafael Nadal', 2.0, 3.05, 2.70, '2019-12-20 16:50'),
	   ('Barcelona - Ludogorets', 1.01, 4.20, 15.20, '2018-11-04 16:50')

go

select * from GvcSportsBetting.dbo.[SportEvents]







