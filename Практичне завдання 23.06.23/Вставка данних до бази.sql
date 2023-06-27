insert into Athletes(Id, Name, Surname, Patronymic, Country, BirthDate)
values(1, 'Name1', 'Sur1', 'Patr1', 'Country1', '01-01-1999'), (2, 'Name2', 'Sur2', null, 'Country2', '01-01-1999'), (3, 'Name3', 'Sur3', 'Patr3', 'Country3', '01-01-1999'), (4, 'Name4', 'Sur4', null, 'Country4', '01-01-1999'), 
(5, 'Name5', 'Sur5', 'Patr1', 'Country5', '01-01-1999'), (6, 'Name6', 'Sur6', 'Patr6', 'Country6', '01-01-1999'), (7, 'Name7', 'Sur7', null, 'Country7', '01-01-1999'), (8, 'Name8', 'Sur8', 'Patr8', 'Country8', '01-01-1999'), 
(9, 'Name9', 'Sur9', null, 'Country9', '01-01-1999'), (10, 'Name10', 'Sur10', 'Patr10', 'Country10', '01-01-1999'), (11, 'Name11', 'Sur11', null, 'Country11', '01-01-1999'), (12, 'Name12', 'Sur12', 'Patr12', 'Country12', '01-01-1999'), 
(13, 'Name13', 'Sur13', 'Patr1', 'Country13', '01-01-1999'), (14, 'Name14', 'Sur14', 'Patr14', 'Country14', '01-01-1999'), (15, 'Name15', 'Sur15', null, 'Country15', '01-01-1999'), (16, 'Name16', 'Sur16', 'Patr16', 'Country16', '01-01-1999'), 
(17, 'Name17', 'Sur17', null, 'Country17', '01-01-1999'), (18, 'Name18', 'Sur18', 'Patr18', 'Country18', '01-01-1999'), (19, 'Name19', 'Sur19', null, 'Country19', '01-01-1999'), (20, 'Name20', 'Sur20', 'Patr20', 'Country20', '01-01-1999')

insert into KindOfSports(Id, Name, NumberOfParticipants)
values(1, 'Kind1', 4), (2, 'Kind2', 2), (3, 'Kind3', 5), (4, 'Kind4', 3)

insert into OlympicsInfo(Id, Season, HostCountryName, HostCityName, Year)
values(1,'Autumn','Country1','City1',2020)

insert into KindOfSportsAndAthletes(Id, KindOfSportId, AthleteId, Result)
values(1, 1, 2, 37.1), (2, 2, 4, 24), (3, 2, 6, 59), (4, 1, 10, 11), (5, 1, 12, 89.5), (6, 1, 11, 77), (7, 4, 17, 53), (8, 4, 15, 14.3), (9, 3, 7, 84), (10, 4, 20, 98.4), (11, 3, 1, 99), (12, 3, 3, 14.4), (13, 3, 9, 83), (14, 3, 8, 47)

insert into OlympicsOverallInfo(Id, OlympicsInfoId, KindOfSportAndAthleteId)
values(1, 1, 2), (2, 1, 1), (3, 1, 3), (4, 1, 4), (5, 1, 5), (6, 1, 6), (7, 1, 7), (8, 1, 9), (9, 1, 8), (10, 1, 10), (11, 1, 11), (12, 1, 12), (13, 1, 13), (14, 1, 14)