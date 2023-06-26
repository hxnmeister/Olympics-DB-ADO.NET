select OI.Season, OI.Year, OI.HostCountryName as [Host Country], OI.HostCityName as [Host City], KS.Name as [Kind of Sport], 
KS.NumberOfParticipants as [Number of Participants], A.Name, A.Surname, A.Patronymic, A.Country
from Athletes as A, KindOfSports as KS, OlympicsInfo as OI, KindOfSportsAndAthletes as KSA, OlympicsOverallInfo as OOI
where A.Id = KSA.AthleteId and KS.Id = KSA.KindOfSportId and OI.Id = OOI.OlympicsInfoId and KSA.Id = OOI.KindOfSportAndAthleteId
order by KS.Name