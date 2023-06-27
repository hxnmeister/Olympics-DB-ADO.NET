create table Athletes
(
  Id int not null primary key,
  Name varchar(25) not null,
  Surname varchar(35) not null,
  Patronymic varchar(35),
  Country varchar(50) not null,
  BirthDate date not null,
)
create table KindOfSports
(
  Id int not null primary key,
  Name varchar(40) not null,
  NumberOfParticipants int not null
)
create table OlympicsInfo
(
  Id int not null primary key,
  Season varchar(10) not null,
  HostCountryName varchar(50) not null,
  HostCityName varchar(50) not null,
  Year int not null
)
create table KindOfSportsAndAthletes
(
  Id int not null primary key,
  KindOfSportId int not null,
  AthleteId int not null,
  Result int not null,
  
  foreign key (KindOfSportId) references KindOfSports(Id),
  foreign key (AthleteId) references Athletes(Id)
)
create table OlympicsOverallInfo
(
  Id int not null primary key,
  OlympicsInfoId int not null,
  KindOfSportAndAthleteId int not null,
  
  foreign key (OlympicsInfoId) references OlympicsInfo(Id),
  foreign key (KindOfSportAndAthleteId) references KindOfSportsAndAthletes(Id)
)